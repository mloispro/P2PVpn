using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PVpn.Utilities
{
    public static class OpenVPN
    {
        private static string _p2pVpnSettings = string.Format("#P2PVpn Settings#{0}" + 
                                                                "auth-user-pass \"C:\\Program Files (x86)\\OpenVPN\\config\\vpnbook-creds.txt\"{0}" +
                                                                "plugin \"C:\\Program Files (x86)\\OpenVPN\\config\\fix-dns-leak-32.dll\"{0}", 
                                                                Environment.NewLine);

        public static void SavePassword(string password)
        {
            Settings settings = Settings.Get();
            var binDir = Settings.AppDir;
            var localCredsFile = binDir + @"\vpnbook-creds.txt";
            var openVpnCredsFile = settings.OpenVPNDirectory + @"\config\vpnbook-creds.txt";

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
            var binDir = Settings.AppDir;
            var localCredsFile = binDir + @"\vpnbook-creds.txt";
            var openVpnCredsFile = settings.OpenVPNDirectory + @"\config\vpnbook-creds.txt";

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
        
    }
}
