using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SDEClient
{
    public partial class Workload : Form
    {
        public Workload()
        {
            InitializeComponent();
        }

        private void Workload_Load(object sender, EventArgs e)
        {
            Remind.RemindTools call = new Remind.RemindTools();
            dateTimePicker_E.Text = call.GetDate();
            dateTimePicker_S.Text = dateTimePicker_E.Text;
            textBox_PWD.PasswordChar = '*';
            GetGroup();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (authorize(textBox_PWD.Text))
            {
            Query_List();
            }
            else
            {
            MessageBox.Show(this,"密码不正确，不能授权查询！","服务平台增强工具");
            }            
        }
        private void GetGroup()
        {
            Remind.RemindTools call = new Remind.RemindTools();
            DataTable GroupDt = new DataTable();
            DataColumn ADC1 = new DataColumn("GroupID", typeof(string));
            DataColumn ADC2 = new DataColumn("GroupName", typeof(string));
            GroupDt.Columns.Add(ADC1);
            GroupDt.Columns.Add(ADC2);;

            DataTable dt = call.GetWorkloadGroup().Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow Dtr = GroupDt.NewRow();
                Dtr[0] = dt.Rows[i][0];
                Dtr[1] = dt.Rows[i][1];
                GroupDt.Rows.Add(Dtr);
            }

            comboBox_Group.DataSource = GroupDt;
            comboBox_Group.ValueMember = "GroupID";
            comboBox_Group.DisplayMember = "GroupName";
            comboBox_Group.SelectedIndex = 0;
        }
        private void Query_List()
        {
            Remind.RemindTools call = new Remind.RemindTools();
            DataTable DT = new DataTable();
           
            DT = call.GetWorkload(comboBox_Group.SelectedValue.ToString(), dateTimePicker_S.Value.ToShortDateString(), dateTimePicker_E.Value.ToShortDateString()).Tables[0];
            CreateGridColumn(dataGridView, DT);
            dataGridView.AutoGenerateColumns = true;
            dataGridView.DataSource = DT;

            dataGridView.RowHeadersVisible = false;
            this.dataGridView.AllowUserToResizeColumns = true;
            this.dataGridView.Columns[0].Resizable = DataGridViewTriState.True;

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView.Columns[1].Frozen = true;

            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView.Columns[0].Width = 30;  //序号
            this.dataGridView.Columns[1].Width = 70;  //员工号
            this.dataGridView.Columns[2].Width = 90;    //员工姓名
            this.dataGridView.Columns[3].Width = 120;     //未完成主单
            this.dataGridView.Columns[4].Width = 120;      //未完成子单

            dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //必须设置SortMode 为NotSortable，否则设置的单元格样式不会生效
            //dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //必须设置SortMode 为NotSortable，否则设置的单元格样式不会生效 
            dataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //必须设置SortMode 为NotSortable，否则设置的单元格样式不会生效
            //dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //必须设置SortMode 为NotSortable，否则设置的单元格样式不会生效
            dataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //必须设置SortMode 为NotSortable，否则设置的单元格样式不会生效
            //dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;         
            
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
        private bool authorize(string Token)
        { 
            bool flag = false ;
            Remind.RemindTools call = new Remind.RemindTools();
            if (call.GetAuthorize(Token))
            {
                flag = true ;
            }
            return flag;        
        }
    }
}
