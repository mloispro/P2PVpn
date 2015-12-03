using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Files;

namespace P2PVpn.Utilities
{
    public class FileSync : IDisposable
    {
        private static string _metadataFile = "filesync.metadata";
        private static SyncOrchestrator _agent = new SyncOrchestrator();
        static FileSync()
        {
            Settings settings = Settings.Get();
            var fileSystemWatcher = new FileSystemWatcher(settings.MediaFileTransfer.SourceDirectory);
            fileSystemWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size | NotifyFilters.DirectoryName;

            //fileSystemWatcher.Filter = "*.*";
            fileSystemWatcher.IncludeSubdirectories = true;
            fileSystemWatcher.Changed += (s, a) =>
            {
                if (a.Name.Contains(_metadataFile)) return;
                WatchFileSystem(settings.MediaFileTransfer.SourceDirectory, settings.MediaFileTransfer.TargetDirectory);
            };
            fileSystemWatcher.Renamed += (s, a) =>
            {
                if (a.Name.Contains(_metadataFile)) return;
                WatchFileSystem(settings.MediaFileTransfer.SourceDirectory, settings.MediaFileTransfer.TargetDirectory);
            };
            //fileSystemWatcher.Error += _fileSystemWatcher_Error;
            //fileSystemWatcher.Deleted += fileSystemWatcher_Deleted;

            fileSystemWatcher.EnableRaisingEvents = true;
        }
        public static void WatchFileSystem(string sourceDirectory, string targetDirectory)
        {
            if (string.IsNullOrEmpty(sourceDirectory) || string.IsNullOrEmpty(targetDirectory) ||
                !Directory.Exists(sourceDirectory) || !Directory.Exists(targetDirectory))
            {
                return;
            }

            try
            {
                // Set options for the synchronization operation
                FileSyncOptions options = FileSyncOptions.ExplicitDetectChanges |
                        FileSyncOptions.RecycleDeletedFiles |
                        FileSyncOptions.RecyclePreviousFileOnUpdates |
                        FileSyncOptions.RecycleConflictLoserFiles;

                FileSyncScopeFilter filter = new FileSyncScopeFilter();
                //filter.FileNameExcludes.Add("*.lnk"); // Exclude all *.lnk files
                filter.FileNameIncludes.Add("*.mp4");
                filter.FileNameIncludes.Add("*.mkv");
                filter.FileNameIncludes.Add("*.avi");
                filter.FileNameIncludes.Add("*.divx");
                filter.FileNameIncludes.Add("*.png");
                filter.FileNameIncludes.Add("*.jpg");
                filter.FileNameIncludes.Add("*.gif");
                filter.FileNameIncludes.Add("*.zip");
                filter.FileNameIncludes.Add("*.rar");

                // Explicitly detect changes on both replicas upfront, to avoid two change
                // detection passes for the two-way synchronization

                DetectChangesOnFileSystemReplica(
                        sourceDirectory, filter, options);
                //DetectChangesOnFileSystemReplica(
                //    targetDirectory, filter, options);

                // Synchronization in both directions
                SyncFileSystemReplicasOneWay(sourceDirectory, targetDirectory, filter, options);
                //SyncFileSystemReplicasOneWay(sourceDirectory, targetDirectory, null, options);
            }
            catch (Exception e)
            {
                Logging.Log("\nException from File Synchronization Provider:\n" + e.ToString());
            }
        }

        public static void DetectChangesOnFileSystemReplica(
        string replicaRootPath,
        FileSyncScopeFilter filter, FileSyncOptions options)
        {
            FileSyncProvider provider = null;

            try
            {
                provider = new FileSyncProvider(replicaRootPath, filter, options);
                provider.DetectChanges();

            }
            finally
            {
                // Release resources
                if (provider != null)
                    provider.Dispose();
            }
        }

