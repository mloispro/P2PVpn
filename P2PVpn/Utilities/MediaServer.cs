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
            if (!Directory.Exists(mediaServer.ShareName + _offlinePostfix) && !Directory.Exists(mediaServer.ShareName))
            {
                throw new Exception("Media Share Not Found: " + mediaServer.ShareName);
            }
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
                Logging.Log("Media Share Online");
                mediaServer.ParentalControlsLastEnabled = DateTime.Now;
                return false;
            }
            else
            {
                FileIO.ChangeFolderName(mediaServer.ShareName, mediaServer.ShareName + _offlinePostfix);
                Logging.Log("Media Share Offline");
                return true;
            }
        }
        public static bool LoginToMediaShare(Models.MediaServer mediaServer)
        {
            if (!string.IsNullOrWhiteSpace(mediaServer.ShareName) && !mediaServer.ShareName.StartsWith("\\\\"))
            {
                //Networking.LogoutOfUNCShare(mediaServer.Username, mediaServer.Password, mediaServer.Domain, mediaServer.ShareName);
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
        public static string GetSelectedOfflineValue(Models.MediaServer mediaServer)
        {
            if (mediaServer.EnableParentalControlsEvery == 0) return "";
            string offlineVal = "";
            if (mediaServer.EnableParentalControlsEvery == 1)
            {
                offlineVal = mediaServer.EnableParentalControlsEvery.ToString() + " hour";
            }
            else
            {
                offlineVal = mediaServer.EnableParentalControlsEvery.ToString() + " hours";
            }


            return offlineVal;
        }
    }
}
