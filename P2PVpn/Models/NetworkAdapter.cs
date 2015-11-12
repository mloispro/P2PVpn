using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PVpn.Models
{
    public class NetworkAdapter
    {
        //string description = "";
        //string name = "";
        //long speed = 0;
        //string wmiMacAddress = "";
        //string primaryDns = "";
        //string secondaryDns = "";
        //string ipAddress = "";
        ////string nicName = "";
        //bool nicActive = false;
        //long bytesSent = 0;
        //long bytesRecevied = 0;

        public string Description { get; set; }
        public string Name { get; set; }
        public long Speed { get; set; }
        public string WmiMacAddress { get; set; }
        public string PrimaryDns { get; set; }
        public string SecondaryDns { get; set; }
        public long BytesSent { get; set; }
        public long BytesReceived { get; set; }
        public string IpAddress { get; set; }
        public Guid Id { get; set; }
        public string ConnectivityString { get; set; }
        public bool IsConnected { get; set; }
        public bool IsConnectedToInternet { get; set; }
        public string NetworkName { get; set; }
        public string StartupPrimaryDNS { get; set; }
        public string StartupSecondaryDNS { get; set; }
        public string GatewayIP { get; set; }

        public static NetworkAdapter OpenVpnAdapter { get; set; }
    }
}
