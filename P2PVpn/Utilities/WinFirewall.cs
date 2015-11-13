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
    public class WinFirewall
    {
        private static string _scriptPath = Path.GetFullPath(Settings.AppDir + @"\Assets\CreateFirewallRules.ps1");
        public static void CreateFirewallRules()
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
    }
}
