using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PVpn.Models
{
    public class VPNGateServer
    {
        //HostName,IP,Score,Ping,Speed,CountryLong,CountryShort,NumVpnSessions,Uptime,TotalUsers,TotalTraffic,LogType,Operator,Message,OpenVPN_ConfigData_Base64
        public string HostName { get; set; }
        public string IP { get; set; }
        public string Ping { get; set; }
        public int Speed { get; set; }
        public int Score { get; set; }
        public string CountryLong { get; set; }
        public string CountryShort { get; set; }
        public string NumVpnSessions { get; set; }
        public long Uptime { get; set; }
        public int TotalUsers { get; set; }
        public long TotalTraffic { get; set; }
        public string LogType { get; set; }
        public string Operator { get; set; }
        public string Message { get; set; }
        public string OpenVPNConfigData { get; set; }
    }
}
