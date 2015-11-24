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
        //private string _currentFileTransfer = "";
        private static List<FileTransfer> _transferQue = new List<FileTransfer>();

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
  
            _fileSystemWatcher.Changed -= _fileSystemWatcher_Created;
            _fileSystemWatcher.Error -= _fileSystemWatcher_Error;

            _fileSystemWatcher = new FileSystemWatcher(fileTransfer.SourceDirectory);
            //_fileSystemWatcher.WaitForChanged(WatcherChangeTypes.Created);
            _fileSystemWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;

            _fileSystemWatcher.Filter = "*.*";
            _fileSystemWatcher.IncludeSubdirectories = false;
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
                string sourceDir = GetPath(_fileTransfer.SourceDirectory);
                string sourceDir2 = GetPath(e.FullPath);

                if (sourceDir == sourceDir2)
                {
                    string targetFile = Path.Combine(_fileTransfer.TargetDirectory, e.Name);

                    FinshedFileTransferEventArgs FinshedFileTransferInfo =
                         new FinshedFileTransferEventArgs(e.FullPath, targetFile);

                    // if anyone has subscribed, notify them
                    if (FinshedFileTransfer != null)
                    {

                        MediaServer.LoginToMediaShare(_mediaServer);

                        TransferFile(new FileTransfer { SourceDirectory = e.FullPath, TargetDirectory = targetFile });

                        FinshedFileTransfer(this, FinshedFileTransferInfo);
                    }
                }
              

            }
        }
        public void TransferFile(FileTransfer fileTransfer)
        {
            if (_transferQue.Find(x => x.SourceDirectory == fileTransfer.SourceDirectory) != null) return;
            
            _transferQue.Add(fileTransfer);
            if (_transferQue.Count > 1) return;

            while (!IsFileClosed(fileTransfer.SourceDirectory))
            {
                Thread.Sleep(500);
            }

            try
            {
                Logging.Log("Transfering File: " + fileTransfer.SourceDirectory);
                //File.Copy(source, destination);

                FileCopyLib.FileCopier.CopyWithProgress(fileTransfer.SourceDirectory, fileTransfer.TargetDirectory,
                    (x) => FileTransferProgress(this, new FileTransferProgressEventArgs(x.Percentage, fileTransfer)));
                    //(x) => Logging.Log("Copying {0}", x.Percentage));

                Logging.Log("Finished Transfering File: {0}{1}{2}to: {3} ", fileTransfer.SourceDirectory, Environment.NewLine, "\t", fileTransfer.TargetDirectory);

                if (File.Exists(fileTransfer.SourceDirectory))
                {
                    while (!IsFileClosed(fileTransfer.SourceDirectory))
                    {
                        Thread.Sleep(500);
                    }
                    File.Delete(fileTransfer.SourceDirectory);
                }
            }
            catch (Exception e)
            {
                Logging.Log("Error: File Transfer Failed {0} {1} {2}", fileTransfer.SourceDirectory, Environment.NewLine, e.Message);
                //return 1;
            }
            finally
            {
                _transferQue.Remove(fileTransfer);
            }
            
            ProcessTransferQueue();
        }

        private void ProcessTransferQueue()
        {
            while (_transferQue.Count > 0)
            {
                Thread.Sleep(5000);
                TransferFile(_transferQue.First());
            }
        }
        private void _fileSystemWatcher_Error(object sender, ErrorEventArgs e)
        {
            Logging.Log("File Transfer Error: " + e.GetException());
        }
        public string GetPath(string fileOrDirPath)
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
