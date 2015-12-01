using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PVpn.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Diagnostics;

    /// <summary>
    /// Class to maintain connectivity to a specific VPN connection
    /// </summary>
    /// <history>
    ///     [Tim Hibbard]   01/24/2007  Created
    /// </history>
    public class VPNManager
    {
        #region --Const--
        /// <summary>
        /// Where the rasphone.exe lives
        /// </summary>
        private const string VPNPROCESS = "C:\\WINDOWS\\system32\\rasphone.exe";
        #endregion

        #region --Fields--
        /// <summary>
        /// Internal variable for VPNConnectionName
        /// </summary>
        private string _VPNConnectionName = "";
        /// <summary>
        /// Internal variable for IPToPing
        /// </summary>
        private string _IPToPing = "";
        /// <summary>
        /// Internal variable for IsConnected
        /// </summary>
        private bool _isConnected = false;
        /// <summary>
        /// Timer that manages the Manage function
        /// </summary>
        private System.Timers.Timer MonitorTimer;
        /// <summary>
        /// Bool to flag if the system is currently checking for network validity
        /// </summary>
        private bool _isChecking = false;
        /// <summary>
        /// Bool to flag if the system is currently in a Manage loop
        /// </summary>
        private bool _isManaging = false;
        #endregion

        #region --Events--
        public delegate void PingingHandler();
        public delegate void ConnectingHandler();
        public delegate void DisconnectingHandler();
        public delegate void IdleHandler();
        public delegate void ConnectionStatusChangedHandler(bool Connected);

        /// <summary>
        /// Fires when validating connectivity
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        public event PingingHandler Pinging;

        /// <summary>
        /// Fired when it is trying to connect to the VPN
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        public event ConnectingHandler Connecting;

        /// <summary>
        /// Fired when it is trying to disconnect from the VPN
        /// </summary>
        public event DisconnectingHandler Disconnecting;

        /// <summary>
        /// Fired when it is done working for the moment
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        public event IdleHandler Idle;

        /// <summary>
        /// Fired when the IsConnected Property changes
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        public event ConnectionStatusChangedHandler ConnectionStatusChanged;

        /// <summary>
        /// Call to raise Pinging event
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        protected void OnPinging()
        {
            if (Pinging != null)
            {
                Pinging();
            }
        }

        /// <summary>
        /// Call to raise Connecting event
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        protected void OnConnecting()
        {
            if (Connecting != null)
            {
                Connecting();
            }
        }

        /// <summary>
        /// Call to raise Disconnecting event
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        protected void OnDisconnecting()
        {
            if (Disconnecting != null)
            {
                Disconnecting();
            }
        }

        /// <summary>
        /// Call to raise Idle event
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        protected void OnIdle()
        {
            if (Idle != null)
            {
                Idle();
            }
        }

        /// <summary>
        /// Call to raise ConnectionStatusChanged event
        /// </summary>
        /// <param name="Connected">If connected to network</param>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        protected void OnConnectionStatusChanged(bool Connected)
        {
            if (ConnectionStatusChanged != null)
            {
                ConnectionStatusChanged(Connected);
            }
        }

        #endregion

        #region --Properties--
        /// <summary>
        /// Returns if you are connected to the network
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        public bool IsConnected
        {
            get { return _isConnected; }
        }

        /// <summary>
        /// IP to ping to validate connectivity
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        public string IPToPing
        {
            get { return _IPToPing; }
            set { _IPToPing = value; }
        }

        /// <summary>
        /// Name of VPN connection as seen in network connections (not case sensitive)
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        public string VPNConnectionName
        {
            get { return _VPNConnectionName; }
            set { _VPNConnectionName = value; }
        }
        #endregion

        #region --Private Methods--
        /// <summary>
        /// Pings the provided IP to validate connection
        /// </summary>
        /// <returns>True if you are connected</returns>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        public bool TestConnection()
        {
            bool RV = false;
            _isChecking = true;
            try
            {
                OnPinging();
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();

                if (ping.Send(_IPToPing).Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    RV = true;
                }
                else
                {
                    RV = false;
                }
                ping = null;
                if (RV != _isConnected)
                {
                    _isConnected = RV;
                    OnConnectionStatusChanged(_isConnected);
                }
                OnIdle();
            }
            catch (Exception Ex)
            {
                Debug.Assert(false, Ex.ToString());
                RV = false;
                OnIdle();
            }
            _isChecking = false;
            return RV;
        }

        /// <summary>
        /// Shells the command to connect to the VPN
        /// </summary>
        /// <returns>True if connected</returns>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        private bool ConnectToVPN()
        {
            bool RV = false;
            try
            {
                OnConnecting();
                Process.Start(VPNPROCESS, " -d " + _VPNConnectionName);
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(5000);
                System.Windows.Forms.Application.DoEvents();
                RV = true;
                OnIdle();

            }
            catch (Exception Ex)
            {
                Debug.Assert(false, Ex.ToString());
                RV = false;
                OnIdle();
            }
            return RV;
        }

        /// <summary>
        /// Shells the command to disconnect from the VPN connection
        /// </summary>
        /// <returns>True if successfully disconnected</returns>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        private bool DisconnectFromVPN()
        {
            bool RV = false;
            try
            {
                OnDisconnecting();
                System.Diagnostics.Process.Start(VPNPROCESS, " -h " + _VPNConnectionName);
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(8000);
                System.Windows.Forms.Application.DoEvents();
                RV = true;
                OnIdle();
            }
            catch (Exception Ex)
            {
                Debug.Assert(false, Ex.ToString());
                RV = false;
                OnIdle();
            }
            return RV;
        }

        /// <summary>
        /// Handles the grunt work.
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        private void Manage()
        {
            try
            {
                if (!_isManaging)
                {
                    _isManaging = true;
                    if (!_isChecking)
                    {
                        if (!TestConnection())
                        {
                            ConnectToVPN();
                            if (!TestConnection())
                            {
                                DisconnectFromVPN();
                                ConnectToVPN();
                                if (!TestConnection())
                                {
                                    DisconnectFromVPN();
                                    ConnectToVPN();
                                }
                            }
                        }
                    }
                    _isManaging = false;
                }
            }
            catch (Exception)
            {
                _isManaging = false;
            }
        }
        #endregion

        #region --Public Methods--
        /// <summary>
        /// Overloaded end point to begin monitoring VPN status
        /// </summary>
        /// <param name="VPNName">Name of VPN connection as seen in network connections (not case sensitive)</param>
        /// <param name="IPtoPing">IP to ping to validate connectivity</param>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        public void StartManaging(string VPNName, string IPtoPing)
        {
            _VPNConnectionName = VPNName;
            _IPToPing = IPtoPing;
            StartManaging();

        }

        /// <summary>
        /// End point to begin monitoring VPN status
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        public void StartManaging()
        {
            if (!string.IsNullOrEmpty(_VPNConnectionName) & !string.IsNullOrEmpty(_IPToPing))
            {
                MonitorTimer = new System.Timers.Timer(15000);
                MonitorTimer.Enabled = true;
                MonitorTimer.Elapsed += new System.Timers.ElapsedEventHandler(MonitorTimer_Elapsed);
                System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += new System.Net.NetworkInformation.NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);
                Microsoft.Win32.SystemEvents.PowerModeChanged += new Microsoft.Win32.PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
                Manage();
            }
        }
        #endregion

        #region --Constructors--
        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="VPNName">Name of VPN connection as seen in network connections (not case sensitive)</param>
        /// <param name="IPtoPing">IP to ping to validate connectivity</param>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        public VPNManager(string VPNName, string IPtoPing)
        {
            StartManaging(VPNName, IPtoPing);
        }

        /// <summary>
        /// Default empty constructor
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        public VPNManager()
        {
        }
        #endregion

        #region --Event Handlers--
        /// <summary>
        /// Handles the event that is raised when the computer goes into, or comes out of, standby.  Very useful for laptops
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        void SystemEvents_PowerModeChanged(object sender, Microsoft.Win32.PowerModeChangedEventArgs e)
        {
            if (e.Mode == Microsoft.Win32.PowerModes.Resume)
            {
                MonitorTimer.Stop();
                System.Threading.Thread.Sleep(15000);
                MonitorTimer.Start();
            }
            if (e.Mode == Microsoft.Win32.PowerModes.Suspend)
            {
                MonitorTimer.Stop();
            }
        }

        /// <summary>
        /// Handles the event that is raised when the network status changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        void NetworkChange_NetworkAvailabilityChanged(object sender, System.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
            {
                if (!MonitorTimer.Enabled)
                {
                    MonitorTimer.Start();
                }
            }
            else
            {
                MonitorTimer.Stop();
            }
        }

        /// <summary>
        /// Handles the event the timer raises when it elapses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [Tim Hibbard]   01/24/2007  Created
        /// </history>
        void MonitorTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Manage();
        }
        #endregion
    }
}

