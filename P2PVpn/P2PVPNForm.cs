using System;
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

namespace P2PVpn
{
    public partial class P2PVPNForm : Form
    {
        private Networking _network;
        //BindingSource appsBindingSource = new BindingSource();

        public P2PVPNForm()
        {
            InitializeComponent();
            
            Logging.Init(lbLog, statusStrip, lblStatusText, lblStatusColor);
            _network = new Networking(lbLog);
            _network.NetworkListManager.NetworkConnectivityChanged += NetworkListManager_NetworkConnectivityChanged;
            _network.ShowNetworkTraffic();
            PopulateSettings();
            PopulateLaunchAppsGrid();
            PopulateControls();
            
        }

        private async void NetworkListManager_NetworkConnectivityChanged(Guid networkId, NETWORKLIST.NLM_CONNECTIVITY newConnectivity)
        {
            this.EnableForm(false);
            //wait for network to disconnect
            await ControlHelpers.Sleep(2000);
            PopulateControls();
            this.EnableForm();
        }
        private void PopulateControls()
        {
            string connections = "";
            bool vpnFound = false;
            if (_network.ActiveNetworkAdapters.Count == 0)
            {
                lblVPNConnectionStatus.SetLabelText("Disconnected");
                lblConnectionStatus.SetLabelText("Disconnected");
                Logging.SetStatus("OpenVPN Disconnected", Logging.Colors.Red);
            }
            foreach (var adapter in _network.ActiveNetworkAdapters)
            {
                if (Networking.IsVPNAdapter(adapter))
                {
                    vpnFound = true;
                    lblVPNConnectionStatus.SetLabelText(string.Format("{0} {1} {2}bytes sent: {3}k  bytes received: {4}k  speed: {5}k{2}", 
                        adapter.Description, adapter.ConnectivityString, Environment.NewLine, adapter.BytesSent, adapter.BytesReceived, adapter.Speed));
                }
                else
                {
                    connections += string.Format("{0} {1} {2}bytes sent: {3}k  bytes received: {4}k  speed: {5}k{2}", 
                        adapter.Description, adapter.ConnectivityString, Environment.NewLine, adapter.BytesSent, adapter.BytesReceived, adapter.Speed);
                    lblConnectionStatus.SetLabelText(connections);
                }
                if (!vpnFound)
                {
                    lblVPNConnectionStatus.SetLabelText("Disconnected");
                }
            }
            

            if (_network.IsOpenVPNConnected())
            {
                btnConnect.SetButtonText("Disconnect");
                Logging.SetStatus("OpenVPN Connected", Logging.Colors.Green);
            }
            else
            {
                btnConnect.SetButtonText("Connect");
                Logging.SetStatus("OpenVPN Disconnected", Logging.Colors.Red);
            }
            var apps = Apps.Get();
            string runningApps = "";
            foreach(var app in apps)
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
            ControlHelpers.SetLabelText(lblRunningApps, runningApps);
            
        }
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            
            this.EnableForm(false);

            if (_network.IsOpenVPNConnected())
            {
                //connected to vpn, so dissconnect
                await Disconnect();

                //Networking.DisableDisconnect = true;
                _network.ScanNetworkInterfaces();
            }
            else
            {
                Logging.SetStatus("Connecting...", Logging.Colors.Yellow);
                //not connected to vpn, so connect
                ControlHelpers.StartProcess("taskkill", "/F /IM openvpn-gui.exe");
                ControlHelpers.StartProcess("taskkill", "/F /IM openvpn.exe");
                
                Thread.Sleep(5000);
                _network.EnableAllNeworkInterfaces();
                Networking.DisableDisconnect = true;

                //wait for vpn to connect
                Settings settings = Settings.Get();
                var openVPN = settings.OpenVPNDirectory + @"\bin\openvpn-gui.exe";
                var openVPNargs = "--connect " + settings.OpenVPNConfig;
                ControlHelpers.StartProcess(openVPN, openVPNargs, true, 25);
                
                //Thread.Sleep(25000);
                
                _network.ScanNetworkInterfaces();
                if (_network.IsOpenVPNConnected())
                {
                    string primaryDnsIp = settings.PrimaryDNS;
                    string secondaryDnsIp = settings.SecondaryDNS;
                    bool setDns = false;
                    foreach (var adapter in _network.ActiveNetworkAdapters)
                    {
                        //if (Networking.IsVPNAdapter(adapter)) continue;

                        //change Dns for added security
                        string name = adapter.Name;
                        
                        if (!string.IsNullOrWhiteSpace(primaryDnsIp) && primaryDnsIp != "0.0.0.0")
                        {
                            string primaryDns = string.Format("interface IPv4 set dnsserver \"{0}\" static {1} both", name, primaryDnsIp);
                            ControlHelpers.StartProcess(@"netsh", primaryDns);
                            setDns = true;
                        }
                        if (!string.IsNullOrWhiteSpace(secondaryDnsIp) && secondaryDnsIp != "0.0.0.0")
                        {
                            string secondaryDns = string.Format("interface ipv4 add dnsserver \"{0}\" address={1} index=2", name, secondaryDnsIp);
                            ControlHelpers.StartProcess(@"netsh", secondaryDns);
                            setDns = true;
                        }
                        if (setDns) lbLog.Log("Set DNS on {0} to {1}, {2}", name, primaryDnsIp, secondaryDnsIp);
                        Logging.SetStatus("OpenVPN Connected", Logging.Colors.Green);
                    }
                    if (setDns)
                    {
                        ControlHelpers.StartProcess(@"ipconfig.exe", @"/flushdns");
                        ControlHelpers.StartProcess(@"ipconfig.exe", @"/registerdns");
                    }
                    
                    //Process.Start(@"ipconfig.exe", @"/flushdns").WaitForExit();
                    _network.OpenPrograms();
                }
                //Thread.Sleep(60000);
                //Networking.DisableDisconnect = false;
            }
            this.EnableForm();
            
