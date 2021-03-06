﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using P2PVpn.Models;
using P2PVpn.Utilities;
//using com.LandonKey.SocksWebProxy;
//using com.LandonKey.SocksWebProxy.Proxy;

namespace P2PVpn
{
    public partial class P2PVPNForm : Form
    {
        public Networking Network;
        //BindingSource appsBindingSource = new BindingSource();

        public P2PVPNForm()
        {
            InitializeComponent();

            Logging.Init(lbLog, statusStrip, lblStatusText);
            Network = new Networking(timerMediaServerOffline, lbLog);
            Network.NetworkListManager.NetworkConnectivityChanged += NetworkListManager_NetworkConnectivityChanged;
            Network.ShowNetworkTraffic();

            ControlHelpers.P2PVPNForm = this;

            //if (Debugger.IsAttached)
            //{
            //    FileIO.ResetTransfers();
            //}

            PopulateSettings();
            PopulateLaunchAppsGrid();
            PopulateControls();
            if (!bwFileTransfer.IsBusy)
                bwFileTransfer.RunWorkerAsync();
        }

        private void NetworkListManager_NetworkConnectivityChanged(Guid networkId, NETWORKLIST.NLM_CONNECTIVITY newConnectivity)
        {
            this.EnableForm(false);
            //wait for network to disconnect
            ControlHelpers.Sleep(2000).Wait();
            PopulateControls();
            this.EnableForm();
        }
        private void PopulateControls()
        {
            string connections = "";
            bool vpnFound = false;
            if (Networking.ActiveNetworkAdapters.Count == 0)
            {
                lblVPNConnectionStatus.SetControlText("Disconnected");
                lblConnectionStatus.SetControlText("Disconnected");
                Logging.SetStatus("OpenVPN Disconnected", Logging.Colors.Red);
            }
            foreach (var adapter in Networking.ActiveNetworkAdapters)
            {
                if (Networking.IsVPNAdapter(adapter))
                {
                    vpnFound = true;
                    lblVPNConnectionStatus.SetControlText(string.Format("{0}    IP: {1} {2}bytes sent: {3}k  bytes received: {4}k  speed: {5}k{2}",
                        adapter.Name, adapter.IpAddress, Environment.NewLine, adapter.BytesSent, adapter.BytesReceived, adapter.Speed));
                }
                else
                {
                    connections += string.Format("{0}    IP: {1} {2}bytes sent: {3}k  bytes received: {4}k  speed: {5}k{2}",
                        adapter.Name, adapter.IpAddress, Environment.NewLine, adapter.BytesSent, adapter.BytesReceived, adapter.Speed);
                    lblConnectionStatus.SetControlText(connections);
                }
                if (!vpnFound)
                {
                    lblVPNConnectionStatus.SetControlText("Disconnected");
                }
            }


            if (Network.IsOpenVPNConnected())
            {
                btnConnect.SetControlText("Disconnect");
                Logging.SetStatus("OpenVPN Connected", Logging.Colors.Green);
            }
            else
            {
                btnConnect.SetControlText("Connect");
                Logging.SetStatus("OpenVPN Disconnected", Logging.Colors.Red);
            }
            var apps = Apps.Get();
            string runningApps = "";
            foreach (var app in apps)
            {
                var name = Path.GetFileNameWithoutExtension(app.Program);
                var procs = Process.GetProcessesByName(name);
                if (procs != null && procs.Count() > 0)
                {
                    runningApps += name + Environment.NewLine;
                }
            }
            if (string.IsNullOrEmpty(runningApps))
            {
                runningApps = "None.";
            }
            ControlHelpers.SetControlText(lblRunningApps, runningApps);
            //timerMediaServerOffline.Tick += timerMediaServerOffline_Tick;

        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            Connect();

        }
        public void Connect()
        {
            this.EnableForm(false);

            if (Network.IsOpenVPNConnected())
            {
                //connected to vpn, so dissconnect
                Disconnect();

                //Networking.DisableDisconnect = true;
                Network.ScanNetworkInterfaces();
            }
            else
            {
                Logging.SetStatus("Connecting...", Logging.Colors.Yellow);
                Network.SaveOriginalDnsSettings();
                //not connected to vpn, so connect
                ControlHelpers.StartProcess("taskkill", "/F /IM openvpn-gui.exe");
                ControlHelpers.StartProcess("taskkill", "/F /IM openvpn.exe");

                Thread.Sleep(5000);
                CopyOpenVPNAssets();
                OpenVPN.SecureConfigs(false);

                Network.EnableAllNeworkInterfaces();
                Networking.DisableDisconnect = true;

                //wait for vpn to connect
                Settings settings = Settings.Get();
                var openVPN = settings.OpenVPNDirectory + @"\bin\openvpn-gui.exe";
                var config = settings.OpenVPNConfig;
                if (settings.VPNServer.VPNGate) config = VPNGate.VpnGateConifg;

                var openVPNargs = "--connect " + config;
                ControlHelpers.StartProcess(openVPN, openVPNargs, true, 25);

                //Thread.Sleep(25000);

                Network.ScanNetworkInterfaces();
                if (Network.IsOpenVPNConnected())
                {
                    if (settings.SplitRoute)
                    {
                        Network.SetRoutesAndDNS();
                    }
                    else
                    {
                        Network.SetAutoDNS(false);
                    }

                    Logging.SetStatus("OpenVPN Connected", Logging.Colors.Green);

                    //ensure dns is flushed
                    ControlHelpers.StartProcess(@"ipconfig.exe", @"/registerdns");
                    ControlHelpers.StartProcess(@"ipconfig.exe", @"/flushdns");
                    Network.OpenPrograms();
                    if (settings.PreventSystemSleep)
                    {
                        SystemUtils.PreventSleep();
                    }
                }
                //Thread.Sleep(60000);
                //Networking.DisableDisconnect = false;
                if (!bwFileTransfer.IsBusy)
                    bwFileTransfer.RunWorkerAsync();
            }
            this.EnableForm();

            PopulateControls();
            // Thread.Sleep(10000);
            Task.Delay(5000).Wait();
            //Thread.Sleep(20000);
            Networking.DisableDisconnect = false;

        }

