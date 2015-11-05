using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NETWORKLIST;
using P2PVpn.Models;

namespace P2PVpn.Utilities
{
    public class Networking : IDisposable
    {
        private static ListBox _log;
        public List<NetworkAdapter> ActiveNetworkAdapters = new List<NetworkAdapter>();
        public NetworkListManager NetworkListManager;
        public static bool DisableDisconnect = false;

        public Networking(ListBox listBox)
        {
            
            listBox.Items.Clear();
            _log = listBox;
            //EnableAllNeworkInterfaces();
            NetworkListManager = new NetworkListManager();
            ScanNetworkInterfaces();
            LogNetworkInfo();
            NetworkListManager.NetworkConnectivityChanged += NetworkListManager_NetworkConnectivityChanged;

        }
        
        private async void NetworkListManager_NetworkConnectivityChanged(Guid networkId, NLM_CONNECTIVITY newConnectivity)
        {

            _log.Log("**Network Connectivity Changed**");

            if (!DisableDisconnect && 
                newConnectivity == NLM_CONNECTIVITY.NLM_CONNECTIVITY_DISCONNECTED)
            {
                DisableDisconnect = true;
                //NetworkListManager.NetworkConnectivityChanged -= NetworkListManager_NetworkConnectivityChanged;
                var adapters = GetActiveNetworkInterfaces();
                EnableAllNeworkInterfaces(false);
                _log.Log("**Disconnect Triggered**");
                await ClosePrograms();
                EnableAllNeworkInterfaces();
                if (Settings.Get().ResetDNS)
                {
                    foreach (var adapter in adapters)
                    {
                        string primaryDns = string.Format("interface IPv4 set dnsserver \"{0}\" dhcp", adapter.Name);
                        string secondaryDns = string.Format("interface ip set dns \"{0}\" dhcp", adapter.Name);
                        ControlHelpers.StartProcess(@"netsh", primaryDns);
                        ControlHelpers.StartProcess(@"netsh", secondaryDns);
                    }
                    ControlHelpers.StartProcess(@"ipconfig.exe", @"/flushdns");
                }
                if (this.IsOpenVPNConnected())
                {
                    DisableDisconnect = true;
                }
            }
            //else if (newConnectivity == NLM_CONNECTIVITY.NLM_CONNECTIVITY_IPV4_INTERNET)
            //{

            //    if (this.IsOpenVPNConnected())
            //    {
            //        OpenPrograms();
            //    }
            //}
            ScanNetworkInterfaces();
            LogNetworkInfo();

        }
        public async Task ClosePrograms()
        {
            var closePrograms = Apps.Get().FindAll(x => x.Close);
            foreach (var program in closePrograms)
            {
                var name = Path.GetFileName(program.Program);
                ControlHelpers.StartProcess("taskkill", "/F /IM " + name);
            }
            
            await ControlHelpers.Sleep(10000);
        }
        public void OpenPrograms()
        {
            if (this.IsOpenVPNConnected())
            {
                var openPrograms = Apps.Get().FindAll(x => x.Launch);
                foreach (var program in openPrograms)
                {
                    ControlHelpers.StartProcess(program.Program, "", false);
                }
            }
        }
       
