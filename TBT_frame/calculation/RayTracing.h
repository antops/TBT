/*
*	created by liyun 2019/3/4
*   function 计算光线追踪 支持解析或stl两种格式 输出反射光线和交点以及是否相交
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

		// 输入解析参数
		void setAnalysis(const CompontParam& param);

		// 输入stl
		void setSTL(const CompontParam& param, vtkSmartPointer<vtkPolyData> data);

		// 计算直线与mirror相交，并输出交点与法线
		void calcNormalOfLine_Mirror(
			const Vector3& startPiont,
			Vector3 &normal,
			Vector3 &intersection,
			bool &isIntersect,
			double &t) {};

		// 计算反射，输入起点，方向，输出, isFilter是否过滤没有反射的点
		int calcReflect(
			const RayLineCluster & in,
			const CalcOption& opt,
			RayLineCluster & out);

		// 根据入射光线和法线求反射光线
		static Vector3 reflectLight(const Vector3& a, const Vector3& n);

	protected:
		virtual void prepare();

		// 将restrition指针变为数组 
		// 加限制条件，支持圆柱和立方体
		void switchRestriction();

		// 如果是解析需要 提前计算好变换矩阵
		void calcMatrix();

		int checkData();

		// 根据模型剖分数据计算反射
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

		// 三角形与直线相交判断
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

		// 直线与面相交 可能有两个解
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

		// 世界坐标系转到模型的相对坐标系矩阵（逆矩阵）先旋转后平移
		vector<Matrix4D> R_rotatMatrix;
		vector<Matrix4D> R_translateMatrix;

		// 模型的相对坐标系转到世界坐标矩阵
		vector<Matrix4D> rotatMatrix;
		vector<Matrix4D> translateMatrix; 

		CompontParam* restriction;
		std::vector<CompontParam*> restrictio_vec;

		vtkSmartPointer<vtkPolyData> polyData;
	};

}

#endif // RAYTRACING_H