        private void SaveOriginalDnsSettings()
        {
            throw new NotImplementedException();
        }

        private void Disconnect()
        {
            Networking.DisableRetryConnect = true;
            Logging.SetStatus("Disconnecting...", Logging.Colors.Yellow);
            lbLog.Log("Closing OpenVpn...");
            ControlHelpers.StartProcess("taskkill", "/F /IM openvpn-gui.exe");
            ControlHelpers.StartProcess("taskkill", "/F /IM openvpn.exe");
            Network.ResetNetworkInterfaces();
            lbLog.Log("Flushing DNS...");
            ControlHelpers.StartProcess(@"ipconfig.exe", @"/flushdns");
            ControlHelpers.StartProcess(@"ipconfig.exe", @"/registerdns");
            btnSaveApps_Click(null, null);

            //wait for dns flush
            //await ControlHelpers.Sleep(10000);
            Network.ClosePrograms(); //**dont add await here it causes hang
            Networking.DisableDisconnect = true;
            Network.EnableAllNeworkInterfaces();

            Task.Delay(60000).ContinueWith((t) =>
            {
                Networking.DisableRetryConnect = false;
            });

            //Task.Run(() =>
            //{


            //});
            //delayReconnect.Start();

            Logging.SetStatus("OpenVPN Disconnected", Logging.Colors.Red);
        }

        private void timerSpeed_Tick(object sender, EventArgs e)
        {

            Network.ScanNetworkInterfaces();
            PopulateControls();
        }

        private void P2PVPNForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //FileIO.StopQueue = true;
            tabs.SelectedTab = tabVPNTraffic;
            Disconnect();
            try
            {
                Utilities.MediaServer.TakeShareOffline(true);
                FileSync.Dispose(true);
            }
            catch
            {
            }

            //FileIO.ResetTransfers();
        }
        private void CopyOpenVPNAssets()
        {
            Settings settings = Settings.Get();
            var sourceDir = Directory.GetFiles(Settings.AppDir + @"\Assets\OpenVPN");
            var targetDir = Path.GetFullPath(settings.OpenVPNDirectory + @"\bin");
            foreach (var file in sourceDir)
            {
                var filename = Path.GetFileName(file);
                var targetFile = targetDir + @"\" + filename;
                if (!File.Exists(targetFile))
                {
                    File.Copy(file, targetFile, true);
                }
            }

        }
        //private void cdTorProxyChrome_CheckedChanged(object sender, EventArgs e)
        //{
        //    cdTorProxyChrome.CheckedChanged -= cdTorProxyChrome_CheckedChanged;

        //    Settings settings = Settings.Get();
        //    settings.EnableTorProxyForChrome = cdTorProxyChrome.Checked;
        //    Settings.Save(settings);
        //    Networking.SetTorProxyForChrome();

        //    cdTorProxyChrome.CheckedChanged += cdTorProxyChrome_CheckedChanged;
        //}
        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        #region MediaServer

