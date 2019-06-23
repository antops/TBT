using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBT_APP
{
    public partial class PlaneForm : CompontBaseForm
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
        public System.Windows.Forms.TextBox tb_focus;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox tx_name;
        public System.Windows.Forms.TextBox tb_radius;

        override protected CompontData genData(bool is_tmp_show)
        {
            CompontData base_data = base.genData(is_tmp_show);
            if (!base.getBaseData(base_data))
            {
                return null;
            }
            PlaneData data;
            if (is_tmp_show || !is_modify_flag)
            {
                data = new PlaneData(base_data);
            }
            else
            {
                data = (PlaneData)base_data;
            }
            data.setData(float.Parse(tb_focus.Text), float.Parse(tb_radius.Text));
            data.show_name = tx_name.Text;
            return data;
        }

        public override void setData(CompontData data)
        {
            base.setData(data);
            PlaneData pdata = (PlaneData)data;
            tb_focus.Text = pdata.width.ToString();
            tb_radius.Text = pdata.depth.ToString();
            this.tx_name.Text = pdata.show_name;
            return;
        }

        public PlaneForm()
        {
            InitializeComponent();
            this.tabControl1.SelectedIndex = 1;
            this.tx_name.Text = "Plane " + config.componet_index.ToString();
        }

        private void InitializeComponent()
        {
            this.tabPageParam = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tb_focus = new System.Windows.Forms.TextBox();
            this.tb_radius = new System.Windows.Forms.TextBox();
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
            this.splitContainer3.Size = new System.Drawing.Size(382, 679);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageParam);
            this.tabControl1.Size = new System.Drawing.Size(382, 623);
            this.tabControl1.Controls.SetChildIndex(this.tabPageParam, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage2, 0);
            // 
            // tabPage2
            // 
            this.tabPage2.Size = new System.Drawing.Size(374, 594);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Size = new System.Drawing.Size(368, 590);
            // 
            // groupBox4
            // 
            this.groupBox4.Size = new System.Drawing.Size(368, 191);
            // 
            // splitContainer5
            // 
            this.splitContainer5.Size = new System.Drawing.Size(368, 395);
            // 
            // groupBox5
            // 
            this.groupBox5.Size = new System.Drawing.Size(368, 83);
            // 
            // groupBox6
            // 
            this.groupBox6.Size = new System.Drawing.Size(368, 308);
            // 
            // tabPageParam
            // 
            this.tabPageParam.Controls.Add(this.tableLayoutPanel8);
            this.tabPageParam.Location = new System.Drawing.Point(4, 25);
            this.tabPageParam.Name = "tabPageParam";
            this.tabPageParam.Size = new System.Drawing.Size(373, 594);
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
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 4;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 202F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(373, 594);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // groupBox11
            // 
            this.groupBox11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox11.Location = new System.Drawing.Point(3, 3);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(367, 246);
            this.groupBox11.TabIndex = 2;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Transmisson efficiency";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.tableLayoutPanel6);
            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox10.Location = new System.Drawing.Point(3, 255);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(367, 196);
            this.groupBox10.TabIndex = 4;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Geometry";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.label15, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tb_focus, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.tb_radius, 1, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(361, 172);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 121);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 15);
            this.label15.TabIndex = 7;
            this.label15.Text = "Depth";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 35);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 15);
            this.label14.TabIndex = 6;
            this.label14.Text = "Width";
            // 
            // tb_focus
            // 
            this.tb_focus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_focus.Location = new System.Drawing.Point(83, 30);
            this.tb_focus.Name = "tb_focus";
            this.tb_focus.Size = new System.Drawing.Size(275, 25);
            this.tb_focus.TabIndex = 8;
            this.tb_focus.Text = "1";
            // 
            // tb_radius
            // 
            this.tb_radius.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_radius.Location = new System.Drawing.Point(83, 116);
            this.tb_radius.Name = "tb_radius";
            this.tb_radius.Size = new System.Drawing.Size(275, 25);
            this.tb_radius.TabIndex = 9;
            this.tb_radius.Text = "1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel9);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 457);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(367, 74);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transmisson efficiency";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(361, 50);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(153, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(205, 25);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "1";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Power efficiency";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tx_name);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 537);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 54);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "name";
            // 
            // tx_name
            // 
            this.tx_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tx_name.Location = new System.Drawing.Point(3, 21);
            this.tx_name.Name = "tx_name";
            this.tx_name.Size = new System.Drawing.Size(361, 25);
            this.tx_name.TabIndex = 10;
            this.tx_name.Text = "Plane";
            // 
            // PlaneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(382, 679);
            this.Name = "PlaneForm";
            this.Text = "PlaneForm";
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
