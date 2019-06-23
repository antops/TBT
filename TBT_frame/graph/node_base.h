/*
*	created by liyun 2019/3/3
*   function compont基类 作为接口输出
*   version 1.0
*/
#pragma once
#include "../../TBT_common/interface_cpp.h"

namespace TBTBack {
	class __declspec(dllexport) NodeBase
	{
	public:
		NodeBase();
		virtual ~NodeBase();

		// 初始化参数，计算前必须调用
		virtual int init(CompontParam& param);

		// 计算场
		virtual int clacField(const FieldBase& in, FieldBase& out, int* count = nullptr, bool is_end = true) = 0;

		// 计算光线
		virtual int clacLight(const RayLineCluster& in, const CalcOption opt, RayLineCluster& out) = 0;

		// 计算高斯
		virtual int clacGaussian(const GaussianCluster& in, const CalcOption opt, GaussianCluster& out) = 0;
	protected:
		CompontParam  cpt_param_;
	};

	// 生成
	class __declspec(dllexport) NodeBaseFactory
	{
	public:
		static NodeBase* genCompont(CompontParam& param);

	};
}