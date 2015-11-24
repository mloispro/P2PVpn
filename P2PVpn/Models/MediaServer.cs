using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PVpn.Models
{
    public class MediaServer
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ShareName { get; set; }
        public string Domain { get; set; }
        public int EnableParentalControlsEvery { get; set; }
        public DateTime ParentalControlsLastEnabled { get; set; }
        //public string Username { get; set; }
    }
}