        private void PopulateMediaServerControls()
        {
            Settings settings = Settings.Get();
            tbMediaUsername.Text = settings.MediaServer.Username;
            tbMediaPassword.Text = settings.MediaServer.Password;
            tbMediaDomain.Text = settings.MediaServer.Domain;

            lblMediaSource.Text = settings.MediaFileTransfer.SourceDirectory;

            lblMediaDestination.Text = settings.MediaFileTransfer.TargetDirectory;
            lblExcludeFolder.Text = settings.ExcludedFolderFromMediaTransfer;

            bool isOffline = true;

            try
            {
                isOffline = Utilities.MediaServer.IsShareOffline(settings.MediaServer);

            }
            catch (Exception ex)
            {
                //timerMediaServerOffline.Enabled = false;
                //settings.MediaServer.ShareName = "";
                //Settings.Save(settings);
                ControlHelpers.ShowMessageBox(ex.Message, ControlHelpers.MessageBoxType.Error);

            }
            lblMediaNetworkShare.Text = settings.MediaServer.ShareName;
            if (!string.IsNullOrEmpty(settings.MediaServer.ShareName))
            {
                //timerMediaServerOffline.Enabled = true;
            }
            if (isOffline)
            {
                btnMediaFolderOffline.Text = "Bring Media Share Online";
                toolTip.SetToolTip(picParentalControls, "Media Share is Offline (Parental Controls are On)");
                picParentalControls.Image = P2PVpn.Properties.Resources.Stop_red1;
                btnMediaFolderOffline.Image = P2PVpn.Properties.Resources.Start2;
                statusStrip.SetStatusBarLabel(lblStatusMediaShare, "Media Share is Offline", P2PVpn.Properties.Resources.Stop_red1);
            }
            else
            {
                btnMediaFolderOffline.Text = "Bring Media Share Offline";
                toolTip.SetToolTip(picParentalControls, "Media Share is Online (Parental Controls are Off)");
                picParentalControls.Image = P2PVpn.Properties.Resources.Start1;
                btnMediaFolderOffline.Image = P2PVpn.Properties.Resources.Stop_red2;
                statusStrip.SetStatusBarLabel(lblStatusMediaShare, "Media Share is Online", P2PVpn.Properties.Resources.Start2);
            }
            lblMediaCopyProgress.Text = "";
            cbMediaParentalTime.SelectedIndexChanged -= cbMediaParentalTime_SelectedIndexChanged;
            cbMediaParentalTime.Text = Utilities.MediaServer.GetSelectedOfflineValue(settings.MediaServer);
            cbMediaParentalTime.SelectedIndexChanged += cbMediaParentalTime_SelectedIndexChanged;
            //timerMediaServerOffline_Tick(null, null);
            //WatchFileSystem();
        }

        private void btnMediaFolderOffline_Click(object sender, EventArgs e)
        {
            //string shareName = @"\\OLSONHOME\Movies\STREAMING\Movies";

            //Models.MediaServer mediaServer = new Models.MediaServer()
            //{
            //    Username = "Mitch",
            //    Password = "1",
            //    Domain = "olsonhome",
            //    ShareName = shareName
            //};
            this.EnableForm(false);
            Settings settings = Settings.Get();
            if (string.IsNullOrWhiteSpace(settings.MediaServer.ShareName))
            {
                ControlHelpers.ShowMessageBox("Enter a media share.", ControlHelpers.MessageBoxType.Warning, false);
                return;
            }
            try
            {
                bool isOffline = Utilities.MediaServer.TakeShareOffline();
            }
            catch (Exception ex)
            {
                //timerMediaServerOffline.Enabled = false;
                //settings.MediaServer.ShareName = "";

                settings.MediaServer.EnableParentalControlsEvery = 0;
                Settings.Save(settings);
                ControlHelpers.ShowMessageBox(ex.Message, ControlHelpers.MessageBoxType.Error);
            }

            PopulateMediaServerControls();
            this.EnableForm();

        }

        public void WatchFileSystem()
        {
            try
            {
                Settings settings = Settings.Get();
                FileIO.FinshedFileTransfer += (sender, info) =>
                {
                    lblMediaCopyProgress.SetControlText("Finished Copying " + info.SourceFile);
                    //ControlHelpers.s
                    Logging.Log("Finished Copying " + info.SourceFile);
                };

                FileIO.FileTransferProgress += (sender, info) =>
                {
                    string transfer = string.Format("Copying: {0}   {1}%", info.SourceFile, info.PercentComplete);
                    lblMediaCopyProgress.SetControlText(transfer);

                    //var fileTransfer = settings.MediaFileTransferQue.FirstOrDefault(x => x.SourceDirectory == sender.SourceDirectory);

                    //if (fileTransfer != null && !fileTransfer.IsTransfering)
                    //{
                    //    fileTransfer.IsTransfering = true;
                    //    Settings.Save(settings);
                    //}
                    //Logging.Log("File Transfer Percent: " + percentComplete.ToString());
                };



                if (string.IsNullOrWhiteSpace(settings.MediaFileTransfer.SourceDirectory) ||
                string.IsNullOrWhiteSpace(settings.MediaFileTransfer.TargetDirectory))
                {
                    return;
                }
                FileSync.WatchFileSystem(settings.MediaFileTransfer.SourceDirectory, settings.MediaFileTransfer.TargetDirectory);
                //FileIO.WatchFileSystem();
                //FileIO.ProcessFileTransferQueue();
            }
            catch (Exception ex)
            {
                ControlHelpers.ShowMessageBox(ex.Message, ControlHelpers.MessageBoxType.Error);
                return;
            }


        }

