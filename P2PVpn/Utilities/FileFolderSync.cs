using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Files;
using System.Runtime.InteropServices;

namespace P2PVpn.Utilities
{
    public class FileFolderSync
    {
        private static SyncOrchestrator _agent;
        public static void WatchFileSystem(string sourceFolder, string targetFolder)
        {
            // Create file system provider
            FileSyncProvider sourceProvider = new FileSyncProvider(sourceFolder);
            FileSyncProvider targetProvider = new FileSyncProvider(targetFolder);
            
            // Ask providers to detect changes
            sourceProvider.DetectChanges();
            //targetProvider.DetectChanges();
           
            targetProvider.CopyingFile += (s, a) =>
            {
                int percent = a.PercentCopied;
                string fileName = a.FilePath;
            };


            sourceProvider.CopyingFile += (s, a) =>
            {
                int percent = a.PercentCopied;
            };


            RunSync(sourceProvider, targetProvider);
        }
        private static void RunSync(FileSyncProvider sourceProvider, FileSyncProvider targetProvider)
        {
            //await Task.Run(() =>
            //{
            // Sync changes
            _agent = new SyncOrchestrator();
            _agent.LocalProvider = sourceProvider;
            _agent.RemoteProvider = targetProvider;
            _agent.Direction = SyncDirectionOrder.Upload;

            SyncOperationStatistics stats = _agent.Synchronize();
            //Task.Run(() => { agent.Synchronize(); });
            //});
        }
        public static void CancelSync()
        {
            _agent.Cancel();
        }
    }
}
