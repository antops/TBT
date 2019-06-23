using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBTfront;
using Kitware.VTK;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;

namespace TBT_APP
{
    public enum CmpontType
    {
        PLANEMIRROR = 0,
        QUADRICSURFACE,
        PARABOLOIDMIRROR,
        HYPERNOLOIDMIRROR,
        ELLIPSOIDMIRROR,
        STLMIRROR,
        //restriction
        RES_CYLINDER,
        RES_CUBE,
        // source
        SOURCE = 1024,
        GUASSIANSOURCE,
        // light source
        LIGHTSOURCE
    }

    public class CompontData: CompontParam
    {
        public static string getNameByType(CmpontType type)
        {
            if (type == CmpontType.PLANEMIRROR)
            {
                return "PlaneMirror";
            }
            else if (type == CmpontType.PARABOLOIDMIRROR)
            {
                return "ParaboloidMirror";
            }
            else if (type == CmpontType.HYPERNOLOIDMIRROR)
            {
                return "HypernoloidMirror";
            }
            else if (type == CmpontType.ELLIPSOIDMIRROR)
            {
                return "EllipsoidMirror";
            }
            else if (type == CmpontType.GUASSIANSOURCE)
            {
                return "GuassianSource";
            }
            else if (type == CmpontType.STLMIRROR)
            {
                return "STLMirror";
            }
            return "";
        }

        public CompontData()
        {
            coor = new Coordinate();
        }

        public CompontData(CompontData data)
        {
            coor = data.coor;
            actor = data.actor;
            type = data.type;
            name = data.name;
        }

        public void setUV(Vector3 U_, Vector3 V_)
        {
            coor.U = U_;
            coor.V = V_;
            Ant.coordinateUV2Rotate(coor);
        }

        public void setRotate(Vector3 rotate_, double rotate_theta_)
        {
            coor.rotate_axis = rotate_;
            coor.rotate_theta = rotate_theta_;        
            Ant.coordinateRotate2UV(coor);
        }
        [BrowsableAttribute(false)]
        public CmpontType type { get; set; }

        //[BrowsableAttribute(false)]
        [Category("Basepara"), DisplayName("旋转角"), ReadOnly(true)]
        public string rotate_theta_str {
            get
            {
                return coor.rotate_theta.ToString();
            }
        }

        [BrowsableAttribute(false)]
        public vtkProp3D actor { get; set; }

        [Category("Basepara"), DisplayName("type"), ReadOnly(true)]
        public string type_str
        {
            get
            {
                return getNameByType(type);
            }
        }

        [Category("Basepara"), DisplayName("旋转轴"), ReadOnly(true)]
        public string rotate_str
        {
            get
            {
                return coor.rotate_axis.x.ToString() + ","
                    + coor.rotate_axis.y.ToString() + ","
                    + coor.rotate_axis.z.ToString();
            }
        }

        [Category("Basepara"), DisplayName("Central point"), ReadOnly(true)]
        public string central_point_str
        {
            get
            {
                return coor.pos.x.ToString() + ","
                    + coor.pos.y.ToString() + ","
                    + coor.pos.z.ToString();
            }
        }

        [Category("Basepara"), DisplayName("U"), ReadOnly(true)]
        public string U_str
        {
            get
            {
                return coor.U.x.ToString() + ","
                    + coor.U.y.ToString() + ","
                    + coor.U.z.ToString();
            }
        }

        [Category("Basepara"), DisplayName("V"), ReadOnly(true)]
        public string V_str
        {
            get
            {
                return coor.V.x.ToString() + ","
                    + coor.V.y.ToString() + ","
                    + coor.V.z.ToString();
            }
        }

        [Category("Basepara"), DisplayName("name"), ReadOnly(true)]
        public string show_name { get; set; }

        [BrowsableAttribute(false)]
        public int index { get; set; }

        public override string ToString()
        {
            return show_name;
        }

        public void setRestriction(Restriction restric)
        {
            restriction = restric;
        }

        public void removeRestriction()
        {
            restriction = null;
        }
    }

    // F(x, y, z) = a0* x^2 + a1* y^2 + a2* z^2 + a3* x* y + a4* y* z + a5* x* z + a6* x + a7* y + a8* z + a9
    // a0~a9 对应 data[0]~data[9]
    // xmin xmax ymin ymax zmin zmax 对应 data[10]~data[15]
    public class QuadricSurfaceData : CompontData
    {
        public QuadricSurfaceData(CompontData data_)
            : base(data_)
        {
            param = new List<double>();
            for (int i = 0; i < 16; i++)
            {
                param.Add(0);
            }
            name = "quadricSurface";
            type = CmpontType.QUADRICSURFACE;
        }
    }

    public class PlaneData : QuadricSurfaceData
    {
        public PlaneData(CompontData data)
            : base(data)
        {
            type = CmpontType.PLANEMIRROR;
        }
        public void setData(double width_, double depth_)
        {
            width = width_;
            depth = depth_;
            param[8] = 1.0;
            param[10] = -width_/2;
            param[11] = width_/2;
            param[12] = -depth_ /2;
            param[13] = depth_/2;
            param[14] = -0.1;
            param[15] = 0.1;
        }
        public double width { get; private set; }
        public double depth { get; private set; }
    }

    public class ParaboloidData : QuadricSurfaceData
    {
        public ParaboloidData(CompontData data)
            : base(data)
        {
            type = CmpontType.PARABOLOIDMIRROR;
        }
        public void setData(double focus_, double radius_)
        {
            focus = focus_;
            radius = radius_;
            param[0] = 1.0;
            param[1] = 1.0;
            param[8] = -4.0 * focus;
            param[10] = -radius;
            param[11] = radius;
            param[12] = -radius;
            param[13] = radius;
            param[14] = 0.0;
            param[15] = radius * radius / 4.0 / focus;

        }
        public double focus { get; private set; }
        public double radius { get; private set; }
    }

