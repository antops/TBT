#include "ant.h"
#include "../TBT_frame/graph/node_base.h"
#include "../TBT_common/MyLog.h"
#include "Bridge.h"
TBTfront::Ant::Ant()
{
	return;
}

TBTfront::Ant::~Ant()
{
	return;
}

int TBTfront::Ant::clacLight(CompontList componts, RayLineCluster ^ input,
	System::Collections::Generic::List<RayLineCluster^>^ output,
	CalcOption^ option)
{
	count = 0;
	count++;
	std::vector<TBTBack::NodeBase*> compont_back;
	if (createCompont(componts, compont_back) != 0) {
		// create back componts failed
		return -1;
	}
	count++;
	TBTBack::RayLineCluster* input_back = new TBTBack::RayLineCluster();
	TBTBack::RayLineCluster* output_back = new TBTBack::RayLineCluster();
	TBTBack::CalcOption op_back;
	Bridge::transCalcOption(option, op_back);
	// 将输出转换为后台数据
	Bridge::transRayLineCluster(input, *output_back);
	output->Add(input);
	for (const auto& item : compont_back) {
		count++;
		std::swap(input_back, output_back);
		output_back->clear();

		size_t s = input_back->ray_cluster.size();
		s = output_back->ray_cluster.size();

		// 调用计算核
		item->clacLight(*input_back, op_back, *output_back);

		RayLineCluster^ line_cluster;
		Bridge::transRayLineCluster(*output_back, line_cluster);
		output->Add(line_cluster);
	}
	count++;
	// 得到数据结果在转换回来

	for (auto & x : compont_back) {
		if (x != nullptr) {
			delete x;
		}
	}
	delete input_back;
	delete output_back;
	return 0;
}

int TBTfront::Ant::clacGaussian(CompontList componts, GaussianCluster ^ input, 
	System::Collections::Generic::List<GaussianCluster^>^ output, 
	CalcOption ^ option)
{
	count = 0;
	count++;
	std::vector<TBTBack::NodeBase*> compont_back;
	if (createCompont(componts, compont_back) != 0) {
		// create back componts failed
		return -1;
	}
	count++;
	TBTBack::GaussianCluster* input_back = new TBTBack::GaussianCluster();
	TBTBack::GaussianCluster* output_back = new TBTBack::GaussianCluster();
	TBTBack::CalcOption op_back;
	Bridge::transCalcOption(option, op_back);
	// 将输出转换为后台数据
	Bridge::transGaussianCluster(input, *output_back);
	output->Add(input);
	for (const auto& item : compont_back) {
		count++;
		std::swap(input_back, output_back);
		output_back->clear();

		// 调用计算核
		item->clacGaussian(*input_back, op_back, *output_back);

		GaussianCluster^ line_cluster;
		Bridge::transGaussianCluster(*output_back, line_cluster);
		output->Add(line_cluster);
	}
	count++;
	// 得到数据结果在转换回来

	for (auto & x : compont_back) {
		if (x != nullptr) {
			delete x;
		}
	}
	delete input_back;
	delete output_back;
	return 0;
}

int TBTfront::Ant::clacField(CompontList componts, FieldBase ^ input, FieldBase ^ output)
{
	count = 0;

	count++;
	// 创建compont
	std::vector<TBTBack::NodeBase*> compont_back;
	if (createCompont(componts, compont_back) != 0) {
		// create back componts failed
		return -1;
	}
	count++;

	TBTBack::FieldBase* input_back = new TBTBack::FieldBase();
	TBTBack::FieldBase* output_back = new TBTBack::FieldBase();
	// 将输出转换为后台数据
	Bridge::transFieldBase(input, *output_back);

	for (const auto& item : compont_back) {
		count++;
		std::swap(input_back, output_back);
		// need clear output_back
		output_back->clear();

		// 调用计算核
		item->clacField(*input_back, *output_back);
	}
	Bridge::transFieldBase(*output_back, output);
	count++;
	// 得到数据结果在转换回来

	for (auto & x : compont_back) {
		if (x != nullptr) {
			delete x;
		}
	}
	delete input_back;
	delete output_back;
	return 0;
}

int TBTfront::Ant::createCompont(CompontList compont_front, std::vector<TBTBack::NodeBase*>& compont_back)
{
	for (int i = 0; i < compont_front->Count; i++) {
		TBTBack::CompontParam param;
		Bridge::transCompontParam(compont_front[i], param);
		TBTBack::NodeBase* item = TBTBack::NodeBaseFactory::genCompont(param);
		if (item == nullptr) {
			for (auto & x : compont_back) {
				if (x != nullptr) {
					delete x;
				}
			}
			compont_back.clear();
			return -1;
		}
		compont_back.push_back(item);
	}
	return 0;
}

void TBTfront::Ant::coordinateUV2Rotate(Coordinate ^coor)
{
	TBTBack::Coordinate back_coor;
	TBTBack::Vector3 u_back;
	TBTBack::Vector3 v_back;
	Bridge::transVector3(coor->U, u_back);
	Bridge::transVector3(coor->V, v_back);
	Bridge::transVector3(coor->pos, back_coor.pos);

	back_coor.setUV(u_back, v_back);
	Vector3^ axis;

	Bridge::transVector3(back_coor.rotate_axis, axis);
	coor->rotate_axis = axis;
	coor->rotate_theta = back_coor.rotate_theta;
	return;

}

void TBTfront::Ant::coordinateRotate2UV(Coordinate ^coor)
{
	TBTBack::Coordinate back_coor;
	TBTBack::Vector3 rotate_axis_back;
	Bridge::transVector3(coor->rotate_axis, rotate_axis_back);
	Bridge::transVector3(coor->pos, back_coor.pos);
	back_coor.setRotate(rotate_axis_back, coor->rotate_theta);
	Vector3^ u;
	Vector3^ v;
	Bridge::transVector3(back_coor.U, u);
	Bridge::transVector3(back_coor.V, v);
	coor->U = u;
	coor->V = v;
	return;
}

