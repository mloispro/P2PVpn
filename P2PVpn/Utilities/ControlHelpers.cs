using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2PVpn.Utilities
{
    public static class ControlHelpers
    {
        public static void SetLabelText(this Label lbl, string text)
        {
            if (lbl.InvokeRequired)
            {
                lbl.Invoke(new Action(() => lbl.Text = text));
            }
            else
            {
                lbl.Text = text;
            }
        }
        public static void SetButtonText(this Button btn, string text)
        {
            if (btn.InvokeRequired)
            {
                btn.Invoke(new Action(() => btn.Text = text));
            }
            else
            {
                btn.Text = text;
            }
        }
        public static void EnableForm(this Form frm, bool enable = true)
        {
            try
            {
                if (frm.InvokeRequired)
                {
                    frm.Invoke(new Action(() => frm.UseWaitCursor = !enable));
                    frm.Invoke(new Action(() => frm.Enabled = enable));
                }
                else
                {
                    frm.UseWaitCursor = !enable;
                    frm.Enabled = enable;
                }
            }
            catch (ObjectDisposedException ex)
            {

            }
        }
        public static async Task Sleep(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }
        public static void StartProcess(string program, string args, bool waitForExit = true, int waitInSeconds = 10)
        {
            if (program.IsUrl())
            {
                Process.Start(program);
                return;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = program;
            startInfo.Arguments = args;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process processTemp = new Process();
            processTemp.StartInfo = startInfo;
            processTemp.EnableRaisingEvents = true;

            try
            {
                processTemp.Start();
                if (waitForExit)
                {
                    processTemp.WaitForExit(1000 * waitInSeconds);
                }
            }
            catch (Exception e)
            {
                Logging.Log(e.Message + ": " + program);
            }
        }
        public static bool IsUrl(this string url)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp
                              || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }
        public static void GridRowMoveUp(this BindingSource aBindingSource)
        {
            int position = aBindingSource.Position;
            if (position == 0) return;  // already at top

            aBindingSource.RaiseListChangedEvents = false;

            object current = aBindingSource.Current;
            aBindingSource.Remove(current);

            position--;

            aBindingSource.Insert(position, current);
            aBindingSource.Position = position;

            aBindingSource.RaiseListChangedEvents = true;
            aBindingSource.ResetBindings(false);
        }
        public static void GridRowMoveDown(this BindingSource aBindingSource)
        {
            int position = aBindingSource.Position;
            if (position == aBindingSource.Count - 1) return;  // already at bottom

            aBindingSource.RaiseListChangedEvents = false;

            object current = aBindingSource.Current;
            aBindingSource.Remove(current);

            position++;

            aBindingSource.Insert(position, current);
            aBindingSource.Position = position;

            aBindingSource.RaiseListChangedEvents = true;
            aBindingSource.ResetBindings(false);
        }
        private static void GridRowMoveUp(this DataGridView grid)
        {
            if (grid.CurrentRow == null) return;
            grid.Rows[grid.CurrentCell.RowIndex].Selected = true;

            if (grid.SelectedRows.Count > 0)
            {
                DataGridViewRow row = grid.SelectedRows[0];
                int selRowIndex = row.Index;

                if (selRowIndex == 0) { return; }

                grid.Rows.Remove(row);
                grid.Rows.Insert(selRowIndex - 1, row);
                grid.Rows[selRowIndex - 1].Selected = true;
                grid.CurrentCell = grid.Rows[selRowIndex - 1].Cells[0];
            }
        }
        private static void GridRowDownUp(this DataGridView grid)
        {
            if (grid.CurrentRow == null) return;
            grid.Rows[grid.CurrentCell.RowIndex].Selected = true;

            if (grid.SelectedRows.Count > 0)
            {
                DataGridViewRow row = grid.SelectedRows[0];
                int selRowIndex = row.Index;

                if ((selRowIndex + 1) == grid.Rows.Count - 1) { return; }

                grid.Rows.Remove(row);
                grid.Rows.Insert(selRowIndex + 1, row);
                grid.Rows[selRowIndex + 1].Selected = true;
                grid.CurrentCell = grid.Rows[selRowIndex + 1].Cells[0];
            }
        }

        public static void MoveRightOf(this Control ctr, Control leftCtr)
        {
            int ctrX = leftCtr.Bounds.Right + ctr.Margin.Left;
            ctr.SetBounds(ctrX, ctr.Bounds.Y, ctr.Bounds.Width, ctr.Bounds.Height);
        }

        public enum MessageBoxType
        {
            Error,
            Info,
            Warning
        }
        public static DialogResult ShowMessageBox(string message, MessageBoxType type = MessageBoxType.Info, bool log = true)
        {
            string caption = "";
            if (type != MessageBoxType.Info)
            {
                caption = type.ToString();
            }
            if (log)
            {
                Logging.Log(message);
            }
            var result = MessageBox.Show(message, caption, MessageBoxButtons.OK);
            return result;
        }
        public static void ShowMessageBoxYesNo(string message, string yesProcess, MessageBoxType type = MessageBoxType.Info)
        {
            string caption = "";
            if (type != MessageBoxType.Info)
            {
                caption = type.ToString();
            }
            DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ControlHelpers.StartProcess(yesProcess, "");
            }
            //if (log)
            //{
            //    Logging.Log(message);
            //}
        }
        public static void EnableTab(this TabPage page, bool enable)
        {
            foreach (Control ctl in page.Controls) ctl.Enabled = enable;
        }
        private static ToolStripStatusLabel _lblStatusColor;
        private static StatusStrip _statusBar;
        public static void SetStatusBarLabel(this StatusStrip statusBar, ToolStripStatusLabel label, string text, Bitmap image=null)
        {

            try
            {

                if (statusBar.InvokeRequired)
                {
                    statusBar.Invoke(new Action(() => label.Text = text));
                    if (image != null)
                    {
                        statusBar.Invoke(new Action(() => label.Image = image));
                    }
                }
                else
                {
                    label.Text = text;
                    if (image != null)
                    {
                        label.Image = image;
                    }

                }


            }
            catch (ObjectDisposedException ex)
            {

            }
        }
    }
}
