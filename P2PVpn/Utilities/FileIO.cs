using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public FileIO(FileTransfer fileTransfer)
        {
            _fileTransfer = fileTransfer;

            //var di = new DirectoryInfo(Path.GetDirectoryName(fileTransfer.TargetDirectory));
            //di.Attributes = FileAttributes.Normal;

            //DirectorySecurity ds = Directory.GetAccessControl(Path.GetDirectoryName(fileTransfer.TargetDirectory));
            //ds.AddAccessRule(new FileSystemAccessRule(WindowsIdentity.GetCurrent().Name, FileSystemRights.FullControl, AccessControlType.Allow));
            //Directory.SetAccessControl(Path.GetDirectoryName(fileTransfer.TargetDirectory), ds);

            _fileSystemWatcher = new FileSystemWatcher(fileTransfer.SourceDirectory);
            //_fileSystemWatcher.WaitForChanged(WatcherChangeTypes.Created);
            _fileSystemWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                                | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            _fileSystemWatcher.Filter = "*.*";
            _fileSystemWatcher.Created += _fileSystemWatcher_Created;
            _fileSystemWatcher.Changed += _fileSystemWatcher_Created;
            _fileSystemWatcher.Error += _fileSystemWatcher_Error;
            //_fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;

            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        public FileIO(FileTransfer fileTransfer, FileTransfer fileTransfer2)
            : this(fileTransfer)
        {
            _fileTransfer2 = fileTransfer2;
            _fileSystemWatcher2 = new FileSystemWatcher(fileTransfer2.SourceDirectory);
            _fileSystemWatcher2.Created += _fileSystemWatcher_Created;
            _fileSystemWatcher2.Error += _fileSystemWatcher_Error;
            _fileSystemWatcher2.NotifyFilter = NotifyFilters.LastWrite;
            _fileSystemWatcher2.Filter = "*.*";
            _fileSystemWatcher2.EnableRaisingEvents = true;
        }


        private void _fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                string sourceDir = Path.GetDirectoryName(_fileTransfer.SourceDirectory);
                string sourceDir2 = Path.GetDirectoryName(e.FullPath);

                if (sourceDir == sourceDir2)
                {
                    string targetFile = Path.Combine(_fileTransfer.TargetDirectory, e.Name);
                    FinshedFileTransferEventArgs FinshedFileTransferInfo =
                         new FinshedFileTransferEventArgs(e.FullPath, targetFile);

                    // if anyone has subscribed, notify them
                    if (FinshedFileTransfer != null)
                    {
                        TransferFile(e.FullPath, targetFile);
                        FinshedFileTransfer(this, FinshedFileTransferInfo);
                    }
                }
                else if (sourceDir == sourceDir2)
                {
                    string targetFile = Path.Combine(_fileTransfer.TargetDirectory, e.Name);

                    FinshedFileTransferEventArgs FinshedFileTransferInfo =
                        new FinshedFileTransferEventArgs(e.FullPath, targetFile);

                    // if anyone has subscribed, notify them
                    if (FinshedFileTransfer != null)
                    {
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

        /// <summary> Time the Move
        /// </summary> 
        /// <param name="source">Source file path</param> 
        /// <param name="destination">Destination file path</param> 
        public void TransferFile(string source, string destination)
        {
            Logging.Log("Transfering File: " + source);
            DateTime start_time = DateTime.Now;
            FastTransferFile(source, destination);
            long size = new FileInfo(destination).Length;
            int milliseconds = 1 + (int)((DateTime.Now - start_time).TotalMilliseconds);
            // size time in milliseconds per hour
            long tsize = size * 3600000 / milliseconds;
            tsize = tsize / (int)Math.Pow(2, 30);
            //Console.WriteLine(tsize + "GB/hour");
            Logging.Log("Copying {0} to {1} at {2} GB/hour", source, destination, tsize);
        }

        /// <summary> Fast file move with big buffers
        /// </summary>
        /// <param name="source">Source file path</param> 
        /// <param name="destination">Destination file path</param> 
        private void FastTransferFile(string source, string destination)
        {

            //while (true)
            //{
            //    try
            //    {
            //        using (FileStream Fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None, 100))
            //        {
            //            //the file is close
            //            break;
            //        }
            //    }
            //    catch (IOException)
            //    {
            //        //wait and retry
            //        Thread.Sleep(1000);
            //    }
            //}

            int array_length = (int)Math.Pow(2, 19);
            byte[] dataArray = new byte[array_length];
            while (true)
            {
                try
                {
                    using (FileStream fsread = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.None, array_length))
                    {
                        using (BinaryReader bwread = new BinaryReader(fsread))
                        {
                            using (FileStream fswrite = new FileStream
                            (destination, FileMode.Create, FileAccess.Write, FileShare.None, array_length))
                            {
                                using (BinaryWriter bwwrite = new BinaryWriter(fswrite))
                                {
                                    for (; ; )
                                    {
                                        int read = bwread.Read(dataArray, 0, array_length);

                                        FileTransferProgressEventArgs fileTransferProgressInfo =
                                            new FileTransferProgressEventArgs(read, array_length);

                                        // if anyone has subscribed, notify them
                                        if (FileTransferProgress != null)
                                        {
                                            FileTransferProgress(this, fileTransferProgressInfo);
                                        }

                                        if (0 == read)
                                            break;
                                        bwwrite.Write(dataArray, 0, read);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (IOException)
                {
                    //wait and retry
                    Thread.Sleep(1000);
                }
            }
            File.Delete(source);
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
