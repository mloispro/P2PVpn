using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2PVpn.Utilities
{
    public static class Logging
    {
        //System.IO.Log.
        private static ListBox _log;
        private static ToolStripStatusLabel _lblStatusText;
        private static StatusStrip _statusBar;

        public static void Init(ListBox log, StatusStrip statusBar, ToolStripStatusLabel lblStatusText)
        {
            _log = log;
            _lblStatusText = lblStatusText;
            _statusBar = statusBar;
        }

        public static void Log(this ListBox log, string text)
        {
            try
            {
                _log = log;
                text = DateTime.Now + " - " + text;
                if (log.InvokeRequired)
                {
                    log.BeginInvoke(new Action(() => log.Items.Add(text)));
                }
                else
                {
                    log.Items.Add(text);
                }
            }
            catch (ObjectDisposedException ex)
            {

            }
        }
        public static void Log(this ListBox log, string text, params object[] args)
        {
            _log = log;
            log.Log(string.Format(text, args));
        }
        public static void Log(string text)
        {
            _log.Log(text);
        }
        public static void Log(string text, params object[] args)
        {
            Log(string.Format(text, args));
        }
        public enum Colors
        {
            Red,
            Yellow,
            Green
        }
        public static void SetStatus(string text, Colors color)
        {
            
            try
            {
                Bitmap theColor = new Bitmap(P2PVpn.Properties.Resources.Stop_red2);
                if (color == Colors.Green)
                {
                    theColor = P2PVpn.Properties.Resources.Accept2;
                }
                else if (color == Colors.Red)
                {
                    theColor = P2PVpn.Properties.Resources.Stop_red2;
                }
                else if (color == Colors.Yellow)
                {
                    theColor = P2PVpn.Properties.Resources.Prohibit2;
                }
                if (_statusBar.InvokeRequired)
                {
                    _statusBar.Invoke(new Action(() => _lblStatusText.Text = text));
                    _statusBar.Invoke(new Action(() => _lblStatusText.Image = theColor));
                    
                }
                else
                {
                    _lblStatusText.Text = text;
                    _lblStatusText.Image = theColor;
                }
             
                
            }
            catch (ObjectDisposedException ex)
            {

            }
        }
    }
}
