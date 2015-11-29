using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using P2PVpn.Models;


namespace P2PVpn.Utilities
{
    public class FileIO:IDisposable
    {
        private FileSystemWatcher _fileSystemWatcher = new FileSystemWatcher();
        private FileSystemWatcher _fileSystemWatcher2 = new FileSystemWatcher();

        //private FileTransfer _fileTransfer;
        //private FileTransfer _fileTransfer2;
        //private Models.MediaServer _mediaServer;
        //private string _currentFileTransfer = "";
        //public static bool StopQueue = false;
        //private static bool _isTranserferingFile = false;

        // the delegate the subscribers must implement
        public delegate void FinshedFileTransferHandler(FileTransfer fileTransfer,
                             FinshedFileTransferEventArgs finshedFileTransferInfo);

        // an instance of the delegate
        public static FinshedFileTransferHandler FinshedFileTransfer;

        // the delegate the subscribers must implement
        public delegate void FileTransferProgressHandler(object sender,
                             FileTransferProgressEventArgs fileTransferProgressInfo);

        // an instance of the delegate
        public static FileTransferProgressHandler FileTransferProgress;

        //public FileIO(FileTransfer fileTransfer, Models.MediaServer mediaServer)
        public FileIO()
        {
            //_fileTransfer = fileTransfer;
            //_mediaServer = mediaServer;

            //var di = new DirectoryInfo(Path.GetDirectoryName(fileTransfer.TargetDirectory));
            //di.Attributes = FileAttributes.Normal;

            //DirectorySecurity ds = Directory.GetAccessControl(Path.GetDirectoryName(fileTransfer.TargetDirectory));
            //ds.AddAccessRule(new FileSystemAccessRule(WindowsIdentity.GetCurrent().Name, FileSystemRights.FullControl, AccessControlType.Allow));
            //Directory.SetAccessControl(Path.GetDirectoryName(fileTransfer.TargetDirectory), ds);

        }

        //public FileIO(FileTransfer fileTransfer, FileTransfer fileTransfer2)
        //    : this(fileTransfer)
        //{
        //    _fileTransfer2 = fileTransfer2;
        //    _fileSystemWatcher2 = new FileSystemWatcher(fileTransfer2.SourceDirectory);
        //    _fileSystemWatcher2.Created += _fileSystemWatcher_Created;
        //    _fileSystemWatcher2.Error += _fileSystemWatcher_Error;
        //    _fileSystemWatcher2.NotifyFilter = NotifyFilters.LastWrite;
        //    _fileSystemWatcher2.Filter = "*.*";
        //    _fileSystemWatcher2.EnableRaisingEvents = true;
        //}
        static FileIO()
        {

        }
        public static void WatchFileSystem()
        {
            //todo: fix this
            Settings settings = Settings.Get();

            if (string.IsNullOrWhiteSpace(settings.MediaFileTransfer.SourceDirectory) ||
                string.IsNullOrWhiteSpace(settings.MediaFileTransfer.TargetDirectory))
            {
                throw new Exception("Select a Source and Target Directory");
            }

            var fileSystemWatcher = new FileSystemWatcher(settings.MediaFileTransfer.SourceDirectory);
            fileSystemWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;

            fileSystemWatcher.Filter = "*.*";
            fileSystemWatcher.IncludeSubdirectories = false;
            fileSystemWatcher.Changed += fileSystemWatcher_Changed;
            //fileSystemWatcher.Error += _fileSystemWatcher_Error;
            fileSystemWatcher.Deleted += fileSystemWatcher_Deleted;

            fileSystemWatcher.EnableRaisingEvents = true;
        }

