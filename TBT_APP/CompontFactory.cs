using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;
using TBTfront;

namespace TBT_APP
{
    class CompontFactory
    {
        static public vtkProp3D genActor(CompontData data, vtkProperty pro = null)
        {
            if (pro == null)
            {
                pro = new vtkProperty();
                // 默认颜色
                pro.SetColor(config.default_color[0], config.default_color[1],
                    config.default_color[2]);
            }
            if (data.type == CmpontType.PLANEMIRROR 
                || data.type == CmpontType.PARABOLOIDMIRROR
                || data.type == CmpontType.HYPERNOLOIDMIRROR
                || data.type == CmpontType.ELLIPSOIDMIRROR)
            {
                return genQuadricSurfaceActor(data, pro);
            }
            else if (data.type == CmpontType.GUASSIANSOURCE)
            {
                SourceData tmp = (SourceData) data;
                return genFieldActor(tmp.field);
            }
            else if (data.type == CmpontType.RES_CYLINDER)
            {
                return genCylinderActor(data, pro);
            }
            else if (data.type == CmpontType.STLMIRROR)
            {
                return genSTLActor(data, pro);
            }
            return null;
        }

        static public vtkProp3D genSTLActor(CompontData data, vtkProperty pro)
        {
            if (data.restriction != null) return genActorByRestriction(data, pro);
            vtkSTLReader reader = vtkSTLReader.New();
            reader.SetFileName(data.param_str);
            reader.Update();
            return genUserActor(data, reader.GetOutputPort(), pro);
        }
        
        static public vtkProp3D genQuadricSurfaceActor(CompontData data, vtkProperty pro)
        {
            if (data.restriction != null) return genActorByRestriction(data, pro);
            QuadricSurfaceData s = (QuadricSurfaceData)data;

            vtkQuadric quadric = vtkQuadric.New();
            quadric.SetCoefficients(s.param[0], s.param[1], s.param[2], s.param[3], s.param[4], s.param[5], s.param[6],
                s.param[7], s.param[8], s.param[9] + 1);

            //二次函数采样分辨率
            vtkSampleFunction sample = vtkSampleFunction.New();
            sample.SetSampleDimensions(60, 60, 30);
            sample.SetImplicitFunction(quadric);
            sample.SetModelBounds(s.param[10], s.param[11], s.param[12], s.param[13], s.param[14], s.param[15]);
            vtkContourFilter contourFilter =vtkContourFilter.New();
            contourFilter.SetInputConnection(sample.GetOutputPort());
            contourFilter.GenerateValues(1, 1, 1);
            contourFilter.Update();

            return genUserActor(data, contourFilter.GetOutputPort(), pro);
        }

        static public vtkProp3D genCylinderActor(CompontData data, vtkProperty pro)
        {
            double r = data.param[0];
            double h = data.param[1];
            vtkCylinderSource cylinder = vtkCylinderSource.New();
            cylinder.SetHeight(h);
            cylinder.SetRadius(r);
            cylinder.SetCenter(0, h / 2, 0);
            cylinder.SetResolution(40);
            cylinder.Update();
            vtkTransform transform = vtkTransform.New();
            transform.RotateWXYZ(90, 1, 0, 0);

            vtkTransformPolyDataFilter transFilter = vtkTransformPolyDataFilter.New();
            transFilter.SetInputConnection(cylinder.GetOutputPort());
            transFilter.SetTransform(transform); //use vtkTransform (or maybe vtkLinearTransform)
            transFilter.Update();

            return genUserActor(data, transFilter.GetOutputPort(), pro);
        }

        static public vtkProp3D genUserActor(CompontData data, vtkAlgorithmOutput vtkAlgorithmOutput, 
            vtkProperty pro)
        {
            vtkTransform transform = vtkTransform.New();
            // 用户自定义平移旋转 (先移动后旋转)
            transform.Translate(data.coor.pos.x, data.coor.pos.y, data.coor.pos.z);
            transform.RotateWXYZ(data.coor.rotate_theta, data.coor.rotate_axis.x, data.coor.rotate_axis.y, data.coor.rotate_axis.z);

            vtkTransformPolyDataFilter transFilter = vtkTransformPolyDataFilter.New();
            transFilter.SetInputConnection(vtkAlgorithmOutput);
            transFilter.SetTransform(transform); //use vtkTransform (or maybe vtkLinearTransform)
            transFilter.Update();

            //vtkShrinkPolyData shrink = vtkShrinkPolyData.New();
            //shrink.SetInputConnection(transFilter.GetOutputPort());
            //shrink.SetShrinkFactor(1);

            // 改
            //vtkSTLWriter writer = vtkSTLWriter.New();
            //calPolyData(polyData, 0.01);
            //writer.SetInputConnection(transFilter.GetOutputPort());
            //writer.SetFileName("test.stl");
            //writer.Update();

            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(transFilter.GetOutputPort());
            mapper.ScalarVisibilityOff();
            // The actor links the data pipeline to the rendering subsystem
            vtkActor actor = vtkActor.New();
            actor.SetProperty(pro);
            actor.SetMapper(mapper);
            return actor;
        }