    public class HyperboloidData : QuadricSurfaceData
    {
        public HyperboloidData(CompontData data)
            : base(data)
        {
            type = CmpontType.HYPERNOLOIDMIRROR;
        }
        public void setData(double a_, double b_, double c_, double d_)
        {
            depth = d_;
            a = a_;
            b = b_;
            c = c_;
            param[0] = 1.0 / a / a;
            param[1] = -1.0 / b / b;
            param[2] = -1.0 / c / c;
            param[9] = -1.0;
            param[10] = 0;
            param[11] = a + depth;
            double tmp_y = Math.Sqrt((a + depth) * (a + depth) / a / a - 1)*b;
            param[12] = -tmp_y;
            param[13] = +tmp_y;
            double tmp_z = Math.Sqrt((a + depth) * (a + depth) / a / a - 1)*c;
            param[14] = -tmp_z;
            param[15] = +tmp_z;

        }
        public double a { get; private set; }
        public double b { get; private set; }
        public double c { get; private set; }
        public double depth { get; private set; }
    }

    public class EllipsoidData : QuadricSurfaceData
    {
        public EllipsoidData(CompontData data)
            : base(data)
        {
            type = CmpontType.HYPERNOLOIDMIRROR;
        }
        public void setData(double a_, double b_, double c_)
        {
            a = a_;
            b = b_;
            c = c_;
            param[0] = 1 / a / a;
            param[1] = 1 / b / b;
            param[2] = 1 / c / c;
            param[9] = -1;
            param[10] = -a;
            param[11] = a;
            param[12] = -b;
            param[13] = b;
            param[14] = -c;
            param[15] = 0;

        }
        public double a { get; private set; }
        public double b { get; private set; }
        public double c { get; private set; }
    }

    public class SourceData : CompontData
    {
        public SourceData(CompontData data_)
            : base(data_)
        {
            param = new List<double>();
            for (int i = 0; i < 3; i++)
            {
                param.Add(0);
            }
        }
        public FieldBase field;
        public RayLineCluster rayline_cluster;
        public void setData(double width_, double depth_, double ds_)
        {
            width = width_;
            depth = depth_;
            ds = ds_;
            param[0] = width;
            param[1] = depth;
            param[2] = ds;
        }

        public double width { get; private set; }
        public double depth { get; private set; }
        public double ds { get; private set; }

        public void update()
        {
            // 调后台生成场
            TBTfront.Ant ant = new TBTfront.Ant();
            List<TBTfront.CompontParam> list_data = new List<TBTfront.CompontParam>();
            list_data.Add(this);
            this.field = new TBTfront.FieldBase();
            ant.clacField(list_data, this.field, this.field);
            // 调后台生成光线
            List<TBTfront.RayLineCluster> rayline_list = new List<TBTfront.RayLineCluster>();
            CalcOption opt = new CalcOption();
            ant.clacLight(list_data, this.rayline_cluster, rayline_list, opt);
            this.rayline_cluster = rayline_list[1];
        }
    }

    public class GuassianData : SourceData
    {
        public GuassianData(CompontData data_)
            : base(data_)
        {
            param = new List<double>();
            for (int i = 0; i < 6; i++)
            {
                param.Add(0);
            }
            name = "guassian";
            type = CmpontType.GUASSIANSOURCE;
        }
        public void setGuassianData(double z0_, double w0_, double fre_)
        {
            z0 = z0_;
            w0 = w0_;
            fre = fre_;
            param[3] = z0;
            param[4] = w0;
            param[5] = fre;
        }
        public double z0 { get; private set; }
        public double w0 { get; private set; }
        public double fre { get; private set; }
    }

    public class Combination
    {
        public List<CompontData> data_list { get; set; } = new List<CompontData>();
        // save some user info
        public vtkProp3D actor { get; set; }

        // save some user info
        public vtkProp3D actorGaussian { get; set; }
    }

    public class Restriction : CompontData
    {
        public Restriction(CompontData data_)
            : base(data_)
        {
            param = new List<double>();
            for (int i = 0; i < 4; i++)
            {
                param.Add(-1);
            }
            name = "restriction";
            type = CmpontType.RES_CYLINDER;
            
        }

        public void setData(double width_, double depth_)
        {
            param[0] = width_;
            param[1] = depth_;
        }

        public void setOwner(CompontData owner_)
        {
            owner = owner_;
        }

        public CompontData owner { get; private set; }

        //递归得到最终的owner
        static public CompontData getOwner(CompontData data)
        {
            if ((data.type == CmpontType.RES_CYLINDER) || 
                (data.type == CmpontType.RES_CUBE))
            {
                Restriction tmp = (Restriction)data;
                return getOwner(tmp.owner);
            }
            return data;
        }

        public void genRays(RayLineCluster rayline_cluster, double ds = 0)
        {
            // 调后台生成光线
            TBTfront.Ant ant = new TBTfront.Ant();
            List<TBTfront.CompontParam> list_data = new List<TBTfront.CompontParam>();
            list_data.Add(this);
            List<TBTfront.RayLineCluster> rayline_list = new List<TBTfront.RayLineCluster>();
            CalcOption opt = new CalcOption();
            opt.is_ign_non_intersection = true;
            opt.is_ign_restriction = true;
            ant.clacLight(list_data, rayline_cluster, rayline_list, opt);
            rayline_cluster = rayline_list[1];
        }

    }

    // 自定义的光线源
    public class lightSource : CompontData
    {
        // 生成的光线簇为绝对坐标不用旋转
        public RayLineCluster rayline_cluster;
    }
}
