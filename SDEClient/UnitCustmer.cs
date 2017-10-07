using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SDEClient
{
    public partial class UnitCustmer : Form
    {
        public UnitCustmer()
        {
            InitializeComponent();
        }

        private void UnitCustmer_Load(object sender, EventArgs e)
        {
            dataGridView.Height = this.Height - 95;
            checkBox.Top = this.Height - 21;
            checkBox.Checked = false;
            textBox1.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            { GetHosp(); }

        }
        private void GetHosp()
        {
            Remind.RemindTools call = new Remind.RemindTools();
            DataTable DT = new DataTable();

            DT = call.GetTestHosp ().Tables[0];

            CreateGridColumn(dataGridView, DT);
            dataGridView.AutoGenerateColumns = true;
            dataGridView.DataSource = DT;
            dataGridView.Columns["分中心编码"].Visible = false;
            

            dataGridView.RowHeadersVisible = false;
            this.dataGridView.AllowUserToResizeColumns = true;
            this.dataGridView.Columns[0].Resizable = DataGridViewTriState.True;

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView.Columns[2].Frozen = true;

            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView.Columns[0].Width = 30;  //序号
            this.dataGridView.Columns[1].Width = 20;  //选择
            this.dataGridView.Columns[2].Width = 60;  //医院编码
            this.dataGridView.Columns[3].Width = 180;    //医院名称
            this.dataGridView.Columns[4].Width = 80;     //医院级别
            this.dataGridView.Columns[5].Width = 80;      //医院类型
            this.dataGridView.Columns[6].Width = 80;     //所在区县
            this.dataGridView.Columns[7].Width = 90;     //分中心名称
           
            dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //必须设置SortMode 为NotSortable，否则设置的单元格样式不会生效
            //dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //必须设置SortMode 为NotSortable，否则设置的单元格样式不会生效
            //dataGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //必须设置SortMode 为NotSortable，否则设置的单元格样式不会生效
            //dataGridView.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            int num_1=0, num_2=0, num_3=0;
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                dataGridView.Rows[i].Cells[0].Value = 1;                                  
                switch (dataGridView.Rows[i].Cells[8].Value.ToString())
                {
                    case "5000":
                        num_1++;
                        break;
                    case "6000":
                        num_2++;
                        break;
                    case "7000":
                        num_3++;
                        break;
                }
            }
            label1.Text = label1.Text + num_1.ToString();
            label2.Text = label2.Text + num_2.ToString();
            label3.Text = label3.Text + num_3.ToString();
            GetSelectNum();
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
            DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
            newColumn.HeaderText = "选择";            
            dgv.Columns.Insert(1, newColumn);
            
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

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString ()=="1")
                { 
                    dataGridView.Rows[e.RowIndex].Cells[0].Value = 0;
                }
                else
                {
                    dataGridView.Rows[e.RowIndex].Cells[0].Value = 1;
                }
            }
            GetSelectNum();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView.Rows.Count;i++ )
            {
                if (dataGridView.Rows[i].Cells[0].Value.ToString() == "1")
                {
                    dataGridView.Rows[i].Cells[0].Value = 0;
                }
                else
                {
                    dataGridView.Rows[i].Cells[0].Value = 1;
                }
            }
            GetSelectNum();
        }
        private void GetSelectNum()
        {
            int num_tj = 0;
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (dataGridView.Rows[i].Cells[0].Value.ToString() == "1")
                    num_tj++;
            }
            label4.Text = "已选数量：" + num_tj.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = dataGridView.Rows.Count;

            Remind.RemindTools call = new Remind.RemindTools();
            for (int i = 0; i < dataGridView.Rows.Count;i++ )
            {
                if (dataGridView.Rows[i].Cells [0].Value.ToString ()=="1")
                { 
                string unit=dataGridView.Rows[i].Cells[8].Value .ToString ();
                string hosp=dataGridView.Rows[i].Cells [2].Value .ToString ();
                if (!call.SetUnitRange(unit, hosp))
                {
                    MessageBox.Show(this, "提交数据异常，！", "服务平台增强工具");
                    break;
                }
                }
                progressBar.Value = progressBar.Value + 1;
            }
            MessageBox.Show(this, "提交数据成功！", "服务平台增强工具");
            
        }

       

    }
}