        static public vtkProp3D genActorByRestriction(CompontData data, vtkProperty pro)
        {
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(calcRestriction(data));
            mapper.ScalarVisibilityOff();
            // The actor links the data pipeline to the rendering subsystem
            vtkActor actor = vtkActor.New();
            actor.SetProperty(pro);
            actor.SetMapper(mapper);
            return actor;
        }

        static public vtkAlgorithmOutput calcRestriction(CompontData data)
        {
            //calculation::RayTracing rayTracing(this);
            Restriction rest = (Restriction)data.restriction;
            if (rest == null)
            {
                return null;
            }

            // 调后台生成光线
            TBTfront.Ant ant = new TBTfront.Ant();
            List<TBTfront.CompontParam> list_data = new List<TBTfront.CompontParam>();
            list_data.Add(rest);
            list_data.Add(data);
            List<TBTfront.RayLineCluster> rayline_list = new List<TBTfront.RayLineCluster>();
            RayLineCluster rayline_cluster = new RayLineCluster();
            CalcOption opt = new CalcOption();
            opt.is_ign_non_intersection = true;
            opt.is_ign_restriction = true;
            ant.clacLight(list_data, rayline_cluster, rayline_list, opt);
            rayline_cluster = rayline_list[2];

           
            vtkPoints points = vtkPoints.New();
            foreach (var ray in rayline_cluster.ray_cluster)
            {
                points.InsertNextPoint(ray.start_point.x,
                    ray.start_point.y,
                    ray.start_point.z);
            }

            vtkPolyData polyData = vtkPolyData.New();
            polyData.SetPoints(points);

            vtkDelaunay2D delaunay = vtkDelaunay2D.New();
            delaunay.SetInput(polyData);
            delaunay.Update();
            return delaunay.GetOutputPort();
        }
    
        static public vtkProp3D genFieldActor(FieldBase data)
        {
            int N_width = data.Ex.Count;
            int M_depth = data.Ex.Count;
            double ds = data.ds_x;
            double width = N_width * ds;
            double depth = M_depth * ds;

            vtkImageData img = vtkImageData.New();
            img.SetDimensions(N_width, M_depth, 1);
            img.SetSpacing(0.01 * ds / 0.01, 0.01 * ds / 0.01, 1);
            img.SetScalarTypeToDouble();
            img.SetNumberOfScalarComponents(1);

            double max = -100000000, min = 0;
            List<List<Complex>> tempEH = null;

            int content = 1;
            bool isPhs = false;
            bool isLinear = true;
            const double dB_RABNGE = 60;
            switch (content)
            {
                case 0:
                    tempEH = data.Ex;
                    break;
                case 1:
                    tempEH = data.Ey;
                    break;
                case 2:
                    tempEH = data.Ez;
                    break;
                case 3:
                    tempEH = data.Hx;
                    break;
                case 4:
                    tempEH = data.Hy;
                    break;
                case 5:
                    tempEH = data.Hz;
                    break;
                default:
                    break;
            }
            double[] data_tmp = new double[M_depth * N_width];
            int count = 0;
            for (int j = 0; j < M_depth; j++)
                for (int i = 0; i < N_width; i++)
                {
                    double tempD;
                    Complex temp;
                    temp = tempEH[i][j];

                    if (isPhs)
                    {
                        if (temp.real != 0)
                            tempD = Math.Atan2(temp.imag, temp.real);
                        else
                            tempD = 0;
                    }
                    else
                        tempD = Math.Pow((temp.real * temp.real + temp.imag * temp.imag), 0.5);
                    if (!isLinear && !isPhs)
                    {
                        tempD = 20 * Math.Log(tempD + 0.000000001);
                        if (min > tempD)
                            min = tempD;
                        if (max < tempD)
                            max = tempD;
                    }
                    else
                    {
                        if (max < tempD)
                            max = tempD;
                        if (min > tempD)
                            min = tempD;
                    }
                    data_tmp[count++] = tempD;      
                }
            
            //ptr = img.GetScalarPointer();
            vtkLookupTable colorTable = vtkLookupTable.New();
            if (!isLinear && !isPhs)
                min = max - dB_RABNGE;
            if (!isPhs)
            {
                for (int i = 0; i < N_width * M_depth * 1; i++)
                {
                    data_tmp[i] = max - data_tmp[i];
                }
                colorTable.SetRange(0, max - min);
            }
            else
                colorTable.SetRange(min, max);

            IntPtr ptr = img.GetScalarPointer();
            System.Runtime.InteropServices.Marshal.Copy(data_tmp, 0, ptr, M_depth * N_width);
            colorTable.Build();

            vtkImageMapToColors colorMap = vtkImageMapToColors.New();
            colorMap.SetInput(img);
            colorMap.SetLookupTable(colorTable);
            colorMap.Update();

            vtkTransform transform = vtkTransform.New();
            transform.Translate(data.coordinate.pos.x, data.coordinate.pos.y, data.coordinate.pos.z); 
            transform.RotateWXYZ(data.coordinate.rotate_theta, data.coordinate.rotate_axis.x,
                data.coordinate.rotate_axis.y, data.coordinate.rotate_axis.z);
            transform.Translate(-width / 2, -depth / 2, 0);

            vtkImageActor actor = vtkImageActor.New();
            actor.SetInput(colorMap.GetOutput());
            actor.SetUserTransform(transform);

            return actor;
        }

