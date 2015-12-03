using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace P2PVpn.Utilities
{
    public class MediaServer
    {
        private static string _scriptPath = Path.GetFullPath(Settings.AppDir + @"\Assets\RetartMediaServer.ps1");
        private static string _offlinePostfix = "_p2pvOffline";

        public static bool IsShareOffline(Models.MediaServer mediaServer)
        {
            if (!Networking.IsLocalNetworkConnected()) return true;
            if (!LoginToMediaShare(mediaServer)) return true;
            if (!Directory.Exists(mediaServer.ShareName + _offlinePostfix) && !Directory.Exists(mediaServer.ShareName))
            {
                throw new Exception("Media Share Not Found: " + mediaServer.ShareName);
            }
            return Directory.Exists(mediaServer.ShareName + _offlinePostfix);
           // return shareName.EndsWith(_offlinePostfix);
        }
        public static bool TakeShareOffline(bool force=false)
        {
            Models.MediaServer mediaServer = Settings.Get().MediaServer;
            if (string.IsNullOrWhiteSpace(mediaServer.ShareName))
            {
                return false;
            }

            //int count = 0;
            //while (!Networking.IsLocalNetworkConnected())
            //{
            //    if (count == 5) break;
            //    ControlHelpers.Sleep(1000).Wait();
            //    count++;
            //}

            if (force && !IsShareOffline(mediaServer))
            {
                
                FileIO.ChangeFolderName(mediaServer.ShareName, mediaServer.ShareName + _offlinePostfix);
                RestartMediaServer(); 
                Logging.Log("Media Share Offline");
                return true;
            }
            else if (IsShareOffline(mediaServer))
            {
                FileIO.ChangeFolderName(mediaServer.ShareName + _offlinePostfix, mediaServer.ShareName);
                RestartMediaServer();
                Logging.Log("Media Share Online");
                mediaServer.ParentalControlsLastEnabled = DateTime.Now;
                return false;
            }
            else
            {
                FileIO.ChangeFolderName(mediaServer.ShareName, mediaServer.ShareName + _offlinePostfix);
                RestartMediaServer();
                Logging.Log("Media Share Offline");
                return true;
            }
            
        }
        private static void RestartMediaServer()
        {

            // create Powershell runspace
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();

            RunspaceInvoke runSpaceInvoker = new RunspaceInvoke(runspace);
            runSpaceInvoker.Invoke("Set-ExecutionPolicy Unrestricted");

            // create a pipeline and feed it the script text
            Pipeline pipeline = runspace.CreatePipeline();
            Command command = new Command(_scriptPath);
            //foreach (var file in filesToMerge)
            //{
            //    command.Parameters.Add(null, file);
            //}
            //command.Parameters.Add(null, outputFilename);
            pipeline.Commands.Add(command);

            pipeline.Invoke();
            runspace.Close();
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
