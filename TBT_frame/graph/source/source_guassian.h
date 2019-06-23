/*
*	created by liyun 2019/3/3
*   function mirror的基类 集成CompontBase
*   version 1.0
*/
#pragma once
#include "source_base.h"

namespace TBTBack {
	class SourceGuassian : public SourceBase
	{
	public:
		SourceGuassian();
		virtual ~SourceGuassian();

		// 初始化参数，计算前必须调用
		virtual int init(CompontParam& param);

		// 计算场对于源，只输出
		virtual int clacField(const FieldBase& in, FieldBase& out, int* count = nullptr, bool is_end = true);

		virtual int clacLight(const RayLineCluster& in, const CalcOption opt, RayLineCluster& out);

		// 计算高斯 暂时不实现
		virtual int clacGaussian(const GaussianCluster& in, const CalcOption opt, GaussianCluster& out);

	protected:
		void CreateGaussian(double r, double w0, double fre,
			double z0, double &Real, double &Imag) const;

		void CreateGaussianGaussain(FieldBase& out);

	};

}