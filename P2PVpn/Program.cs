using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2PVpn
{
    static class Program
    {
        static Mutex mutex = new Mutex(false, "E8BC201C-0C55-4768-999B-6E5D2B40FE00");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new P2PVPNForm());

            // if you like to wait a few seconds in case that the instance is just 
            // shutting down
            if (!mutex.WaitOne(TimeSpan.FromSeconds(2), false) && !System.Diagnostics.Debugger.IsAttached)
            {
              MessageBox.Show("P2PVpn Monitor is already running", "", MessageBoxButtons.OK);
               return 0;
            }

            try
            {

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new P2PVPNForm());

            }
            catch { }
            finally
            {
                try
                {
                    mutex.ReleaseMutex();
                }
                catch { }
            }
            return 0;
        }
    }
}