        private static void fileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Deleted)
            {
                ProcessFileTransferQueue();
            }
        }

        private static void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                Settings settings = Settings.Get();

                string sourceDir = GetPath(settings.MediaFileTransfer.SourceDirectory);
                string sourceDir2 = GetPath(e.FullPath);

                if (sourceDir == sourceDir2)
                {
                    string targetFile = Path.Combine(settings.MediaFileTransfer.TargetDirectory, e.Name);

                    var prepedTransfer = new FileTransfer { SourceDirectory = e.FullPath, TargetDirectory = targetFile };
                    
                    //add to queue
                    if (!settings.MediaFileTransferQue.Contains(prepedTransfer))
                    {
                        settings.MediaFileTransferQue.Add(prepedTransfer);
                        Settings.Save(settings);
                        ProcessFileTransferQueue();
                    }
                }
            }
        }

        private static void TransferToMediaServer(FileTransfer fileTransfer)
        {
            if (!Networking.IsLocalNetworkConnected()) return;

            FinshedFileTransferEventArgs FinshedFileTransferInfo =
                        new FinshedFileTransferEventArgs(fileTransfer.SourceDirectory, fileTransfer.TargetDirectory);

            // if anyone has subscribed, notify them

            Settings settings = Settings.Get();
            MediaServer.LoginToMediaShare(settings.MediaServer);

            //Transfer File
            while (!IsFileClosed(fileTransfer.SourceDirectory))
            {
                //Thread.Sleep(500);
                ControlHelpers.Sleep(500).Wait();
            }
            fileTransfer.IsTransfering = true;
            Settings.Save(settings);

            Logging.Log("Transfering File: " + fileTransfer.SourceDirectory);

            Task fileCopyTask = Task.Run(() =>
            {

                FileCopyLib.FileCopier.CopyWithProgress(fileTransfer.SourceDirectory, fileTransfer.TargetDirectory,
                    (x) => FileTransferProgress(null, new FileTransferProgressEventArgs(x.Percentage, fileTransfer)));


            }).ContinueWith((t) =>
            {

                if (!t.IsFaulted && !t.IsCanceled)
                {
                    Logging.Log("Finished Transfering File: {0}{1}{2}to: {3} ", fileTransfer.SourceDirectory, Environment.NewLine, "\t", fileTransfer.TargetDirectory);

                    File.Delete(fileTransfer.SourceDirectory);

                    fileTransfer.IsTransfering = false;
                    Settings.Save(settings);

                    if (FinshedFileTransfer != null)
                    {
                        FinshedFileTransfer(fileTransfer, FinshedFileTransferInfo);
                    }
                }
                else if (t.IsFaulted)
                {
                    Exception ex = t.Exception;
                    while (ex is AggregateException && ex.InnerException != null)
                        ex = ex.InnerException;

                    var dialogResult = ControlHelpers.ShowMessageBox(ex.Message, ControlHelpers.MessageBoxType.Error);
                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        ProcessFileTransferQueue();
                    }
                }
            });

            //if (fileCopyTask.IsFaulted)
            //{
               
            //}


        }
        //private static Task FileTransferCompleted()
        //{

        //}
        public static void ProcessFileTransferQueue()
        {
            CleanTransferQueue();

            Settings settings = Settings.Get();
            var transferQue = settings.MediaFileTransferQue;

            if (transferQue.Count == 0) return;
            if (IsFileTransferInProgress()) return;

            var transferFile = transferQue.First();

            TransferToMediaServer(transferFile);

            FileIO.FinshedFileTransfer += (fileTransfer, info) =>
            {
                transferQue.Remove(fileTransfer);
                Settings.Save(settings);
                ProcessFileTransferQueue();
            };
        }
        private static void CleanTransferQueue()
        {
            Settings settings = Settings.Get();
            if (settings.MediaFileTransferQue.Count == 0) return;

            settings.MediaFileTransferQue = settings.MediaFileTransferQue.DistinctBy(x => x.SourceDirectory).ToList();
            settings.MediaFileTransferQue.RemoveAll(x => !File.Exists(x.SourceDirectory));
            
            Settings.Save(settings);

        }
        private static bool IsFileTransferInProgress()
        {
            Settings settings = Settings.Get();
            bool anyTransfers = settings.MediaFileTransferQue.Any(x => x.IsTransfering);
            return anyTransfers;
        }
        public static void ResetTransfers()
        {
            var settings = Settings.Get();
            settings.MediaFileTransferQue.ForEach(x => x.IsTransfering = false);
            Settings.Save(settings);
        }
        public static string GetPath(string fileOrDirPath)
        {
            bool isDirectory = IsDirectory(fileOrDirPath);
            if (!isDirectory)
            {
                fileOrDirPath = Path.GetDirectoryName(fileOrDirPath) + @"\";
            }
            if (isDirectory)
            {
                fileOrDirPath = fileOrDirPath.TrimEnd(@"\".ToCharArray());
                fileOrDirPath += @"\";
                fileOrDirPath =  Path.GetDirectoryName(fileOrDirPath) + @"\";
            }
            return fileOrDirPath;
        }
        private static bool IsDirectory(string path)
        {
            System.IO.FileAttributes fa = System.IO.File.GetAttributes(path);
            bool isDirectory = false;
            if ((fa & FileAttributes.Directory) != 0)
            {
                isDirectory = true;
            }
            return isDirectory;
        }
        

        public static bool IsFileClosed(string filename)
        {
            try
            {
                using (var inputStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
        }
        public static void ChangeFolderName(string folderName, string newFolderName)
        {
            
            if (folderName.Equals(newFolderName)) return;
            try
            {
                Directory.Move(folderName, newFolderName);
            }
            catch(Exception ex)
            {
                Logging.Log("Error: Folder not available : " + folderName);
                throw ex;
            }
        }

       #region Dispose

        private bool _disposed;
        ~FileIO()
        {
            Dispose(true);
        }

        public async void Dispose()
        {
            await Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    
                }
                
                ResetTransfers();
                _disposed = true;
            }

        }
        #endregion Dispose
    }

    public class FinshedFileTransferEventArgs : EventArgs
    {
        public string SourceFile{get;set;}
        public string TargetFile{get;set;}

        public FinshedFileTransferEventArgs(string sourceFile, string targetFile)
        {
            SourceFile = sourceFile;
            TargetFile = targetFile;
        }
    }
    public class FileTransferProgressEventArgs : EventArgs
    {
        //public int TranserfedBytes { get; set; }
        //public int TotalBytes { get; set; }
        public string PercentComplete { get; set; }
        public string SourceFile { get; set; }

        //public FileTransferProgressEventArgs(int transferedBytes, int totalBytes)
        //{
        //    TranserfedBytes = transferedBytes;
        //    TotalBytes = totalBytes;
        //    PercentComplete = transferedBytes / totalBytes;
        //}
        public FileTransferProgressEventArgs(double percentComplete, FileTransfer fileTransfer)
        {
            percentComplete = (int)Math.Round(percentComplete, 0);
            PercentComplete = percentComplete.ToString();
            SourceFile = Path.GetFileName(fileTransfer.SourceDirectory);
        }
    }
}
