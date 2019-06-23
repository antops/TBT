/*
*	created by liyun 2019/3/3
*   function mirror�Ļ��� ����CompontBase
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

		// ��ʼ������������ǰ�������
		virtual int init(CompontParam& param) { return NodeBase::init(param); };

		// ���㳡
		virtual int clacField(const FieldBase& in, FieldBase& out, int* count = nullptr, bool is_end = true) = 0;

		// �������
		virtual int clacLight(const RayLineCluster& in, const CalcOption opt, RayLineCluster& out) = 0;

		// �����˹ ��ʱ��ʵ��
		virtual int clacGaussian(const GaussianCluster& in, const CalcOption opt, GaussianCluster& out) { return 0; };

	protected:
		//int calcRestriction();
	};

}