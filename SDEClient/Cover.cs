using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using SDEClient.Properties;
using System.IO;

namespace SDEClient
{
    public partial class Cover : Form
    {
        public Cover()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.TopMost = true;
        }

        private void Cover_Load(object sender, EventArgs e)
        {
            Rectangle ScreenArea = System.Windows.Forms.Screen.GetWorkingArea(this);
            this.Left = ScreenArea.Width - 200;
            this.Top = 200;
            this.Height = 50;
            this.Width = 50;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImage = Resource1.界面;
            string Ver_local = Application.ProductVersion.ToString();
            this.Text = "服务平台增强工具_V" + Ver_local;
            string Path_local = System.Environment.CurrentDirectory;
            for (int i = 0; i < 2; i++)
            {
                if (File.Exists(Path_local + "\\update.exe"))
                {
                    try { File.Delete(Path_local + "\\update.exe"); }
                    catch
                    {

                    }
                }
                else { break; }
            }           
            try
            {
                Attachment.AttachmentTools call = new Attachment.AttachmentTools();
                string date = call.GetDateTime();
            }
            catch
            {
                MessageBox.Show(this, "服务器无法连接，请检查配置文件与网络是否匹配", "服务平台增强工具");
                System.Environment.Exit(0);
            }

            update(Ver_local);


        }
        private update updateForm = null;
        private void update(string Ver_local)
        {
            UPDATE.UPDATE call = new UPDATE.UPDATE();
            string Ver_remote = call.GetVersion(Path.GetFileName(Application.ExecutablePath));
            if (Ver_remote != Ver_local)
            {                
                Rectangle ScreenArea = System.Windows.Forms.Screen.GetWorkingArea(this);
                if (updateForm == null || updateForm.IsDisposed)
                {
                    updateForm = new update();
                }
                int updateFormtop = 0, updateFormleft = 0;
                updateForm.StartPosition = FormStartPosition.Manual;
                if (this.Left >= 542)
                {
                    updateFormleft = this.Left - 542;
                }
                else
                {
                    updateFormleft = this.Left + 50;
                }
                if (ScreenArea.Height - this.Top >= 149 || this.Top <= 149)
                {
                    updateFormtop = this.Top;
                }
                else
                {
                    updateFormtop = this.Top - 149 + 50;
                }
                updateForm.Location = new Point(updateFormleft, updateFormtop);
                updateForm.Show();
                updateForm.TopMost = true;
            }
        }

        private Point offset;
        private void Cover_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left != e.Button) return;
            Point cur = this.PointToScreen(e.Location);
            offset = new Point(cur.X - this.Left, cur.Y - this.Top);  
        }

        private void Cover_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left != e.Button) return;
            Point cur = MousePosition;
            this.Location = new Point(cur.X - offset.X, cur.Y - offset.Y);  
        }

        private void 打开主界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            打开主界面();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void 退出ToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            打开主界面();
        }
        private void Cover_DoubleClick(object sender, EventArgs e)
        {
            打开主界面();
        }

        private Form1 MainForm = null;
        public void 打开主界面()
        {
            Rectangle ScreenArea = System.Windows.Forms.Screen.GetWorkingArea(this);
            
            if (MainForm == null || MainForm.IsDisposed)
            {
                MainForm = new Form1();
            }
            else if (MainForm.WindowState == FormWindowState.Minimized) //判断当前窗体的状态是否为最小化
            {

                MainForm.WindowState = FormWindowState.Normal;//将当前窗体状态恢复为正常                   

            }

            int MainFormtop = 0, MainFormleft = 0;
            MainForm.StartPosition = FormStartPosition.Manual;
            if (this.Left >= 1041)
            {
                MainFormleft = this.Left - 1041;
            }
            else
            {
                MainFormleft = this.Left + 50;
            }
            if (ScreenArea.Height - this.Top >= 604 || this.Top <= 604)
            {
                MainFormtop = this.Top;
            }
            else
            {
                MainFormtop = this.Top - 604 + 50;
            }
            MainForm.Location = new Point(MainFormleft, MainFormtop);
            //MainForm.TopMost = true;
            MainForm.Show();
        }
        private void UpdateConfig(string key, string value)
        {
            // 利用Configuration  
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
