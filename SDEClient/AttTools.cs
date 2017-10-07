using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SDEClient.Properties;


namespace SDEClient
{
    public partial class AttTools : Form
    {
        string Dir = string.Empty;
        public AttTools()
        {
            InitializeComponent();
        }
        private void AttTools_Load(object sender, EventArgs e)
        { 
            
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text = "服务平台附件工具_V" + version;
            Dir = Settings.Default.Dir;
            if (Dir == "")
            {
                Dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }
            try
            {
                Attachment.AttachmentTools call = new Attachment.AttachmentTools();
                dateTimePicker2.Text = call.GetDateTime();
                dateTimePicker1.Text = DateTime.Parse(dateTimePicker2.Text).AddDays(-7).ToShortDateString();
                附件数量.Text = call.GetAttachmentNum();
                dataGridView1.SelectionMode = FullRowSelect;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                button3.Visible = false;
            }
            catch
            {
                MessageBox .Show (this,"服务器无法连接，请检查配置文件与网络是否匹配","服务平台附件工具");
                this.Close();
            }
            
            //dateTimePicker2.Text = DynamicWebService("GetDateTime", null).ToString();
                       

            
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value >= dateTimePicker1.Value)
            {
                index();                
            }
            else
            {
                MessageBox.Show(this,"查询条件有误，请重新输入", "服务平台附件工具");
            }
            
        }

        private void index()
        {
            Attachment.AttachmentTools call = new Attachment.AttachmentTools();
            DataSet DS = call.Index(textBox1.Text, dateTimePicker1.Value.ToShortDateString()+ " 00:00:00" , dateTimePicker2.Value.ToShortDateString() + " 23:59:59");
            /*
            string[] args = new string[3];
            args[0] = textBox1.Text;
            args[1] = dateTimePicker1.Value.ToShortDateString() + " 00:00:00";
            args[2] = dateTimePicker2.Value.ToShortDateString() + " 23:59:59";

            DataSet DS = DynamicWebService("Index", args) as DataSet;
            */
            
            CreateGridColumn(dataGridView1, DS.Tables[0]);
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = DS.Tables[0];            
            //dataGridView1.Columns["附件ID"].Visible = false;
            dataGridView1.Columns["故障单号"].Visible = false;

            dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.AllowUserToResizeColumns = true;
            this.dataGridView1.Columns[0].Resizable = DataGridViewTriState.True;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.Columns[1].Frozen = true;

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView1.Columns[0].Width = 30;  //序号
            //this.dataGridView1.Columns[1].Width = 100;  //故障单号
            this.dataGridView1.Columns[2].Width = 220;    //附件名称
            this.dataGridView1.Columns[3].Width = 90;     //附件描述
            this.dataGridView1.Columns[4].Width = 120;      //上传时间
            


            if (textBox1.Text!="")
            {
                string str = call.GetREQUESTS(textBox1.Text);
                string[] sArray = str.Split('|');
                textBox6.Text = sArray[0];
                textBox3.Text = sArray[1];
                textBox8.Text = sArray[2];
                textBox4.Text = sArray[3];
                textBox5.Text = sArray[4];
                textBox9.Text = sArray[5];   
            }
        }