        public void ScanNetworkInterfaces()
        {
            ActiveNetworkAdapters = GetActiveNetworkInterfaces();
        }
        private List<NetworkAdapter> GetActiveNetworkInterfaces()
        {

            //Get collection of all network interfaces on the given machine.
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            var activeNics = nics.Where(x => x.OperationalStatus == OperationalStatus.Up &&
                x.GetIPProperties() != null && x.Description != null &&
                x.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                x.GetIPProperties().DnsAddresses.Count > 0);

            var adapters = new List<NetworkAdapter>();
            foreach (NetworkInterface nic in activeNics)
            {

                var adapter = new NetworkAdapter
                {
                    Description = nic.Description,
                    Name = nic.Name,
                    Speed = nic.Speed / 1024,
                    WmiMacAddress = nic.GetPhysicalAddress().ToString(),
                    PrimaryDns = nic.GetIPProperties().DnsAddresses[0].ToString(),
                    BytesSent = nic.GetIPStatistics().BytesSent / 1024,
                    BytesReceived = nic.GetIPStatistics().BytesReceived / 1024,
                    Id = new Guid(nic.Id)
                };

                if (nic.GetIPProperties().DnsAddresses.Count > 1)
                {
                    adapter.SecondaryDns = nic.GetIPProperties().DnsAddresses[1].ToString();
                }
                foreach (UnicastIPAddressInformation ip in nic.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        adapter.IpAddress = ip.Address.ToString();
                    }
                }
                FillNetworkInfo(adapter);
                adapters.Add(adapter);
                

            }
            return adapters;
        }

        private void FillNetworkInfo(NetworkAdapter adapter)
        {

            //List the connected networks. There are many other APIs 
            //can be called to get network information.

            IEnumNetworkConnections connections = NetworkListManager.GetNetworkConnections();
            foreach (INetworkConnection con in connections)
            {
                var adapterId = con.GetAdapterId();
                if (adapter.Id != adapterId) continue;

                //var connectionId = con.GetConnectionId();
                adapter.ConnectivityString = GetConnectivity(con.GetConnectivity());
                adapter.IsConnected = con.IsConnected;
                adapter.IsConnectedToInternet = con.IsConnectedToInternet;
                INetwork network = con.GetNetwork();
                adapter.NetworkName = network.GetName();
               // var networkCategory = network.GetCategory();
                //var adapter = _networkAdapters.FirstOrDefault(x => x.Id == adapterId);

            }

            //AdviseforNetworklistManager();

            // UnAdviseforNetworklistManager();
        }

        private string GetConnectivity(NLM_CONNECTIVITY newConnectivity)
        {
            //Console.WriteLine("");
            //Console.WriteLine("-------------------------------------------" +
            //                  "--------------------------------");
            //Log("NetworkList just informed the connectivity change." +
            //                  " The new connectivity is:");

            string connectivity = "";

            if (newConnectivity == NLM_CONNECTIVITY.NLM_CONNECTIVITY_DISCONNECTED)
            {
                connectivity = "disconnected";
            }
            if (((int)newConnectivity &
                 (int)NLM_CONNECTIVITY.NLM_CONNECTIVITY_IPV4_INTERNET) != 0)
            {
                connectivity = "connected to internet with IPv4 capability";
            }
            if (((int)newConnectivity &
                 (int)NLM_CONNECTIVITY.NLM_CONNECTIVITY_IPV6_INTERNET) != 0)
            {
                connectivity = "connected to internet with IPv6 capability";
            }
            if (((int)newConnectivity &
              (int)NLM_CONNECTIVITY.NLM_CONNECTIVITY_IPV4_LOCALNETWORK) != 0)
            {
                if (!string.IsNullOrEmpty(connectivity))
                {
                    connectivity = connectivity + " and connected to local network";
                }
                else
                {
                    connectivity = "connected to local network";
                }
            }
            //if ((((int)newConnectivity &
            //      (int)NLM_CONNECTIVITY.NLM_CONNECTIVITY_IPV4_INTERNET) == 0) &&
            //      (((int)newConnectivity &
            //      (int)NLM_CONNECTIVITY.NLM_CONNECTIVITY_IPV6_INTERNET) == 0))
            //{
            //    Log("The machine is not connected to internet yet ");
            //}
            return connectivity;
        }
        public bool IsOpenVPNConnected() 
        {
            bool isConnected = false;
            var vpnAdapter = ActiveNetworkAdapters.FirstOrDefault(x => Networking.IsVPNAdapter(x));
            isConnected = (vpnAdapter != null);
            return isConnected;
        }


