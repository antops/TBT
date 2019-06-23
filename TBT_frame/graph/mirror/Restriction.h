/*
*	created by liyun 20190526
*   ����
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

		// ��ʼ������������ǰ�������
		virtual int init(CompontParam& param);

		// ���㳡
		virtual int clacField(const FieldBase& in, FieldBase& out, int* count = nullptr, bool is_end = true);

		// �������
		virtual int clacLight(const RayLineCluster& in, const CalcOption opt, RayLineCluster& out);

		enum RestrictionType
		{
			RES_CYLINDER,
			RES_CUBE
		};

		// �ж�restriction ����
		static RestrictionType judgeType(double data);

	private:


	};
}


#endif // RESTRICTION_H