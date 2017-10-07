using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SDEClient
{
    public partial class RemindTools : Form
    {
        public RemindTools()
        {
            InitializeComponent();
        }

        private void RemindTools_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            //dataGridView.Left = 2;
            dataGridView.Top = 40;           
            dataGridView.Width = this.Width - 10;
            dataGridView.Height = this.Height - 43;

            Remind.RemindTools call = new Remind.RemindTools();
            dateTimePicker_E.Text = call.GetDate();
            dateTimePicker_S.Text = dateTimePicker_E.Text;
            timer_New.Interval = 1000*60*5;   //5分钟
            checkBox_New.CheckState = CheckState.Checked;
            timer_New.Start();
            GetEngineer();
        }

        private void GetEngineer()
        {
            Remind.RemindTools call = new Remind.RemindTools();
            DataTable EngDt = new DataTable();
            DataColumn ADC1 = new DataColumn("EngineerID", typeof(int));
            DataColumn ADC2 = new DataColumn("EngineerName", typeof(string));
            EngDt.Columns.Add(ADC1);
            EngDt.Columns.Add(ADC2);

            DataRow ADR = EngDt.NewRow();
            ADR[0] = 0;
            ADR[1] = "全部L1坐席";
            EngDt.Rows.Add(ADR);

            DataTable dt = call.GetEngineer().Tables [0];
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                DataRow Dtr = EngDt.NewRow();
                Dtr[0] = dt.Rows[i][0];
                Dtr[1] = dt.Rows[i][1];
                EngDt.Rows.Add(Dtr);
            }

            comboBox_Engineer.DataSource = EngDt;
            comboBox_Engineer.ValueMember = "EngineerID";
            comboBox_Engineer.DisplayMember = "EngineerName";
            comboBox_Engineer.SelectedIndex = 0;
        }       

        private void button1_Click(object sender, EventArgs e)
        {
            Query_List();
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
         
        private void GetColor()
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                string LED = dataGridView.Rows[i].Cells[10].Value .ToString();
                if (LED == "red")
                {
                    dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
                if (LED == "green")
                {
                    dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                if (LED == "black")
                {
                    dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                if (LED == "blue")
                {
                    dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }               
            }
         }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewCellStyle style = dataGridView.Rows[e.RowIndex].DefaultCellStyle;
                style.Font = new Font(dataGridView.Font, FontStyle.Bold);
                Clipboard.SetDataObject(dataGridView.Rows [e.RowIndex].Cells [1].Value .ToString ());
            }
        }

        private void dataGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellStyle style = dataGridView.Rows[e.RowIndex].DefaultCellStyle;
            style.Font = new Font(dataGridView.Font, FontStyle.Regular );
        }

        private void dataGridView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
           
            //   如果是学号或成绩列，则按浮点数处理
            if(e.Column.Name=="序号")
            {
                e.SortResult = (Convert.ToDouble(e.CellValue1) - Convert.ToDouble(e.CellValue2) > 0) ? 1 : (Convert.ToDouble(e.CellValue1) - Convert.ToDouble(e.CellValue2) < 0)?-1:0;
            }
            //否则，按字符串比较
            else
            {
                e.SortResult = System.String.Compare(Convert.ToString(e.CellValue1), Convert.ToString(e.CellValue2));
            }         
            
            e.Handled = true;//不能省掉，不然没效果
            GetColor();
        }

        private void dataGridView_Sorted(object sender, EventArgs e)
        {            
            GetColor();
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //e.Value = (int)e.Value];
            //e.FormattingApplied = true;
        }

        private void timer_New_Tick(object sender, EventArgs e)
        {
            Query_List();
        }
        private void Query_List()
        {
            Remind.RemindTools call = new Remind.RemindTools();
            DataTable DT = new DataTable();
            if (comboBox_Engineer.Text == "全部L1坐席")
            {
                DT = call.GetRemindList("", "", "").Tables[0];
            }
            else
            {
                DT = call.GetRemindList(comboBox_Engineer.Text, "", "").Tables[0];
            }

            CreateGridColumn(dataGridView, DT);
            dataGridView.AutoGenerateColumns = true;
            dataGridView.DataSource = DT;
            dataGridView.Columns["子单数量"].Visible = false;
            dataGridView.Columns["显示状态"].Visible = false;
            

            dataGridView.RowHeadersVisible = false;
            this.dataGridView.AllowUserToResizeColumns = true;
            this.dataGridView.Columns[0].Resizable = DataGridViewTriState.True;

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView.Columns[1].Frozen = true;

            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView.Columns[0].Width = 30;  //序号
            this.dataGridView.Columns[1].Width = 90;  //故障单号
            //this.dataGridView.Columns[2].Width = 80;    //子单数量
            this.dataGridView.Columns[3].Width = 180;     //故障情况
            this.dataGridView.Columns[4].Width = 80;      //用户编码
            this.dataGridView.Columns[5].Width = 100;     //用户名称
            this.dataGridView.Columns[6].Width = 90;
            //this.dataGridView.Columns[7].Width = 80;   //受理人
            //this.dataGridView.Columns[8].Width = 80;    //处理时间
            //this.dataGridView.Columns[9].Width = 80;    //责任工程师
            //this.dataGridView.Columns[10].Width = 50;   //显示状态
            //this.dataGridView.Columns[11].Width = 0;    //现场状态
            //this.dataGridView.Columns[12].Width = 0;    //L1状态


            dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //必须设置SortMode 为NotSortable，否则设置的单元格样式不会生效
            //dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //必须设置SortMode 为NotSortable，否则设置的单元格样式不会生效
            //dataGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //必须设置SortMode 为NotSortable，否则设置的单元格样式不会生效
            //dataGridView.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //必须设置SortMode 为NotSortable，否则设置的单元格样式不会生效
            //dataGridView.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //必须设置SortMode 为NotSortable，否则设置的单元格样式不会生效
            //dataGridView.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //必须设置SortMode 为NotSortable，否则设置的单元格样式不会生效
            //dataGridView.Columns[9].SortMode = DataGridViewColumnSortMode.NotSortable;

            GetColor();
        }

        private void checkBox_New_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_New.CheckState == CheckState.Checked)
            {
                timer_New.Start();
            }
            else
            {
                timer_New.Stop();
            }
        }

        }

    }