        static public vtkProp3D genRayActor(List<RayLineCluster> data)
        {
            vtkPoints points = vtkPoints.New();
            vtkCellArray pLineCell = vtkCellArray.New();

            vtkLine p1 = vtkLine.New();
            int cout = 0;
            System.Windows.Forms.MessageBox.Show("cluster.start_radius:" + data.Count.ToString());
            for (int i = 0; i < data.Count; i++)
            {
                RayLineCluster cluster = data[i];
                if (i != data.Count - 1)
                {
                    // 第一个的起点连第二次的起点
                    List<RayLine> rays_start = cluster.ray_cluster;
                    List<RayLine> rays_end = data[i+1].ray_cluster;
                    for (int j = 0; j < rays_start.Count; j++)
                    {
                        points.InsertNextPoint(rays_start[j].start_point.x,
                                rays_start[j].start_point.y, rays_start[j].start_point.z);
                        points.InsertNextPoint(rays_end[j].start_point.x,
                            rays_end[j].start_point.y, rays_end[j].start_point.z);

                        p1.GetPointIds().SetId(0, cout++);
                        p1.GetPointIds().SetId(1, cout++);
                        pLineCell.InsertNextCell(p1);
                    }
                }
                else
                {
                    foreach (var line in cluster.ray_cluster)
                    {
                        points.InsertNextPoint(line.start_point.x,
                                line.start_point.y, line.start_point.z);
                        points.InsertNextPoint(
                            line.start_point.x + 0.5 * line.normal_line.x,
                            line.start_point.y + 0.5 * line.normal_line.y,
                            line.start_point.z + 0.5 * line.normal_line.z);
                        p1.GetPointIds().SetId(0, cout++);
                        p1.GetPointIds().SetId(1, cout++);
                        pLineCell.InsertNextCell(p1);
                    }
                }
            }

            vtkPolyData pointsData = vtkPolyData.New();
            pointsData.SetPoints(points); //获得网格模型中的几何数据：点集  
            pointsData.SetLines(pLineCell);

            vtkPolyDataMapper pointMapper = vtkPolyDataMapper.New();
            pointMapper.SetInput(pointsData);
            pointMapper.Update();

            vtkActor actor = vtkActor.New();
            actor.SetMapper(pointMapper);
            actor.GetProperty().SetColor(1, 0, 0);
            return actor;
        }

        static public vtkProperty genClickProperty()
        {
            vtkProperty pro = new vtkProperty();
            pro.SetColor(config.click_color[0], config.click_color[1],
                    config.click_color[2]);
            pro.SetOpacity(0.5);
            return pro;
        }
    }
}
