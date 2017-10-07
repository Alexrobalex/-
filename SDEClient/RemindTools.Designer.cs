namespace SDEClient
{
    partial class RemindTools
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemindTools));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.comboBox_Engineer = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimePicker_S = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_E = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_New = new System.Windows.Forms.CheckBox();
            this.timer_New = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(0, 37);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(800, 500);
            this.dataGridView.TabIndex = 3;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_CellFormatting);
            this.dataGridView.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_RowLeave);
            this.dataGridView.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridView_SortCompare);
            this.dataGridView.Sorted += new System.EventHandler(this.dataGridView_Sorted);
            // 
            // comboBox_Engineer
            // 
            this.comboBox_Engineer.FormattingEnabled = true;
            this.comboBox_Engineer.Location = new System.Drawing.Point(7, 10);
            this.comboBox_Engineer.Name = "comboBox_Engineer";
            this.comboBox_Engineer.Size = new System.Drawing.Size(115, 20);
            this.comboBox_Engineer.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(128, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 21);
            this.button1.TabIndex = 4;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimePicker_S
            // 
            this.dateTimePicker_S.Location = new System.Drawing.Point(575, 7);
            this.dateTimePicker_S.Name = "dateTimePicker_S";
            this.dateTimePicker_S.Size = new System.Drawing.Size(102, 21);
            this.dateTimePicker_S.TabIndex = 5;
            this.dateTimePicker_S.Visible = false;
            // 
            // dateTimePicker_E
            // 
            this.dateTimePicker_E.Location = new System.Drawing.Point(713, 7);
            this.dateTimePicker_E.Name = "dateTimePicker_E";
            this.dateTimePicker_E.Size = new System.Drawing.Size(102, 21);
            this.dateTimePicker_E.TabIndex = 6;
            this.dateTimePicker_E.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(681, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "——";
            this.label1.Visible = false;
            // 
            // checkBox_New
            // 
            this.checkBox_New.AutoSize = true;
            this.checkBox_New.Location = new System.Drawing.Point(242, 11);
            this.checkBox_New.Name = "checkBox_New";
            this.checkBox_New.Size = new System.Drawing.Size(126, 16);
            this.checkBox_New.TabIndex = 8;
            this.checkBox_New.Text = "自动刷新（5分钟）";
            this.checkBox_New.UseVisualStyleBackColor = true;
            this.checkBox_New.CheckedChanged += new System.EventHandler(this.checkBox_New_CheckedChanged);
            // 
            // timer_New
            // 
            this.timer_New.Tick += new System.EventHandler(this.timer_New_Tick);
            // 
            // RemindTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 578);
            this.Controls.Add(this.checkBox_New);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker_E);
            this.Controls.Add(this.dateTimePicker_S);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.comboBox_Engineer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RemindTools";
            this.Text = "RemindTools";
            this.Load += new System.EventHandler(this.RemindTools_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ComboBox comboBox_Engineer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_S;
        private System.Windows.Forms.DateTimePicker dateTimePicker_E;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_New;
        private System.Windows.Forms.Timer timer_New;

    }
}