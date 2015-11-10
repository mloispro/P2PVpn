using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PVpn.Models
{
    public class NetworkAdapterDns
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PrimaryDNS { get; set; }
        public string SecondaryDNS { get; set; }
    }
}
