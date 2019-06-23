#include "source_base.h"
#include "../../../TBT_common/Constant_Var.h"
#include "../../../TBT_common/MyLog.h"
TBTBack::SourceBase::SourceBase()
{
}

TBTBack::SourceBase::~SourceBase()
{
}

int TBTBack::SourceBase::init(CompontParam & param)
{
	return NodeBase::init(param);
}


int TBTBack::SourceBase::clacLight(const RayLineCluster& in, const CalcOption opt, RayLineCluster& out)
{
	// test
	RayLine line;
	line.normal_line = Vector3(0, 0, 1);
	line.start_point = Vector3(0, 0, 0);
	out.ray_cluster.push_back(line);
	line.normal_line = Vector3(0, -1, 1);
	line.start_point = Vector3(0, 0, 0);
	out.ray_cluster.push_back(line);
	line.normal_line = Vector3(0, 1, 1);
	line.start_point = Vector3(0, 0, 0);
	out.ray_cluster.push_back(line);
	return 0;
}

int TBTBack::SourceBase::clacGaussian(const GaussianCluster & in, const CalcOption opt, GaussianCluster & out)
{
	LOG(ERROR) << "out.angle" << 20;
	// test
	out.angle = 20;
	// ÆðÊ¼°ë¾¶
	out.start_radius = 0.1;
	// ÊÇ·ñ»ã¾Û
	out.is_foucs = false;
	//ÊøÑü
	out.foucs_radius = 0.001;

	out.distance = 1;
	RayLine line;
	line.start_point = Vector3(0, 0, 0);
	line.normal_line = Vector3(0, 0, 1);
	out.rayline.ray_cluster.push_back(line);

	out.coordinate = cpt_param_.coor;
	LOG(ERROR) << "out.angle" << 20;
	return 0;
}