        private bool IsNetworkConnected(NetworkInterface nic)
        {
            bool isConnected = false;
            if (nic != null && nic.OperationalStatus == OperationalStatus.Up &&
                nic.GetIPProperties() != null && nic.Description != null &&
                nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            {
                isConnected = true;
            }

            //Helper.doLog(m_viewForLogging, "isNetworkConnected " + nic.Id + " = " + bReturn, m_config.DebugMode, m_config.ConsoleMaxSize);

            return isConnected;
        }

        public void EnableAllNeworkInterfaces(bool enable = true)
        {
            string verb = "Enable";
            if (!enable) verb = "Disable";

            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionId != NULL");
            ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            foreach (ManagementObject item in searchProcedure.Get())
            {
                if (!enable)
                {
                    //dissable
                    var adapter = this.ActiveNetworkAdapters.FirstOrDefault(x => x.Name == (string)item["NetConnectionId"]);
                    if (adapter != null) item.InvokeMethod(verb, null);
                    
                }
                else
                {
                    //enable
                    item.InvokeMethod(verb, null);
                    
                }
            }
            _log.Log(verb + " All Network Interfaces");
        }
        public static bool IsVPNAdapter(NetworkAdapter adapter)
        {
            bool isVpnAdapter = adapter.Description.ToLower().StartsWith("tap");
            return isVpnAdapter;
        }
        public void ShowNetworkTraffic()
        {
            //PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
            //string instance = performanceCounterCategory.GetInstanceNames()[5]; // 1st NIC !
            //PerformanceCounter performanceCounterSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instance);
            //PerformanceCounter performanceCounterReceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", instance);

            //for (int i = 0; i < 10; i++)
            //{
            //    Debug.WriteLine("bytes sent: {0}k\tbytes received: {1}k", performanceCounterSent.NextValue() / 1024, performanceCounterReceived.NextValue() / 1024);
            //    Log("bytes sent: {0}k\tbytes received: {1}k", performanceCounterSent.NextValue() / 1024, performanceCounterReceived.NextValue() / 1024);
            //    Thread.Sleep(500);
            //}
        }

        #region Log

        private void LogNetworkInfo()
        {
            foreach (var adapter in this.ActiveNetworkAdapters)
            {
                _log.Log("Network Name: {0} {1}", adapter.NetworkName, adapter.ConnectivityString);
                _log.Log("\tActive Nework Interface: {0} ({1}) Id:{2}", adapter.Name, adapter.Description, adapter.Id);
                _log.Log("\tIP: {0}\tDNS: {1},{2}", adapter.IpAddress, adapter.PrimaryDns, adapter.SecondaryDns);
            }
        }

     
        #endregion Log

        #region Dispose

        private bool _disposed;
        ~Networking()
        {
            Dispose(true);
        }

        public async void Dispose()
        {
            await Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //_networkListManager.NetworkConnectivityChanged -= _networkListManager_NetworkConnectivityChanged;
                }
                await ClosePrograms();
                if (NetworkListManager != null)
                {
                    NetworkListManager = null;
                }
                ActiveNetworkAdapters = null;
                _disposed = true;
            }

        }
        #endregion Dispose

        //private IConnectionPoint m_icp;
        //private int m_cookie;
        //public void UnAdviseforNetworklistManager()
        //{
        //    Log("Un Subscribing the INetworkListManagerEvents");
        //    m_icp.Unadvise(m_cookie);
        //}
        //public void AdviseforNetworklistManager()
        //{
        //    Log("Subscribing the INetworkListManagerEvents");
        //    IConnectionPointContainer icpc = (IConnectionPointContainer)_networkListManager;
        //    //similar event subscription can be used 
        //    //for INetworkEvents and INetworkConnectionEvents
        //    Guid tempGuid = typeof(INetworkListManagerEvents).GUID;
        //    icpc.FindConnectionPoint(ref tempGuid, out m_icp);
        //    m_icp.Advise(this, out m_cookie);
        //}
    }
}
