/*
*	created by liyun 20190526
*   截面
*/

#ifndef RESTRICTION_H  
#define RESTRICTION_H

#include "node_mirror.h"
#include <vector>
namespace TBTBack {
	class Restriction : public NodeMirror
	{
	public:
		Restriction();

		virtual ~Restriction();

		// 初始化参数，计算前必须调用
		virtual int init(CompontParam& param);

		// 计算场
		virtual int clacField(const FieldBase& in, FieldBase& out, int* count = nullptr, bool is_end = true);

		// 计算光线
		virtual int clacLight(const RayLineCluster& in, const CalcOption opt, RayLineCluster& out);

		enum RestrictionType
		{
			RES_CYLINDER,
			RES_CUBE
		};

		// 判断restriction 类型
		static RestrictionType judgeType(double data);

	private:


	};
}


#endif // RESTRICTION_H