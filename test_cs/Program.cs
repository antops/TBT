using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBTfront;

namespace test_cs
{
    class Program
    {
        static void Main(string[] args)
        {
            List<CompontParam> param_list = new List<CompontParam>();
            CompontParam param = new CompontParam();
            param.name = "test";
            param.param = new List<double>();
            param.param.Add(11);
            param_list.Add(param);
            RayLineCluster input = new RayLineCluster();
            input.ray_cluster = new List<RayLine>();
            RayLine line = new RayLine();
            line.start_point = new Vector3();
            line.normal_line = new Vector3();
            line.start_point.x = 1;
            line.start_point.y = 2;
            line.start_point.z = 3;
            line.normal_line.x = 4;
            line.normal_line.y = 5;
            line.normal_line.z = 6;

            input.ray_cluster.Add(line);
            List<RayLineCluster> output = new List<RayLineCluster>();

            TBTfront.Ant ant = new TBTfront.Ant();
            int res = ant.clacLight(param_list, input, output);
            
            Console.WriteLine(output.Count);
            Console.WriteLine(output[0].ray_cluster[0].start_point.x);
            Console.WriteLine(output[0].ray_cluster[0].normal_line.y);
            Console.ReadKey();
        }
    }
}
