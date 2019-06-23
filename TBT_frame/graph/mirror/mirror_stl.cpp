#include "mirror_stl.h"
#include "../../calculation/RayTracing.h"
#include "../../calculation/ray_tracing_stl_index.h"
#include "../../../TBT_common/MyLog.h"
#include <vtkSTLReader.h>
namespace TBTBack {
	MirrorSTL::MirrorSTL()
	{
	}
	MirrorSTL::~MirrorSTL()
	{
	}
	int MirrorSTL::init(CompontParam & param)
	{
		int res = 0;
		res = NodeMirror::init(param);
		if (res != 0) return -1;
		LOG(INFO) << "stl path:" << param.param_str;
		stl_path_ = param.param_str;
		vtkSmartPointer<vtkSTLReader> reader =
			vtkSmartPointer<vtkSTLReader>::New();
		reader->SetFileName(stl_path_.c_str());
		reader->Update();
		polyData_ = reader->GetOutput();
		return 0;
	}

	int MirrorSTL::clacField(const FieldBase & in, FieldBase & out, int * count, bool is_end)
	{
		return 0;
	}

	int MirrorSTL::clacLight(const RayLineCluster & in, const CalcOption opt, RayLineCluster & out)
	{
		RayTracing* ray_trace = nullptr;
		//if (opt.is_use_index_reflect)
			ray_trace = new RayTracingSTLIndex;
		//else
		//	ray_trace = new RayTracing;
		ray_trace->setSTL(cpt_param_, polyData_);
		int ret = ray_trace->calcReflect(in, opt, out);
		delete ray_trace;
		return ret;
	}

	int MirrorSTL::clacGaussian(const GaussianCluster & in, const CalcOption opt, GaussianCluster & out)
	{
		return 0;
	}
}