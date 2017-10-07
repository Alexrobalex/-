using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SDEClient
{
    public partial class update : Form
    {
        public update()
        {
            InitializeComponent();
            this.Width = 542;
            this.Height = 149;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.ShowInTaskbar = false;
        }
        private void update_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UPDATE.UPDATE call = new UPDATE.UPDATE();

            byte[] bufferBytes;
            string Path_local = System.Environment.CurrentDirectory;
            string main_name = "服务平台增强工具";
            string File_tmp = Path_local + "\\" + main_name + ".tmp";
            string Update = "Update.exe";

            progressBar.Value = 0;
            progressBar.Minimum = 0;
            progressBar.Maximum = 1000;
            timer.Enabled  = true;

            int FileLong = int.Parse(call.GetFileInfo(main_name + ".exe"));
            if (File.Exists(File_tmp))
            {
                try { File.Delete(File_tmp); }
                catch
                {
                    MessageBox.Show(this, "【" + File_tmp + "】文件已存在，且无法删除", "SDETools");
                    return;
                }
            }
            if (File.Exists(Update))
            {
                try { File.Delete(Update); }
                catch
                {
                    MessageBox.Show(this, "【" + Update + "】文件已存在，且无法删除", "SDETools");
                    return;
                }
            }


            int byteslong = 20 * 1024 * 1024;
            long countOfPk = FileLong / Convert.ToInt64(byteslong);
            countOfPk += FileLong % byteslong == 0 ? 0 : 1;


            FileStream fsWrite = new FileStream(File_tmp, FileMode.Append);
            for (int ii = 0; ii < countOfPk; ii++)
            {
                int bufferlength;
                if (ii == countOfPk - 1)
                {
                    bufferlength = FileLong % byteslong;
                }
                else
                {
                    bufferlength = byteslong;
                }
                bufferBytes = call.DownFile(main_name + ".exe", ii * byteslong, bufferlength);

                fsWrite.Write(bufferBytes, 0, bufferBytes.Length);

                Application.DoEvents();
            }
            fsWrite.Close();
            FileStream updateWrite = new FileStream(Update, FileMode.Create);
            bufferBytes = call.DownFile(Update, 0, int.Parse (call.GetFileInfo(Update)));
            updateWrite.Write(bufferBytes, 0, bufferBytes.Length);
            updateWrite.Close();
            System.Diagnostics.Process.Start(Path_local + "\\" + "update.exe");
            Application.Exit();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value < progressBar.Maximum)
            {
                progressBar.Value++;
            }
            else
            {
                progressBar.Value = 0;
            }
        }

        
        
    }
}
