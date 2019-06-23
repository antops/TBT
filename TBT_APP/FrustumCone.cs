using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;
using TBTfront;

namespace TBT_APP
{
    class FrustumCone
    {
        static public vtkProp3D genActor(List<GaussianCluster> data)
        {
            vtkProperty pro = new vtkProperty();
            // 默认颜色
            pro.SetColor(config.cone_color[0], config.cone_color[1],
                config.cone_color[2]);
            pro.SetOpacity(0.4);
            vtkAppendPolyData polydata = vtkAppendPolyData.New();
            for (int i = 1; i < data.Count; i++)
            {
                var cluster = data[i];
                System.Windows.Forms.MessageBox.Show("cluster.start_radius:" + cluster.start_radius.ToString() +
                    "cluster.distance:" + cluster.distance.ToString() +
                    "cluster.is_foucs:" + cluster.is_foucs.ToString()+
                    "cluster.foucs_radius:" + cluster.foucs_radius.ToString());
                vtkTransform transform = vtkTransform.New();
                transform.Translate(cluster.coordinate.pos.x, cluster.coordinate.pos.y, cluster.coordinate.pos.z);
                transform.RotateWXYZ(cluster.coordinate.rotate_theta, cluster.coordinate.rotate_axis.x, 
                    cluster.coordinate.rotate_axis.y, cluster.coordinate.rotate_axis.z);

                vtkTransformPolyDataFilter transFilter = vtkTransformPolyDataFilter.New();
                transFilter.SetInputConnection(combiFrustumCone(cluster.start_radius,
                    1, cluster.angle, true, 0.01));
                transFilter.SetTransform(transform);
                transFilter.Update();
                polydata.AddInputConnection(transFilter.GetOutputPort());
            }
            
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(polydata.GetOutputPort());
            mapper.ScalarVisibilityOff();
            // The actor links the data pipeline to the rendering subsystem
            vtkActor actor = vtkActor.New();
            actor.SetProperty(pro);
            actor.SetMapper(mapper);
            return actor;
        }

        static public vtkAlgorithmOutput combiFrustumCone(double start_radius, double distance,
            double angle, bool is_foucs, double foucs_radius)
        {
            double tanArc = Math.Tan(Math.PI * angle / 180);
            double origin_dis = start_radius / tanArc;
            if (is_foucs) // 缩小
            {
                double origin_foucs_dis = foucs_radius / tanArc;
                double fouce_dis = origin_dis  - origin_foucs_dis;
                if (distance > fouce_dis) //缩小到焦点后放大
                {
                    vtkAppendPolyData polydata = vtkAppendPolyData.New();
                    polydata.AddInputConnection(genFrustumCone(origin_dis, start_radius, fouce_dis, false));
                    vtkTransform transform = vtkTransform.New();
                    transform.Translate(0, 0, fouce_dis);

                    vtkTransformPolyDataFilter transFilter = vtkTransformPolyDataFilter.New();
                    origin_dis = origin_foucs_dis + distance - fouce_dis;
                    transFilter.SetInputConnection(genFrustumCone(origin_dis, origin_dis * tanArc, distance - fouce_dis, true));
                    transFilter.SetTransform(transform); //use vtkTransform (or maybe vtkLinearTransform)
                    transFilter.Update();
                    polydata.AddInputConnection(transFilter.GetOutputPort());
                    polydata.Update();
                    return polydata.GetOutputPort();
                }
                else
                {
                    return genFrustumCone(origin_dis, start_radius, distance, false);
                }
            }
            else // 放大
            {
                double total_dis = origin_dis + distance;
                double end_radius = total_dis * tanArc;

                return genFrustumCone(total_dis, end_radius, distance, true);
            }
        }

        static public vtkAlgorithmOutput genFrustumCone(double total_dis, double end_radius,
            double distance, bool is_reverse)
        {
            vtkConeSource cone = vtkConeSource.New();
            cone.SetHeight(total_dis * 1.2);
            cone.SetRadius(end_radius * 1.2);
            cone.SetCenter(0, 0, total_dis * 0.4);
            cone.SetResolution(80);
            cone.SetDirection(0, 0, 1);
            cone.Update();

            vtkPlane plane = vtkPlane.New();
            plane.SetOrigin(0, 0, 0);
            plane.SetNormal(0, 0, 1);

            vtkClipPolyData clipPolyData = vtkClipPolyData.New();
            clipPolyData.SetInputConnection(cone.GetOutputPort());
            clipPolyData.SetClipFunction(plane);
            clipPolyData.GenerateClippedOutputOn();
            clipPolyData.Update();

            vtkPlane plane2 = vtkPlane.New();
            plane2.SetOrigin(0, 0, distance);
            plane2.SetNormal(0, 0, -1);

            vtkClipPolyData clipPolyData2 = vtkClipPolyData.New();
            clipPolyData2.SetInputConnection(clipPolyData.GetOutputPort());
            clipPolyData2.SetClipFunction(plane2);
            clipPolyData2.GenerateClippedOutputOn();
            clipPolyData2.Update();

            if (is_reverse)
            {
                vtkTransform transform = vtkTransform.New();
                transform.RotateWXYZ(180, 0, 1, 0);
                transform.Translate(0, 0, -distance);

                vtkTransformPolyDataFilter transFilter = vtkTransformPolyDataFilter.New();
                transFilter.SetInputConnection(clipPolyData2.GetOutputPort());
                transFilter.SetTransform(transform); //use vtkTransform (or maybe vtkLinearTransform)
                transFilter.Update();
                return transFilter.GetOutputPort();
            }
            return clipPolyData2.GetOutputPort();
        }
    }
}
