using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBT_APP
{
    public partial class CombinationForm : Form
    {
        public CombinationForm()
        {
            InitializeComponent();
        }

        public bool setData(DataManager data_manager)
        {
            List<CompontData> data_list = data_manager.getAllCompont();
            foreach (var data in data_list)
            {
                if (data.type >= CmpontType.SOURCE)
                {
                    comboBox1.Items.Add(data);
                }
                else
                {
                    listBox2.Items.Add(data);
                } 
            }
            if (comboBox1.Items.Count == 0)
            {
                return false;
            }
            comboBox1.SelectedIndex = 0;
            return true;
        }

        private void bt_add_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                listBox1.Items.Add(listBox2.SelectedItem);
            }
            
        }

        private void bt_rm_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        // 定义委托
        public delegate void getDataHandler(object sender, Combination e);

        // 声明事件
        public event getDataHandler getData;

        public void getDataEvent(object sender, Combination e)
        {
            if (getData != null)
            {
                getData(this, e);
            }
        }

        private void bt_ok_Click(object sender, EventArgs e)
        {
            Combination combo = new Combination();
            // 第一个一定是源
            combo.data_list.Add((CompontData)comboBox1.SelectedItem);
            foreach (var data in listBox1.Items)
            {
                combo.data_list.Add((CompontData)data);
            }
            getDataEvent(this, combo);
            Close();
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
