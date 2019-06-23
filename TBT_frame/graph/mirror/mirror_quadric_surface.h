/*
*	created by liyun 2017/11/28
*   function 二次曲面 
*   F(x,y,z) = a0*x^2 + a1*y^2 + a2*z^2 + a3*x*y + a4*y*z + a5*x*z + a6*x + a7*y + a8*z + a9
*   a0~a9 对应 m_data[0]~m_data[9]
*   xmin xmax ymin ymax zmin zmax 对应 m_data[10]~m_data[15]
*   version 1.0
*/

#ifndef QUADRICSURFACEMIRROR_H  
#define QUADRICSURFACEMIRROR_H

#include "node_mirror.h"
#include <vector>
namespace TBTBack {
	class MirrorQuadricSurface : public NodeMirror
	{
	public:
		MirrorQuadricSurface();

		virtual ~MirrorQuadricSurface();

		// 初始化参数，计算前必须调用
		virtual int init(CompontParam& param);

		// 计算场
		virtual int clacField(const FieldBase& in, FieldBase& out, int* count = nullptr, bool is_end = true);

		// 计算光线
		virtual int clacLight(const RayLineCluster& in, const CalcOption opt, RayLineCluster& out);


		virtual int clacGaussian(const GaussianCluster& in, const CalcOption opt, GaussianCluster& out);

	private:


	};
}


#endif // QUADRICSURFACEMIRROR_H