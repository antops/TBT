/*
*	created by liyun 2019/3/3
*   function compont���� ��Ϊ�ӿ����
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

		// ��ʼ������������ǰ�������
		virtual int init(CompontParam& param);

		// ���㳡
		virtual int clacField(const FieldBase& in, FieldBase& out, int* count = nullptr, bool is_end = true) = 0;

		// �������
		virtual int clacLight(const RayLineCluster& in, const CalcOption opt, RayLineCluster& out) = 0;

		// �����˹
		virtual int clacGaussian(const GaussianCluster& in, const CalcOption opt, GaussianCluster& out) = 0;
	protected:
		CompontParam  cpt_param_;
	};

	// ����
	class __declspec(dllexport) NodeBaseFactory
	{
	public:
		static NodeBase* genCompont(CompontParam& param);

	};
}