            PopulateControls();
            await ControlHelpers.Sleep(10000);
            //Thread.Sleep(20000);
            Networking.DisableDisconnect = false;
        }

        private async Task Disconnect()
        {
            Logging.SetStatus("Disconnecting...", Logging.Colors.Yellow);
            lbLog.Log("Closing OpenVpn...");
            ControlHelpers.StartProcess("taskkill", "/F /IM openvpn-gui.exe");
            ControlHelpers.StartProcess("taskkill", "/F /IM openvpn.exe");
            lbLog.Log("Flushing DNS...");
            ControlHelpers.StartProcess(@"ipconfig.exe", @"/flushdns");
            ControlHelpers.StartProcess(@"ipconfig.exe", @"/registerdns");
            btnSaveApps_Click(null, null);

            //wait for dns flush
            await ControlHelpers.Sleep(10000);
            //_network.ClosePrograms();
            Networking.DisableDisconnect = true;
            _network.EnableAllNeworkInterfaces();

            //wait for network interfaces to reset
            await ControlHelpers.Sleep(10000);
            Logging.SetStatus("OpenVPN Disconnected", Logging.Colors.Red);
        }



        private void timerSpeed_Tick(object sender, EventArgs e)
        {
            
            _network.ScanNetworkInterfaces();
            PopulateControls();
        }

        private async void P2PVPNForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await Disconnect();
        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        #region Apps
        private void PopulateLaunchAppsGrid()
        {
            dgOpenApps.AutoGenerateColumns = false;

            List<Apps> data = Apps.Get();

            appsBindingSource.DataSource = data;
            dgOpenApps.DataSource = appsBindingSource;

            dgOpenApps.AutoGenerateColumns = false;
        }
        private void btnSaveApps_Click(object sender, EventArgs e)
        {
          
            this.appsBindingSource.EndEdit();
            List<Apps> apps = this.appsBindingSource.DataSource as List<Apps>;
            Apps.Save(apps);
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
            SetRadioButton();
        }
        private void PopulateVPNConfigs()
        {
            Settings settings = Settings.Get();

            cbOpenVPNConfig.Text = settings.OpenVPNConfig;
            cbOpenVPNConfig.SelectedText = settings.OpenVPNConfig;
            cbOpenVPNConfig.DataSource = new BindingSource(settings.OpenVPNConfigs, null);
            cbOpenVPNConfig.DisplayMember = "Key";
            cbOpenVPNConfig.ValueMember = "Value";
            cbOpenVPNConfig.Text = settings.OpenVPNConfig;
            
            
        }
        private void PopulateTextBoxes()
        {
            Settings settings = Settings.Get();

            tbPrimaryDNS.Text = settings.PrimaryDNS;
            tbSecondaryDNS.Text = settings.SecondaryDNS;
            tbVPNPassword.Text = settings.VPNBookPassword;
            tbVPNUsername.Text = settings.VPNBookUsername;


        }
        private void btnBrowseForOpenVpn_Click(object sender, EventArgs e)
        {
            var result = this.openVPNFolderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                Settings settings = new Settings();
                settings.OpenVPNDirectory = this.openVPNFolderBrowser.SelectedPath;
                Settings.Save(settings);
                PopulateSettings();
            }
        }

        private void btnOpenVpnDirDefault_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.OpenVPNDirectory = Settings.DefaultOpenVPNDirectory;
            Settings.Save(settings);
            PopulateSettings();
        }
        private void hlVPNBookConfigDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ControlHelpers.StartProcess(Settings.DefaultVPNBookConfigDownload,"");
        }


        private void cbOpenVPNConfig_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            var selected = ((KeyValuePair<string, string>)cbOpenVPNConfig.SelectedItem).Key;
            settings.OpenVPNConfig = selected;
            Settings.Save(settings);
        }
        private void btnVPNBookUser_Click(object sender, EventArgs e)
        {
            try
            {
                var web = new HtmlWeb();
                var document = web.Load(Settings.DefaultVPNBookCredsPage);
                var page = document.DocumentNode;
               
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
        private void cbResetOnDisconn_CheckedChanged(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            settings.ResetDNS = cbResetOnDisconn.Checked;
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
            SetRadioButton();
        }

        private void tbSecondaryDNS_Leave(object sender, EventArgs e)
        {
            Settings settings = Settings.Get();
            IPAddress ipAddress = new IPAddress(tbSecondaryDNS.GetAddressBytes());
            settings.SecondaryDNS = ipAddress.ToString();
            Settings.Save(settings);
            SetRadioButton();
        }
        private void SetRadioButton()
        {
            rbGoogleDNS.CheckedChanged-=rbGoogleDNS_CheckedChanged;
            rbOpenDNS.CheckedChanged-=rbOpenDNS_CheckedChanged;
            rbComodoDNS.CheckedChanged-=rbComodoDNS_CheckedChanged;

            Settings settings = Settings.Get();
            cbResetOnDisconn.Checked = settings.ResetDNS;

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
            rbGoogleDNS.CheckedChanged += rbGoogleDNS_CheckedChanged;
            rbOpenDNS.CheckedChanged += rbOpenDNS_CheckedChanged;
            rbComodoDNS.CheckedChanged += rbComodoDNS_CheckedChanged;
        }
        #endregion Settings

    }
}
