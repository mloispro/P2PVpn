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

        private static bool IsShareOffline(Models.MediaServer mediaServer)
        {
            LoginToMediaShare(mediaServer);
            return Directory.Exists(mediaServer.ShareName + _offlinePostfix);
           // return shareName.EndsWith(_offlinePostfix);
        }
        public static bool TakeShareOffline(Models.MediaServer mediaServer)
        {

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
        public static void LoginToMediaShare(Models.MediaServer mediaServer)
        {
            Networking.LoginToUNCShare(mediaServer.Username, mediaServer.Password, mediaServer.Domain, mediaServer.ShareName);
        }
    }
}
