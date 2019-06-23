using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kitware.VTK;

namespace TBT_APP
{
    public partial class CompontBaseForm : Form
    {
        public CompontBaseForm()
        {
            InitializeComponent();
            is_modify_flag = false;
        }

        protected void OKBtn_Click(object sender, EventArgs e)
        {
            if (_render != null)
            {
                _render.RemoveViewProp(_actor);
                _renWin.Render();
            }
            CompontData data = genData(false);
            if (data == null)
            {
                return;
            }
            getDataEvent(this, data);
            config.componet_index++;
            Close();
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            if (_render != null)
            {
                _render.RemoveViewProp(_actor);
                _renWin.Render();
            }
            if (cancel != null)
            {
                cancel(this);
            }
            Close();
        }

        // 定义委托
        public delegate void getDataHandler(object sender, CompontData e);

        // 声明事件
        public event getDataHandler getData;

        // 定义委托
        public delegate void getTmpDataHandler(object sender, CompontData e);

        // 定义委托
        public event getTmpDataHandler getTmpData;

        public delegate void cancelHandler(object sender);

        public event cancelHandler cancel;

        public void getDataEvent(object sender, CompontData e)
        {
            if (getData != null)
            {
                getData(this, e);
            }
        }

        public void getTmpDataEvent(object sender, CompontData e)
        {
            if (e == null) return;
            if (getTmpData != null)
            {
                getTmpData(this, e);
            }
        }

        public void on_valueChanged()
        {
            if (_render == null) return;
            _render.RemoveViewProp(_actor);
            CompontData data = genData(true);
            if (data == null) return;
            _actor = CompontFactory.genActor(data, CompontFactory.genClickProperty());
            _render.AddViewProp(_actor);
            _renWin.Render();
        }

        virtual protected CompontData genData(bool is_tmp_show)
        {
            if (is_tmp_show)
            {
                return new CompontData();
            }
            if (_data == null)
                return new CompontData();
            else
                return _data;
        }

        protected bool getBaseData(CompontData data)
        {
            data.coor.pos = new TBTfront.Vector3();
            if (!getDataByText(tb_cp_x, out data.coor.pos.x))
            {
                return false;
            }
            if (!getDataByText(tb_cp_y, out data.coor.pos.y))
            {
                return false;
            }
            if (!getDataByText(tb_cp_z, out data.coor.pos.z))
            {
                return false;
            }

            TBTfront.Vector3 U = new TBTfront.Vector3();
            if (!getDataByText(tb_u_x, out U.x))
            {
                return false;
            }
            if (!getDataByText(tb_u_y, out U.y))
            {
                return false;
            }
            if (!getDataByText(tb_u_z, out U.z))
            {
                return false;
            }

            TBTfront.Vector3 V = new TBTfront.Vector3();
            if (!getDataByText(tb_v_x, out V.x))
            {
                return false;
            }
            if (!getDataByText(tb_v_y, out V.y))
            {
                return false;
            }
            if (!getDataByText(tb_v_z, out V.z))
            {
                return false;
            }
            double dot = U.Dot(V);
            if (Math.Abs(dot) > 0.000001) return false;
            TBTfront.Vector3 N = U.Cross(V);
            tb_n_x.Text = N.x.ToString();
            tb_n_y.Text = N.y.ToString();
            tb_n_z.Text = N.z.ToString();
            data.setUV(U, V);
            return checkBaseLegal();
        }

        virtual public void setData(CompontData data)
        {
            tb_cp_x.Text = data.coor.pos.x.ToString();
            tb_cp_y.Text = data.coor.pos.y.ToString();
            tb_cp_z.Text = data.coor.pos.z.ToString();

            tb_u_x.Text = data.coor.U.x.ToString();
            tb_u_y.Text = data.coor.U.y.ToString();
            tb_u_z.Text = data.coor.U.z.ToString();

            tb_v_x.Text = data.coor.V.x.ToString();
            tb_v_y.Text = data.coor.V.y.ToString();
            tb_v_z.Text = data.coor.V.z.ToString();
            _data = data;
            is_modify_flag = true;
            return;
        }

        public void setRender(vtkRenderWindow renWin)
        {
            _renWin = renWin;
            _render = renWin.GetRenderers().GetFirstRenderer();
            CompontData data = genData(true);
            _actor = CompontFactory.genActor(data, CompontFactory.genClickProperty());
            _render.AddViewProp(_actor);
            _renWin.Render();
        }

        static public bool getDataByText(TextBox box, out double res)
        {
            if (double.TryParse(box.Text, out res))
            {
                box.BackColor = Color.White;
                return true;
            }
            else
            {
                box.BackColor = Color.Red;
                return false;
            }
        }

        private void tb_cp_x_TextChanged(object sender, EventArgs e)
        {
            on_valueChanged();
        }

        private void tb_cp_y_TextChanged(object sender, EventArgs e)
        {
            on_valueChanged();
        }

        private void tb_cp_z_TextChanged(object sender, EventArgs e)
        {
            on_valueChanged();
        }

        private void tb_u_x_TextChanged(object sender, EventArgs e)
        {
            on_valueChanged();
        }

        private void tb_u_y_TextChanged(object sender, EventArgs e)
        {
            on_valueChanged();
        }

        private void tb_u_z_TextChanged(object sender, EventArgs e)
        {
            on_valueChanged();
        }

        private void tb_v_x_TextChanged(object sender, EventArgs e)
        {
            on_valueChanged();
        }

        private void tb_v_y_TextChanged(object sender, EventArgs e)
        {
            on_valueChanged();
        }

        private void tb_v_z_TextChanged(object sender, EventArgs e)
        {
            on_valueChanged();
        }

        protected bool checkBaseLegal()
        {
            return true;
        }

        private CompontData _data;

        private vtkRenderer _render;

        private vtkRenderWindow _renWin;

        private vtkProp3D _actor;

        protected bool is_modify_flag;
    }
}
