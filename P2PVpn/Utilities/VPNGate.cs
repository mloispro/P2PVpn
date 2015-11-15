using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using P2PVpn.Models;

namespace P2PVpn.Utilities
{
    public class VPNGate
    {
        public static string ServersCSV = Path.GetFullPath(Settings.AppDir + @"\Assets\VPNGateServers.csv");
        public static string VpnGateCredsFile = "vpngate-creds.txt";
        private static List<VPNGateServer> _servers = new List<VPNGateServer>();
        private static List<VPNGateServer> _fastestServers = new List<VPNGateServer>();
        public static string VpnGateConifg = "vpn_gate.ovpn";
        public const string VPNGateServerListUrl = @"http://www.vpngate.net/api/iphone/";
        //private static DateTime _lastDownloadTime;

        private static void DownloadServerList()
        {
            WebClient wc = Networking.GetTorWebClient();
            var doc = "";
            try
            {
                doc = wc.DownloadString(VPNGateServerListUrl);
                
            }
            catch (WebException ex)
            {
                try
                {
                    WebClient wc2 = new WebClient();
                    doc = wc2.DownloadString(VPNGateServerListUrl);
                }
                catch (WebException ex2)
                {
                    Logging.Log("Error: Can't Connect to VPNGate, using previous servers file, if it exists");
                    return;
                }
            }
            

            FileStream file = null;
            if (File.Exists(ServersCSV))
            {
                File.Delete(ServersCSV);
            }
            file = File.OpenWrite(ServersCSV);
            
            using (file)
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.Write(doc);
                    
                }
            }
            Settings settings = Settings.Get();
            settings.VPNServer.LastVPNGateServerListDownload = DateTime.Now;
            Settings.Save(settings);
        }
        private static void LoadServers()
        {
            Settings settings = Settings.Get();
            if (settings.VPNServer.LastVPNGateServerListDownload == null ||
                settings.VPNServer.LastVPNGateServerListDownload < DateTime.Now.AddMinutes(-30))
            {
                DownloadServerList();
            }
            _servers.Clear();
            var csvLines = File.ReadAllLines(ServersCSV).Skip(2);
            foreach (string line in csvLines)
            {
                if (line.StartsWith("*")) continue;
                var server = FromCsv(line);
                _servers.Add(server);
            }

        }
        public static List<VPNGateServer> GetFastestServers()
        {

            LoadServers();
            
            //if (_servers.Count == 0) LoadServers();
            //if (_fastestServers.Count > 0) return _fastestServers;

            var fastestServers = _servers.OrderByDescending(x => x.Speed)
                .Where(x => x.CountryShort != "US" && 
                    x.Uptime > 5401134 && 
                    x.TotalUsers > 20 && 
                    x.Score > 41772);

            fastestServers = fastestServers.Take(10);
            return fastestServers.ToList();
        }
        public static void SelectServer(VPNGateServer server)
        {

            var config = Path.GetFullPath(OpenVPN.GetOpenVpnDirectory() + @"\config\" + VpnGateConifg);

            FileStream file = null;
            if (!File.Exists(config))
            {
                file = File.Create(config);
            }
            else
            {
                File.WriteAllText(config, "");
                file = File.OpenWrite(config);
                
            }
            using (file)
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.Write(server.OpenVPNConfigData);
                    
                }
            }
            OpenVPN.SecureConfigs();


        }
        private static VPNGateServer FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            VPNGateServer server = new VPNGateServer();
            server.CountryLong = values[5];
            server.CountryShort = values[6];
            server.HostName = values[0];
            server.IP = values[1];
            server.Score = int.Parse(values[2]);
            server.LogType = values[11];
            server.Message = values[13];
            server.NumVpnSessions = values[7];
            server.Uptime = long.Parse(values[8]);
            server.Operator = values[12];
            server.Ping = values[3];
            server.Speed = int.Parse(values[4]);
            server.TotalTraffic = long.Parse(values[10]);
            server.TotalUsers = int.Parse(values[9]);
            byte[] data = Convert.FromBase64String(values[14]);
            string decodedString = Encoding.UTF8.GetString(data);
            server.OpenVPNConfigData = decodedString;
         

            return server;
        }
       
    }
}
