namespace TBT_APP
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mirrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planeMirrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paraboloidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hyperboloidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ellipsoidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sTLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gussianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCombinationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateRaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.renderWindowControl1 = new Kitware.VTK.RenderWindowControl();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.treeViewMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRestrictionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateGaussionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.treeViewMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.compontToolStripMenuItem,
            this.calculateToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1284, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // compontToolStripMenuItem
            // 
            this.compontToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mirrorToolStripMenuItem,
            this.sourceToolStripMenuItem});
            this.compontToolStripMenuItem.Name = "compontToolStripMenuItem";
            this.compontToolStripMenuItem.Size = new System.Drawing.Size(90, 24);
            this.compontToolStripMenuItem.Text = "Compont";
            // 
            // mirrorToolStripMenuItem
            // 
            this.mirrorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.planeMirrorToolStripMenuItem,
            this.paraboloidToolStripMenuItem,
            this.hyperboloidToolStripMenuItem,
            this.ellipsoidToolStripMenuItem,
            this.sTLToolStripMenuItem});
            this.mirrorToolStripMenuItem.Name = "mirrorToolStripMenuItem";
            this.mirrorToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.mirrorToolStripMenuItem.Text = "Mirror";
            // 
            // planeMirrorToolStripMenuItem
            // 
            this.planeMirrorToolStripMenuItem.Name = "planeMirrorToolStripMenuItem";
            this.planeMirrorToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.planeMirrorToolStripMenuItem.Text = "Plane";
            this.planeMirrorToolStripMenuItem.Click += new System.EventHandler(this.planeMirrorToolStripMenuItem_Click);
            // 
            // paraboloidToolStripMenuItem
            // 
            this.paraboloidToolStripMenuItem.Name = "paraboloidToolStripMenuItem";
            this.paraboloidToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.paraboloidToolStripMenuItem.Text = "Paraboloid";
            this.paraboloidToolStripMenuItem.Click += new System.EventHandler(this.paraboloidToolStripMenuItem_Click);
            // 
            // hyperboloidToolStripMenuItem
            // 
            this.hyperboloidToolStripMenuItem.Name = "hyperboloidToolStripMenuItem";
            this.hyperboloidToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.hyperboloidToolStripMenuItem.Text = "Hyperboloid";
            this.hyperboloidToolStripMenuItem.Click += new System.EventHandler(this.hyperboloidToolStripMenuItem_Click);
            // 
            // ellipsoidToolStripMenuItem
            // 
            this.ellipsoidToolStripMenuItem.Name = "ellipsoidToolStripMenuItem";
            this.ellipsoidToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.ellipsoidToolStripMenuItem.Text = "Ellipsoid";
            this.ellipsoidToolStripMenuItem.Click += new System.EventHandler(this.ellipsoidToolStripMenuItem_Click);
            // 
            // sTLToolStripMenuItem
            // 
            this.sTLToolStripMenuItem.Name = "sTLToolStripMenuItem";
            this.sTLToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.sTLToolStripMenuItem.Text = "STL";
            this.sTLToolStripMenuItem.Click += new System.EventHandler(this.sTLToolStripMenuItem_Click);
            // 
            // sourceToolStripMenuItem
            // 
            this.sourceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gussianToolStripMenuItem});
            this.sourceToolStripMenuItem.Name = "sourceToolStripMenuItem";
            this.sourceToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.sourceToolStripMenuItem.Text = "Source";
            // 
            // gussianToolStripMenuItem
            // 
            this.gussianToolStripMenuItem.Name = "gussianToolStripMenuItem";
            this.gussianToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.gussianToolStripMenuItem.Text = "Gaussian";
            this.gussianToolStripMenuItem.Click += new System.EventHandler(this.gussianToolStripMenuItem_Click);
            // 
            // calculateToolStripMenuItem
            // 
            this.calculateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCombinationToolStripMenuItem,
            this.calculateRaysToolStripMenuItem,
            this.calculateGaussionToolStripMenuItem});
            this.calculateToolStripMenuItem.Name = "calculateToolStripMenuItem";
            this.calculateToolStripMenuItem.Size = new System.Drawing.Size(87, 24);
            this.calculateToolStripMenuItem.Text = "Calculate";
            // 
            // addCombinationToolStripMenuItem
            // 
            this.addCombinationToolStripMenuItem.Name = "addCombinationToolStripMenuItem";
            this.addCombinationToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.addCombinationToolStripMenuItem.Text = "AddCombination";
            this.addCombinationToolStripMenuItem.Click += new System.EventHandler(this.addCombinationToolStripMenuItem_Click);
            // 
            // calculateRaysToolStripMenuItem
            // 
            this.calculateRaysToolStripMenuItem.Name = "calculateRaysToolStripMenuItem";
            this.calculateRaysToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.calculateRaysToolStripMenuItem.Text = "Calculate Rays";
            this.calculateRaysToolStripMenuItem.Click += new System.EventHandler(this.calculateRaysToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1284, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 679);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1284, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.renderWindowControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.propertyGrid1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 53);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1284, 626);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 2);
            this.treeView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(194, 622);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // renderWindowControl1
            // 
            this.renderWindowControl1.AddTestActors = false;
            this.renderWindowControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderWindowControl1.Location = new System.Drawing.Point(204, 2);
            this.renderWindowControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.renderWindowControl1.Name = "renderWindowControl1";
            this.renderWindowControl1.Size = new System.Drawing.Size(876, 622);
            this.renderWindowControl1.TabIndex = 1;
            this.renderWindowControl1.TestText = null;
            this.renderWindowControl1.Load += new System.EventHandler(this.renderWindowControl1_Load);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(1087, 2);
            this.propertyGrid1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(194, 622);
            this.propertyGrid1.TabIndex = 2;
            // 
            // treeViewMenu
            // 
            this.treeViewMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.treeViewMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem,
            this.modifyToolStripMenuItem,
            this.addRestrictionToolStripMenuItem});
            this.treeViewMenu.Name = "contextMenuStrip1";
            this.treeViewMenu.Size = new System.Drawing.Size(193, 76);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(192, 24);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // modifyToolStripMenuItem
            // 
            this.modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
            this.modifyToolStripMenuItem.Size = new System.Drawing.Size(192, 24);
            this.modifyToolStripMenuItem.Text = "Modify";
            this.modifyToolStripMenuItem.Click += new System.EventHandler(this.modifyToolStripMenuItem_Click);
            // 
            // addRestrictionToolStripMenuItem
            // 
            this.addRestrictionToolStripMenuItem.Name = "addRestrictionToolStripMenuItem";
            this.addRestrictionToolStripMenuItem.Size = new System.Drawing.Size(192, 24);
            this.addRestrictionToolStripMenuItem.Text = "Add Restriction";
            this.addRestrictionToolStripMenuItem.Click += new System.EventHandler(this.addRestrictionToolStripMenuItem_Click);
            // 
            // calculateGaussionToolStripMenuItem
            // 
            this.calculateGaussionToolStripMenuItem.Name = "calculateGaussionToolStripMenuItem";
            this.calculateGaussionToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.calculateGaussionToolStripMenuItem.Text = "Calculate Gaussion";
            this.calculateGaussionToolStripMenuItem.Click += new System.EventHandler(this.calculateGaussionToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 701);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "TBT";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.treeViewMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mirrorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem planeMirrorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paraboloidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView treeView1;
        private Kitware.VTK.RenderWindowControl renderWindowControl1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ToolStripMenuItem gussianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCombinationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculateRaysToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip treeViewMenu;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hyperboloidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ellipsoidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRestrictionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sTLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculateGaussionToolStripMenuItem;
    }
}

