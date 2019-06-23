#pragma once
#include "node_mirror.h"
#include <string>
#include <vtkSmartPointer.h>
#include <vtkPolyData.h>
namespace TBTBack {
	class MirrorSTL : public NodeMirror
	{
	public:
		MirrorSTL();

		virtual ~MirrorSTL();

		// 初始化参数，计算前必须调用
		virtual int init(CompontParam& param);

		// 计算场
		virtual int clacField(const FieldBase& in, FieldBase& out, int* count = nullptr, bool is_end = true);

		// 计算光线
		virtual int clacLight(const RayLineCluster& in, const CalcOption opt, RayLineCluster& out);

		virtual int clacGaussian(const GaussianCluster& in, const CalcOption opt, GaussianCluster& out);
	private:
		std::string stl_path_;
		vtkSmartPointer<vtkPolyData> polyData_;
	};
}