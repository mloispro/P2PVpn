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
        private static ToolStripStatusLabel _lblStatusColor;
        private static StatusStrip _statusBar;

        public static void Init(ListBox log, StatusStrip statusBar, ToolStripStatusLabel lblStatusText, ToolStripStatusLabel lblStatusColor)
        {
            _log = log;
            _lblStatusColor = lblStatusColor;
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
                    log.Invoke(new Action(() => log.Items.Add(text)));
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
                Color theColor = new Color();
                if (color == Colors.Green)
                {
                    theColor = Color.Green;
                }
                else if (color == Colors.Red)
                {
                    theColor = Color.Red;
                }
                else if (color == Colors.Yellow)
                {
                    theColor = Color.Yellow;
                }
                if (_statusBar.InvokeRequired)
                {
                    _statusBar.Invoke(new Action(() => _lblStatusText.Text = text));
                    _statusBar.Invoke(new Action(() => _lblStatusColor.BackColor = theColor));
                }
                else
                {
                    _lblStatusText.Text = text;
                    _lblStatusColor.BackColor = theColor;
                }
             
                
            }
            catch (ObjectDisposedException ex)
            {

            }
        }
    }
}
