using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P2PVpn.Models;
using P2PVpn.Utilities;

namespace P2PVpn.Tests
{
    [TestClass]
    public class FileTransferTest
    {
        [TestMethod]
        public void TransferFileTest()
        {
            //FileTransfer fileTranser = new FileTransfer()
            //{
            //    SourceDirectory = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\Assets\"),
            //    TargetDirectory = @"C:\Temp"
            //};

            //FileIO fileIO = new FileIO(fileTranser);

            ////_entryValidator.OnInvalidEntry += delegate { eventRaised = true; };

            ////Assert.IsFalse(_entryValidator.IsValidEntry(_selectedEntry, _selectedEntry.Ticker));
            ////Assert.IsTrue(eventRaised);

            ////EventHandler handler = (s, e) => MessageBox.Show("Woho");

            //var sourceFile = "";
            //var targetFile= "";

            //fileIO.FinshedFileTransfer += (sender, info) =>
            //{
            //    sourceFile = info.SourceFile;
            //    targetFile = info.TargetFile;
            //};
            //var threadTest = new ThreadRunner();
            //var finished = new ManualResetEvent(false);
            //threadTest.Finished += delegate(object sender, EventArgs e)
            //{
            //    finished.Set();
            //};
            //threadTest.StartThreadTest();
            //threadTest.FinishThreadTest();
            //fileIO.FinshedFileTransfer.
            //Assert.IsTrue(finished.WaitOne(1000));
            //fileIO.FinshedFileTransfer += delegate { };


        }
    }
}
