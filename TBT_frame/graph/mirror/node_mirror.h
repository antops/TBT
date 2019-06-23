/*
*	created by liyun 2019/3/3
*   function mirror的基类 集成CompontBase
*   version 1.0
*/
#pragma once
#include "../node_base.h"

namespace TBTBack {
	class NodeMirror : public NodeBase
	{
	public:
		NodeMirror() {};
		virtual ~NodeMirror() {};

		// 初始化参数，计算前必须调用
		virtual int init(CompontParam& param) { return NodeBase::init(param); };

		// 计算场
		virtual int clacField(const FieldBase& in, FieldBase& out, int* count = nullptr, bool is_end = true) = 0;

		// 计算光线
		virtual int clacLight(const RayLineCluster& in, const CalcOption opt, RayLineCluster& out) = 0;

		// 计算高斯 暂时不实现
		virtual int clacGaussian(const GaussianCluster& in, const CalcOption opt, GaussianCluster& out) { return 0; };

	protected:
		//int calcRestriction();
	};

}