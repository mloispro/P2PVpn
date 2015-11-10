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
            this.lblStatusColor = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.openVPNFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.tabVPNTraffic = new System.Windows.Forms.TabPage();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.tabVPNBook = new System.Windows.Forms.TabPage();
            this.btnVPNBookUser = new System.Windows.Forms.Button();
            this.tbVPNPassword = new System.Windows.Forms.TextBox();
            this.tbVPNUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.tbSecondaryDNS = new IPAddressControlLib.IPAddressControl();
            this.tbPrimaryDNS = new IPAddressControlLib.IPAddressControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbGoogleDNS = new System.Windows.Forms.RadioButton();
            this.rbOpenDNS = new System.Windows.Forms.RadioButton();
            this.rbComodoDNS = new System.Windows.Forms.RadioButton();
            this.cbDontResetOnDisconn = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbOpenVPNConfig = new System.Windows.Forms.ComboBox();
            this.hlVPNBookConfigDownload = new System.Windows.Forms.LinkLabel();
            this.lblOpenVPNConfigLabel = new System.Windows.Forms.Label();
            this.btnOpenVpnDirDefault = new System.Windows.Forms.Button();
            this.btnBrowseForOpenVpn = new System.Windows.Forms.Button();
            this.lblOpenVPNDirectory = new System.Windows.Forms.Label();
            this.lblOpenVPNDirLabel = new System.Windows.Forms.Label();
            this.tabOpenApps = new System.Windows.Forms.TabPage();
            this.btnMoveAppDown = new System.Windows.Forms.Button();
            this.btnMoveAppUp = new System.Windows.Forms.Button();
            this.btnSaveApps = new System.Windows.Forms.Button();
            this.dgOpenApps = new System.Windows.Forms.DataGridView();
            this.tabConnection = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.lblConnectionStatuslbl = new System.Windows.Forms.Label();
            this.lblRunningApps = new System.Windows.Forms.Label();
            this.lblRunningAppsLabel = new System.Windows.Forms.Label();
            this.lblVPNConnectionStatus = new System.Windows.Forms.Label();
            this.lblVPNConnectionStatusLabel = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tabs = new System.Windows.Forms.TabControl();
            this.cdTorProxyChrome = new System.Windows.Forms.CheckBox();
            this.lnkChromeIpLeak = new System.Windows.Forms.LinkLabel();
            this.launchDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.closeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.programDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip.SuspendLayout();
            this.tabVPNTraffic.SuspendLayout();
            this.tabVPNBook.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabOpenApps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOpenApps)).BeginInit();
            this.tabConnection.SuspendLayout();
            this.tabs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appsBindingSource)).BeginInit();
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
            this.lblStatusColor,
            this.lblStatusText});
            this.statusStrip.Location = new System.Drawing.Point(0, 301);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(604, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            this.statusStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip_ItemClicked);
            // 
            // lblStatusColor
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(291, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "TODO: secure ovpn, openvpn wait time, vpn ip on statusbar";
            this.lblStatusColor.BackColor = System.Drawing.Color.Yellow;
            this.lblStatusColor.Name = "lblStatusColor";
            this.lblStatusColor.Size = new System.Drawing.Size(16, 17);
            this.lblStatusColor.Text = "   ";
            this.lblStatusColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatusText
            // 
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(134, 17);
            this.lblStatusText.Text = "OpenVPN Disconnected";
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
            this.tabVPNTraffic.Size = new System.Drawing.Size(543, 239);
            this.tabVPNTraffic.TabIndex = 4;
            this.tabVPNTraffic.Text = "Log";
            this.tabVPNTraffic.UseVisualStyleBackColor = true;
            // 
            // lbLog
            // 
            this.lbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLog.FormattingEnabled = true;
            this.lbLog.HorizontalScrollbar = true;
            this.lbLog.Items.AddRange(new object[] {
            "hello",
            "good"});
            this.lbLog.Location = new System.Drawing.Point(6, 5);
            this.lbLog.Name = "lbLog";
            this.lbLog.ScrollAlwaysVisible = true;
            this.lbLog.Size = new System.Drawing.Size(531, 212);
            this.lbLog.TabIndex = 0;
            // 
            // tabVPNBook
            // 
            this.tabVPNBook.Controls.Add(this.btnVPNBookUser);
            this.tabVPNBook.Controls.Add(this.tbVPNPassword);
            this.tabVPNBook.Controls.Add(this.tbVPNUsername);
            this.tabVPNBook.Controls.Add(this.label2);
            this.tabVPNBook.Controls.Add(this.label1);
            this.tabVPNBook.Location = new System.Drawing.Point(4, 22);
            this.tabVPNBook.Name = "tabVPNBook";
            this.tabVPNBook.Padding = new System.Windows.Forms.Padding(3);
            this.tabVPNBook.Size = new System.Drawing.Size(543, 239);
            this.tabVPNBook.TabIndex = 5;
            this.tabVPNBook.Text = "VPN Book";
            this.tabVPNBook.UseVisualStyleBackColor = true;
            // 
            // btnVPNBookUser
            // 
            this.btnVPNBookUser.Location = new System.Drawing.Point(237, 16);
            this.btnVPNBookUser.Name = "btnVPNBookUser";
            this.btnVPNBookUser.Size = new System.Drawing.Size(138, 23);
            this.btnVPNBookUser.TabIndex = 10;
            this.btnVPNBookUser.Text = "Retreive VPN Book Creds";
            this.btnVPNBookUser.UseVisualStyleBackColor = true;
            this.btnVPNBookUser.Click += new System.EventHandler(this.btnVPNBookUser_Click);
            // 
            // tbVPNPassword
            // 
            this.tbVPNPassword.Location = new System.Drawing.Point(95, 51);
            this.tbVPNPassword.Name = "tbVPNPassword";
            this.tbVPNPassword.Size = new System.Drawing.Size(121, 20);
            this.tbVPNPassword.TabIndex = 9;
            this.tbVPNPassword.Leave += new System.EventHandler(this.tbVPNPassword_Leave);
            // 
            // tbVPNUsername
            // 
            this.tbVPNUsername.Location = new System.Drawing.Point(95, 18);
            this.tbVPNUsername.Name = "tbVPNUsername";
            this.tbVPNUsername.Size = new System.Drawing.Size(121, 20);
            this.tbVPNUsername.TabIndex = 7;
            this.tbVPNUsername.Leave += new System.EventHandler(this.tbVPNUsername_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "VPN Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "VPN Username:";
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.lnkChromeIpLeak);
            this.tabSettings.Controls.Add(this.cdTorProxyChrome);
            this.tabSettings.Controls.Add(this.tbSecondaryDNS);
            this.tabSettings.Controls.Add(this.tbPrimaryDNS);
            this.tabSettings.Controls.Add(this.panel1);
            this.tabSettings.Controls.Add(this.cbDontResetOnDisconn);
            this.tabSettings.Controls.Add(this.label3);
            this.tabSettings.Controls.Add(this.cbOpenVPNConfig);
            this.tabSettings.Controls.Add(this.hlVPNBookConfigDownload);
            this.tabSettings.Controls.Add(this.lblOpenVPNConfigLabel);
            this.tabSettings.Controls.Add(this.btnOpenVpnDirDefault);
            this.tabSettings.Controls.Add(this.btnBrowseForOpenVpn);
            this.tabSettings.Controls.Add(this.lblOpenVPNDirectory);
            this.tabSettings.Controls.Add(this.lblOpenVPNDirLabel);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(543, 239);
            this.tabSettings.TabIndex = 3;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // tbSecondaryDNS
            // 
            this.tbSecondaryDNS.AllowInternalTab = false;
            this.tbSecondaryDNS.AutoHeight = true;
            this.tbSecondaryDNS.BackColor = System.Drawing.SystemColors.Window;
            this.tbSecondaryDNS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tbSecondaryDNS.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbSecondaryDNS.Location = new System.Drawing.Point(209, 160);
            this.tbSecondaryDNS.MinimumSize = new System.Drawing.Size(87, 20);
            this.tbSecondaryDNS.Name = "tbSecondaryDNS";
            this.tbSecondaryDNS.ReadOnly = false;
            this.tbSecondaryDNS.Size = new System.Drawing.Size(87, 20);
            this.tbSecondaryDNS.TabIndex = 21;
            this.tbSecondaryDNS.Text = "...";
            this.tbSecondaryDNS.Leave += new System.EventHandler(this.tbSecondaryDNS_Leave);
            // 
            // tbPrimaryDNS
            // 
            this.tbPrimaryDNS.AllowInternalTab = false;
            this.tbPrimaryDNS.AutoHeight = true;
            this.tbPrimaryDNS.BackColor = System.Drawing.SystemColors.Window;
            this.tbPrimaryDNS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tbPrimaryDNS.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbPrimaryDNS.Location = new System.Drawing.Point(115, 160);
            this.tbPrimaryDNS.MinimumSize = new System.Drawing.Size(87, 20);
            this.tbPrimaryDNS.Name = "tbPrimaryDNS";
            this.tbPrimaryDNS.ReadOnly = false;
            this.tbPrimaryDNS.Size = new System.Drawing.Size(87, 20);
            this.tbPrimaryDNS.TabIndex = 20;
            this.tbPrimaryDNS.Text = "...";
            this.tbPrimaryDNS.Leave += new System.EventHandler(this.tbPrimaryDNS_Leave);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbGoogleDNS);
            this.panel1.Controls.Add(this.rbOpenDNS);
            this.panel1.Controls.Add(this.rbComodoDNS);
            this.panel1.Location = new System.Drawing.Point(107, 186);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(275, 27);
            this.panel1.TabIndex = 19;
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
            this.rbGoogleDNS.CheckedChanged += new System.EventHandler(this.rbGoogleDNS_CheckedChanged);
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
            this.rbOpenDNS.CheckedChanged += new System.EventHandler(this.rbOpenDNS_CheckedChanged);
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
            this.rbComodoDNS.CheckedChanged += new System.EventHandler(this.rbComodoDNS_CheckedChanged);
            // 
            // cbDontResetOnDisconn
            // 
            this.cbDontResetOnDisconn.AutoSize = true;
            this.cbDontResetOnDisconn.Location = new System.Drawing.Point(302, 162);
            this.cbDontResetOnDisconn.Name = "cbDontResetOnDisconn";
            this.cbDontResetOnDisconn.Size = new System.Drawing.Size(154, 17);
            this.cbDontResetOnDisconn.TabIndex = 18;
            this.cbDontResetOnDisconn.Text = "Don\'t Reset on Disconnect";
            this.cbDontResetOnDisconn.UseVisualStyleBackColor = true;
            this.cbDontResetOnDisconn.CheckedChanged += new System.EventHandler(this.cbResetOnDisconn_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "DNS:";
            // 
            // cbOpenVPNConfig
            // 
            this.cbOpenVPNConfig.FormattingEnabled = true;
            this.cbOpenVPNConfig.Location = new System.Drawing.Point(118, 49);
            this.cbOpenVPNConfig.Name = "cbOpenVPNConfig";
            this.cbOpenVPNConfig.Size = new System.Drawing.Size(241, 21);
            this.cbOpenVPNConfig.TabIndex = 5;
            this.cbOpenVPNConfig.SelectionChangeCommitted += new System.EventHandler(this.cbOpenVPNConfig_SelectionChangeCommitted);
            // 
            // hlVPNBookConfigDownload
            // 
            this.hlVPNBookConfigDownload.AutoSize = true;
            this.hlVPNBookConfigDownload.Location = new System.Drawing.Point(365, 52);
            this.hlVPNBookConfigDownload.Name = "hlVPNBookConfigDownload";
            this.hlVPNBookConfigDownload.Size = new System.Drawing.Size(134, 13);
            this.hlVPNBookConfigDownload.TabIndex = 11;
            this.hlVPNBookConfigDownload.TabStop = true;
            this.hlVPNBookConfigDownload.Text = "Download From VPN Book";
            this.hlVPNBookConfigDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.hlVPNBookConfigDownload_LinkClicked);
            // 
            // lblOpenVPNConfigLabel
            // 
            this.lblOpenVPNConfigLabel.AutoSize = true;
            this.lblOpenVPNConfigLabel.Location = new System.Drawing.Point(18, 52);
            this.lblOpenVPNConfigLabel.Name = "lblOpenVPNConfigLabel";
            this.lblOpenVPNConfigLabel.Size = new System.Drawing.Size(91, 13);
            this.lblOpenVPNConfigLabel.TabIndex = 4;
            this.lblOpenVPNConfigLabel.Text = "OpenVPN Config:";
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
            // tabOpenApps
            // 
            this.tabOpenApps.Controls.Add(this.btnMoveAppDown);
            this.tabOpenApps.Controls.Add(this.btnMoveAppUp);
            this.tabOpenApps.Controls.Add(this.btnSaveApps);
            this.tabOpenApps.Controls.Add(this.dgOpenApps);
            this.tabOpenApps.Location = new System.Drawing.Point(4, 22);
            this.tabOpenApps.Name = "tabOpenApps";
            this.tabOpenApps.Padding = new System.Windows.Forms.Padding(3, 3, 3, 40);
            this.tabOpenApps.Size = new System.Drawing.Size(543, 239);
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
            this.btnSaveApps.Location = new System.Drawing.Point(235, 205);
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
            this.dgOpenApps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.launchDataGridViewCheckBoxColumn,
            this.closeDataGridViewCheckBoxColumn,
            this.programDataGridViewTextBoxColumn});
            this.dgOpenApps.DataSource = this.appsBindingSource;
            this.dgOpenApps.Location = new System.Drawing.Point(63, 3);
            this.dgOpenApps.Name = "dgOpenApps";
            this.dgOpenApps.RowHeadersVisible = false;
            this.dgOpenApps.Size = new System.Drawing.Size(477, 195);
            this.dgOpenApps.TabIndex = 0;
            // 
            // tabConnection
            // 
            this.tabConnection.AutoScroll = true;
            this.tabConnection.Controls.Add(this.label4);
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
            this.tabConnection.Size = new System.Drawing.Size(572, 273);
            this.tabConnection.TabIndex = 0;
            this.tabConnection.Text = "Connection Info";
            this.tabConnection.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(309, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "TODO: secure ovpn, openvpn wait time";
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
            this.btnConnect.Location = new System.Drawing.Point(239, 199);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
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
            this.tabs.Controls.Add(this.tabVPNBook);
            this.tabs.Controls.Add(this.tabVPNTraffic);
            this.tabs.Location = new System.Drawing.Point(12, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(580, 299);
            this.tabs.TabIndex = 0;
            // 
            // cdTorProxyChrome
            // 
            this.cdTorProxyChrome.AutoSize = true;
            this.cdTorProxyChrome.Enabled = false;
            this.cdTorProxyChrome.Location = new System.Drawing.Point(115, 86);
            this.cdTorProxyChrome.Name = "cdTorProxyChrome";
            this.cdTorProxyChrome.Size = new System.Drawing.Size(161, 17);
            this.cdTorProxyChrome.TabIndex = 22;
            this.cdTorProxyChrome.Text = "EnableTor Proxy For Chrome";
            this.cdTorProxyChrome.UseVisualStyleBackColor = true;
            this.cdTorProxyChrome.CheckedChanged += new System.EventHandler(this.cdTorProxyChrome_CheckedChanged);
            // 
            // lnkChromeIpLeak
            // 
            this.lnkChromeIpLeak.AutoSize = true;
            this.lnkChromeIpLeak.Location = new System.Drawing.Point(115, 110);
            this.lnkChromeIpLeak.Name = "lnkChromeIpLeak";
            this.lnkChromeIpLeak.Size = new System.Drawing.Size(154, 13);
            this.lnkChromeIpLeak.TabIndex = 23;
            this.lnkChromeIpLeak.TabStop = true;
            this.lnkChromeIpLeak.Text = "Get Chrome IP Leak Protection";
            this.lnkChromeIpLeak.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkChromeIpLeak_LinkClicked);
            // 
            // launchDataGridViewCheckBoxColumn
            // 
            this.launchDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.launchDataGridViewCheckBoxColumn.DataPropertyName = "Launch";
            this.launchDataGridViewCheckBoxColumn.HeaderText = "Launch";
            this.launchDataGridViewCheckBoxColumn.Name = "launchDataGridViewCheckBoxColumn";
            this.launchDataGridViewCheckBoxColumn.Width = 49;
            // 
            // closeDataGridViewCheckBoxColumn
            // 
            this.closeDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.closeDataGridViewCheckBoxColumn.DataPropertyName = "Close";
            this.closeDataGridViewCheckBoxColumn.HeaderText = "Close";
            this.closeDataGridViewCheckBoxColumn.Name = "closeDataGridViewCheckBoxColumn";
            this.closeDataGridViewCheckBoxColumn.Width = 39;
            // 
            // programDataGridViewTextBoxColumn
            // 
            this.programDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.programDataGridViewTextBoxColumn.DataPropertyName = "Program";
            this.programDataGridViewTextBoxColumn.HeaderText = "Program";
            this.programDataGridViewTextBoxColumn.Name = "programDataGridViewTextBoxColumn";
            // 
            // appsBindingSource
            // 
            this.appsBindingSource.DataSource = typeof(P2PVpn.Models.Apps);
            // 
            // P2PVPNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 323);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "P2PVPNForm";
            this.Text = "P2P VPN Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.P2PVPNForm_FormClosing);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabVPNTraffic.ResumeLayout(false);
            this.tabVPNBook.ResumeLayout(false);
            this.tabVPNBook.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabOpenApps.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgOpenApps)).EndInit();
            this.tabConnection.ResumeLayout(false);
            this.tabConnection.PerformLayout();
            this.tabs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.appsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerSpeed;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusColor;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusText;
        private System.Windows.Forms.BindingSource appsBindingSource;
        private System.Windows.Forms.FolderBrowserDialog openVPNFolderBrowser;
        private System.Windows.Forms.TabPage tabVPNTraffic;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.TabPage tabVPNBook;
        private System.Windows.Forms.TabPage tabSettings;
        private IPAddressControlLib.IPAddressControl tbSecondaryDNS;
        private IPAddressControlLib.IPAddressControl tbPrimaryDNS;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbGoogleDNS;
        private System.Windows.Forms.RadioButton rbOpenDNS;
        private System.Windows.Forms.RadioButton rbComodoDNS;
        private System.Windows.Forms.CheckBox cbDontResetOnDisconn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel hlVPNBookConfigDownload;
        private System.Windows.Forms.Button btnVPNBookUser;
        private System.Windows.Forms.TextBox tbVPNPassword;
        private System.Windows.Forms.TextBox tbVPNUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbOpenVPNConfig;
        private System.Windows.Forms.Label lblOpenVPNConfigLabel;
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.Label lblConnectionStatuslbl;
        private System.Windows.Forms.Label lblRunningApps;
        private System.Windows.Forms.Label lblRunningAppsLabel;
        private System.Windows.Forms.Label lblVPNConnectionStatus;
        private System.Windows.Forms.Label lblVPNConnectionStatusLabel;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.CheckBox cdTorProxyChrome;
        private System.Windows.Forms.LinkLabel lnkChromeIpLeak;
    }
}

