using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;

namespace TBT_APP
{
    public class DataManager
    {
        public DataManager()
        {
            _compont_dict = new Dictionary<int, CompontData>();
            _list_combo = new List<Combination>();
            _index_compont = 0;
        }

        // 非线程安全
        public int addCompont(CompontData data)
        {
            int index = _index_compont;
            _index_compont++;
            try
            {
                _compont_dict.Add(index, data);
            }
            catch (ArgumentException)
            {
                return -1;
            }
            data.index = index;
            return index;
        }

        public void setCompont(int index, CompontData data)
        {
            _compont_dict[index] = data;
            return;
        }

        public void removeCompont(int index)
        {
            if (!_compont_dict.ContainsKey(index))
            {
                return;
            }

            _compont_dict.Remove(index);
            return;
        }

        public CompontData getCompont(int index)
        {
            if (!_compont_dict.ContainsKey(index))
            {
                return null;
            }
            return _compont_dict[index];
        }

        public List<CompontData> getAllCompont()
        {
            List<CompontData> list = new List<CompontData>();
            foreach (var item in _compont_dict)
            {
                list.Add(item.Value);
            }
            return list;
        }

        // 保存组件数据

        private int _index_compont;
        private Dictionary<int, CompontData> _compont_dict;

        [Category("版本"), ReadOnly(true), Description("根据版本维护")]
        public string AppVersion { get; set; } = "1.0";

        [Category("全局参数"), DisplayName("光速m/s"), ReadOnly(true)]
        public string cS { get; set; } = "2.9979e8";

        [Category("全局参数"), DisplayName("频率GHz")]
        public string freqencyS { get; set; } = "0.44";

        [Category("全局参数"), DisplayName("波长m"), ReadOnly(true)]
        public double lamda { get; set; } = 2.997956e8 / 0.44e9;

        public List<Combination> _list_combo;

    }
}
