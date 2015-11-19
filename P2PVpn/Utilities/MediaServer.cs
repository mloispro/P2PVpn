using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PVpn.Utilities
{
    public class MediaServer
    {
        private static string _offlinePostfix = "_p2pvOffline";

        public static bool IsShareOffline(Models.MediaServer mediaServer)
        {
            if (!LoginToMediaShare(mediaServer)) return true;
            return Directory.Exists(mediaServer.ShareName + _offlinePostfix);
           // return shareName.EndsWith(_offlinePostfix);
        }
        public static bool TakeShareOffline(Models.MediaServer mediaServer)
        {
            if (string.IsNullOrWhiteSpace(mediaServer.ShareName))
            {
                return false;
            }

            if (IsShareOffline(mediaServer))
            {
                FileIO.ChangeFolderName(mediaServer.ShareName + _offlinePostfix, mediaServer.ShareName);
                return false;
            }
            else
            {
                FileIO.ChangeFolderName(mediaServer.ShareName, mediaServer.ShareName + _offlinePostfix);
                return true;
            }
        }
        public static bool LoginToMediaShare(Models.MediaServer mediaServer)
        {
            if (!string.IsNullOrWhiteSpace(mediaServer.ShareName) && !mediaServer.ShareName.StartsWith("@\\"))
            {
                return true;
            }
            if (string.IsNullOrWhiteSpace(mediaServer.Username) || string.IsNullOrWhiteSpace(mediaServer.Password) ||
                string.IsNullOrWhiteSpace(mediaServer.Domain) || string.IsNullOrWhiteSpace(mediaServer.ShareName))
            {
                return false;
            }
            Networking.LoginToUNCShare(mediaServer.Username, mediaServer.Password, mediaServer.Domain, mediaServer.ShareName);
            return true;
        }
    }
}
