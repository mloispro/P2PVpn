using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using P2PVpn.Utilities;

namespace P2PVpn
{
    static class Program
    {
        //static Mutex mutex = new Mutex(false, "E8BC201C-0C55-4768-999B-6E5D2B40FE00");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new P2PVPNForm());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string[] args = Environment.GetCommandLineArgs();
            SingleInstanceController controller = new SingleInstanceController();
            controller.Run(args);
        }
        
    }
    public class SingleInstanceController : WindowsFormsApplicationBase
    {
        public SingleInstanceController()
        {
            IsSingleInstance = true;

            StartupNextInstance += this_StartupNextInstance;
        }

        void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            P2PVPNForm form = MainForm as P2PVPNForm; //My derived form type
            //form.LoadFile(e.CommandLine[1]);
            if (e.CommandLine.Count() == 2)
            {
                string sourceDir = e.CommandLine[1];
                form.bwTorrentDownloadComplete.RunWorkerAsync(sourceDir);
            }
            e.BringToForeground = true;
           
        }

        protected override void OnCreateMainForm()
        {
            MainForm = new P2PVPNForm();
        }
    }

    public partial class P2PVPNForm : Form
    {

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string[] args = Environment.GetCommandLineArgs();

            //LoadFile(args[1]);
        }
     
    }
}