        private void btnMediaTarget_Click(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            this.openMediaDestFolderBrowser.SelectedPath = settings.MediaFileTransfer.TargetDirectory;

            var result = this.openMediaDestFolderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                settings.MediaFileTransfer.TargetDirectory = this.openMediaDestFolderBrowser.SelectedPath;
                Settings.Save(settings);
                //PopulateMediaServerControls();
                lblMediaDestination.Text = settings.MediaFileTransfer.TargetDirectory;
                //timerMediaServerOffline.Enabled = true;
                //WatchFileSystem();
                if (!bwFileTransfer.IsBusy)
                    bwFileTransfer.RunWorkerAsync();
            }
        }
        private void btnMediaSource_Click(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            this.openMediaDestFolderBrowser.SelectedPath = settings.MediaFileTransfer.SourceDirectory;

            var result = this.openMediaDestFolderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                settings.MediaFileTransfer.SourceDirectory = this.openMediaDestFolderBrowser.SelectedPath;
                Settings.Save(settings);
                //PopulateMediaServerControls();
                lblMediaSource.Text = settings.MediaFileTransfer.SourceDirectory;
                //WatchFileSystem();
                if (!bwFileTransfer.IsBusy)
                    bwFileTransfer.RunWorkerAsync();
            }
        }
        private void btnExcludeFolder_Click(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            this.openMediaDestFolderBrowser.SelectedPath = settings.ExcludedFolderFromMediaTransfer;

            var result = this.openMediaDestFolderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                settings.ExcludedFolderFromMediaTransfer = this.openMediaDestFolderBrowser.SelectedPath;
                Settings.Save(settings);
                //PopulateMediaServerControls();
                lblExcludeFolder.Text = settings.ExcludedFolderFromMediaTransfer;
                //WatchFileSystem();
                //bwFileTransfer.RunWorkerAsync();
            }
        }
        private void tbMediaUsername_Leave(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            settings.MediaServer.Username = tbMediaUsername.Text;
            Settings.Save(settings);
        }

        private void tbMediaPassword_Leave(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            settings.MediaServer.Password = tbMediaPassword.Text;
            Settings.Save(settings);
        }

        private void tbMediaDomain_Leave(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            settings.MediaServer.Domain = tbMediaDomain.Text;
            Settings.Save(settings);
        }

        private void btnMediaNetworkShare_Click(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            this.openMediaDestFolderBrowser.SelectedPath = settings.MediaServer.ShareName;

            var result = this.openMediaDestFolderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                settings.MediaServer.ShareName = this.openMediaDestFolderBrowser.SelectedPath;
                Settings.Save(settings);
                //PopulateSettings();
                lblMediaNetworkShare.Text = settings.MediaServer.ShareName;
            }
        }

        private void cbMediaParentalTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            string offlineVal = cbMediaParentalTime.Text;
            if (string.IsNullOrEmpty(offlineVal)) return;
            string val = offlineVal[0].ToString();
            settings.MediaServer.EnableParentalControlsEvery = int.Parse(val);
            Settings.Save(settings);
        }
        private void timerMediaServerOffline_Tick(object sender, EventArgs e)
        {
            if (!Networking.IsLocalNetworkConnected()) return;

            Settings settings = Settings.Get();

            if (string.IsNullOrWhiteSpace(settings.MediaFileTransfer.SourceDirectory) ||
                string.IsNullOrWhiteSpace(settings.MediaFileTransfer.TargetDirectory))
            {
                return;
            }

            try
            {
                if (settings.MediaServer.EnableParentalControlsEvery > 0 && !Utilities.MediaServer.IsShareOffline(settings.MediaServer))
                {
                    DateTime bringMediaServerOfflineTime = settings.MediaServer.ParentalControlsLastEnabled.AddHours(settings.MediaServer.EnableParentalControlsEvery);
                    TimeSpan timeRemaining = bringMediaServerOfflineTime - DateTime.Now;

                    lblMediaTimeRemaining.Text = string.Format("{0}:{1} Remaining", timeRemaining.Hours, timeRemaining.Minutes);
                    statusStrip.SetStatusBarLabel(lblStatusMediaShare, string.Format("Media Share is Online {0}:{1}", timeRemaining.Hours, timeRemaining.Minutes), P2PVpn.Properties.Resources.Start2);
                    if (DateTime.Now >= bringMediaServerOfflineTime)
                    {
                        btnMediaFolderOffline_Click(null, null);
                        lblMediaTimeRemaining.Text = "";
                        
                    }
                }
                else
                {
                    lblMediaTimeRemaining.Text = "";
                }
            }
            catch { } 
        }
        private void bwTorrentDownloadComplete_DoWork(object sender, DoWorkEventArgs e)
        {
            string sourceDir = e.Argument.ToString();
            FileIO.CopyTorrentDownload(sourceDir);
        }
        #endregion MediaServer

        #region Apps
        private void PopulateLaunchAppsGrid()
        {
            //dgOpenApps.AutoGenerateColumns = false;

            List<Apps> data = Apps.Get();

            appsBindingSource.DataSource = data;
            //dgApps.DataSource = data;
            dgOpenApps.DataSource = appsBindingSource;

            //appsBindingSource.DataSource = data;
            //appsBindingSource.ResetBindings(true);
            ////dgOpenApps.DataBindings.bi
            //dgOpenApps.DataSource = appsBindingSource;
            //appsBindingSource.ResetBindings(true);

            ////this.appsBindingSource.EndEdit();

            //dgOpenApps.AutoGenerateColumns = false;

        }
        private void btnSaveApps_Click(object sender, EventArgs e)
        {

            this.appsBindingSource.EndEdit();
            List<Apps> apps = this.appsBindingSource.DataSource as List<Apps>;
            Apps.Save(apps);
            OpenVPN.UpdateDownScript();
            PopulateLaunchAppsGrid();
        }

        private void btnMoveAppUp_Click(object sender, EventArgs e)
        {
            this.appsBindingSource.EndEdit();
            this.appsBindingSource.GridRowMoveUp();
        }

        private void btnMoveAppDown_Click(object sender, EventArgs e)
        {

            this.appsBindingSource.EndEdit();
            this.appsBindingSource.GridRowMoveDown();
        }

        #endregion Apps


        #region Settings
        private void PopulateSettings()
        {
            Settings settings = Settings.Get();
            lblOpenVPNDirectory.Text = settings.OpenVPNDirectory;
            openVPNFolderBrowser.SelectedPath = settings.OpenVPNDirectory;
            btnBrowseForOpenVpn.MoveRightOf(lblOpenVPNDirectory);
            btnOpenVpnDirDefault.MoveRightOf(btnBrowseForOpenVpn);
            PopulateVPNConfigs();
            PopulateTextBoxes();
            PopulateMediaServerControls();
            SetRadioButtons();
            PopulateSystemSleepText();
        }

       
        private void PopulateVPNConfigs()
        {
            Settings settings = Settings.Get();

            if (settings.OpenVPNConfigs == null || settings.OpenVPNConfigs.Count == 0)
            {
                ControlHelpers.ShowMessageBox("No OpenVpn Configs found.", ControlHelpers.MessageBoxType.Error);
                return;
            }

            cbVPNBookOpenVPNConfig.Text = settings.OpenVPNConfig;
            cbVPNBookOpenVPNConfig.SelectedText = settings.OpenVPNConfig;
            cbVPNBookOpenVPNConfig.DataSource = new BindingSource(settings.OpenVPNConfigs, null);
            cbVPNBookOpenVPNConfig.DisplayMember = "Key";
            cbVPNBookOpenVPNConfig.ValueMember = "Value";
            cbVPNBookOpenVPNConfig.Text = settings.OpenVPNConfig;


        }
        private void PopulateTextBoxes()
        {
            Settings settings = Settings.Get();

            tbPrimaryDNS.Text = settings.PrimaryDNS;
            tbSecondaryDNS.Text = settings.SecondaryDNS;
            tbVPNPassword.Text = settings.VPNBookPassword;
            tbVPNUsername.Text = settings.VPNBookUsername;


        }
        private void SetRadioButtons()
        {
            rbGoogleDNS.CheckedChanged -= rbGoogleDNS_CheckedChanged;
            rbOpenDNS.CheckedChanged -= rbOpenDNS_CheckedChanged;
            rbComodoDNS.CheckedChanged -= rbComodoDNS_CheckedChanged;
            //cbRouteSplit.CheckedChanged -= cbRouteSplit_CheckedChanged;
            cbRetrieveVPNBookCredsOnLoad.CheckedChanged -= cbRetrieveVPNBookCredsOnLoad_CheckedChanged;
            rbVPNGate.CheckedChanged -= rbVPNGate_CheckedChanged;
            rbVPNBook.CheckedChanged -= rbVPNBook_CheckedChanged;

            Settings settings = Settings.Get();
            cbDontResetOnDisconn.Checked = settings.DontResetDNS;

            rbGoogleDNS.Checked = false;
            rbComodoDNS.Checked = false;
            rbOpenDNS.Checked = false;
            if (settings.PrimaryDNS == Settings.DefaultGoogleDNSPrimary &&
                settings.SecondaryDNS == Settings.DefaultGoogleDNSSecondary)
            {
                rbGoogleDNS.Checked = true;
            }
            else if (settings.PrimaryDNS == Settings.DefaultComodoDNSPrimary &&
               settings.SecondaryDNS == Settings.DefaultComodoDNSSecondary)
            {
                rbComodoDNS.Checked = true;
            }
            else if (settings.PrimaryDNS == Settings.DefaultOpenDNSPrimary &&
               settings.SecondaryDNS == Settings.DefaultOpenDNSSecondary)
            {
                rbOpenDNS.Checked = true;
            }
            cbRouteSplit.Checked = settings.SplitRoute;

            rbVPNBook.Checked = settings.VPNServer.VPNBook;
            rbVPNGate.Checked = settings.VPNServer.VPNGate;
            //settings.VPNServer.VPNBook = (!settings.VPNServer.VPNBook && !settings.VPNServer.VPNGate);
            cbRetrieveVPNBookCredsOnLoad.Checked = settings.RetrieveVPNBookCredsOnLoad;
            cbRetrieveVPNBookCredsOnLoad.Enabled = settings.VPNServer.VPNBook;
            if (settings.VPNServer.VPNBook)
            {
                lblVPNConnectionStatusLabel.Text = "VPN Book Connection:";
                this.statusStrip.SetStatusBarLabel(lblOpenVPNConifg, "Config: " + settings.OpenVPNConfig);
                if (settings.RetrieveVPNBookCredsOnLoad)
                {
                    btnVPNBookUser_Click(null, null);
                    
                }
            }
            if (settings.VPNServer.VPNGate)
            {
                lblVPNConnectionStatusLabel.Text = "VPN Gate Connection:";
                this.statusStrip.SetStatusBarLabel(lblOpenVPNConifg, "Config: " + VPNGate.VpnGateConifg);
            }
            rbVPNGate.CheckedChanged += rbVPNGate_CheckedChanged;
            rbVPNBook.CheckedChanged += rbVPNBook_CheckedChanged;
            rbGoogleDNS.CheckedChanged += rbGoogleDNS_CheckedChanged;
            rbOpenDNS.CheckedChanged += rbOpenDNS_CheckedChanged;
            rbComodoDNS.CheckedChanged += rbComodoDNS_CheckedChanged;
            cbRetrieveVPNBookCredsOnLoad.CheckedChanged += cbRetrieveVPNBookCredsOnLoad_CheckedChanged;
            //cbRouteSplit.CheckedChanged += cbRouteSplit_CheckedChanged;
        }
        private void btnBrowseForOpenVpn_Click(object sender, EventArgs e)
        {
            var result = this.openVPNFolderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                Settings settings = Settings.Get();
                settings.OpenVPNDirectory = this.openVPNFolderBrowser.SelectedPath;
                Settings.Save(settings);
                PopulateSettings();
            }
        }

        private void btnOpenVpnDirDefault_Click(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            settings.OpenVPNDirectory = Settings.DefaultOpenVPNDirectory;
            Settings.Save(settings);
            PopulateSettings();
        }
        private void hlVPNBookConfigDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ControlHelpers.StartProcess(Settings.DefaultVPNBookConfigDownload, "");
        }


        private void cbOpenVPNConfig_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            var selected = ((KeyValue)cbVPNBookOpenVPNConfig.SelectedItem).Key;
            settings.OpenVPNConfig = selected;
            Settings.Save(settings);
        }
        private void btnVPNBookUser_Click(object sender, EventArgs e)
        {
            try
            {

                WebClient wc = Networking.GetTorWebClient();

                var doc = wc.DownloadString(Settings.DefaultVPNBookCredsPage);

                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(doc);

                //var web = new HtmlWeb();

                //var document = web.Load(Settings.DefaultVPNBookCredsPage);
                var page = document.DocumentNode;

                //check if blocked
                if (page.InnerHtml.ToLower().Contains(Settings.BlockedVpnBookProxyText))
                {
                    ControlHelpers.ShowMessageBox("vpnbook.com blocked by your proxy settings", ControlHelpers.MessageBoxType.Error);
                }

                var ul = page.QuerySelector("ul.disc");
                var liUsername = ul.ChildNodes.First(li => li.InnerText.ToLower().Contains("username"));
                var username = liUsername.InnerText.TrimStart("Username:".ToCharArray()).Trim();
                var liPassword = ul.ChildNodes.First(li => li.InnerText.ToLower().Contains("password"));
                var password = liPassword.InnerText.TrimStart("Password:".ToCharArray()).Trim();
                tbVPNUsername.Text = username;
                tbVPNPassword.Text = password;
                tbVPNUsername_Leave(sender, e);
                tbVPNPassword_Leave(sender, e);
            }
            catch (WebException ex)
            {
                string message = string.Format(@"{0}. proxy={1}{2}{2} Would you like to install Tor Browser? ", ex.Message, Settings.BrowserProxy, Environment.NewLine);
                ControlHelpers.ShowMessageBoxYesNo(message, @"https://www.torproject.org/download/download-easy.html.en", ControlHelpers.MessageBoxType.Error);
            }
            catch (Exception ex)
            {
                lbLog.Log(ex.Message);
            }
        }
        private void tbVPNUsername_Leave(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            settings.VPNBookUsername = tbVPNUsername.Text;
            Settings.Save(settings);
            OpenVPN.SaveUsername(settings.VPNBookUsername);
        }
        private void tbVPNPassword_Leave(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            settings.VPNBookPassword = tbVPNPassword.Text;
            Settings.Save(settings);
            OpenVPN.SavePassword(settings.VPNBookPassword);
        }
        private void cbRouteSplit_CheckedChanged(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            settings.SplitRoute = cbRouteSplit.Checked;
            pnlDns.Enabled = settings.SplitRoute;
            Settings.Save(settings);
        }
        private void rbVPNBook_CheckedChanged(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();

            settings.VPNServer.VPNBook = rbVPNBook.Checked;
            settings.VPNServer.VPNGate = rbVPNGate.Checked;

            Settings.Save(settings);

            SetRadioButtons();
            //cbRetrieveVPNBookCredsOnLoad_CheckedChanged(sender, e);
        }
        private void rbVPNGate_CheckedChanged(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();

            settings.VPNServer.VPNGate = rbVPNGate.Checked;
            settings.VPNServer.VPNBook = rbVPNBook.Checked;

            Settings.Save(settings);

            SetRadioButtons();
        }
        private void cbRetrieveVPNBookCredsOnLoad_CheckedChanged(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            settings.RetrieveVPNBookCredsOnLoad = cbRetrieveVPNBookCredsOnLoad.Checked;
            Settings.Save(settings);
        }
        private void cbDisableSystemSleep_CheckedChanged(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            settings.PreventSystemSleep = cbDisableSystemSleep.Checked;
            Settings.Save(settings);
            PopulateSystemSleepText();
            if (settings.PreventSystemSleep)
            {
                SystemUtils.PreventSleep();
            }
            else
            {
                SystemUtils.AllowSleep();
            }
        }
        private void PopulateSystemSleepText()
        {
            Settings settings = Settings.Get();
            cbDisableSystemSleep.Checked = settings.PreventSystemSleep;
            if (settings.PreventSystemSleep)
            {
                cbDisableSystemSleep.Text = "Sleep Disabled";
            }
            else
            {
                cbDisableSystemSleep.Text = "Sleep Enabled";
            }
        }
        #region DNS Settings
        public void cbResetOnDisconn_CheckedChanged(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            settings.DontResetDNS = cbDontResetOnDisconn.Checked;
            Settings.Save(settings);
        }
        private void rbGoogleDNS_CheckedChanged(object sender, EventArgs e)
        {
            tbPrimaryDNS.Text = Settings.DefaultGoogleDNSPrimary;
            tbSecondaryDNS.Text = Settings.DefaultGoogleDNSSecondary;
            tbPrimaryDNS_Leave(sender, e);
            tbSecondaryDNS_Leave(sender, e);
        }

        private void rbOpenDNS_CheckedChanged(object sender, EventArgs e)
        {
            tbPrimaryDNS.Text = Settings.DefaultOpenDNSPrimary;
            tbSecondaryDNS.Text = Settings.DefaultOpenDNSSecondary;
            tbPrimaryDNS_Leave(sender, e);
            tbSecondaryDNS_Leave(sender, e);
        }

        private void rbComodoDNS_CheckedChanged(object sender, EventArgs e)
        {
            tbPrimaryDNS.Text = Settings.DefaultComodoDNSPrimary;
            tbSecondaryDNS.Text = Settings.DefaultComodoDNSSecondary;
            tbPrimaryDNS_Leave(sender, e);
            tbSecondaryDNS_Leave(sender, e);
        }
        private void tbPrimaryDNS_Leave(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            IPAddress ipAddress = new IPAddress(tbPrimaryDNS.GetAddressBytes());
            settings.PrimaryDNS = ipAddress.ToString();
            Settings.Save(settings);
            SetRadioButtons();
        }

        private void tbSecondaryDNS_Leave(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            IPAddress ipAddress = new IPAddress(tbSecondaryDNS.GetAddressBytes());
            settings.SecondaryDNS = ipAddress.ToString();
            Settings.Save(settings);
            SetRadioButtons();
        }
        #endregion DNS Settings

      
        #endregion Settings


        #region Links
        private void lnkChromeIpLeak_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ControlHelpers.StartProcess(Settings.ChromeWebRTCExtensionUrl, "");
        }

        private void linkGetTorBrowser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ControlHelpers.StartProcess(Settings.TorBrowserDownloadUrl, "", false);
        }

        private void linkDownloadPeerBlock_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ControlHelpers.StartProcess(Settings.PeerBlockDownloadUrl, "", false);
        }

        private void linkDownloadOpenVPN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ControlHelpers.StartProcess(Settings.OpenVPNDownloadUrl, "", false);
        }

        private void linkDownloadCCCleaner_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ControlHelpers.StartProcess(Settings.CCCleanerDownloadUrl, "", false);
        }

        private void linkDownloadCPorts_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ControlHelpers.StartProcess(Settings.CPortsDownloadUrl, "", false);
        }

        private void linkDownloadqBittorrent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ControlHelpers.StartProcess(Settings.QBittorrentUrl, "", false);
        }

        private void linkIPLeak_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ControlHelpers.StartProcess(Settings.IPLeakUrl, "", false);
        }

        private void btnFirewallRules_Click(object sender, EventArgs e)
        {
            lblFirewallRules.Text = "Repairing Filewall Rules...";
            WinFirewall.CreateFirewallRules();
            lblFirewallRules.Text = "Finished Repairing Filewall Rules";
            firewallTimer.Enabled = true;
 
        }

        private void firewallTimer_Tick(object sender, EventArgs e)
        {
            lblFirewallRules.Text = "";
            firewallTimer.Enabled = false;
        }

        private void tabs_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabVPNGate)
            {
                Settings settings = Settings.Get();
                if (!settings.VPNServer.VPNGate)
                {
                    tabVPNGate.EnableTab(false);
                    return;
                }
                else
                {
                    tabVPNGate.EnableTab(true);
                }

                this.EnableForm(false);
                
                var servers = VPNGate.GetFastestServers();
                var selectedServer = servers.FirstOrDefault(x => x.HostName == settings.VPNGateServerHost);
                if (selectedServer == null)
                {
                    selectedServer = servers[0];
                }

                cbVPNGateServer.SelectedIndexChanged -= cbVPNGateServer_SelectedIndexChanged;
                cbVPNGateServer.DisplayMember = "CountryAndHost";
                cbVPNGateServer.DataSource = servers;
                cbVPNGateServer.SelectedItem = selectedServer;
                var server = (VPNGateServer)cbVPNGateServer.SelectedItem;

                lblVPNGateServerInfo.Text = server.ToString();

                cbVPNGateServer.SelectedIndexChanged += cbVPNGateServer_SelectedIndexChanged;

                cbVPNGateConnectRetry.CheckedChanged-=cbVPNGateConnectRetry_CheckedChanged;
                cbVPNGateConnectRetry.Checked = settings.RetryVPNGateConnect;
                cbVPNGateConnectRetry.CheckedChanged += cbVPNGateConnectRetry_CheckedChanged;

                this.EnableForm();
            }
            if (e.TabPage == tabVPNBook)
            {
                Settings settings = Settings.Get();
                if (!settings.VPNServer.VPNBook)
                {
                    tabVPNBook.EnableTab(false);
                    return;
                }
                else
                {
                    tabVPNBook.EnableTab(true);
                }
            }
        }

        private void cbVPNGateServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var server = (VPNGateServer)cbVPNGateServer.SelectedItem;
            lblVPNGateServerInfo.Text = server.ToString();
            Settings settings = Settings.Get();
            settings.VPNGateServerHost = server.HostName;
            Settings.Save(settings);
            VPNGate.SelectServer(server);
            SetRadioButtons();
        }
        private void cbVPNGateConnectRetry_CheckedChanged(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            settings.RetryVPNGateConnect = cbVPNGateConnectRetry.Checked;
            Settings.Save(settings);
        }

        private void P2PVPNForm_Leave(object sender, EventArgs e)
        {

        }

        private void P2PVPNForm_Load(object sender, EventArgs e)
        {
            
        }

        private void P2PVPNForm_Shown(object sender, EventArgs e)
        {
            
        }

        private void bwFileSync_DoWork(object sender, DoWorkEventArgs e)
        {
            WatchFileSystem();
        }

        private void bwFileTransfer_DoWork(object sender, DoWorkEventArgs e)
        {
            Logging.Log("?Background worker complete");
            WatchFileSystem();
        }

        private void bwFileTransfer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Logging.Log("?Background worker complete");
        }

        #endregion Links

        

        
    }
}
