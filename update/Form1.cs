using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace update
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
            

        private void button1_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory;
            string File_name = "服务平台增强工具";
            File.Delete(path + "\\" + File_name + ".exe");
            File.Move(path + "\\" + File_name + ".tmp", path + "\\" + File_name + ".exe");
            System.Diagnostics.Process.Start(path + "\\" + File_name + ".exe");
            Application.Exit();
        }
       
    }
}