        private void CreateGridColumn(DataGridView dgv, DataTable dt)
        {
            dgv.ReadOnly = true;
            dgv.Columns.Clear();
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AutoGenerateColumns = false;
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
            dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //使用dt的字段名，创建表格字段名            
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                AddColumnToGrid(dgv, dt.Columns[i].ColumnName, dt.Columns[i].ColumnName, true);
            }
        }
        private static void AddColumnToGrid(DataGridView dgv, string sFieldName, string sCaption, bool bShow)
        {
            DataGridViewColumn dgvc = new DataGridViewTextBoxColumn();
            dgvc.HeaderText = sCaption;
            dgvc.Name = sFieldName;
            dgvc.DataPropertyName = sFieldName;
            dgvc.Visible = bShow;
            dgv.Columns.Add(dgvc);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool flag;
            string uploadPath = string.Empty;//上传服务器文件夹路径
            string path = textBox2.Text;//本地路径
            string Attachment_Name = System.IO.Path.GetFileName(textBox2.Text);
            string Attachment_Name_new = string.Empty;
            string Attachment_Type = System.IO.Path.GetExtension(textBox2.Text);
            string Attachment_Desc = textBox7 .Text;
            string REQCODE = string.Empty;
            if (textBox2.Text=="")
            {
                MessageBox.Show(this, "请确定需要上传的文件", "服务平台附件工具");
                return;
            }
            if (textBox1.Text != "")
            {
                REQCODE = textBox1.Text;
            }
            else if (textBox9.Text != "")
            {
                REQCODE = textBox9.Text;
            }
            else
            {
                MessageBox.Show(this, "请确定上传附件的故障单号", "服务平台附件工具");
                return;
            }
            try
            {                
                Attachment.AttachmentTools call = new Attachment.AttachmentTools();
                //object[] args = new object[8];                
                long fileLength = FileTrans.LengthOfFile(path);
                long countOfPk = fileLength / Convert.ToInt64(20*1024*1024);
                countOfPk += fileLength % (20 * 1024 * 1024) == 0 ? 0 : 1;
                progressBar.Minimum = 0;
                progressBar.Maximum = Convert.ToInt32(countOfPk + 2);

                bool ifEnd = false;
                progressBar.Value = 0;
                long endfPk = countOfPk - 1;
                for (long ii = 0; ii < countOfPk + 2; ii++)
                {
                    if (ii == countOfPk )
                        ifEnd = true;
                    progressBar.Value = Convert.ToInt32(ii + 1);
                    string rst;
                    if (ii == 0)
                    { 
                        //创建文件
                        Attachment_Name_new = call.Save(REQCODE, FileTrans.ConvertToBinary(ii, ifEnd, fileLength, path), uploadPath, Attachment_Name, Attachment_Name_new, Attachment_Type, Attachment_Desc, "0");
                        /*
                        args[0] = REQCODE;
                        args[1] = FileTrans.ConvertToBinary(ii, ifEnd, fileLength, path);
                        args[2] = uploadPath;
                        args[3] = Attachment_Name;
                        args[4] = Attachment_Name_new;
                        args[5] = Attachment_Type;
                        args[6] = Attachment_Desc;
                        args[7] = "0";
                        Attachment_Name_new = DynamicWebService("Save", args).ToString(); 
                         */
                        rst = Attachment_Name_new;
                    }
                    else if (ii == countOfPk + 1)
                    {
                        //关闭文件
                        rst=call.Save(REQCODE, FileTrans.ConvertToBinary(ii-2, ifEnd, fileLength, path), uploadPath, Attachment_Name, Attachment_Name_new, Attachment_Type, Attachment_Desc, "9");
                        /*
                        args[0] = REQCODE;
                        args[1] = FileTrans.ConvertToBinary(ii - 2, ifEnd, fileLength, path);
                        args[2] = uploadPath;
                        args[3] = Attachment_Name;
                        args[4] = Attachment_Name_new;
                        args[5] = Attachment_Type;
                        args[6] = Attachment_Desc;
                        args[7] = "9";
                        rst = DynamicWebService("Save", args).ToString(); 
                         */
                    }
                    else
                    { 
                        //中间过程
                        rst=call.Save(REQCODE, FileTrans.ConvertToBinary(ii-1, ifEnd, fileLength, path), uploadPath, Attachment_Name, Attachment_Name_new, Attachment_Type, Attachment_Desc, "1");
                        /*
                        args[0] = REQCODE;
                        args[1] = FileTrans.ConvertToBinary(ii - 1, ifEnd, fileLength, path);
                        args[2] = uploadPath;
                        args[3] = Attachment_Name;
                        args[4] = Attachment_Name_new;
                        args[5] = Attachment_Type;
                        args[6] = Attachment_Desc;
                        args[7] = "1";
                        rst = DynamicWebService("Save", args).ToString();  
                         */
                    }
                    Application.DoEvents();
                    if (rst == "-1")
                    {
                        MessageBox.Show(this,"文件上传过程失败",  "服务平台附件工具");
                        return;
                    }
                    

                }
                flag = true;

                //if (call.Save(REQCODE, bytes, uploadPath, Attachment_Name, Attachment_Name_new, Attachment_Type, Attachment_Desc)) { flag = true; }
                
                //else { flag = false; }
            }
            catch
            {
                flag = false;
            }
            if (flag == true)
            {
                MessageBox.Show(this, "上传成功", "服务平台附件工具");
                index();
            }
            else
            {
                MessageBox.Show(this, "上传结果失败", "服务平台附件工具");
            }
        }

        ///
        /// 根据文件的路径获取图片的byte[]
        ///
        ///
        ///
        public static byte[] GetBytesByPath(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] bytes = br.ReadBytes((int)fs.Length);
            fs.Flush();
            fs.Close();
            return bytes;
        }      

        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            
            
            OpenFileDialog openFileDialog=new OpenFileDialog();
            openFileDialog.InitialDirectory = Dir;  //默认桌面
            //openFileDialog.Filter="文本文件|*.*|C#文件|*.cs|所有文件|*.*";
            openFileDialog.RestoreDirectory=true;
            openFileDialog.FilterIndex=1;
            if (openFileDialog.ShowDialog()==DialogResult.OK)
            {
                textBox2.Text=openFileDialog.FileName;
                //WrtRegedit(textBox2.Text);
            }
        }

        private void WrtRegedit(string Dir)
        {
            RegistryKey key = Registry.LocalMachine;
            //在HKEY_LOCAL_MACHINE\SOFTWARE下新建名为VangoCalibration的注册表项。如果已经存在则不影响！  
            RegistryKey software = key.CreateSubKey("software\\AttachmentTools");
            software.SetValue("Dir", Dir.ToString(),RegistryValueKind.String );            
            key.Close();  
        }

        private String RedRegedit()
        {
            RegistryKey key = Registry.LocalMachine;
            //在HKEY_LOCAL_MACHINE\SOFTWARE下新建名为VangoCalibration的注册表项。如果已经存在则不影响！  
            RegistryKey software = key.CreateSubKey("software\\AttachmentTools");
            string Dir = software.GetValue("Dir", "").ToString();
            key.Close();
            return Dir;
        }

        #region 获取另一系统文本框值
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "FindEx")]
        public static extern IntPtr FindEx(IntPtr hwnd, IntPtr hwndChild, string lpClassName, string lpWindowName);

        //[DllImport("user32.dll", EntryPoint = "GetWindowText")]
        //public static extern int GetWindowText(int hwnd, string lpString, int cch);



        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll ", EntryPoint = "GetDlgItem")]
        public static extern IntPtr GetDlgItem(IntPtr hParent, int nIDParentItem);

        [DllImport("user32.dll", EntryPoint = "GetWindowText")]
        public static extern int GetWindowText(IntPtr hwnd, StringBuilder lpString, int cch);

        #endregion



        private void button3_Click(object sender, EventArgs e)
        {
            IntPtr maindHwnd = FindWindow(null, "事件处理详细信息"); //获得句柄   
            int i = 0;
            if (maindHwnd != IntPtr.Zero)
            {
                MessageBox.Show("找到了窗体！");
                //找到窗体后这里如何去获取找到的这个窗体上的文本框的值？？？？？？？

                //控件id
                int controlId = 0x00EC1EEC;
                //获取子窗口句柄
                IntPtr EdithWnd = GetDlgItem(maindHwnd, controlId);

                SendMessage(EdithWnd, i, (IntPtr)0, string.Format("当前时间是:{0}", DateTime.Now)); //赋值没问题，表示句柄正确
                StringBuilder stringBuilder = new StringBuilder(512);
                GetWindowText(EdithWnd, stringBuilder, stringBuilder.Capacity);
                MessageBox.Show(string.Format("取到的值是：{0}", stringBuilder.ToString()));//取值一直是空字符串
                

            }
            else
            {
                MessageBox.Show("没有找到窗口");
            }

        }


        public DataGridViewSelectionMode FullRowSelect { get; set; }

        public DataGridViewAutoSizeColumnsMode DisplayedCells { get; set; }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string str = string.Empty;
            if (e.RowIndex > -1)
            {
                Attachment.AttachmentTools call = new Attachment.AttachmentTools();
                str = call.Select(dataGridView1.CurrentRow.Cells["ID"].Value.ToString());
                /*
               string[] args = new string[1];
               args[0] = dataGridView1.CurrentRow.Cells["附件ID"].Value.ToString();
               str = DynamicWebService("Select", args).ToString ();
                */
                if (str != "")
                {
                    string[] sArray = str.Split('|');
                    textBox6.Text = sArray[0];
                    textBox3.Text = sArray[1];
                    textBox8.Text = sArray[2];
                    textBox4.Text = sArray[3];
                    textBox5.Text = sArray[4];
                    textBox9.Text = dataGridView1.CurrentRow.Cells["故障单号"].Value.ToString();
                    textBox7.Text = sArray[5];
                }
                else 
                {
                    MessageBox.Show(this, "无法查询故障单信息", "服务平台附件工具");
                }                               
            } 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            int FileLong;
            int Row = dataGridView1.CurrentRow.Index;
            string AttachmentID = dataGridView1.Rows[Row].Cells[0].Value.ToString();
            byte[] bufferBytes;
            
            if (Row > -1)
            {
                saveFileDialog.FilterIndex = 2; 
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = dataGridView1.Rows[Row].Cells[2].Value.ToString();
                saveFileDialog.Filter = "下载文件类型（*" + System.IO.Path.GetExtension(saveFileDialog.FileName) + "）|*" + System.IO.Path.GetExtension(saveFileDialog.FileName);;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = saveFileDialog.FileName;                    
                    if (File.Exists(saveFileDialog.FileName))
                    {
                        try { File.Delete(saveFileDialog.FileName); }
                        catch
                        {
                            MessageBox.Show(this, "文件已存在，且无法删除", "服务平台附件工具");
                        }
                    }
                    Attachment.AttachmentTools call = new Attachment.AttachmentTools();
                    FileLong = Convert .ToInt32 ( call.GetFileInfo (AttachmentID ));
                    /*
                    object[] args = new object[1];
                    args[0]=AttachmentID;
                    FileLong = Convert .ToInt32 (DynamicWebService("GetFileInfo", args));
                    */                    
                    int byteslong = 20*1024*1024;
                    long countOfPk = FileLong / Convert.ToInt64(byteslong);
                    countOfPk += FileLong % byteslong == 0 ? 0 : 1;
                    progressBar.Minimum = 0;
                    progressBar.Maximum = Convert.ToInt32(countOfPk);

                    FileStream fsWrite = new FileStream(saveFileDialog.FileName, FileMode.Append);
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
                        bufferBytes = call.DownFile(AttachmentID, ii * byteslong, bufferlength);
                        /*
                        object[] arg = new object[3];
                        arg[0] = AttachmentID;
                        arg[1] = ii * byteslong;
                        arg[2] = bufferlength;

                        //object obj = DynamicWebService("DownFile", arg);
                        //Serialize(obj, saveFileDialog.FileName);


                        
                        bufferBytes = GetBinaryFormatData(DynamicWebService("DownFile", arg));
                        */                      
                        fsWrite.Write(bufferBytes, 0, bufferBytes.Length );
                         
                        progressBar.Value = Convert.ToInt32(ii + 1);
                         
                        Application.DoEvents();
                    }
                    fsWrite.Close(); 
                    if (File.Exists(saveFileDialog.FileName ))
                    {
                        MessageBox.Show(this, "指定附件下载成功", "服务平台附件工具");
                        System.Diagnostics.Process.Start(saveFileDialog.FileName );
                    }
                    else
                    {
                        MessageBox.Show(this, "附件下载失败！", "服务平台附件工具");
                    }   
                }
              } 
        }       

        private void button5_Click(object sender, EventArgs e)
        {
            int Row = dataGridView1.CurrentRow.Index;
            if (Row > -1)
            {
                Attachment.AttachmentTools call = new Attachment.AttachmentTools();
                if (call.Delete(dataGridView1.Rows[Row].Cells[0].Value.ToString()))
                {
                    MessageBox.Show(this, "删除附件成功！", "服务平台附件工具");
                    index();
                }
                else
                {
                    MessageBox.Show(this, "删除附件失败！", "服务平台附件工具");
                }
                

            }
        }
        public static byte[] GetBinaryFormatData(object pObj)
        {
            if (pObj == null)
                return null;
            //内存实例
            MemoryStream ms = new MemoryStream();
            //创建序列化的实例
            BinaryFormatter formatter = new BinaryFormatter();
            long size = ms.GetBuffer().Length;
            formatter.Serialize(ms, pObj);//序列化对象，写入ms流中  
            ms.Position = 0;
            //byte[] bytes = new byte[ms.Length];//这个有错误
            byte[] bytes = ms.GetBuffer();
            ms.Read(bytes, 0, bytes.Length);
            ms.Close();
            return bytes;
        }


        public void Serialize(object obj, string filePath)
        {
            BinaryFormatter transfer = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            transfer.Serialize(ms, obj);

            byte[] b = new byte[ms.Length];
            b = ms.GetBuffer();

            //FileStream fs = File.Create(filePath);

            FileStream fsWrite = new FileStream(saveFileDialog.FileName, FileMode.Append);

            fsWrite.Write(b, 0, b.Length);

            ms.Close();
            fsWrite.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            /*
            object pObj = "test";
            string str = "test";
            byte[] buffer = GetBinaryFormatData(pObj);
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(str);
            */

            FileStream rtream = new FileStream(@"D:\1.pdf", FileMode.Open);

            byte[] read = new byte[rtream.Length];
            rtream.Read(read, 0, Convert.ToInt32(rtream.Length));


            /*
            FileStream wtream = new FileStream(@"D:\2.pdf", FileMode.Create);
            BinaryFormatter bFormat = new BinaryFormatter();
            bFormat.Serialize(wtream, pObj);
            */
        }

        private void AttTools_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Dir = Dir;
            Settings.Default.Save();
        }

    }
    }