        public static void SyncFileSystemReplicasOneWay(
                string sourceReplicaRootPath, string destinationReplicaRootPath,
                FileSyncScopeFilter filter, FileSyncOptions options)
        {
            FileSyncProvider sourceProvider = null;
            FileSyncProvider destinationProvider = null;

            try
            {
                sourceProvider = new FileSyncProvider(
                    sourceReplicaRootPath, filter, options);

                //filter.SubdirectoryExcludes.Add(@".\");

                destinationProvider = new FileSyncProvider(
                    destinationReplicaRootPath, filter, options);

                sourceProvider.AppliedChange +=
                    new EventHandler<AppliedChangeEventArgs>(OnAppliedChange);
                sourceProvider.SkippedChange +=
                    new EventHandler<SkippedChangeEventArgs>(OnSkippedChange);

                sourceProvider.CopyingFile += (s, a) =>
                {
                    string file = a.FilePath;
                    int percent = a.PercentCopied;
                };
                sourceProvider.AppliedChange += (s, a) =>
                {
                    string file = a.NewFilePath;
                    var change = a.ChangeType;
                };

                destinationProvider.CopyingFile += (s, a) =>
                {
                    string file = a.FilePath;
                    int percent = a.PercentCopied;
                    FileIO.FileTransferProgress(null, new FileTransferProgressEventArgs(percent, file));
                };
                destinationProvider.AppliedChange += (s, a) =>
                {
                    string file = a.NewFilePath;
                    var change = a.ChangeType;
                    FileIO.FinshedFileTransfer(null, new FinshedFileTransferEventArgs(file));
                    string path = FileIO.GetPath(sourceReplicaRootPath);
                    try
                    {
                        File.Delete(path + file);
                    }
                    catch (Exception ex) { Logging.Log(ex.Message); }
                };

                //SyncOrchestrator agent = new SyncOrchestrator();
                _agent.LocalProvider = sourceProvider;
                _agent.RemoteProvider = destinationProvider;
                _agent.Direction = SyncDirectionOrder.Upload;

                Logging.Log("Synchronizing changes to replica: " +
                    destinationProvider.RootDirectoryPath);

                _agent.StateChanged += (s, a) =>
                {
                    if (a.OldState == SyncOrchestratorState.Uploading && a.NewState == SyncOrchestratorState.Ready)
                    {
                        string path = FileIO.GetPath(destinationReplicaRootPath);
                        string metadataFile = path + _metadataFile;
                        File.Delete(metadataFile);
                    }
                };
                _agent.Synchronize();
                //keep background worker alive
                //while (true) { };
            }
            finally
            {
                // Release resources
                if (sourceProvider != null) sourceProvider.Dispose();
                if (destinationProvider != null) destinationProvider.Dispose();
            }
        }

        public static void OnAppliedChange(object sender, AppliedChangeEventArgs args)
        {
            switch (args.ChangeType)
            {
                case ChangeType.Create:
                    Logging.Log("-- Applied CREATE for file " + args.NewFilePath);
                    break;
                case ChangeType.Delete:
                    Logging.Log("-- Applied DELETE for file " + args.OldFilePath);
                    break;
                case ChangeType.Update:
                    Logging.Log("-- Applied OVERWRITE for file " + args.OldFilePath);
                    break;
                case ChangeType.Rename:
                    Logging.Log("-- Applied RENAME for file " + args.OldFilePath +
                                      " as " + args.NewFilePath);
                    break;
            }
        }

        public static void OnSkippedChange(object sender, SkippedChangeEventArgs args)
        {
            Logging.Log("-- Skipped applying " + args.ChangeType.ToString().ToUpper()
                  + " for " + (!string.IsNullOrEmpty(args.CurrentFilePath) ?
                                args.CurrentFilePath : args.NewFilePath) + " due to error");

            if (args.Exception != null)
                Logging.Log("   [" + args.Exception.Message + "]");
        }


        #region Dispose

        private static bool _disposed;
        ~FileSync()
        {
            Dispose(true);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public static void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //_networkListManager.NetworkConnectivityChanged -= _networkListManager_NetworkConnectivityChanged;
                }
                if (_agent.State != SyncOrchestratorState.Ready)
                {
                    _agent.Cancel();
                    _agent = null;
                }
                _disposed = true;
            }

        }
        #endregion Dispose

    }
}

