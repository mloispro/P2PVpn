using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using P2PVpn.Models;

namespace P2PVpn.Utilities
{
    public static class OpenVPN
    {
        public static string VPNBookCredsFile = "vpnbook-creds.txt";
        public static string VPNBookDownFile = "p2pVpn-disconnect.cmd";

        private static string _p2pVpnSettings = "{0}{0}#P2PVpn Settings#{0}" +
                            "script-security 2{0}" +
                            "auth-user-pass \"{1}\\\\config\\\\{2}\"{0}" +
                            "plugin \"{1}\\\\bin\\\\fix-dns-leak-32.dll\"{0}" +
                            "down-pre{0}" +
                            "down \"{3}\"{0}";
                            //"up-restart{0}" +

        private static string _p2pVpnRouteSettings =
           string.Format("route 0.0.0.0 192.0.0.0 net_gateway{0}" +
                           "route 64.0.0.0 192.0.0.0 net_gateway{0}" +
                           "route 128.0.0.0 192.0.0.0 net_gateway{0}" +
                           "route 192.0.0.0 192.0.0.0 net_gateway",
                           Environment.NewLine);

        private static string _openVPNDirectory;

        public static string GetOpenVpnDirectory()
        {
            if (string.IsNullOrEmpty(_openVPNDirectory))
            {
                _openVPNDirectory = Settings.Get().OpenVPNDirectory.Replace(@"\", @"\\");
            }
            return _openVPNDirectory;
        }
        public static void SavePassword(string password)
        {
            Settings settings = Settings.Get();
            var binDir = Settings.UserSettingsDir;
            var localCredsFile = binDir + @"\" + VPNBookCredsFile;
            var openVpnCredsFile = settings.OpenVPNDirectory + @"\config\" + VPNBookCredsFile;

            if (!File.Exists(localCredsFile))
            {
                File.Create(localCredsFile).Close();
            }
            using (FileStream file = File.Open(localCredsFile, FileMode.OpenOrCreate,  FileAccess.ReadWrite))
            {
                string contents = "";
                using (StreamReader sr = new StreamReader(file))
                {
                    contents = sr.ReadToEnd();
                }
                if (string.IsNullOrWhiteSpace(contents) || !contents.Contains(Environment.NewLine))
                {
                    contents = Environment.NewLine + password;
                }
                else
                {
                    int newLineIndex = contents.IndexOf(Environment.NewLine) + 2;
                    if (newLineIndex < contents.Length)
                    {
                        contents = contents.Remove(newLineIndex);
                    }
                    contents = contents.Insert(newLineIndex, password);
                }
                using (StreamWriter sw = new StreamWriter(localCredsFile))
                {
                    sw.Write(contents);
                    
                }
                File.Copy(localCredsFile, openVpnCredsFile, true);
            }
          
        }
        public static void SaveUsername(string username)
        {
            Settings settings = Settings.Get();
            var binDir = Settings.UserSettingsDir;
            var localCredsFile = binDir + @"\" + VPNBookCredsFile;
            var openVpnCredsFile = settings.OpenVPNDirectory + @"\config\" + VPNBookCredsFile;

            if (!File.Exists(localCredsFile))
            {
                File.Create(localCredsFile).Close();
            }
            using (FileStream file = File.Open(localCredsFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                string contents = "";
                using (StreamReader sr = new StreamReader(file))
                {
                    contents = sr.ReadToEnd();
                }
                if (string.IsNullOrWhiteSpace(contents) || !contents.Contains(Environment.NewLine))
                {
                    contents = username + Environment.NewLine;
                }
                else
                {
                    int newLineIndex = contents.IndexOf(Environment.NewLine);
                    contents = contents.Remove(0,newLineIndex);
                    contents = contents.Insert(0, username);
                }
                using (StreamWriter sw = new StreamWriter(localCredsFile))
                {
                    sw.Write(contents);
                    
                }
                File.Copy(localCredsFile, openVpnCredsFile, true);
            }
          
        }
        public static void UpdateDownScript()
        {
            StringBuilder buffer = new StringBuilder();
            var closePrograms = Apps.Get().FindAll(x => x.Close);
            foreach (var program in closePrograms)
            {
                var name = Path.GetFileName(program.Program);
                buffer.AppendLine("taskkill /F /IM " + name);
                //ControlHelpers.StartProcess("taskkill", "/F /IM " + name);
            }
            Settings settings = Settings.Get();

            var openVpnDownScript = settings.OpenVPNDirectory + @"\config\" + VPNBookDownFile;

            using (var file = File.Create(openVpnDownScript))
            {
                using (var sw = new StreamWriter(file))
                {
                    sw.Write(buffer.ToString());
                }
            } 

        }
        public static void SecureConfigs(bool addRouts = false)
        {
            Settings settings = Settings.Get();

            foreach (var file in settings.OpenVPNConfigs)
            {
                var configFile = Path.GetFullPath(file.Value);

                var configFileText="";
                using (var sw = new StreamReader(configFile))
                {
                    configFileText = sw.ReadToEnd();
                }

                var endToken = "</key>";
                var endIndex = configFileText.LastIndexOf(endToken) + endToken.Length;
                var cleanedConfigFileText = configFileText.Substring(0, endIndex);
                var p2pVPNConfigFileText = cleanedConfigFileText + PopulateP2PVpnConfigData(configFile);

                //resolv-retry 10
                Regex retryRegex = new Regex("resolv-retry.*");
                p2pVPNConfigFileText = retryRegex.Replace(p2pVPNConfigFileText, "resolv-retry 10");
                p2pVPNConfigFileText = p2pVPNConfigFileText.Replace("resolv-retry 10\n", "resolv-retry 10\r\n");
                

                if (addRouts)
                {
                    p2pVPNConfigFileText += _p2pVpnRouteSettings;
                }

                using (var cfile = File.OpenWrite(configFile))
                {
                    using (var sr = new StreamWriter(cfile))
                    {
                        sr.Write(p2pVPNConfigFileText);
      
                    }
                }
                
            }
        }
        private static string PopulateP2PVpnConfigData(string configFile)
        {
            string credFile = "";

            if (Path.GetFileName(configFile) == VPNGate.VpnGateConifg)
            {
                credFile = VPNGate.VpnGateCredsFile;
                var configDir = Path.GetDirectoryName(configFile);
                var openVpnCredsFile = Path.GetFullPath(configDir + @"\" + VPNGate.VpnGateCredsFile);
                if (!File.Exists(openVpnCredsFile))
                {
                    using (var file = File.Create(openVpnCredsFile))
                    {
                        using (StreamWriter sw = new StreamWriter(file))
                        {
                            var credsString = "vpn" + Environment.NewLine + "vpn";
                            sw.Write(credsString);
                           
                        }
                    }
                }
            }
            else
            {
                credFile = VPNBookCredsFile;
            }

            //up-restart
            //down-pre
            //down "p2pVpn-disconnect.cmd"


            string p2pVpnSettings =
            string.Format(_p2pVpnSettings,
                            Environment.NewLine, GetOpenVpnDirectory(), credFile, VPNBookDownFile);

            return p2pVpnSettings;
        }
        
    }
}
