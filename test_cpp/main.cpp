#include <iostream>
#include <vector>
#include <string>
#include <ctime>
#include "../TBT_frame/graph/node_base.h"
#include <vtkSmartPointer.h>
#include <vtkPolyData.h>
using namespace TBTBack;
int main()
{
	CompontParam param;
	param.param_str = "C:/Users/ly/Desktop/test.stl";
	param.name = "stl";
	NodeBase* mirror = NodeBaseFactory::genCompont(param);
	RayLineCluster in;
	RayLine ray;
	ray.start_point = Vector3(0, 0, 1);
	ray.normal_line = Vector3(-0.3, 0.2, -2);
	//in.ray_cluster.push_back(ray);

	int num = 100;
	for (int i = 0; i < num; i++) {
		RayLine ray;
		ray.start_point = Vector3(double(i)/100.0, double(i) / 100.0, 1.0);
		ray.normal_line = Vector3(0, 0.1, -1);
		in.ray_cluster.push_back(ray);
	}

	CalcOption opt;
	RayLineCluster out;
	clock_t startTime, endTime;
	startTime = clock();//计时开始
	mirror->clacLight(in, opt, out);
	endTime = clock();//计时结束
	int numi = 5;
	for (int i = numi; i < numi + 5; i++) {
		auto& x = out.ray_cluster[i];
		std::cout << "r:" << "[" << x.start_point.x << "," << x.start_point.y << "," << x.start_point.z << "]"<< std::endl;
		std::cout << "n:" << "[" << x.normal_line.x << "," << x.normal_line.y << "," << x.normal_line.z << "]" << std::endl;
	}
	
	std::cout << "The run time is: " << (double)(endTime - startTime) / CLOCKS_PER_SEC << "s" << endl;
	out.clear();
	opt.is_use_index_reflect = true;
	startTime = clock();//计时开始
	mirror->clacLight(in, opt, out);
	endTime = clock();//计时结束
	std::cout << "use_index_reflect" << std::endl;
	for (int i = numi; i < numi + 5; i++) {
		auto& x = out.ray_cluster[i];
		std::cout << "r:" << "[" << x.start_point.x << "," << x.start_point.y << "," << x.start_point.z << "]" << std::endl;
		std::cout << "n:" << "[" << x.normal_line.x << "," << x.normal_line.y << "," << x.normal_line.z << "]" << std::endl;
	}
	
	std::cout << "The run time is: " << (double)(endTime - startTime) / CLOCKS_PER_SEC << "s" << endl;

	getchar();
	return 0;
}