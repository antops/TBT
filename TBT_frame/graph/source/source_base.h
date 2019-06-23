/*
*	created by liyun 2019/3/27
*   source base�Ļ��� ����CompontBase
*   version 1.0
*/
#pragma once
#include "../node_base.h"

namespace TBTBack {
	class SourceBase : public NodeBase
	{
	public:
		SourceBase();
		virtual ~SourceBase();

		// ��ʼ������������ǰ�������
		virtual int init(CompontParam& param);

		// ���㳡����Դ��ֻ���
		virtual int clacField(const FieldBase& in, FieldBase& out, int* count = nullptr, bool is_end = true) = 0;

		// ������߶���Դ��ֻ���
		virtual int clacLight(const RayLineCluster& in, const CalcOption opt, RayLineCluster& out);

		// �����˹ ��ʱ��ʵ��
		virtual int clacGaussian(const GaussianCluster& in, const CalcOption opt, GaussianCluster& out);

	protected:

	};

}