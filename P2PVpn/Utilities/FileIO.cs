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
    public class FileIO
    {
        private FileSystemWatcher _fileSystemWatcher = new FileSystemWatcher();
        private FileSystemWatcher _fileSystemWatcher2 = new FileSystemWatcher();

        private FileTransfer _fileTransfer;
        private FileTransfer _fileTransfer2;
        private Models.MediaServer _mediaServer;

        // the delegate the subscribers must implement
        public delegate void FinshedFileTransferHandler(object sender,
                             FinshedFileTransferEventArgs finshedFileTransferInfo);

        // an instance of the delegate
        public FinshedFileTransferHandler FinshedFileTransfer;

        // the delegate the subscribers must implement
        public delegate void FileTransferProgressHandler(object sender,
                             FileTransferProgressEventArgs fileTransferProgressInfo);

        // an instance of the delegate
        public FileTransferProgressHandler FileTransferProgress;

        public FileIO(FileTransfer fileTransfer, Models.MediaServer mediaServer)
        {
            _fileTransfer = fileTransfer;
            _mediaServer = mediaServer;

            //var di = new DirectoryInfo(Path.GetDirectoryName(fileTransfer.TargetDirectory));
            //di.Attributes = FileAttributes.Normal;

            //DirectorySecurity ds = Directory.GetAccessControl(Path.GetDirectoryName(fileTransfer.TargetDirectory));
            //ds.AddAccessRule(new FileSystemAccessRule(WindowsIdentity.GetCurrent().Name, FileSystemRights.FullControl, AccessControlType.Allow));
            //Directory.SetAccessControl(Path.GetDirectoryName(fileTransfer.TargetDirectory), ds);
  
            _fileSystemWatcher.Changed += _fileSystemWatcher_Created;
            _fileSystemWatcher.Error += _fileSystemWatcher_Error;

            _fileSystemWatcher = new FileSystemWatcher(fileTransfer.SourceDirectory);
            //_fileSystemWatcher.WaitForChanged(WatcherChangeTypes.Created);
            _fileSystemWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                                | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            _fileSystemWatcher.Filter = "*.*";
            //_fileSystemWatcher.Created += _fileSystemWatcher_Created;
            _fileSystemWatcher.Changed += _fileSystemWatcher_Created;
            _fileSystemWatcher.Error += _fileSystemWatcher_Error;
            //_fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;

            _fileSystemWatcher.EnableRaisingEvents = true;
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


        private void _fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                string sourceDir = EnsureDirectoryTrialingSlash(_fileTransfer.SourceDirectory);
                string sourceDir2 = EnsureDirectoryTrialingSlash(Path.GetDirectoryName(e.FullPath));

                if (sourceDir == sourceDir2)
                {
                    string targetFile = Path.Combine(_fileTransfer.TargetDirectory, e.Name);
                    FinshedFileTransferEventArgs FinshedFileTransferInfo =
                         new FinshedFileTransferEventArgs(e.FullPath, targetFile);

                    // if anyone has subscribed, notify them
                    if (FinshedFileTransfer != null)
                    {

                        MediaServer.LoginToMediaShare(_mediaServer);

                        TransferFile(e.FullPath, targetFile);

                        FinshedFileTransfer(this, FinshedFileTransferInfo);
                    }
                }
              

            }
        }

        private void _fileSystemWatcher_Error(object sender, ErrorEventArgs e)
        {
            Logging.Log("File Transfer Error: " + e.GetException());
        }
        public string EnsureDirectoryTrialingSlash(string filePath)
        {
            filePath = filePath.TrimEnd(@"\".ToCharArray());
            filePath += @"\";
            return Path.GetDirectoryName(filePath) + @"\";
        }
        public void TransferFile(string source, string destination)
        {
            Thread.Sleep(5000);
            if (!IsFileClosed(source)) return;

            
            try
            {
                Logging.Log("Transfering File: " + source);
                //File.Copy(source, destination);
                
                FileCopyLib.FileCopier.CopyWithProgress(source, destination,
                    (x) => Logging.Log("Copying {0}", x.Percentage));

                //return 0;
            }
            catch (Exception e)
            {
                Logging.Log("Error: File Transfer Failed {0} {1} {2}", source, Environment.NewLine, e.Message);
                //return 1;
            }
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
            //Networking.LoginToUNCShare("Mitch", "1", "olsonhome", "olsonhome");
            if (folderName.Equals(newFolderName)) return;
            Directory.Move(folderName, newFolderName);
        }
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
        public int TranserfedBytes { get; set; }
        public int TotalBytes { get; set; }
        public double PercentComplete { get; set; }

        public FileTransferProgressEventArgs(int transferedBytes, int totalBytes)
        {
            TranserfedBytes = transferedBytes;
            TotalBytes = totalBytes;
            PercentComplete = transferedBytes / totalBytes;
        }
    }
}
