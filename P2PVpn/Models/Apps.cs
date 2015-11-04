using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using P2PVpn.Utilities;

namespace P2PVpn.Models
{
    public class Apps 
    {
        public string Program { get; set; }
        public bool Launch { get; set; }
        public bool Close { get; set; }
        private static List<Apps> _apps;

        public static void Save(List<Apps> apps)
        {
            apps.RemoveAll(x => string.IsNullOrWhiteSpace(x.Program));
            Settings.SaveJson(apps, "apps.json");

            _apps = apps;
        }
        public static List<Apps> Get()
        {
            if (_apps != null && _apps.Count() > 0)
            {
                return _apps;
            }

            _apps = Settings.GetJson<Apps>("apps.json");
            
            return _apps;
        }

        
    }
    
}
