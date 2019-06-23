using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBT_APP
{
    public partial class HyperboloidForm : CompontBaseForm
    {
        private System.Windows.Forms.TabPage tabPageParam;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.GroupBox groupBox10;
        public System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        public System.Windows.Forms.Label label15;
        public System.Windows.Forms.Label label14;
        public System.Windows.Forms.TextBox tb_a;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox tx_name;
        public System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox tb_c;
        public System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox tb_depth;
        public System.Windows.Forms.TextBox tb_b;

        override protected CompontData genData(bool is_tmp_show)
        {
            CompontData base_data = base.genData(is_tmp_show);
            if (!base.getBaseData(base_data))
            {
                return null;
            }
            HyperboloidData data;
            if (is_tmp_show || !is_modify_flag)
            {
                data = new HyperboloidData(base_data);
            }
            else
            {
                data = (HyperboloidData)base_data;
            }
            data.setData(float.Parse(tb_a.Text), float.Parse(tb_b.Text), float.Parse(tb_c.Text), float.Parse(tb_depth.Text));
            data.show_name = tx_name.Text;
            return data;
        }

        public override void setData(CompontData data)
        {
            base.setData(data);
            HyperboloidData pdata = (HyperboloidData)data;
            tb_a.Text = pdata.a.ToString();
            tb_b.Text = pdata.b.ToString();
            tb_c.Text = pdata.c.ToString();
            tb_depth.Text = pdata.depth.ToString();
            this.tx_name.Text = pdata.show_name;
            return;
        }

        public HyperboloidForm()
        {
            InitializeComponent();
            this.tabControl1.SelectedIndex = 1;
            this.tx_name.Text = "Hyperboloid " + config.componet_index.ToString();
        }

        private void InitializeComponent()
        {
            this.tabPageParam = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tb_a = new System.Windows.Forms.TextBox();
            this.tb_b = new System.Windows.Forms.TextBox();
            this.tb_c = new System.Windows.Forms.TextBox();
            this.tb_depth = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tx_name = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.tabPageParam.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer3
            // 
            this.splitContainer3.SplitterDistance = 497;
            this.splitContainer3.SplitterWidth = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageParam);
            this.tabControl1.Size = new System.Drawing.Size(381, 497);
            this.tabControl1.Controls.SetChildIndex(this.tabPageParam, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage2, 0);
            // 
            // tabPage2
            // 
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tabPage2.Size = new System.Drawing.Size(373, 468);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Location = new System.Drawing.Point(4, 2);
            this.splitContainer4.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.splitContainer4.Size = new System.Drawing.Size(365, 464);
            this.splitContainer4.SplitterDistance = 119;
            this.splitContainer4.SplitterWidth = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox4.Size = new System.Drawing.Size(365, 119);
            // 
            // splitContainer5
            // 
            this.splitContainer5.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.splitContainer5.Size = new System.Drawing.Size(365, 343);
            this.splitContainer5.SplitterDistance = 53;
            this.splitContainer5.SplitterWidth = 2;
            // 
            // groupBox5
            // 
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox5.Size = new System.Drawing.Size(365, 53);
            // 
            // groupBox6
            // 
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox6.Size = new System.Drawing.Size(365, 288);
            // 
            // tabPageParam
            // 
            this.tabPageParam.Controls.Add(this.tableLayoutPanel8);
            this.tabPageParam.Location = new System.Drawing.Point(4, 25);
            this.tabPageParam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageParam.Name = "tabPageParam";
            this.tabPageParam.Size = new System.Drawing.Size(373, 468);
            this.tabPageParam.TabIndex = 2;
            this.tabPageParam.Text = "Geomotry";
            this.tabPageParam.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.AutoSize = true;
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.groupBox11, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.groupBox10, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.groupBox1, 0, 3);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 4;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 202F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(373, 468);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // groupBox11
            // 
            this.groupBox11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox11.Location = new System.Drawing.Point(3, 2);
            this.groupBox11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox11.Size = new System.Drawing.Size(367, 122);
            this.groupBox11.TabIndex = 2;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Transmisson efficiency";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.tableLayoutPanel6);
            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox10.Location = new System.Drawing.Point(3, 128);
            this.groupBox10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox10.Size = new System.Drawing.Size(367, 198);
            this.groupBox10.TabIndex = 4;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Geometry";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.label18, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.label16, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.label15, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tb_a, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.tb_b, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.tb_c, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.tb_depth, 1, 3);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 20);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(361, 176);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(16, 146);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(47, 15);
            this.label18.TabIndex = 13;
            this.label18.Text = "depth";
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(32, 102);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 15);
            this.label16.TabIndex = 10;
            this.label16.Text = "c";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(32, 58);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(15, 15);
            this.label15.TabIndex = 7;
            this.label15.Text = "b";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(32, 14);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 15);
            this.label14.TabIndex = 6;
            this.label14.Text = "a";
            // 
            // tb_a
            // 
            this.tb_a.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_a.Location = new System.Drawing.Point(83, 9);
            this.tb_a.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_a.Name = "tb_a";
            this.tb_a.Size = new System.Drawing.Size(275, 25);
            this.tb_a.TabIndex = 8;
            this.tb_a.Text = "1";
            // 
            // tb_b
            // 
            this.tb_b.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_b.Location = new System.Drawing.Point(83, 53);
            this.tb_b.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_b.Name = "tb_b";
            this.tb_b.Size = new System.Drawing.Size(275, 25);
            this.tb_b.TabIndex = 9;
            this.tb_b.Text = "1";
            // 
            // tb_c
            // 
            this.tb_c.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_c.Location = new System.Drawing.Point(83, 97);
            this.tb_c.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_c.Name = "tb_c";
            this.tb_c.Size = new System.Drawing.Size(275, 25);
            this.tb_c.TabIndex = 11;
            this.tb_c.Text = "1";
            // 
            // tb_depth
            // 
            this.tb_depth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_depth.Location = new System.Drawing.Point(83, 141);
            this.tb_depth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_depth.Name = "tb_depth";
            this.tb_depth.Size = new System.Drawing.Size(275, 25);
            this.tb_depth.TabIndex = 14;
            this.tb_depth.Text = "1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel9);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 330);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(367, 76);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transmisson efficiency";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 20);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(361, 54);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(152, 14);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(206, 25);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "1";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Power efficiency";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tx_name);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 410);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(367, 56);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "name";
            // 
            // tx_name
            // 
            this.tx_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tx_name.Location = new System.Drawing.Point(3, 20);
            this.tx_name.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tx_name.Name = "tx_name";
            this.tx_name.Size = new System.Drawing.Size(361, 25);
            this.tx_name.TabIndex = 10;
            this.tx_name.Text = "Hyperboloid";
            // 
            // HyperboloidForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(381, 679);
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "HyperboloidForm";
            this.Text = "HyperboloidForm";
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.tabPageParam.ResumeLayout(false);
            this.tabPageParam.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

    }
}
