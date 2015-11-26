using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PVpn.Models
{
    public class FileTransfer
    {
        public string SourceDirectory { get; set; }
        public string TargetDirectory { get; set; }
        public bool IsTransfering { get; set; }
    }
}
