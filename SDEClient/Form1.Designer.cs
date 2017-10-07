namespace SDEClient
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("事件处理-L1坐席");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("事件处理-L2工程师");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("事件处理提醒", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("工作量实时统计");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("平台附件工具");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("测试医院分区划片");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("服务平台增强工具", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeView_Menu = new System.Windows.Forms.TreeView();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(1, -1);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView_Menu);
            this.splitContainer.Size = new System.Drawing.Size(760, 536);
            this.splitContainer.SplitterDistance = 186;
            this.splitContainer.TabIndex = 2;
            // 
            // treeView_Menu
            // 
            this.treeView_Menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Menu.Location = new System.Drawing.Point(0, 0);
            this.treeView_Menu.Name = "treeView_Menu";
            treeNode1.Name = "节点0";
            treeNode1.Text = "事件处理-L1坐席";
            treeNode2.Name = "节点1";
            treeNode2.Text = "事件处理-L2工程师";
            treeNode3.Name = "节点1";
            treeNode3.Text = "事件处理提醒";
            treeNode4.Name = "节点1";
            treeNode4.Text = "工作量实时统计";
            treeNode5.Name = "节点2";
            treeNode5.Text = "平台附件工具";
            treeNode6.Name = "节点0";
            treeNode6.Text = "测试医院分区划片";
            treeNode7.Name = "节点0";
            treeNode7.Text = "服务平台增强工具";
            this.treeView_Menu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7});
            this.treeView_Menu.Size = new System.Drawing.Size(186, 536);
            this.treeView_Menu.TabIndex = 1;
            this.treeView_Menu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_Menu_AfterSelect);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 565);
            this.Controls.Add(this.splitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView treeView_Menu;

    }
}

