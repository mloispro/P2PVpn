namespace P2PVpn
{
    partial class P2PVPNForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P2PVPNForm));
            this.timerSpeed = new System.Windows.Forms.Timer(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblOpenVPNConifg = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusMediaShare = new System.Windows.Forms.ToolStripStatusLabel();
            this.openVPNFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.tabVPNTraffic = new System.Windows.Forms.TabPage();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.tabVPNBook = new System.Windows.Forms.TabPage();
            this.cbRetrieveVPNBookCredsOnLoad = new System.Windows.Forms.CheckBox();
            this.cbVPNBookOpenVPNConfig = new System.Windows.Forms.ComboBox();
            this.hlVPNBookConfigDownload = new System.Windows.Forms.LinkLabel();
            this.lblVPNBookOpenVPNConfigLabel = new System.Windows.Forms.Label();
            this.btnVPNBookUser = new System.Windows.Forms.Button();
            this.tbVPNPassword = new System.Windows.Forms.TextBox();
            this.tbVPNUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.cbDisableSystemSleep = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlRoutes = new System.Windows.Forms.Panel();
            this.pnlDns = new System.Windows.Forms.Panel();
            this.tbPrimaryDNS = new IPAddressControlLib.IPAddressControl();
            this.pnlPublicDns = new System.Windows.Forms.Panel();
            this.rbGoogleDNS = new System.Windows.Forms.RadioButton();
            this.rbOpenDNS = new System.Windows.Forms.RadioButton();
            this.rbComodoDNS = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSecondaryDNS = new IPAddressControlLib.IPAddressControl();
            this.cbDontResetOnDisconn = new System.Windows.Forms.CheckBox();
            this.cbRouteSplit = new System.Windows.Forms.CheckBox();
            this.tbVPNConnWaitTime = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlOpenVPNServer = new System.Windows.Forms.Panel();
            this.rbCustomServer = new System.Windows.Forms.RadioButton();
            this.rbVPNGate = new System.Windows.Forms.RadioButton();
            this.rbVPNBook = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOpenVpnDirDefault = new System.Windows.Forms.Button();
            this.btnBrowseForOpenVpn = new System.Windows.Forms.Button();
            this.lblOpenVPNDirectory = new System.Windows.Forms.Label();
            this.lblOpenVPNDirLabel = new System.Windows.Forms.Label();
            this.lnkChromeIpLeak = new System.Windows.Forms.LinkLabel();
            this.tabOpenApps = new System.Windows.Forms.TabPage();
            this.btnMoveAppDown = new System.Windows.Forms.Button();
            this.btnMoveAppUp = new System.Windows.Forms.Button();
            this.btnSaveApps = new System.Windows.Forms.Button();
            this.dgOpenApps = new System.Windows.Forms.DataGridView();
            this.appsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabConnection = new System.Windows.Forms.TabPage();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.lblConnectionStatuslbl = new System.Windows.Forms.Label();
            this.lblRunningApps = new System.Windows.Forms.Label();
            this.lblRunningAppsLabel = new System.Windows.Forms.Label();
            this.lblVPNConnectionStatus = new System.Windows.Forms.Label();
            this.lblVPNConnectionStatusLabel = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabVPNGate = new System.Windows.Forms.TabPage();
            this.cbVPNGateConnectRetry = new System.Windows.Forms.CheckBox();
            this.lblVPNGateServerInfo = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbVPNGateServer = new System.Windows.Forms.ComboBox();
            this.tabSecurity = new System.Windows.Forms.TabPage();
            this.lblFirewallRules = new System.Windows.Forms.Label();
            this.btnFirewallRules = new System.Windows.Forms.Button();
            this.linkIPLeak = new System.Windows.Forms.LinkLabel();
            this.linkDownloadqBittorrent = new System.Windows.Forms.LinkLabel();
            this.linkDownloadCPorts = new System.Windows.Forms.LinkLabel();
            this.linkDownloadCCCleaner = new System.Windows.Forms.LinkLabel();
            this.linkDownloadOpenVPN = new System.Windows.Forms.LinkLabel();
            this.linkDownloadPeerBlock = new System.Windows.Forms.LinkLabel();
            this.linkGetTorBrowser = new System.Windows.Forms.LinkLabel();
            this.tabMediaServer = new System.Windows.Forms.TabPage();
            this.picParentalControls = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblMediaTimeRemaining = new System.Windows.Forms.Label();
            this.cbMediaParentalTime = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblMediaNetworkShare = new System.Windows.Forms.Label();
            this.btnMediaNetworkShare = new System.Windows.Forms.Button();
            this.btnMediaFolderOffline = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMediaCopyProgress = new System.Windows.Forms.TextBox();
            this.lblMediaDestination = new System.Windows.Forms.Label();
            this.lblMediaSource = new System.Windows.Forms.Label();
            this.btnMediaSource = new System.Windows.Forms.Button();
            this.btnMediaTarget = new System.Windows.Forms.Button();
            this.tbMediaDomain = new System.Windows.Forms.TextBox();
            this.tbMediaPassword = new System.Windows.Forms.TextBox();
            this.tbMediaUsername = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblMediaUsername = new System.Windows.Forms.Label();
            this.firewallTimer = new System.Windows.Forms.Timer(this.components);
            this.openMediaDestFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.timerMediaServerOffline = new System.Windows.Forms.Timer(this.components);
            this.bwFileTransfer = new System.ComponentModel.BackgroundWorker();
            this.bwTorrentDownloadComplete = new System.ComponentModel.BackgroundWorker();
            this.btnExcludeFolder = new System.Windows.Forms.Button();
            this.lblExcludeFolder = new System.Windows.Forms.Label();
            this.statusStrip.SuspendLayout();
            this.tabVPNTraffic.SuspendLayout();
            this.tabVPNBook.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.pnlRoutes.SuspendLayout();
            this.pnlDns.SuspendLayout();
            this.pnlPublicDns.SuspendLayout();
            this.pnlOpenVPNServer.SuspendLayout();
            this.tabOpenApps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOpenApps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appsBindingSource)).BeginInit();
            this.tabConnection.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabVPNGate.SuspendLayout();
            this.tabSecurity.SuspendLayout();
            this.tabMediaServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picParentalControls)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerSpeed
            // 
            this.timerSpeed.Enabled = true;
            this.timerSpeed.Interval = 2000;
            this.timerSpeed.Tick += new System.EventHandler(this.timerSpeed_Tick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusText,
            this.lblOpenVPNConifg,
            this.toolStripStatusLabel1,
            this.lblStatusMediaShare});
            this.statusStrip.Location = new System.Drawing.Point(0, 379);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(676, 24);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            this.statusStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip_ItemClicked);
            // 
            // lblStatusText
            // 
            this.lblStatusText.Image = global::P2PVpn.Properties.Resources.Stop_red2;
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(150, 19);
            this.lblStatusText.Text = "OpenVPN Disconnected";
            this.lblStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOpenVPNConifg
            // 
            this.lblOpenVPNConifg.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblOpenVPNConifg.Margin = new System.Windows.Forms.Padding(8, 3, 0, 2);
            this.lblOpenVPNConifg.Name = "lblOpenVPNConifg";
            this.lblOpenVPNConifg.Size = new System.Drawing.Size(53, 19);
            this.lblOpenVPNConifg.Text = "Conifg: ";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(304, 19);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // lblStatusMediaShare
            // 
            this.lblStatusMediaShare.Image = global::P2PVpn.Properties.Resources.Stop_red2;
            this.lblStatusMediaShare.Margin = new System.Windows.Forms.Padding(8, 3, 0, 2);
            this.lblStatusMediaShare.Name = "lblStatusMediaShare";
            this.lblStatusMediaShare.Size = new System.Drawing.Size(138, 19);
            this.lblStatusMediaShare.Text = "Media Share is Offline";
            // 
            // openVPNFolderBrowser
            // 
            this.openVPNFolderBrowser.Description = "OpenVPN Directory";
            this.openVPNFolderBrowser.RootFolder = System.Environment.SpecialFolder.ProgramFilesX86;
            this.openVPNFolderBrowser.SelectedPath = "C:\\Program Files (x86)\\OpenVPN";
            this.openVPNFolderBrowser.ShowNewFolderButton = false;
            // 
            // tabVPNTraffic
            // 
            this.tabVPNTraffic.Controls.Add(this.lbLog);
            this.tabVPNTraffic.Location = new System.Drawing.Point(4, 22);
            this.tabVPNTraffic.Name = "tabVPNTraffic";
            this.tabVPNTraffic.Padding = new System.Windows.Forms.Padding(3);
            this.tabVPNTraffic.Size = new System.Drawing.Size(644, 292);
            this.tabVPNTraffic.TabIndex = 4;
            this.tabVPNTraffic.Text = "Log";
            this.tabVPNTraffic.UseVisualStyleBackColor = true;
            // 
            // lbLog
            // 
            this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLog.FormattingEnabled = true;
            this.lbLog.HorizontalScrollbar = true;
            this.lbLog.Items.AddRange(new object[] {
            "hello",
            "good"});
            this.lbLog.Location = new System.Drawing.Point(3, 3);
            this.lbLog.Name = "lbLog";
            this.lbLog.ScrollAlwaysVisible = true;
            this.lbLog.Size = new System.Drawing.Size(638, 286);
            this.lbLog.TabIndex = 0;
            // 
            // tabVPNBook
            // 
            this.tabVPNBook.Controls.Add(this.cbRetrieveVPNBookCredsOnLoad);
            this.tabVPNBook.Controls.Add(this.cbVPNBookOpenVPNConfig);
            this.tabVPNBook.Controls.Add(this.hlVPNBookConfigDownload);
            this.tabVPNBook.Controls.Add(this.lblVPNBookOpenVPNConfigLabel);
            this.tabVPNBook.Controls.Add(this.btnVPNBookUser);
            this.tabVPNBook.Controls.Add(this.tbVPNPassword);
            this.tabVPNBook.Controls.Add(this.tbVPNUsername);
            this.tabVPNBook.Controls.Add(this.label2);
            this.tabVPNBook.Controls.Add(this.label1);
            this.tabVPNBook.Location = new System.Drawing.Point(4, 22);
            this.tabVPNBook.Name = "tabVPNBook";
            this.tabVPNBook.Padding = new System.Windows.Forms.Padding(3);
            this.tabVPNBook.Size = new System.Drawing.Size(644, 353);
            this.tabVPNBook.TabIndex = 5;
            this.tabVPNBook.Text = "VPN Book";
            this.tabVPNBook.UseVisualStyleBackColor = true;
            // 
            // cbRetrieveVPNBookCredsOnLoad
            // 
            this.cbRetrieveVPNBookCredsOnLoad.AutoSize = true;
            this.cbRetrieveVPNBookCredsOnLoad.Checked = true;
            this.cbRetrieveVPNBookCredsOnLoad.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRetrieveVPNBookCredsOnLoad.Location = new System.Drawing.Point(14, 117);
            this.cbRetrieveVPNBookCredsOnLoad.Name = "cbRetrieveVPNBookCredsOnLoad";
            this.cbRetrieveVPNBookCredsOnLoad.Size = new System.Drawing.Size(208, 17);
            this.cbRetrieveVPNBookCredsOnLoad.TabIndex = 12;
            this.cbRetrieveVPNBookCredsOnLoad.Text = "Retreive VPN Book Creds On Start Up";
            this.cbRetrieveVPNBookCredsOnLoad.UseVisualStyleBackColor = true;
            // 
            // cbVPNBookOpenVPNConfig
            // 
            this.cbVPNBookOpenVPNConfig.FormattingEnabled = true;
            this.cbVPNBookOpenVPNConfig.Location = new System.Drawing.Point(126, 12);
            this.cbVPNBookOpenVPNConfig.Name = "cbVPNBookOpenVPNConfig";
            this.cbVPNBookOpenVPNConfig.Size = new System.Drawing.Size(241, 21);
            this.cbVPNBookOpenVPNConfig.TabIndex = 5;
            this.cbVPNBookOpenVPNConfig.SelectionChangeCommitted += new System.EventHandler(this.cbOpenVPNConfig_SelectionChangeCommitted);
            // 
            // hlVPNBookConfigDownload
            // 
            this.hlVPNBookConfigDownload.AutoSize = true;
            this.hlVPNBookConfigDownload.Location = new System.Drawing.Point(373, 15);
            this.hlVPNBookConfigDownload.Name = "hlVPNBookConfigDownload";
            this.hlVPNBookConfigDownload.Size = new System.Drawing.Size(134, 13);
            this.hlVPNBookConfigDownload.TabIndex = 11;
            this.hlVPNBookConfigDownload.TabStop = true;
            this.hlVPNBookConfigDownload.Text = "Download From VPN Book";
            this.hlVPNBookConfigDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.hlVPNBookConfigDownload_LinkClicked);
            // 
            // lblVPNBookOpenVPNConfigLabel
            // 
            this.lblVPNBookOpenVPNConfigLabel.AutoSize = true;
            this.lblVPNBookOpenVPNConfigLabel.Location = new System.Drawing.Point(27, 15);
            this.lblVPNBookOpenVPNConfigLabel.Name = "lblVPNBookOpenVPNConfigLabel";
            this.lblVPNBookOpenVPNConfigLabel.Size = new System.Drawing.Size(93, 13);
            this.lblVPNBookOpenVPNConfigLabel.TabIndex = 4;
            this.lblVPNBookOpenVPNConfigLabel.Text = "VPN Book Config:";
            // 
            // btnVPNBookUser
            // 
            this.btnVPNBookUser.Location = new System.Drawing.Point(253, 47);
            this.btnVPNBookUser.Name = "btnVPNBookUser";
            this.btnVPNBookUser.Size = new System.Drawing.Size(138, 23);
            this.btnVPNBookUser.TabIndex = 10;
            this.btnVPNBookUser.Text = "Retreive VPN Book Creds";
            this.btnVPNBookUser.UseVisualStyleBackColor = true;
            this.btnVPNBookUser.Click += new System.EventHandler(this.btnVPNBookUser_Click);
            // 
            // tbVPNPassword
            // 
            this.tbVPNPassword.Location = new System.Drawing.Point(126, 80);
            this.tbVPNPassword.Name = "tbVPNPassword";
            this.tbVPNPassword.Size = new System.Drawing.Size(121, 20);
            this.tbVPNPassword.TabIndex = 9;
            this.tbVPNPassword.Leave += new System.EventHandler(this.tbVPNPassword_Leave);
            // 
            // tbVPNUsername
            // 
            this.tbVPNUsername.Location = new System.Drawing.Point(126, 49);
            this.tbVPNUsername.Name = "tbVPNUsername";
            this.tbVPNUsername.Size = new System.Drawing.Size(121, 20);
            this.tbVPNUsername.TabIndex = 7;
            this.tbVPNUsername.Leave += new System.EventHandler(this.tbVPNUsername_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "VPN Book Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "VPN Book Username:";
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.cbDisableSystemSleep);
            this.tabSettings.Controls.Add(this.label4);
            this.tabSettings.Controls.Add(this.pnlRoutes);
            this.tabSettings.Controls.Add(this.tbVPNConnWaitTime);
            this.tabSettings.Controls.Add(this.label6);
            this.tabSettings.Controls.Add(this.pnlOpenVPNServer);
            this.tabSettings.Controls.Add(this.label5);
            this.tabSettings.Controls.Add(this.btnOpenVpnDirDefault);
            this.tabSettings.Controls.Add(this.btnBrowseForOpenVpn);
            this.tabSettings.Controls.Add(this.lblOpenVPNDirectory);
            this.tabSettings.Controls.Add(this.lblOpenVPNDirLabel);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(644, 353);
            this.tabSettings.TabIndex = 3;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // cbDisableSystemSleep
            // 
            this.cbDisableSystemSleep.AutoSize = true;
            this.cbDisableSystemSleep.Location = new System.Drawing.Point(249, 118);
            this.cbDisableSystemSleep.Name = "cbDisableSystemSleep";
            this.cbDisableSystemSleep.Size = new System.Drawing.Size(97, 17);
            this.cbDisableSystemSleep.TabIndex = 10;
            this.cbDisableSystemSleep.Text = "Sleep Disabled";
            this.cbDisableSystemSleep.UseVisualStyleBackColor = true;
            this.cbDisableSystemSleep.CheckedChanged += new System.EventHandler(this.cbDisableSystemSleep_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(234, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Prevent System Sleep when connected to VPN:";
            // 
            // pnlRoutes
            // 
            this.pnlRoutes.Controls.Add(this.pnlDns);
            this.pnlRoutes.Controls.Add(this.cbRouteSplit);
            this.pnlRoutes.Location = new System.Drawing.Point(65, 164);
            this.pnlRoutes.Name = "pnlRoutes";
            this.pnlRoutes.Size = new System.Drawing.Size(442, 100);
            this.pnlRoutes.TabIndex = 8;
            // 
            // pnlDns
            // 
            this.pnlDns.Controls.Add(this.tbPrimaryDNS);
            this.pnlDns.Controls.Add(this.pnlPublicDns);
            this.pnlDns.Controls.Add(this.label3);
            this.pnlDns.Controls.Add(this.tbSecondaryDNS);
            this.pnlDns.Controls.Add(this.cbDontResetOnDisconn);
            this.pnlDns.Enabled = false;
            this.pnlDns.Location = new System.Drawing.Point(18, 27);
            this.pnlDns.Name = "pnlDns";
            this.pnlDns.Size = new System.Drawing.Size(399, 70);
            this.pnlDns.TabIndex = 24;
            // 
            // tbPrimaryDNS
            // 
            this.tbPrimaryDNS.AllowInternalTab = false;
            this.tbPrimaryDNS.AutoHeight = true;
            this.tbPrimaryDNS.BackColor = System.Drawing.SystemColors.Window;
            this.tbPrimaryDNS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tbPrimaryDNS.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbPrimaryDNS.Location = new System.Drawing.Point(49, 3);
            this.tbPrimaryDNS.MinimumSize = new System.Drawing.Size(87, 20);
            this.tbPrimaryDNS.Name = "tbPrimaryDNS";
            this.tbPrimaryDNS.ReadOnly = false;
            this.tbPrimaryDNS.Size = new System.Drawing.Size(87, 20);
            this.tbPrimaryDNS.TabIndex = 20;
            this.tbPrimaryDNS.Text = "...";
            // 
            // pnlPublicDns
            // 
            this.pnlPublicDns.Controls.Add(this.rbGoogleDNS);
            this.pnlPublicDns.Controls.Add(this.rbOpenDNS);
            this.pnlPublicDns.Controls.Add(this.rbComodoDNS);
            this.pnlPublicDns.Location = new System.Drawing.Point(49, 28);
            this.pnlPublicDns.Name = "pnlPublicDns";
            this.pnlPublicDns.Size = new System.Drawing.Size(275, 27);
            this.pnlPublicDns.TabIndex = 19;
            // 
            // rbGoogleDNS
            // 
            this.rbGoogleDNS.AutoSize = true;
            this.rbGoogleDNS.Location = new System.Drawing.Point(11, 3);
            this.rbGoogleDNS.Name = "rbGoogleDNS";
            this.rbGoogleDNS.Size = new System.Drawing.Size(85, 17);
            this.rbGoogleDNS.TabIndex = 15;
            this.rbGoogleDNS.TabStop = true;
            this.rbGoogleDNS.Text = "Google DNS";
            this.rbGoogleDNS.UseVisualStyleBackColor = true;
            // 
            // rbOpenDNS
            // 
            this.rbOpenDNS.AutoSize = true;
            this.rbOpenDNS.Location = new System.Drawing.Point(102, 3);
            this.rbOpenDNS.Name = "rbOpenDNS";
            this.rbOpenDNS.Size = new System.Drawing.Size(74, 17);
            this.rbOpenDNS.TabIndex = 16;
            this.rbOpenDNS.TabStop = true;
            this.rbOpenDNS.Text = "OpenDNS";
            this.rbOpenDNS.UseVisualStyleBackColor = true;
            // 
            // rbComodoDNS
            // 
            this.rbComodoDNS.AutoSize = true;
            this.rbComodoDNS.Location = new System.Drawing.Point(182, 3);
            this.rbComodoDNS.Name = "rbComodoDNS";
            this.rbComodoDNS.Size = new System.Drawing.Size(90, 17);
            this.rbComodoDNS.TabIndex = 17;
            this.rbComodoDNS.TabStop = true;
            this.rbComodoDNS.Text = "Comodo DNS";
            this.rbComodoDNS.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "DNS:";
            // 
            // tbSecondaryDNS
            // 
            this.tbSecondaryDNS.AllowInternalTab = false;
            this.tbSecondaryDNS.AutoHeight = true;
            this.tbSecondaryDNS.BackColor = System.Drawing.SystemColors.Window;
            this.tbSecondaryDNS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tbSecondaryDNS.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbSecondaryDNS.Location = new System.Drawing.Point(143, 3);
            this.tbSecondaryDNS.MinimumSize = new System.Drawing.Size(87, 20);
            this.tbSecondaryDNS.Name = "tbSecondaryDNS";
            this.tbSecondaryDNS.ReadOnly = false;
            this.tbSecondaryDNS.Size = new System.Drawing.Size(87, 20);
            this.tbSecondaryDNS.TabIndex = 21;
            this.tbSecondaryDNS.Text = "...";
            // 
            // cbDontResetOnDisconn
            // 
            this.cbDontResetOnDisconn.AutoSize = true;
            this.cbDontResetOnDisconn.Location = new System.Drawing.Point(236, 5);
            this.cbDontResetOnDisconn.Name = "cbDontResetOnDisconn";
            this.cbDontResetOnDisconn.Size = new System.Drawing.Size(154, 17);
            this.cbDontResetOnDisconn.TabIndex = 18;
            this.cbDontResetOnDisconn.Text = "Don\'t Reset on Disconnect";
            this.cbDontResetOnDisconn.UseVisualStyleBackColor = true;
            // 
            // cbRouteSplit
            // 
            this.cbRouteSplit.AutoSize = true;
            this.cbRouteSplit.Location = new System.Drawing.Point(4, 4);
            this.cbRouteSplit.Name = "cbRouteSplit";
            this.cbRouteSplit.Size = new System.Drawing.Size(316, 17);
            this.cbRouteSplit.TabIndex = 0;
            this.cbRouteSplit.Text = "Don\'t Route Internet Traffic Through VPN (Not Recomended)";
            this.cbRouteSplit.UseVisualStyleBackColor = true;
            // 
            // tbVPNConnWaitTime
            // 
            this.tbVPNConnWaitTime.Enabled = false;
            this.tbVPNConnWaitTime.Location = new System.Drawing.Point(203, 90);
            this.tbVPNConnWaitTime.Mask = "00";
            this.tbVPNConnWaitTime.Name = "tbVPNConnWaitTime";
            this.tbVPNConnWaitTime.Size = new System.Drawing.Size(28, 20);
            this.tbVPNConnWaitTime.TabIndex = 7;
            this.tbVPNConnWaitTime.Text = "25";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(188, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "VPN Connect Wait Time (in Seconds):";
            // 
            // pnlOpenVPNServer
            // 
            this.pnlOpenVPNServer.Controls.Add(this.rbCustomServer);
            this.pnlOpenVPNServer.Controls.Add(this.rbVPNGate);
            this.pnlOpenVPNServer.Controls.Add(this.rbVPNBook);
            this.pnlOpenVPNServer.Location = new System.Drawing.Point(118, 45);
            this.pnlOpenVPNServer.Name = "pnlOpenVPNServer";
            this.pnlOpenVPNServer.Size = new System.Drawing.Size(247, 26);
            this.pnlOpenVPNServer.TabIndex = 5;
            // 
            // rbCustomServer
            // 
            this.rbCustomServer.AutoSize = true;
            this.rbCustomServer.Enabled = false;
            this.rbCustomServer.Location = new System.Drawing.Point(164, 4);
            this.rbCustomServer.Name = "rbCustomServer";
            this.rbCustomServer.Size = new System.Drawing.Size(60, 17);
            this.rbCustomServer.TabIndex = 2;
            this.rbCustomServer.Text = "Custom";
            this.rbCustomServer.UseVisualStyleBackColor = true;
            // 
            // rbVPNGate
            // 
            this.rbVPNGate.AutoSize = true;
            this.rbVPNGate.Location = new System.Drawing.Point(85, 4);
            this.rbVPNGate.Name = "rbVPNGate";
            this.rbVPNGate.Size = new System.Drawing.Size(73, 17);
            this.rbVPNGate.TabIndex = 1;
            this.rbVPNGate.Text = "VPN Gate";
            this.rbVPNGate.UseVisualStyleBackColor = true;
            this.rbVPNGate.CheckedChanged += new System.EventHandler(this.rbVPNGate_CheckedChanged);
            // 
            // rbVPNBook
            // 
            this.rbVPNBook.AutoSize = true;
            this.rbVPNBook.Checked = true;
            this.rbVPNBook.Location = new System.Drawing.Point(4, 4);
            this.rbVPNBook.Name = "rbVPNBook";
            this.rbVPNBook.Size = new System.Drawing.Size(75, 17);
            this.rbVPNBook.TabIndex = 0;
            this.rbVPNBook.TabStop = true;
            this.rbVPNBook.Text = "VPN Book";
            this.rbVPNBook.UseVisualStyleBackColor = true;
            this.rbVPNBook.CheckedChanged += new System.EventHandler(this.rbVPNBook_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "OpenVPN Server:";
            // 
            // btnOpenVpnDirDefault
            // 
            this.btnOpenVpnDirDefault.Location = new System.Drawing.Point(267, 14);
            this.btnOpenVpnDirDefault.Name = "btnOpenVpnDirDefault";
            this.btnOpenVpnDirDefault.Size = new System.Drawing.Size(51, 19);
            this.btnOpenVpnDirDefault.TabIndex = 3;
            this.btnOpenVpnDirDefault.Text = "Default";
            this.btnOpenVpnDirDefault.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenVpnDirDefault.UseVisualStyleBackColor = true;
            this.btnOpenVpnDirDefault.Click += new System.EventHandler(this.btnOpenVpnDirDefault_Click);
            // 
            // btnBrowseForOpenVpn
            // 
            this.btnBrowseForOpenVpn.Location = new System.Drawing.Point(229, 14);
            this.btnBrowseForOpenVpn.Name = "btnBrowseForOpenVpn";
            this.btnBrowseForOpenVpn.Size = new System.Drawing.Size(32, 19);
            this.btnBrowseForOpenVpn.TabIndex = 2;
            this.btnBrowseForOpenVpn.Text = "...";
            this.btnBrowseForOpenVpn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBrowseForOpenVpn.UseVisualStyleBackColor = true;
            this.btnBrowseForOpenVpn.Click += new System.EventHandler(this.btnBrowseForOpenVpn_Click);
            // 
            // lblOpenVPNDirectory
            // 
            this.lblOpenVPNDirectory.AutoSize = true;
            this.lblOpenVPNDirectory.Location = new System.Drawing.Point(115, 16);
            this.lblOpenVPNDirectory.Name = "lblOpenVPNDirectory";
            this.lblOpenVPNDirectory.Size = new System.Drawing.Size(107, 13);
            this.lblOpenVPNDirectory.TabIndex = 1;
            this.lblOpenVPNDirectory.Text = "lblOpenVPNDirectory";
            // 
            // lblOpenVPNDirLabel
            // 
            this.lblOpenVPNDirLabel.AutoSize = true;
            this.lblOpenVPNDirLabel.Location = new System.Drawing.Point(6, 16);
            this.lblOpenVPNDirLabel.Name = "lblOpenVPNDirLabel";
            this.lblOpenVPNDirLabel.Size = new System.Drawing.Size(103, 13);
            this.lblOpenVPNDirLabel.TabIndex = 0;
            this.lblOpenVPNDirLabel.Text = "OpenVPN Directory:";
            // 
            // lnkChromeIpLeak
            // 
            this.lnkChromeIpLeak.AutoSize = true;
            this.lnkChromeIpLeak.Location = new System.Drawing.Point(170, 92);
            this.lnkChromeIpLeak.Name = "lnkChromeIpLeak";
            this.lnkChromeIpLeak.Size = new System.Drawing.Size(154, 13);
            this.lnkChromeIpLeak.TabIndex = 23;
            this.lnkChromeIpLeak.TabStop = true;
            this.lnkChromeIpLeak.Text = "Get Chrome IP Leak Protection";
            this.lnkChromeIpLeak.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkChromeIpLeak_LinkClicked);
            // 
            // tabOpenApps
            // 
            this.tabOpenApps.Controls.Add(this.btnMoveAppDown);
            this.tabOpenApps.Controls.Add(this.btnMoveAppUp);
            this.tabOpenApps.Controls.Add(this.btnSaveApps);
            this.tabOpenApps.Controls.Add(this.dgOpenApps);
            this.tabOpenApps.Location = new System.Drawing.Point(4, 22);
            this.tabOpenApps.Name = "tabOpenApps";
            this.tabOpenApps.Padding = new System.Windows.Forms.Padding(3, 3, 3, 40);
            this.tabOpenApps.Size = new System.Drawing.Size(644, 353);
            this.tabOpenApps.TabIndex = 1;
            this.tabOpenApps.Text = "Open Apps";
            this.tabOpenApps.UseVisualStyleBackColor = true;
            // 
            // btnMoveAppDown
            // 
            this.btnMoveAppDown.Location = new System.Drawing.Point(6, 54);
            this.btnMoveAppDown.Name = "btnMoveAppDown";
            this.btnMoveAppDown.Size = new System.Drawing.Size(51, 23);
            this.btnMoveAppDown.TabIndex = 3;
            this.btnMoveAppDown.Text = "Down";
            this.btnMoveAppDown.UseVisualStyleBackColor = true;
            this.btnMoveAppDown.Click += new System.EventHandler(this.btnMoveAppDown_Click);
            // 
            // btnMoveAppUp
            // 
            this.btnMoveAppUp.Location = new System.Drawing.Point(6, 25);
            this.btnMoveAppUp.Name = "btnMoveAppUp";
            this.btnMoveAppUp.Size = new System.Drawing.Size(51, 23);
            this.btnMoveAppUp.TabIndex = 2;
            this.btnMoveAppUp.Text = "Up";
            this.btnMoveAppUp.UseVisualStyleBackColor = true;
            this.btnMoveAppUp.Click += new System.EventHandler(this.btnMoveAppUp_Click);
            // 
            // btnSaveApps
            // 
            this.btnSaveApps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveApps.Location = new System.Drawing.Point(281, 305);
            this.btnSaveApps.Name = "btnSaveApps";
            this.btnSaveApps.Size = new System.Drawing.Size(75, 23);
            this.btnSaveApps.TabIndex = 1;
            this.btnSaveApps.Text = "Save";
            this.btnSaveApps.UseVisualStyleBackColor = true;
            this.btnSaveApps.Click += new System.EventHandler(this.btnSaveApps_Click);
            // 
            // dgOpenApps
            // 
            this.dgOpenApps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgOpenApps.AutoGenerateColumns = false;
            this.dgOpenApps.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgOpenApps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOpenApps.DataSource = this.appsBindingSource;
            this.dgOpenApps.Location = new System.Drawing.Point(63, 3);
            this.dgOpenApps.Name = "dgOpenApps";
            this.dgOpenApps.RowHeadersVisible = false;
            this.dgOpenApps.Size = new System.Drawing.Size(575, 284);
            this.dgOpenApps.TabIndex = 0;
            // 
            // tabConnection
            // 
            this.tabConnection.AutoScroll = true;
            this.tabConnection.Controls.Add(this.lblConnectionStatus);
            this.tabConnection.Controls.Add(this.lblConnectionStatuslbl);
            this.tabConnection.Controls.Add(this.lblRunningApps);
            this.tabConnection.Controls.Add(this.lblRunningAppsLabel);
            this.tabConnection.Controls.Add(this.lblVPNConnectionStatus);
            this.tabConnection.Controls.Add(this.lblVPNConnectionStatusLabel);
            this.tabConnection.Controls.Add(this.btnConnect);
            this.tabConnection.Location = new System.Drawing.Point(4, 22);
            this.tabConnection.Name = "tabConnection";
            this.tabConnection.Padding = new System.Windows.Forms.Padding(3);
            this.tabConnection.Size = new System.Drawing.Size(644, 353);
            this.tabConnection.TabIndex = 0;
            this.tabConnection.Text = "Connection Info";
            this.tabConnection.UseVisualStyleBackColor = true;
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Location = new System.Drawing.Point(135, 38);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(73, 13);
            this.lblConnectionStatus.TabIndex = 6;
            this.lblConnectionStatus.Text = "Disconnected";
            // 
            // lblConnectionStatuslbl
            // 
            this.lblConnectionStatuslbl.AutoSize = true;
            this.lblConnectionStatuslbl.Location = new System.Drawing.Point(6, 38);
            this.lblConnectionStatuslbl.Name = "lblConnectionStatuslbl";
            this.lblConnectionStatuslbl.Size = new System.Drawing.Size(97, 13);
            this.lblConnectionStatuslbl.TabIndex = 5;
            this.lblConnectionStatuslbl.Text = "Connection Status:";
            // 
            // lblRunningApps
            // 
            this.lblRunningApps.AutoSize = true;
            this.lblRunningApps.Location = new System.Drawing.Point(89, 125);
            this.lblRunningApps.Name = "lblRunningApps";
            this.lblRunningApps.Size = new System.Drawing.Size(117, 13);
            this.lblRunningApps.TabIndex = 4;
            this.lblRunningApps.Text = "None.. Not Connected.";
            // 
            // lblRunningAppsLabel
            // 
            this.lblRunningAppsLabel.AutoSize = true;
            this.lblRunningAppsLabel.Location = new System.Drawing.Point(6, 125);
            this.lblRunningAppsLabel.Name = "lblRunningAppsLabel";
            this.lblRunningAppsLabel.Size = new System.Drawing.Size(77, 13);
            this.lblRunningAppsLabel.TabIndex = 3;
            this.lblRunningAppsLabel.Text = "Running Apps:";
            // 
            // lblVPNConnectionStatus
            // 
            this.lblVPNConnectionStatus.AutoSize = true;
            this.lblVPNConnectionStatus.Location = new System.Drawing.Point(135, 7);
            this.lblVPNConnectionStatus.Name = "lblVPNConnectionStatus";
            this.lblVPNConnectionStatus.Size = new System.Drawing.Size(73, 13);
            this.lblVPNConnectionStatus.TabIndex = 2;
            this.lblVPNConnectionStatus.Text = "Disconnected";
            // 
            // lblVPNConnectionStatusLabel
            // 
            this.lblVPNConnectionStatusLabel.AutoSize = true;
            this.lblVPNConnectionStatusLabel.Location = new System.Drawing.Point(7, 7);
            this.lblVPNConnectionStatusLabel.Name = "lblVPNConnectionStatusLabel";
            this.lblVPNConnectionStatusLabel.Size = new System.Drawing.Size(122, 13);
            this.lblVPNConnectionStatusLabel.TabIndex = 1;
            this.lblVPNConnectionStatusLabel.Text = "VPN Connection Status:";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(253, 241);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(112, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.tabConnection);
            this.tabs.Controls.Add(this.tabOpenApps);
            this.tabs.Controls.Add(this.tabSettings);
            this.tabs.Controls.Add(this.tabVPNGate);
            this.tabs.Controls.Add(this.tabVPNBook);
            this.tabs.Controls.Add(this.tabSecurity);
            this.tabs.Controls.Add(this.tabMediaServer);
            this.tabs.Controls.Add(this.tabVPNTraffic);
            this.tabs.Location = new System.Drawing.Point(12, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(652, 379);
            this.tabs.TabIndex = 0;
            this.tabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabs_Selected);
            // 
            // tabVPNGate
            // 
            this.tabVPNGate.Controls.Add(this.cbVPNGateConnectRetry);
            this.tabVPNGate.Controls.Add(this.lblVPNGateServerInfo);
            this.tabVPNGate.Controls.Add(this.label7);
            this.tabVPNGate.Controls.Add(this.cbVPNGateServer);
            this.tabVPNGate.Location = new System.Drawing.Point(4, 22);
            this.tabVPNGate.Name = "tabVPNGate";
            this.tabVPNGate.Size = new System.Drawing.Size(644, 353);
            this.tabVPNGate.TabIndex = 7;
            this.tabVPNGate.Text = "VPN Gate";
            this.tabVPNGate.UseVisualStyleBackColor = true;
            // 
            // cbVPNGateConnectRetry
            // 
            this.cbVPNGateConnectRetry.AutoSize = true;
            this.cbVPNGateConnectRetry.Location = new System.Drawing.Point(392, 17);
            this.cbVPNGateConnectRetry.Name = "cbVPNGateConnectRetry";
            this.cbVPNGateConnectRetry.Size = new System.Drawing.Size(185, 17);
            this.cbVPNGateConnectRetry.TabIndex = 3;
            this.cbVPNGateConnectRetry.Text = "Retry Connection if Disconnected";
            this.cbVPNGateConnectRetry.UseVisualStyleBackColor = true;
            this.cbVPNGateConnectRetry.CheckedChanged += new System.EventHandler(this.cbVPNGateConnectRetry_CheckedChanged);
            // 
            // lblVPNGateServerInfo
            // 
            this.lblVPNGateServerInfo.AutoSize = true;
            this.lblVPNGateServerInfo.Location = new System.Drawing.Point(59, 50);
            this.lblVPNGateServerInfo.Name = "lblVPNGateServerInfo";
            this.lblVPNGateServerInfo.Size = new System.Drawing.Size(0, 13);
            this.lblVPNGateServerInfo.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Server:";
            // 
            // cbVPNGateServer
            // 
            this.cbVPNGateServer.FormattingEnabled = true;
            this.cbVPNGateServer.Location = new System.Drawing.Point(62, 14);
            this.cbVPNGateServer.Name = "cbVPNGateServer";
            this.cbVPNGateServer.Size = new System.Drawing.Size(323, 21);
            this.cbVPNGateServer.TabIndex = 0;
            this.cbVPNGateServer.SelectedIndexChanged += new System.EventHandler(this.cbVPNGateServer_SelectedIndexChanged);
            // 
            // tabSecurity
            // 
            this.tabSecurity.Controls.Add(this.lblFirewallRules);
            this.tabSecurity.Controls.Add(this.btnFirewallRules);
            this.tabSecurity.Controls.Add(this.linkIPLeak);
            this.tabSecurity.Controls.Add(this.linkDownloadqBittorrent);
            this.tabSecurity.Controls.Add(this.linkDownloadCPorts);
            this.tabSecurity.Controls.Add(this.linkDownloadCCCleaner);
            this.tabSecurity.Controls.Add(this.linkDownloadOpenVPN);
            this.tabSecurity.Controls.Add(this.linkDownloadPeerBlock);
            this.tabSecurity.Controls.Add(this.linkGetTorBrowser);
            this.tabSecurity.Controls.Add(this.lnkChromeIpLeak);
            this.tabSecurity.Location = new System.Drawing.Point(4, 22);
            this.tabSecurity.Name = "tabSecurity";
            this.tabSecurity.Padding = new System.Windows.Forms.Padding(3);
            this.tabSecurity.Size = new System.Drawing.Size(644, 353);
            this.tabSecurity.TabIndex = 6;
            this.tabSecurity.Text = "Privacy";
            this.tabSecurity.UseVisualStyleBackColor = true;
            // 
            // lblFirewallRules
            // 
            this.lblFirewallRules.AutoSize = true;
            this.lblFirewallRules.Location = new System.Drawing.Point(164, 146);
            this.lblFirewallRules.Name = "lblFirewallRules";
            this.lblFirewallRules.Size = new System.Drawing.Size(0, 13);
            this.lblFirewallRules.TabIndex = 25;
            // 
            // btnFirewallRules
            // 
            this.btnFirewallRules.Location = new System.Drawing.Point(10, 141);
            this.btnFirewallRules.Name = "btnFirewallRules";
            this.btnFirewallRules.Size = new System.Drawing.Size(148, 23);
            this.btnFirewallRules.TabIndex = 24;
            this.btnFirewallRules.Text = "Add/Repair Firewall Rules";
            this.btnFirewallRules.UseVisualStyleBackColor = true;
            this.btnFirewallRules.Click += new System.EventHandler(this.btnFirewallRules_Click);
            // 
            // linkIPLeak
            // 
            this.linkIPLeak.AutoSize = true;
            this.linkIPLeak.Location = new System.Drawing.Point(170, 62);
            this.linkIPLeak.Name = "linkIPLeak";
            this.linkIPLeak.Size = new System.Drawing.Size(91, 13);
            this.linkIPLeak.TabIndex = 7;
            this.linkIPLeak.TabStop = true;
            this.linkIPLeak.Text = "DNS Leak Check";
            this.linkIPLeak.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkIPLeak_LinkClicked);
            // 
            // linkDownloadqBittorrent
            // 
            this.linkDownloadqBittorrent.AutoSize = true;
            this.linkDownloadqBittorrent.Location = new System.Drawing.Point(170, 33);
            this.linkDownloadqBittorrent.Name = "linkDownloadqBittorrent";
            this.linkDownloadqBittorrent.Size = new System.Drawing.Size(106, 13);
            this.linkDownloadqBittorrent.TabIndex = 6;
            this.linkDownloadqBittorrent.TabStop = true;
            this.linkDownloadqBittorrent.Text = "Download qBittorrent";
            this.linkDownloadqBittorrent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDownloadqBittorrent_LinkClicked);
            // 
            // linkDownloadCPorts
            // 
            this.linkDownloadCPorts.AutoSize = true;
            this.linkDownloadCPorts.Location = new System.Drawing.Point(170, 7);
            this.linkDownloadCPorts.Name = "linkDownloadCPorts";
            this.linkDownloadCPorts.Size = new System.Drawing.Size(101, 13);
            this.linkDownloadCPorts.TabIndex = 5;
            this.linkDownloadCPorts.TabStop = true;
            this.linkDownloadCPorts.Text = "Download CurrPorts";
            this.linkDownloadCPorts.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDownloadCPorts_LinkClicked);
            // 
            // linkDownloadCCCleaner
            // 
            this.linkDownloadCCCleaner.AutoSize = true;
            this.linkDownloadCCCleaner.Location = new System.Drawing.Point(7, 92);
            this.linkDownloadCCCleaner.Name = "linkDownloadCCCleaner";
            this.linkDownloadCCCleaner.Size = new System.Drawing.Size(111, 13);
            this.linkDownloadCCCleaner.TabIndex = 4;
            this.linkDownloadCCCleaner.TabStop = true;
            this.linkDownloadCCCleaner.Text = "Download CC Cleaner";
            this.linkDownloadCCCleaner.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDownloadCCCleaner_LinkClicked);
            // 
            // linkDownloadOpenVPN
            // 
            this.linkDownloadOpenVPN.AutoSize = true;
            this.linkDownloadOpenVPN.Location = new System.Drawing.Point(7, 62);
            this.linkDownloadOpenVPN.Name = "linkDownloadOpenVPN";
            this.linkDownloadOpenVPN.Size = new System.Drawing.Size(106, 13);
            this.linkDownloadOpenVPN.TabIndex = 3;
            this.linkDownloadOpenVPN.TabStop = true;
            this.linkDownloadOpenVPN.Text = "Download OpenVPN";
            this.linkDownloadOpenVPN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDownloadOpenVPN_LinkClicked);
            // 
            // linkDownloadPeerBlock
            // 
            this.linkDownloadPeerBlock.AutoSize = true;
            this.linkDownloadPeerBlock.Location = new System.Drawing.Point(7, 33);
            this.linkDownloadPeerBlock.Name = "linkDownloadPeerBlock";
            this.linkDownloadPeerBlock.Size = new System.Drawing.Size(110, 13);
            this.linkDownloadPeerBlock.TabIndex = 1;
            this.linkDownloadPeerBlock.TabStop = true;
            this.linkDownloadPeerBlock.Text = "Download Peer Block";
            this.linkDownloadPeerBlock.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDownloadPeerBlock_LinkClicked);
            // 
            // linkGetTorBrowser
            // 
            this.linkGetTorBrowser.AutoSize = true;
            this.linkGetTorBrowser.Location = new System.Drawing.Point(7, 7);
            this.linkGetTorBrowser.Name = "linkGetTorBrowser";
            this.linkGetTorBrowser.Size = new System.Drawing.Size(141, 13);
            this.linkGetTorBrowser.TabIndex = 0;
            this.linkGetTorBrowser.TabStop = true;
            this.linkGetTorBrowser.Text = "Download Tor Web Browser";
            this.linkGetTorBrowser.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGetTorBrowser_LinkClicked);
            // 
            // tabMediaServer
            // 
            this.tabMediaServer.Controls.Add(this.picParentalControls);
            this.tabMediaServer.Controls.Add(this.groupBox2);
            this.tabMediaServer.Controls.Add(this.groupBox1);
            this.tabMediaServer.Controls.Add(this.tbMediaDomain);
            this.tabMediaServer.Controls.Add(this.tbMediaPassword);
            this.tabMediaServer.Controls.Add(this.tbMediaUsername);
            this.tabMediaServer.Controls.Add(this.label9);
            this.tabMediaServer.Controls.Add(this.label8);
            this.tabMediaServer.Controls.Add(this.lblMediaUsername);
            this.tabMediaServer.Location = new System.Drawing.Point(4, 22);
            this.tabMediaServer.Name = "tabMediaServer";
            this.tabMediaServer.Padding = new System.Windows.Forms.Padding(3);
            this.tabMediaServer.Size = new System.Drawing.Size(644, 353);
            this.tabMediaServer.TabIndex = 8;
            this.tabMediaServer.Text = "Media Server";
            this.tabMediaServer.UseVisualStyleBackColor = true;
            // 
            // picParentalControls
            // 
            this.picParentalControls.Image = global::P2PVpn.Properties.Resources.Stop_red1;
            this.picParentalControls.Location = new System.Drawing.Point(434, 17);
            this.picParentalControls.Name = "picParentalControls";
            this.picParentalControls.Size = new System.Drawing.Size(79, 72);
            this.picParentalControls.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picParentalControls.TabIndex = 13;
            this.picParentalControls.TabStop = false;
            this.toolTip.SetToolTip(this.picParentalControls, "Media Share is Online (Parental Controls are off)");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblMediaTimeRemaining);
            this.groupBox2.Controls.Add(this.cbMediaParentalTime);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.lblMediaNetworkShare);
            this.groupBox2.Controls.Add(this.btnMediaNetworkShare);
            this.groupBox2.Controls.Add(this.btnMediaFolderOffline);
            this.groupBox2.Location = new System.Drawing.Point(6, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(632, 75);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parental Controls";
            // 
            // lblMediaTimeRemaining
            // 
            this.lblMediaTimeRemaining.AutoSize = true;
            this.lblMediaTimeRemaining.Location = new System.Drawing.Point(261, 52);
            this.lblMediaTimeRemaining.Name = "lblMediaTimeRemaining";
            this.lblMediaTimeRemaining.Size = new System.Drawing.Size(0, 13);
            this.lblMediaTimeRemaining.TabIndex = 15;
            // 
            // cbMediaParentalTime
            // 
            this.cbMediaParentalTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMediaParentalTime.FormattingEnabled = true;
            this.cbMediaParentalTime.Items.AddRange(new object[] {
            "",
            "1 hour",
            "2 hours",
            "3 hours",
            "4 hours",
            "5 hours",
            "6 hours",
            "7 hours ",
            "8 hours"});
            this.cbMediaParentalTime.Location = new System.Drawing.Point(169, 18);
            this.cbMediaParentalTime.Name = "cbMediaParentalTime";
            this.cbMediaParentalTime.Size = new System.Drawing.Size(82, 21);
            this.cbMediaParentalTime.TabIndex = 14;
            this.cbMediaParentalTime.SelectedIndexChanged += new System.EventHandler(this.cbMediaParentalTime_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(160, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Enforce Parental Controls Every:";
            // 
            // lblMediaNetworkShare
            // 
            this.lblMediaNetworkShare.AutoSize = true;
            this.lblMediaNetworkShare.Location = new System.Drawing.Point(112, 47);
            this.lblMediaNetworkShare.Name = "lblMediaNetworkShare";
            this.lblMediaNetworkShare.Size = new System.Drawing.Size(43, 13);
            this.lblMediaNetworkShare.TabIndex = 12;
            this.lblMediaNetworkShare.Text = "Not Set";
            // 
            // btnMediaNetworkShare
            // 
            this.btnMediaNetworkShare.Location = new System.Drawing.Point(6, 42);
            this.btnMediaNetworkShare.Name = "btnMediaNetworkShare";
            this.btnMediaNetworkShare.Size = new System.Drawing.Size(100, 23);
            this.btnMediaNetworkShare.TabIndex = 11;
            this.btnMediaNetworkShare.Text = "Network Share";
            this.btnMediaNetworkShare.UseVisualStyleBackColor = true;
            this.btnMediaNetworkShare.Click += new System.EventHandler(this.btnMediaNetworkShare_Click);
            // 
            // btnMediaFolderOffline
            // 
            this.btnMediaFolderOffline.Image = ((System.Drawing.Image)(resources.GetObject("btnMediaFolderOffline.Image")));
            this.btnMediaFolderOffline.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMediaFolderOffline.Location = new System.Drawing.Point(394, 14);
            this.btnMediaFolderOffline.Name = "btnMediaFolderOffline";
            this.btnMediaFolderOffline.Size = new System.Drawing.Size(152, 25);
            this.btnMediaFolderOffline.TabIndex = 0;
            this.btnMediaFolderOffline.Text = "Bring Media Share Offline";
            this.btnMediaFolderOffline.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMediaFolderOffline.UseVisualStyleBackColor = true;
            this.btnMediaFolderOffline.Click += new System.EventHandler(this.btnMediaFolderOffline_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblExcludeFolder);
            this.groupBox1.Controls.Add(this.btnExcludeFolder);
            this.groupBox1.Controls.Add(this.lblMediaCopyProgress);
            this.groupBox1.Controls.Add(this.lblMediaDestination);
            this.groupBox1.Controls.Add(this.lblMediaSource);
            this.groupBox1.Controls.Add(this.btnMediaSource);
            this.groupBox1.Controls.Add(this.btnMediaTarget);
            this.groupBox1.Location = new System.Drawing.Point(6, 182);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(632, 160);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Copy Files";
            // 
            // lblMediaCopyProgress
            // 
            this.lblMediaCopyProgress.Enabled = false;
            this.lblMediaCopyProgress.Location = new System.Drawing.Point(6, 106);
            this.lblMediaCopyProgress.Multiline = true;
            this.lblMediaCopyProgress.Name = "lblMediaCopyProgress";
            this.lblMediaCopyProgress.Size = new System.Drawing.Size(620, 44);
            this.lblMediaCopyProgress.TabIndex = 14;
            // 
            // lblMediaDestination
            // 
            this.lblMediaDestination.AutoSize = true;
            this.lblMediaDestination.Location = new System.Drawing.Point(112, 53);
            this.lblMediaDestination.Name = "lblMediaDestination";
            this.lblMediaDestination.Size = new System.Drawing.Size(43, 13);
            this.lblMediaDestination.TabIndex = 13;
            this.lblMediaDestination.Text = "Not Set";
            // 
            // lblMediaSource
            // 
            this.lblMediaSource.AutoSize = true;
            this.lblMediaSource.Location = new System.Drawing.Point(112, 24);
            this.lblMediaSource.Name = "lblMediaSource";
            this.lblMediaSource.Size = new System.Drawing.Size(43, 13);
            this.lblMediaSource.TabIndex = 13;
            this.lblMediaSource.Text = "Not Set";
            // 
            // btnMediaSource
            // 
            this.btnMediaSource.Location = new System.Drawing.Point(6, 19);
            this.btnMediaSource.Name = "btnMediaSource";
            this.btnMediaSource.Size = new System.Drawing.Size(100, 23);
            this.btnMediaSource.TabIndex = 10;
            this.btnMediaSource.Text = "Media Source";
            this.btnMediaSource.UseVisualStyleBackColor = true;
            this.btnMediaSource.Click += new System.EventHandler(this.btnMediaSource_Click);
            // 
            // btnMediaTarget
            // 
            this.btnMediaTarget.Location = new System.Drawing.Point(6, 48);
            this.btnMediaTarget.Name = "btnMediaTarget";
            this.btnMediaTarget.Size = new System.Drawing.Size(100, 23);
            this.btnMediaTarget.TabIndex = 9;
            this.btnMediaTarget.Text = "Media Destination";
            this.btnMediaTarget.UseVisualStyleBackColor = true;
            this.btnMediaTarget.Click += new System.EventHandler(this.btnMediaTarget_Click);
            // 
            // tbMediaDomain
            // 
            this.tbMediaDomain.Location = new System.Drawing.Point(96, 69);
            this.tbMediaDomain.Name = "tbMediaDomain";
            this.tbMediaDomain.Size = new System.Drawing.Size(100, 20);
            this.tbMediaDomain.TabIndex = 7;
            this.tbMediaDomain.Leave += new System.EventHandler(this.tbMediaDomain_Leave);
            // 
            // tbMediaPassword
            // 
            this.tbMediaPassword.Location = new System.Drawing.Point(96, 43);
            this.tbMediaPassword.Name = "tbMediaPassword";
            this.tbMediaPassword.PasswordChar = '*';
            this.tbMediaPassword.Size = new System.Drawing.Size(100, 20);
            this.tbMediaPassword.TabIndex = 6;
            this.tbMediaPassword.Leave += new System.EventHandler(this.tbMediaPassword_Leave);
            // 
            // tbMediaUsername
            // 
            this.tbMediaUsername.Location = new System.Drawing.Point(96, 17);
            this.tbMediaUsername.Name = "tbMediaUsername";
            this.tbMediaUsername.Size = new System.Drawing.Size(100, 20);
            this.tbMediaUsername.TabIndex = 5;
            this.tbMediaUsername.Leave += new System.EventHandler(this.tbMediaUsername_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(44, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Domain:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Password:";
            // 
            // lblMediaUsername
            // 
            this.lblMediaUsername.AutoSize = true;
            this.lblMediaUsername.Location = new System.Drawing.Point(32, 17);
            this.lblMediaUsername.Name = "lblMediaUsername";
            this.lblMediaUsername.Size = new System.Drawing.Size(58, 13);
            this.lblMediaUsername.TabIndex = 1;
            this.lblMediaUsername.Text = "Username:";
            // 
            // firewallTimer
            // 
            this.firewallTimer.Enabled = true;
            this.firewallTimer.Interval = 20000;
            this.firewallTimer.Tick += new System.EventHandler(this.firewallTimer_Tick);
            // 
            // toolTip
            // 
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // timerMediaServerOffline
            // 
            this.timerMediaServerOffline.Enabled = true;
            this.timerMediaServerOffline.Interval = 10000;
            this.timerMediaServerOffline.Tick += new System.EventHandler(this.timerMediaServerOffline_Tick);
            // 
            // bwFileTransfer
            // 
            this.bwFileTransfer.WorkerReportsProgress = true;
            this.bwFileTransfer.WorkerSupportsCancellation = true;
            this.bwFileTransfer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwFileTransfer_DoWork);
            this.bwFileTransfer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwFileTransfer_RunWorkerCompleted);
            // 
            // bwTorrentDownloadComplete
            // 
            this.bwTorrentDownloadComplete.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwTorrentDownloadComplete_DoWork);
            // 
            // btnExcludeFolder
            // 
            this.btnExcludeFolder.Location = new System.Drawing.Point(6, 77);
            this.btnExcludeFolder.Name = "btnExcludeFolder";
            this.btnExcludeFolder.Size = new System.Drawing.Size(100, 23);
            this.btnExcludeFolder.TabIndex = 15;
            this.btnExcludeFolder.Text = "Exclude Folder";
            this.btnExcludeFolder.UseVisualStyleBackColor = true;
            this.btnExcludeFolder.Click += new System.EventHandler(this.btnExcludeFolder_Click);
            // 
            // lblExcludeFolder
            // 
            this.lblExcludeFolder.AutoSize = true;
            this.lblExcludeFolder.Location = new System.Drawing.Point(112, 82);
            this.lblExcludeFolder.Name = "lblExcludeFolder";
            this.lblExcludeFolder.Size = new System.Drawing.Size(43, 13);
            this.lblExcludeFolder.TabIndex = 16;
            this.lblExcludeFolder.Text = "Not Set";
            // 
            // P2PVPNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 403);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "P2PVPNForm";
            this.Text = "P2P VPN Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.P2PVPNForm_FormClosing);
            this.Load += new System.EventHandler(this.P2PVPNForm_Load);
            this.Shown += new System.EventHandler(this.P2PVPNForm_Shown);
            this.Leave += new System.EventHandler(this.P2PVPNForm_Leave);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabVPNTraffic.ResumeLayout(false);
            this.tabVPNBook.ResumeLayout(false);
            this.tabVPNBook.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            this.pnlRoutes.ResumeLayout(false);
            this.pnlRoutes.PerformLayout();
            this.pnlDns.ResumeLayout(false);
            this.pnlDns.PerformLayout();
            this.pnlPublicDns.ResumeLayout(false);
            this.pnlPublicDns.PerformLayout();
            this.pnlOpenVPNServer.ResumeLayout(false);
            this.pnlOpenVPNServer.PerformLayout();
            this.tabOpenApps.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgOpenApps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appsBindingSource)).EndInit();
            this.tabConnection.ResumeLayout(false);
            this.tabConnection.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.tabVPNGate.ResumeLayout(false);
            this.tabVPNGate.PerformLayout();
            this.tabSecurity.ResumeLayout(false);
            this.tabSecurity.PerformLayout();
            this.tabMediaServer.ResumeLayout(false);
            this.tabMediaServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picParentalControls)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        #endregion

        private System.Windows.Forms.Timer timerSpeed;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusText;
        private System.Windows.Forms.BindingSource appsBindingSource;
        private System.Windows.Forms.FolderBrowserDialog openVPNFolderBrowser;
        private System.Windows.Forms.TabPage tabVPNTraffic;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.TabPage tabVPNBook;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.LinkLabel hlVPNBookConfigDownload;
        private System.Windows.Forms.Button btnVPNBookUser;
        private System.Windows.Forms.TextBox tbVPNPassword;
        private System.Windows.Forms.TextBox tbVPNUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbVPNBookOpenVPNConfig;
        private System.Windows.Forms.Label lblVPNBookOpenVPNConfigLabel;
        private System.Windows.Forms.Button btnOpenVpnDirDefault;
        private System.Windows.Forms.Button btnBrowseForOpenVpn;
        private System.Windows.Forms.Label lblOpenVPNDirectory;
        private System.Windows.Forms.Label lblOpenVPNDirLabel;
        private System.Windows.Forms.TabPage tabOpenApps;
        private System.Windows.Forms.Button btnMoveAppDown;
        private System.Windows.Forms.Button btnMoveAppUp;
        private System.Windows.Forms.Button btnSaveApps;
        private System.Windows.Forms.DataGridView dgOpenApps;
        private System.Windows.Forms.DataGridViewCheckBoxColumn launchDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn closeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn programDataGridViewTextBoxColumn;
        private System.Windows.Forms.TabPage tabConnection;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.Label lblConnectionStatuslbl;
        private System.Windows.Forms.Label lblRunningApps;
        private System.Windows.Forms.Label lblRunningAppsLabel;
        private System.Windows.Forms.Label lblVPNConnectionStatus;
        private System.Windows.Forms.Label lblVPNConnectionStatusLabel;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.LinkLabel lnkChromeIpLeak;
        private System.Windows.Forms.TabPage tabSecurity;
        private System.Windows.Forms.LinkLabel linkDownloadPeerBlock;
        private System.Windows.Forms.LinkLabel linkGetTorBrowser;
        private System.Windows.Forms.LinkLabel linkDownloadOpenVPN;
        private System.Windows.Forms.LinkLabel linkIPLeak;
        private System.Windows.Forms.LinkLabel linkDownloadqBittorrent;
        private System.Windows.Forms.LinkLabel linkDownloadCPorts;
        private System.Windows.Forms.LinkLabel linkDownloadCCCleaner;
        private System.Windows.Forms.Panel pnlOpenVPNServer;
        private System.Windows.Forms.RadioButton rbCustomServer;
        private System.Windows.Forms.RadioButton rbVPNGate;
        private System.Windows.Forms.RadioButton rbVPNBook;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbRetrieveVPNBookCredsOnLoad;
        private System.Windows.Forms.MaskedTextBox tbVPNConnWaitTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlRoutes;
        private System.Windows.Forms.Panel pnlDns;
        private IPAddressControlLib.IPAddressControl tbPrimaryDNS;
        private System.Windows.Forms.Panel pnlPublicDns;
        private System.Windows.Forms.RadioButton rbGoogleDNS;
        private System.Windows.Forms.RadioButton rbOpenDNS;
        private System.Windows.Forms.RadioButton rbComodoDNS;
        private System.Windows.Forms.Label label3;
        private IPAddressControlLib.IPAddressControl tbSecondaryDNS;
        private System.Windows.Forms.CheckBox cbDontResetOnDisconn;
        private System.Windows.Forms.CheckBox cbRouteSplit;
        private System.Windows.Forms.Button btnFirewallRules;
        private System.Windows.Forms.Label lblFirewallRules;
        private System.Windows.Forms.Timer firewallTimer;
        private System.Windows.Forms.ToolStripStatusLabel lblOpenVPNConifg;
        private System.Windows.Forms.TabPage tabVPNGate;
        private System.Windows.Forms.Label lblVPNGateServerInfo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbVPNGateServer;
        private System.Windows.Forms.TabPage tabMediaServer;
        private System.Windows.Forms.Button btnMediaFolderOffline;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblMediaUsername;
        private System.Windows.Forms.Button btnMediaTarget;
        private System.Windows.Forms.TextBox tbMediaDomain;
        private System.Windows.Forms.TextBox tbMediaPassword;
        private System.Windows.Forms.TextBox tbMediaUsername;
        private System.Windows.Forms.FolderBrowserDialog openMediaDestFolderBrowser;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox picParentalControls;
        private System.Windows.Forms.Label lblMediaNetworkShare;
        private System.Windows.Forms.Button btnMediaNetworkShare;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMediaDestination;
        private System.Windows.Forms.Label lblMediaSource;
        private System.Windows.Forms.Button btnMediaSource;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ComboBox cbMediaParentalTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Timer timerMediaServerOffline;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusMediaShare;
        private System.Windows.Forms.Label lblMediaTimeRemaining;
        private System.Windows.Forms.CheckBox cbDisableSystemSleep;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbVPNGateConnectRetry;
        private System.ComponentModel.BackgroundWorker bwFileTransfer;
        private System.Windows.Forms.TextBox lblMediaCopyProgress;
        public System.ComponentModel.BackgroundWorker bwTorrentDownloadComplete;
        private System.Windows.Forms.Label lblExcludeFolder;
        private System.Windows.Forms.Button btnExcludeFolder;
    }
}

