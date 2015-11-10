using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using P2PVpn.Models;

namespace P2PVpn.Utilities
{
    public class Settings
    {
        public const string DefaultOpenVPNDirectory = @"C:\Program Files (x86)\OpenVPN";
        public const string DefaultVPNBookConfigDownload = @"http://www.vpnbook.com/free-openvpn-account/VPNBook.com-OpenVPN-Euro1.zip";
        public const string DefaultVPNBookCredsPage = @"http://www.vpnbook.com/freevpn";
        public const string DefaultGoogleDNSPrimary = @"8.8.8.8";
        public const string DefaultGoogleDNSSecondary = @"8.8.4.4";
        public const string DefaultOpenDNSPrimary = @"208.67.222.222";
        public const string DefaultOpenDNSSecondary = @"208.67.220.220";
        public const string DefaultComodoDNSPrimary = @"8.26.56.26";
        public const string DefaultComodoDNSSecondary = @"8.20.247.20";
        public const string BlockedVpnBookProxyText = @"block.opendns";
        public const string BrowserProxy = "127.0.0.1";
        public const string ChromeExe = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
        public const string ChromeWebRTCExtensionUrl = @"https://chrome.google.com/webstore/detail/webrtc-leak-prevent/eiadekoaikejlgdbkbdfeijglgfdalml?hl=en";
        public static string UserSettingsDir = Application.UserAppDataPath;
        public static string AppDir = Application.StartupPath;

        //[DisplayName("OpenVPN Directory")]
        public string OpenVPNDirectory { get; set; }
        public string OpenVPNConfig { get; set; }
        public string VPNBookUsername { get; set; }
        public string VPNBookPassword { get; set; }
        public string PrimaryDNS { get; set; }
        public string SecondaryDNS { get; set; }
        public bool DontResetDNS { get; set; }
        public List<NetworkAdapterDns> StartupNetworkAdapterDns { get; set; }
        public Dictionary<string, string> OpenVPNConfigs { get; set; }
        public bool EnableTorProxyForChrome { get; set; }


        private static Settings _settings;
        

        public static Settings Get()
        {
            // Create a file that the application will store user specific data in.
            if (_settings != null)
            {
                FillOPNVPNConfigs();
                return _settings;
            }

            _settings = Settings.GetJsonObject<Settings>("settings.json");
            if (string.IsNullOrWhiteSpace(_settings.OpenVPNDirectory))
            {
                _settings.OpenVPNDirectory = DefaultOpenVPNDirectory;
            }
            return _settings;
        }
        private static void FillOPNVPNConfigs()
        {
            string configDir = _settings.OpenVPNDirectory + @"\config";
            _settings.OpenVPNConfigs = new Dictionary<string, string>();

            var ext = new List<string> { ".ovpn" };
            var configFiles = Directory.GetFiles(configDir, "*.*", SearchOption.AllDirectories)
                 .Where(s => ext.Any(e => s.EndsWith(e)));

            foreach (var file in configFiles)
            {
                var fileName = Path.GetFileName(file);
                _settings.OpenVPNConfigs.Add(fileName, file);
            }
        }
        public static void Save(Settings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.OpenVPNDirectory))
            {
                settings.OpenVPNDirectory = DefaultOpenVPNDirectory;
            }
            Settings.SaveJsonObject(settings, "settings.json");
            Logging.Log("Saved Settings To: " + UserSettingsDir);
            _settings = settings;
        }

        public static void SaveJson<T>(List<T> list, string filename)
        {
            //var binDir = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var binDir = UserSettingsDir;
            var file = binDir + @"\" + filename;

            if (!File.Exists(file))
            {
                File.Create(file).Close();
            }
            //clear file
            File.WriteAllText(file, String.Empty);

            var json = JsonConvert.SerializeObject(list);
            File.WriteAllText(file, json);

        }
        public static List<T> GetJson<T>(string filename)
        {
            var binDir = UserSettingsDir;
            var file = binDir + @"\" + filename;

            List<T> list = new List<T>();
            if (!File.Exists(file))
            {
                File.Create(file).Close();
            }
            else
            {
                if (new FileInfo(file).Length == 0)
                {
                    return list;
                }
            }

            list = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(file));
            if (list == null) list = new List<T>();
            return list;
        }
        public static T GetJsonObject<T>(string filename) where T : new()
        {
            var binDir = UserSettingsDir;
            var file = binDir + @"\" + filename;

            T obj = new T();
            if (!File.Exists(file))
            {
                File.Create(file).Close();
            }
            else
            {
                if (new FileInfo(file).Length == 0)
                {
                    return obj;
                }
            }

            obj = JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
            if (obj == null) obj = new T();
            return obj;
        }
        public static void SaveJsonObject<T>(T obj, string filename)
        {
            //var binDir = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var binDir = UserSettingsDir;
            var file = binDir + @"\" + filename;

            if (!File.Exists(file))
            {
                File.Create(file).Close();
            }
            //clear file
            File.WriteAllText(file, String.Empty);

            var json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(file, json);

        }
    }
}
