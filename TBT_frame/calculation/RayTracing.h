/*
*	created by liyun 2019/3/4
*   function �������׷�� ֧�ֽ�����stl���ָ�ʽ ���������ߺͽ����Լ��Ƿ��ཻ
*   version 1.0
*/

#ifndef RAYTRACING_H  
#define RAYTRACING_H

#include "../../TBT_common/Vector3.h"
#include "../../TBT_common/interface_cpp.h"
#include "../../TBT_common/Matrix4D.h"
#include "../../TBT_common/Vector3D.h"
#include <vector>
#include <vtkPolyData.h>
#include <vtkSmartPointer.h>
using std::vector;

namespace TBTBack
{
	class RayTracing
	{
	public:
		RayTracing();

		virtual ~RayTracing();

		// �����������
		void setAnalysis(const CompontParam& param);

		// ����stl
		void setSTL(const CompontParam& param, vtkSmartPointer<vtkPolyData> data);

		// ����ֱ����mirror�ཻ������������뷨��
		void calcNormalOfLine_Mirror(
			const Vector3& startPiont,
			Vector3 &normal,
			Vector3 &intersection,
			bool &isIntersect,
			double &t) {};

		// ���㷴�䣬������㣬�������, isFilter�Ƿ����û�з���ĵ�
		int calcReflect(
			const RayLineCluster & in,
			const CalcOption& opt,
			RayLineCluster & out);

		// ����������ߺͷ����������
		static Vector3 reflectLight(const Vector3& a, const Vector3& n);

	protected:
		virtual void prepare();

		// ��restritionָ���Ϊ���� 
		// ������������֧��Բ����������
		void switchRestriction();

		// ����ǽ�����Ҫ ��ǰ����ñ任����
		void calcMatrix();

		int checkData();

		// ����ģ���ʷ����ݼ��㷴��
		void calcReflectByPolyDataBatch(
			const RayLineCluster & in,
			const CalcOption& opt,
			RayLineCluster & out);

		virtual void calcReflectByPolyData(
			const Vector3& startPiont,
			const Vector3& direction, 
			Vector3 &reflex,
			Vector3 &intersection,
			bool &isIntersect);

		void calcReflectByQuadricSurface(
			const Vector3& startPiont,
			const Vector3& direction, 
			Vector3 &reflex,
			Vector3 &intersection,
			bool &isIntersect);

		void calcNormalOfLine_MirrorByQuadricSurface(
			const Vector3& startPiont,
			const Vector3& direction,
			Vector3 &normal, 
			Vector3 &intersection,
			bool &isIntersect, 
			double &t);

		void calcNormalOfLine_MirrorByQuadricSurfaceBatch(
			const vector<Vector3>& startPiont,
			const vector<Vector3>& direction,
			vector<Vector3> &nomal,
			vector<Vector3> &intersection,
			vector<bool> &isIntersect, 
			vector<float> &port);

		void calcNormalOfLine_MirrorByPolyData(
			const Vector3& startPiont,
			const Vector3& direction,
			Vector3 &normal,
			Vector3 &intersection,
			bool &isIntersect, 
			double &t);

		// ��������ֱ���ཻ�ж�
		bool isIntersect(const Vector3 &orig,
			const Vector3 &dir,
			const Vector3 &v0, 
			const Vector3 &v1, 
			const Vector3 &v2,
			Vector3 &intersection, 
			double &t);

		bool ray_CurvedSurface(
			const vector<double> & a,
			Vector3 n, 
			Vector3 org,
			double &t, 
			Vector3 &interPoint);

		// ֱ�������ཻ ������������
		void line_CurvedSurface(
			const vector<double> & a,
			Vector3 n, 
			Vector3 org,
			double &t1,
			double &t2, 
			bool &isOk1, 
			bool &isOk2, 
			Vector3 &interPoint1,
			Vector3 &interPoint2);

		bool isInRestriction(const Vector3 &intersectionGlobal);

		void calcReflectByPolyDataIndex(
			const Vector3 & startPiont,
			const Vector3 & direction,
			Vector3 & reflex,
			Vector3 & intersection,
			bool & isIntersect);

	protected:

		enum TYPE_MIRROR
		{
			default_type = 0,
			analysis_type = 1,
			stl_type = 2
		};

		TYPE_MIRROR m_type;
		vector<double> m_data;
		Coordinate m_coor;
		bool m_calcmatrix_flag;

		// ��������ϵת��ģ�͵��������ϵ�������������ת��ƽ��
		vector<Matrix4D> R_rotatMatrix;
		vector<Matrix4D> R_translateMatrix;

		// ģ�͵��������ϵת�������������
		vector<Matrix4D> rotatMatrix;
		vector<Matrix4D> translateMatrix; 

		CompontParam* restriction;
		std::vector<CompontParam*> restrictio_vec;

		vtkSmartPointer<vtkPolyData> polyData;
	};

}

#endif // RAYTRACING_H

