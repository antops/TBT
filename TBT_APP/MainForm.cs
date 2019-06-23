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
using TBTfront;

namespace TBT_APP
{       
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            _data_mananger = new DataManager();
            propertyGrid1.SelectedObject = _data_mananger;

            _root_node = new TreeNode("Components");
            treeView1.Nodes.Add(_root_node);
            _source_node = new TreeNode("Sources");
            _mirror_node = new TreeNode("Mirrors");
            _combination_node = new TreeNode("Combinations");
            _root_node.Nodes.Add(_mirror_node);
            _root_node.Nodes.Add(_source_node);
            treeView1.Nodes.Add(_combination_node);
        }

        private void renderWindowControl1_Load(object sender, EventArgs e)
        {
            vtkRenderWindow renWin = renderWindowControl1.RenderWindow;
            _render = renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer();

            //_render.SetBackground(1, 1, 1);
            _render.SetBackground(1.0, 1.0, 1.0);                        // 设置页面底部颜色值
            _render.SetBackground2(0.529, 0.8078, 0.92157);    // 设置页面顶部颜色值
            _render.SetGradientBackground(true);                  // 开启渐变色背景设置

            double axesScale = 1;
            // 初始化vtk窗口
            vtkAxesActor axes = vtkAxesActor.New();
            axes.GetXAxisCaptionActor2D().GetCaptionTextProperty().SetColor(1, 0, 0);//修改X字体颜色为红色  
            axes.GetYAxisCaptionActor2D().GetCaptionTextProperty().SetColor(0, 2, 0);//修改Y字体颜色为绿色  
            axes.GetZAxisCaptionActor2D().GetCaptionTextProperty().SetColor(0, 0, 3);//修改Z字体颜色为蓝色  

            axes.SetConeRadius(0.3);
            axes.SetConeResolution(20);
            //axes->SetTotalLength(10, 10, 10); //修改坐标尺寸

            vtkAxesActor axes1 = vtkAxesActor.New();
            axes1.GetXAxisCaptionActor2D().GetCaptionTextProperty().SetColor(1, 0, 0);//修改X字体颜色为红色  
            axes1.GetYAxisCaptionActor2D().GetCaptionTextProperty().SetColor(0, 2, 0);//修改Y字体颜色为绿色  
            axes1.GetZAxisCaptionActor2D().GetCaptionTextProperty().SetColor(0, 0, 3);//修改Z字体颜色为蓝色  
            axes1.SetConeRadius(0.03);
            axes1.AxisLabelsOff();
            axes1.GetXAxisCaptionActor2D().GetCaptionTextProperty().SetFontSize(1);
            //axes1.SetConeRadius(0.3);
            axes1.SetConeResolution(20);
            _render.AddActor(axes1);

            vtkRenderWindowInteractor interactor = vtkRenderWindowInteractor.New();
            interactor.SetRenderWindow(renWin);

            vtkInteractorStyleTrackballCamera style = vtkInteractorStyleTrackballCamera.New();
            interactor.SetInteractorStyle(style);
            renWin.SetInteractor(interactor);
            interactor.Initialize();

            vtkCamera aCamera = vtkCamera.New();
            aCamera.SetViewUp(0, 0, 1);//设视角位置 
            aCamera.SetPosition(0, -3 * axesScale, 0);//设观察对象位
            aCamera.SetFocalPoint(0, 0, 0);//设焦点 
            aCamera.ComputeViewPlaneNormal();//自动
            _render.SetActiveCamera(aCamera);

            _render.ResetCamera();
            renWin.Render();

            widget1 = vtkOrientationMarkerWidget.New();
            widget1.SetOutlineColor(0.9300, 0.5700, 0.1300);
            widget1.SetOrientationMarker(axes);
            widget1.SetInteractor(interactor);
            widget1.SetViewport(0.0, 0.0, 0.25, 0.25);
            widget1.SetEnabled(1);
            widget1.InteractiveOff();

            //_render.AddViewProp(FrustumCone.genActor(new CompontData()));
        }

        // ******** menu click *************
        private void planeMirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaneForm form = new PlaneForm();
            form.getData += new CompontBaseForm.getDataHandler(AddCompont);
            form.setRender(renderWindowControl1.RenderWindow);
            form.cancel += new CompontBaseForm.cancelHandler(cancelCompont);
            form.Show(this);
        }

        private void paraboloidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParaboloidForm form = new ParaboloidForm();
            form.getData += new CompontBaseForm.getDataHandler(AddCompont);
            form.setRender(renderWindowControl1.RenderWindow);
            form.cancel += new CompontBaseForm.cancelHandler(cancelCompont);
            form.Show(this);
        }

        private void hyperboloidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HyperboloidForm form = new HyperboloidForm();
            form.getData += new CompontBaseForm.getDataHandler(AddCompont);
            form.setRender(renderWindowControl1.RenderWindow);
            form.cancel += new CompontBaseForm.cancelHandler(cancelCompont);
            form.Show(this);
        }

        private void ellipsoidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EllipsoidForm form = new EllipsoidForm();
            form.getData += new CompontBaseForm.getDataHandler(AddCompont);
            form.setRender(renderWindowControl1.RenderWindow);
            form.cancel += new CompontBaseForm.cancelHandler(cancelCompont);
            form.Show(this);
        }

        private void gussianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuassianForm form = new GuassianForm();
            form.getData += new CompontBaseForm.getDataHandler(AddCompont);
            form.setRender(renderWindowControl1.RenderWindow);
            form.cancel += new CompontBaseForm.cancelHandler(cancelCompont);
            form.Show(this);
        }

        private void sTLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            STLForm form = new STLForm();
            form.getData += new CompontBaseForm.getDataHandler(AddCompont);
            form.setRender(renderWindowControl1.RenderWindow);
            form.cancel += new CompontBaseForm.cancelHandler(cancelCompont);
            form.Show(this);
        }

        private void addCombinationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CombinationForm form = new CombinationForm();
            form.setData(_data_mananger);
            form.getData += new CombinationForm.getDataHandler(AddCombination);
            form.ShowDialog(this);
        }

        private void calculateRaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateLight();
        }

        // 新建时点击确认
        public void AddCompont(object sender, CompontData args)
        {
            args.actor = CompontFactory.genActor(args);
            int index = _data_mananger.addCompont(args);
            // Add the actors to the renderer, set the window size
            _render.AddViewProp(args.actor);
            updateVtk();
            AddNodeTree(args);
        }

        // 修改时点击确认
        public void ModifyCompont(object sender, CompontData args)
        {
            _render.RemoveViewProp(args.actor);
            args.actor = CompontFactory.genActor(args);
            _render.AddViewProp(args.actor);
            updateVtk();
        }

        // 点击取消
        public void cancelCompont(object sender)
        {
            updateVtk();
        }

        public void AddRestriction(object sender, CompontData args)
        {
            Restriction tmp = (Restriction)args;
            tmp.owner.restriction = tmp;
            CompontData owner = Restriction.getOwner(tmp);
            _render.RemoveViewProp(owner.actor);
            owner.actor = CompontFactory.genActor(owner);
            // Add the actors to the renderer, set the window size

            _render.AddViewProp(owner.actor);
            updateVtk();
            AddNodeTree(_select_node, args);
        }

        public void AddNodeTree(CompontData args)
        {
            TreeNode node = new TreeNode(args.show_name);
            node.Tag = args;
            if (args.type >= CmpontType.SOURCE)
            {
                _source_node.Nodes.Add(node);
                _source_node.Expand();
            }
            else
            {
                _mirror_node.Nodes.Add(node);
                _mirror_node.Expand();
            }
            
            _root_node.Expand();
        }

        public void AddNodeTree(TreeNode father, CompontData args)
        {
            TreeNode node = new TreeNode(args.show_name);
            node.Tag = args;

            father.Nodes.Add(node);
            father.Expand();

            _root_node.Expand();
        }

        private void treeView1_AfterSelect(object sender, EventArgs e)
        {
            CompontData data = null;
            try
            {
                data = (CompontData)treeView1.SelectedNode.Tag;
            }
            catch
            {

            }
            propertyGrid1.SelectedObject = data;
        }

        public void AddCombination(object sender, Combination args)
        {
            _data_mananger._list_combo.Add(args);
            TreeNode node_f = new TreeNode("combo");
            node_f.Tag = args;
            foreach (var item in args.data_list)
            {
                TreeNode node = new TreeNode(CompontData.getNameByType(item.type));
                node.Tag = item;
                node_f.Nodes.Add(node);
            }
            _combination_node.Nodes.Add(node_f);
            node_f.Expand();
            _combination_node.Expand();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CompontData data = null;
                try
                {
                    data = (CompontData)e.Node.Tag;
                }
                catch
                {
                    return;
                }
                if (data == null) return;
                _select_data = data;
                _select_node = e.Node;
                Point pos = new Point(e.Node.Bounds.X + e.Node.Bounds.Width, e.Node.Bounds.Y + e.Node.Bounds.Height / 2);

                this.treeViewMenu.Show(this.treeView1, pos);
            }
        }

        // 导航树右键菜单
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _render.RemoveViewProp(_select_data.actor);
            _data_mananger.removeCompont(_select_data.index);
            _select_node.Remove();
            updateVtk();
            _select_data = null;
            _select_node = null;
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompontBaseForm form = null;
            switch (_select_data.type)
            {
                case CmpontType.PARABOLOIDMIRROR:
                    form = new ParaboloidForm();
                    break;
                case CmpontType.HYPERNOLOIDMIRROR:
                    form = new HyperboloidForm();
                    break;
                case CmpontType.PLANEMIRROR:
                    form = new PlaneForm();
                    break;
                case CmpontType.ELLIPSOIDMIRROR:
                    form = new EllipsoidForm();
                    break;
                case CmpontType.GUASSIANSOURCE:
                    form = new GuassianForm();
                    break;
                default:
                    return;

            }
            form.setData(_select_data);
            form.getData += new CompontBaseForm.getDataHandler(ModifyCompont);
            form.setRender(renderWindowControl1.RenderWindow);
            form.cancel += new CompontBaseForm.cancelHandler(cancelCompont);
            form.Show(this);
        }

        private void addRestrictionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestrictionForm form = new RestrictionForm();
            form.setSelectData(_select_data);
            form.getData += new CompontBaseForm.getDataHandler(AddRestriction);
            form.setRender(renderWindowControl1.RenderWindow);
            form.cancel += new CompontBaseForm.cancelHandler(cancelCompont);
            form.Show(this);
        }

        private void updateLight()
        {
            foreach (var combo in _data_mananger._list_combo)
            {
                CompontData data = combo.data_list[0];
                SourceData source = (SourceData) data;
                
                List<TBTfront.CompontParam> list_data = new List<TBTfront.CompontParam>();
                for (int i = 1; i < combo.data_list.Count; i++)
                {
                    list_data.Add(combo.data_list[i]);
                }

                // 调后台生成光线
                TBTfront.Ant ant = new TBTfront.Ant();
                List<RayLineCluster> cluster_list = new List<RayLineCluster>();
                CalcOption opt = new CalcOption();
                int res = ant.clacLight(list_data, source.rayline_cluster, cluster_list, opt);
                if (res != 0)
                {
                    System.Windows.Forms.MessageBox.Show("claclate ray failed, res:" + res.ToString());
                    continue;
                }
                _render.RemoveViewProp(combo.actor);
                combo.actor = CompontFactory.genRayActor(cluster_list);
                _render.AddViewProp(combo.actor);
            }
            updateVtk();
        }

        private void calculateGaussionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var combo in _data_mananger._list_combo)
            {
                List<TBTfront.CompontParam> list_data = new List<TBTfront.CompontParam>();
                for (int i = 0; i < combo.data_list.Count; i++)
                {
                    list_data.Add(combo.data_list[i]);
                }

                // 调后台生成光线
                TBTfront.Ant ant = new TBTfront.Ant();
                List<GaussianCluster> cluster_list = new List<GaussianCluster>();
                GaussianCluster cluster = new GaussianCluster();
                CalcOption opt = new CalcOption();
                int res = ant.clacGaussian(list_data, cluster, cluster_list, opt);
                if (res != 0)
                {
                    //MessageBox();
                    continue;
                }
                _render.RemoveViewProp(combo.actorGaussian);
                combo.actorGaussian = FrustumCone.genActor(cluster_list);
                _render.AddViewProp(combo.actorGaussian);
            }
            updateVtk();
        }

        private void updateVtk()
        {
            vtkRenderWindow renWin = renderWindowControl1.RenderWindow;
            renWin.Render();
        }
        
        private vtkRenderer _render;
        private DataManager _data_mananger;

        //static private int TAG_OFFSET = 1024;
        private TreeNode _root_node;
        private TreeNode _source_node;
        private TreeNode _mirror_node;
        private TreeNode _combination_node;
        private vtkOrientationMarkerWidget widget1;

        private CompontData _select_data;
        private TreeNode _select_node;
    }
}
