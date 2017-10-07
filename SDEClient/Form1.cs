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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Width = 1041;
            this.Height = 610;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            //this.ShowInTaskbar = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {        
            splitContainer.Top = 0;
            splitContainer.Left = 0;
            splitContainer.Dock = DockStyle.Fill;
            treeView_Menu.ExpandAll();
            string Ver_local = Application.ProductVersion.ToString();
            this.Text = "服务平台增强工具_V" + Ver_local;
        }        
        private void treeView_Menu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            splitContainer.Panel2.Controls.Clear();
            string Function = treeView_Menu.SelectedNode.Text;
            switch (Function)
            {
                case "事件处理-L1坐席":
                    RemindTools form = new RemindTools();
                    form.FormBorderStyle = FormBorderStyle.None;
                    //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                    form.TopLevel = false;
                    //指示子窗体非顶级窗体
                    this.splitContainer.Panel2.Controls.Add(form);
                    form.Width = this.splitContainer.Panel2.Width;
                    form.Height = this.splitContainer.Panel2.Height;//将子窗体载入panel
                    form.Show();
                    break;
                case "平台附件工具":
                    AttTools form2 = new AttTools();
                    form2.FormBorderStyle = FormBorderStyle.None;
                    //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                    form2.TopLevel = false;
                    //指示子窗体非顶级窗体
                    this.splitContainer.Panel2.Controls.Add(form2);
                    form2.Width = this.splitContainer.Panel2.Width;
                    form2.Height = this.splitContainer.Panel2.Height;//将子窗体载入panel
                    form2.Show();
                    break;
                case "测试医院分区划片":
                    UnitCustmer form3 = new UnitCustmer();
                    form3.FormBorderStyle = FormBorderStyle.None;
                    //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                    form3.TopLevel = false;
                    //指示子窗体非顶级窗体
                    this.splitContainer.Panel2.Controls.Add(form3);
                    form3.Width = this.splitContainer.Panel2.Width;
                    form3.Height = this.splitContainer.Panel2.Height;//将子窗体载入panel
                    form3.Show();
                    break;
                case "工作量实时统计":
                    Workload form4 = new Workload();
                    form4.FormBorderStyle = FormBorderStyle.None;
                    //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                    form4.TopLevel = false;
                    //指示子窗体非顶级窗体
                    this.splitContainer.Panel2.Controls.Add(form4);
                    form4.Width = this.splitContainer.Panel2.Width;
                    form4.Height = this.splitContainer.Panel2.Height;//将子窗体载入panel
                    form4.Show();
                    break;
            }

            //if (treeView_Menu.SelectedNode.Text == "事件处理-L1坐席")
            //{
            //    RemindTools form = new RemindTools();
            //    form.FormBorderStyle = FormBorderStyle.None;
            //    //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            //    form.TopLevel = false;
            //    //指示子窗体非顶级窗体
            //    this.splitContainer.Panel2.Controls.Add(form);
            //    form.Width = this.splitContainer.Panel2.Width;
            //    form.Height = this.splitContainer.Panel2.Height;//将子窗体载入panel
            //    form.Show();

            //}
            //else
            //{
            //    AttTools form = new AttTools();
            //    form.FormBorderStyle = FormBorderStyle.None;
            //    //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            //    form.TopLevel = false;
            //    //指示子窗体非顶级窗体
            //    this.splitContainer.Panel2.Controls.Add(form);       //将子窗体载入panel
            //    form.Width = this.splitContainer.Panel2.Width;
            //    form.Height = this.splitContainer.Panel2.Height;
            //    form.Show();
            //}            
            
        }


                
    }
}
