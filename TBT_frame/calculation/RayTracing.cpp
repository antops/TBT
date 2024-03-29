#include "RayTracing.h"
#include "../../TBT_common/MyLog.h"
#include "../graph/mirror/Restriction.h"
#include <set>

#define THRESHOLD_RAY 0.0000001
static bool IS_USE_CUDE = false;

namespace TBTBack {
	RayTracing::RayTracing()
	{
		m_type = default_type;
		m_calcmatrix_flag = false;
		restriction = nullptr;
	}

	RayTracing::~RayTracing()
	{
	}

	void RayTracing::setAnalysis(const CompontParam& param)
	{
		m_coor = param.coor;
		m_data.assign(param.param.begin(), param.param.end());
		m_type = analysis_type;
		restriction = param.restriction;
		calcMatrix();
		return;
	}

	void RayTracing::setSTL(const CompontParam& param, vtkSmartPointer<vtkPolyData> data)
	{
		polyData = data;
		m_coor = param.coor;
		m_type = stl_type;
		restriction = param.restriction;
		return;
	}

	int RayTracing::calcReflect(
		const RayLineCluster & in,
		const CalcOption& opt,
		RayLineCluster & out)
	{
		if (m_type == default_type) {
		    // 未设置 type
			return -1;
		}
		prepare();
		if (!opt.is_ign_restriction) switchRestriction();
		if (m_type == analysis_type) {
			out.ray_cluster.reserve(in.ray_cluster.size());
			for (const auto& item : in.ray_cluster) {
				RayLine reflect;
				bool isIntersect = false;
				calcReflectByQuadricSurface(item.start_point, item.normal_line,
					reflect.normal_line, reflect.start_point, isIntersect);

				if (!isIntersect && !opt.is_ign_non_intersection) {
					// 保留反射
					out.ray_cluster.push_back(item);
				}
				else {
					out.ray_cluster.push_back(reflect);
				}
			}
		}
		else if (m_type == stl_type) {
			calcReflectByPolyDataBatch(in, opt, out);
			return 0;
		}
		else {
			return -1;
		}
		return 0;
	}

	void RayTracing::calcReflectByPolyDataBatch(
		const RayLineCluster & in,
		const CalcOption& opt,
		RayLineCluster & out)
	{
		
		if (IS_USE_CUDE)
		{/*
			// 以后加 判断是否能运行cuda
			//CUDARayTracing::getCUDAInfo();
			CUDARayTracing cudaAPI;
			cudaAPI.setSTL(mirror->getPolyData().GetPointer());
			cudaAPI.setRays(startPiont, direction);
			if (cudaAPI.run() != 0)
			{
				return;
			}
			cudaAPI.getRes(nomal, intersection, isIntersect, port);
*/
		}
		else
		{
			out.ray_cluster.reserve(in.ray_cluster.size());
			for (const auto& item : in.ray_cluster) {
				RayLine reflect;
				bool isIntersect = false;
				calcReflectByPolyData(item.start_point, item.normal_line,
					reflect.normal_line, reflect.start_point, isIntersect);

				if (!isIntersect && !opt.is_ign_non_intersection) {
					// 保留反射
					out.ray_cluster.push_back(item);
				}
				else {
					out.ray_cluster.push_back(reflect);
				}
			}
		}
	}

	void RayTracing::calcReflectByPolyData(
		const Vector3 & startPiont,
		const Vector3 & direction, 
		Vector3 & reflex,
		Vector3 & intersection,
		bool & isIntersect)
	{
		int EleNum = polyData->GetNumberOfCells();
		double t;
		for (int i = 0; i < EleNum; i++)  //求与反射面的交点
		{
			vtkIdList * p;
			p = polyData->GetCell(i)->GetPointIds();
			double * point;
			point = polyData->GetPoint(p->GetId(0));
			Vector3 NodesXYZ1(point[0], point[1], point[2]);
			point = polyData->GetPoint(p->GetId(1));
			Vector3 NodesXYZ2(point[0], point[1], point[2]);
			point = polyData->GetPoint(p->GetId(2));
			Vector3 NodesXYZ3(point[0], point[1], point[2]);
			if (this->isIntersect(startPiont, direction, NodesXYZ1,
				NodesXYZ2, NodesXYZ3, intersection, t))
			{
				if (t >= 0)
				{
					if (!isInRestriction(intersection)) //不满足限制条件
					{
						intersection = startPiont; // 让交点等于起点 方向不变 避免对无交点时特殊处理
						isIntersect = false;
						return;
					}
					Vector3 tempa = NodesXYZ1 - NodesXYZ2;
					Vector3 tempb = NodesXYZ1 - NodesXYZ3;
					Vector3 n_light = tempa.Cross(tempb);  //法向量

					isIntersect = true;
					reflex = reflectLight(direction, n_light);
					return;
				}
			}
		}
		isIntersect = false;
	}

	void RayTracing::calcReflectByQuadricSurface(
		const Vector3 & startPiont,
		const Vector3 & direction, 
		Vector3 & reflex, 
		Vector3 & intersection, 
		bool & isIntersect)
	{
		double t;
		const vector<double>& tempData = m_data;


		// 将世界坐标系转到模型的相对坐标系
		Vector3 tempStartPiont = R_translateMatrix[0] * R_rotatMatrix[0] * startPiont;
		Vector3 tempDirection = R_rotatMatrix[0] * direction; // 向量只用求旋转

		if (ray_CurvedSurface(tempData, tempDirection, tempStartPiont, t, intersection))
		{
			Vector3 intersectionGlobal = translateMatrix[0] * rotatMatrix[0] * intersection;

			if (!isInRestriction(intersectionGlobal)) //不满足限制条件
			{
				intersection = startPiont; // 让交点等于起点 方向不变 避免对无交点时特殊处理
				isIntersect = false;
				return;
			}

			double x = 2 * tempData[0] * intersection.x + tempData[3] * intersection.y +
				tempData[5] * intersection.z + tempData[6];
			double y = 2 * tempData[1] * intersection.y + tempData[3] * intersection.x +
				tempData[4] * intersection.z + tempData[7];
			double z = 2 * tempData[2] * intersection.z + tempData[4] * intersection.y +
				tempData[5] * intersection.x + tempData[8];
			Vector3 tempn(x, y, z);
			if (tempn.Dot(tempDirection) > 0.0)
				tempn.set(-x, -y, -z);

			reflex = reflectLight(tempDirection, tempn);
			isIntersect = true;

			// 将模型的相对坐标系转到世界坐标系
			intersection = intersectionGlobal;
			reflex = rotatMatrix[0] * reflex; // 更新方向

		}
		else
		{
			intersection = startPiont; // 让交点等于起点 方向不变 避免对无交点时特殊处理
			isIntersect = false;
		}

		return;
	}

	void RayTracing::calcNormalOfLine_MirrorByQuadricSurface(
		const Vector3 & startPiont, const Vector3 & direction,
		Vector3 & normal, Vector3 & intersection, bool & isIntersect, double & t)
	{
		/*
		const vector<double>& tempData = data;

		if (!m_calcmatrix_flag)
		{
			this->calcMatrix();
		}

		// 将世界坐标系转到模型的相对坐标系
		Vector3 tempStartPiont = R_translateMatrix[0] * R_rotatMatrix[0] * startPiont;
		Vector3 tempDirection = R_rotatMatrix[0] * direction; // 向量只用求旋转

		double t1, t2;
		bool isOK1 = false;
		bool isOK2 = false;
		Vector3 intersection1, intersection2;
		line_CurvedSurface(tempData, tempDirection, tempStartPiont, t1, t2, isOK1,
			isOK2, intersection1, intersection2);
		if (isOK1&&isOK2)
		{
			Vector3 intersectionGlobal1 = translateMatrix[0] *
				rotatMatrix[0] * intersection1;
			Vector3 intersectionGlobal2 = translateMatrix[0] *
				rotatMatrix[0] * intersection2;
			bool isInRestriction1 = isInRestriction(intersectionGlobal1);
			bool isInRestriction2 = isInRestriction(intersectionGlobal2);
			if (isInRestriction1&&isInRestriction2) // 两个解都满足 取绝地值小的
			{
				if (abs(t1) > abs(t2))
				{
					t = t2;
					intersection = intersection2;
				}
				else
				{
					t = t1;
					intersection = intersection1;
				}
			}
			else if (isInRestriction1)
			{
				t = t1;
				intersection = intersection1;
			}
			else if (isInRestriction2)
			{
				t = t2;
				intersection = intersection2;
			}
			else
			{
				intersection = startPiont; // 让交点等于起点 方向不变 避免对无交点时特殊处理
				isIntersect = false;
				return;
			}
		}
		else if (isOK1)
		{
			Vector3 intersectionGlobal1 = translateMatrix[0] *
				rotatMatrix[0] * intersection1;
			if (isInRestriction(intersectionGlobal1))
			{
				t = t1;
				intersection = intersection1;
			}
			else
			{
				intersection = startPiont; // 让交点等于起点 方向不变 避免对无交点时特殊处理
				isIntersect = false;
				return;
			}
		}
		else if (isOK2)
		{
			Vector3 intersectionGlobal2 = translateMatrix[0] *
				rotatMatrix[0] * intersection2;
			if (isInRestriction(intersectionGlobal2))
			{
				t = t2;
				intersection = intersection2;
			}
			else
			{
				intersection = startPiont; // 让交点等于起点 方向不变 避免对无交点时特殊处理
				isIntersect = false;
				return;
			}
		}
		else
		{
			intersection = startPiont; // 让交点等于起点 方向不变 避免对无交点时特殊处理
			isIntersect = false;
			return;
		}
		double x = 2 * tempData[0] * intersection.x + tempData[3] * intersection.y +
			tempData[5] * intersection.z + tempData[6];
		double y = 2 * tempData[1] * intersection.y + tempData[3] * intersection.x +
			tempData[4] * intersection.z + tempData[7];
		double z = 2 * tempData[2] * intersection.z + tempData[4] * intersection.y +
			tempData[5] * intersection.x + tempData[8];
		Vector3 tempn(x, y, z);
		if (tempn.Dot(tempDirection) > 0.0)
			tempn.set(-x, -y, -z);

		intersection = translateMatrix[0] *
			rotatMatrix[0] * intersection;
		normal = rotatMatrix[0] * tempn; // 更新方向
		isIntersect = true;
		*/
	}

	void RayTracing::calcNormalOfLine_MirrorByQuadricSurfaceBatch(
		const vector<Vector3>& startPiont, const vector<Vector3>& direction,
		vector<Vector3>& nomal, vector<Vector3>& intersection,
		vector<bool>& isIntersect, vector<float>& port)
	{
		for (int i = 0; i < startPiont.size(); i++)
		{
			bool isTmep = false;
			double t = 0;
			calcNormalOfLine_MirrorByQuadricSurface(startPiont[i], direction[i], nomal[i],
				intersection[i], isTmep, t);
			isIntersect[i] = isTmep;
			port[i] = t;
		}
	}

	void RayTracing::calcNormalOfLine_MirrorByPolyData(
		const Vector3 & startPiont, const Vector3 & direction, Vector3 & normal,
		Vector3 & intersection, bool & isIntersect, double & t)
	{
		/*
		vtkSmartPointer<vtkPolyData> polyData = mirror->getPolyData();
		int EleNum = polyData->GetNumberOfCells();
		for (int i = 0; i < EleNum; i++)  //求与反射面的交点
		{
			vtkIdList * p;
			p = polyData->GetCell(i)->GetPointIds();
			double * point;
			point = polyData->GetPoint(p->GetId(0));
			Vector3 NodesXYZ1(point[0], point[1], point[2]);
			point = polyData->GetPoint(p->GetId(1));
			Vector3 NodesXYZ2(point[0], point[1], point[2]);
			point = polyData->GetPoint(p->GetId(2));
			Vector3 NodesXYZ3(point[0], point[1], point[2]);
			if (this->isIntersect(startPiont, direction, NodesXYZ1,
				NodesXYZ2, NodesXYZ3, intersection, t))
			{
				Vector3 tempa = NodesXYZ1 - NodesXYZ2;
				Vector3 tempb = NodesXYZ1 - NodesXYZ3;
				normal = tempa.Cross(tempb);  //法向量

				isIntersect = true;
				return;
			}
		}
		isIntersect = false;
		*/
	}

	bool RayTracing::isIntersect(const Vector3 & orig, const Vector3 & dir,
		const Vector3 & v0, const Vector3 & v1, const Vector3 & v2,
		Vector3 & intersection, double & t)
	{
		double u, v;
		// E1
		Vector3 E1 = v1 - v0;

		// E2
		Vector3 E2 = v2 - v0;

		// P
		Vector3 P = dir.Cross(E2);

		// determinant
		double det = E1.Dot(P);

		Vector3 T;
		T = orig - v0;

		// If determinant is near zero, ray lies in plane of triangle
		//if (det < 0.00000001 && det > -0.00000001)
		//	return false;

		// Calculate u and make sure u <= 1
		u = T.Dot(P);
		double fInvDet = 1.0f / det;
		u *= fInvDet;

		if (u < 0.0 || u > 1)
			return false;

		// Q
		Vector3 Q = T.Cross(E1);

		// Calculate v and make sure u + v <= 1
		v = dir.Dot(Q);
		v *= fInvDet;
		if (v < 0.0 || u + v > 1)
			return false;

		// Calculate t, scale parameters, ray intersects triangle
		t = E2.Dot(Q);
		t *= fInvDet;

		intersection = orig + dir * t;

		return true;
	}

	bool RayTracing::ray_CurvedSurface(const vector<double> &a,
		Vector3 n, Vector3 org, double & t, Vector3 & interPoint)
	{
		double x0 = org.x, y0 = org.y, z0 = org.z;
		double x1 = n.x, y1 = n.y, z1 = n.z;

		double A = a[0] * x1 * x1 + a[1] * y1 * y1 + a[2] * z1 * z1 + a[3] * x1 * y1 +
			a[4] * z1 * y1 + a[5] * x1 * z1;
		double B = 2 * a[0] * x1 * x0 + 2 * a[1] * y1 * y0 + 2 * a[2] * z1 * z0 +
			a[3] * (x0 * y1 + x1 * y0) + a[4] * (z0 * y1 + z1 * y0) + a[5] * (z0 * x1 + z1 * x0) +
			a[6] * x1 + a[7] * y1 + a[8] * z1;
		double C = a[0] * x0 * x0 + a[1] * y0 * y0 + a[2] * z0 * z0 +
			a[3] * x0 * y0 + a[4] * z0 * y0 + a[5] * x0 * z0 +
			a[6] * x0 + a[7] * y0 + a[8] * z0 + a[9];

		if (A < -THRESHOLD_RAY || A > THRESHOLD_RAY)
		{
			double temp = B * B - 4 * A * C;
			if (temp >= 0)
				temp = pow(temp, 0.5);
			else
				return false;

			double tempt1, tempt2;
			tempt1 = (-B + temp) / 2.0 / A;
			tempt2 = (-B - temp) / 2.0 / A; // 求根公式的两个解

			if (tempt1 >= 0.0 && tempt2 >= 0.0) // 都大于等于0 需要先比较是否在都在限制条件内
			{
				bool isIn1 = false;
				Vector3 interPoint1(x0 + x1 * tempt1, y0 + y1 * tempt1, z0 + z1 * tempt1);
				// 判断是否在给出的区间内
				if (a[10] - THRESHOLD_RAY < interPoint1.x  &&
					interPoint1.x < a[11] + THRESHOLD_RAY &&
					a[12] - THRESHOLD_RAY < interPoint1.y &&
					interPoint1.y < a[13] + THRESHOLD_RAY &&
					a[14] - THRESHOLD_RAY < interPoint1.z &&
					interPoint1.z < a[15] + THRESHOLD_RAY)
				{
					isIn1 = true;
				}
				bool isIn2 = false;
				Vector3 interPoint2(x0 + x1 * tempt2, y0 + y1 * tempt2, z0 + z1 * tempt2);
				// 判断是否在给出的区间内
				if (a[10] - THRESHOLD_RAY < interPoint2.x  &&
					interPoint2.x < a[11] + THRESHOLD_RAY &&
					a[12] - THRESHOLD_RAY < interPoint2.y &&
					interPoint2.y < a[13] + THRESHOLD_RAY &&
					a[14] - THRESHOLD_RAY < interPoint2.z &&
					interPoint2.z < a[15] + THRESHOLD_RAY)
				{
					isIn2 = true;
				}
				if (isIn1 && isIn2)
				{
					if (tempt1 > tempt2)
					{
						interPoint = interPoint1;
						return true;
					}
					else
					{
						interPoint = interPoint2;
						return true;
					}

				}
				else if (isIn1 && !isIn2)
				{
					interPoint = interPoint1;
					return true;
				}
				else if (!isIn1 && isIn2)
				{
					interPoint = interPoint2;
					return true;
				}
				else
				{
					return false;
				}

			}
			else if (tempt1 < 0.0 && tempt2 < 0.0) // 都小于0 无解
			{
				return false;
			}
			else                           // 取正值
			{
				if (tempt1 < tempt2)
					t = tempt2;
				else
					t = tempt1;
			}
		}
		else                          // 只有一个交点，与法线平行
			t = -C / B;

		if (t < 0.0)
			return false;
		else
		{
			// 判断是否在给出的区间内
			interPoint.set(x0 + x1 * t, y0 + y1 * t, z0 + z1 * t);
			if (a[10] - THRESHOLD_RAY < interPoint.x  &&
				interPoint.x < a[11] + THRESHOLD_RAY &&
				a[12] - THRESHOLD_RAY < interPoint.y &&
				interPoint.y < a[13] + THRESHOLD_RAY &&
				a[14] - THRESHOLD_RAY < interPoint.z &&
				interPoint.z < a[15] + THRESHOLD_RAY)
			{
				return true;
			}
			else
				return false;
		}
	}

	void RayTracing::line_CurvedSurface(const vector<double>& a,
		Vector3 n, Vector3 org, double & t1, double & t2, bool & isOk1, bool & isOk2,
		Vector3 &interPoint1, Vector3 &interPoint2)
	{
		double x0 = org.x, y0 = org.y, z0 = org.z;
		double x1 = n.x, y1 = n.y, z1 = n.z;

		double A = a[0] * x1 * x1 + a[1] * y1 * y1 + a[2] * z1 * z1 + a[3] * x1 * y1 +
			a[4] * z1 * y1 + a[5] * x1 * z1;
		double B = 2 * a[0] * x1 * x0 + 2 * a[1] * y1 * y0 + 2 * a[2] * z1 * z0 +
			a[3] * (x0 * y1 + x1 * y0) + a[4] * (z0 * y1 + z1 * y0) + a[5] * (z0 * x1 + z1 * x0) +
			a[6] * x1 + a[7] * y1 + a[8] * z1;
		double C = a[0] * x0 * x0 + a[1] * y0 * y0 + a[2] * z0 * z0 +
			a[3] * x0 * y0 + a[4] * z0 * y0 + a[5] * x0 * z0 +
			a[6] * x0 + a[7] * y0 + a[8] * z0 + a[9];

		if (A < -THRESHOLD_RAY || A > THRESHOLD_RAY)
		{
			double temp = B * B - 4 * A * C;
			if (temp >= 0)
				temp = pow(temp, 0.5);
			else
			{
				isOk1 = false;
				isOk2 = false;
				return;
			}
			double tempt1, tempt2;
			tempt1 = (-B + temp) / 2.0 / A;
			tempt2 = (-B - temp) / 2.0 / A; // 求根公式的两个解

			interPoint1.set(x0 + x1 * tempt1, y0 + y1 * tempt1, z0 + z1 * tempt1);
			// 判断是否在给出的区间内
			if (a[10] - THRESHOLD_RAY < interPoint1.x  &&
				interPoint1.x < a[11] + THRESHOLD_RAY &&
				a[12] - THRESHOLD_RAY < interPoint1.y &&
				interPoint1.y < a[13] + THRESHOLD_RAY &&
				a[14] - THRESHOLD_RAY < interPoint1.z &&
				interPoint1.z < a[15] + THRESHOLD_RAY)
			{
				t1 = tempt1;
				isOk1 = true;
			}
			interPoint2.set(x0 + x1 * tempt2, y0 + y1 * tempt2, z0 + z1 * tempt2);
			// 判断是否在给出的区间内
			if (a[10] - THRESHOLD_RAY < interPoint2.x  &&
				interPoint2.x < a[11] + THRESHOLD_RAY &&
				a[12] - THRESHOLD_RAY < interPoint2.y &&
				interPoint2.y < a[13] + THRESHOLD_RAY &&
				a[14] - THRESHOLD_RAY < interPoint2.z &&
				interPoint2.z < a[15] + THRESHOLD_RAY)
			{
				t2 = tempt2;
				isOk2 = true;
			}
		}
		else // 只有一个交点，与法线平行
		{
			isOk2 = false;
			double tempt1 = -C / B;
			interPoint1.set(x0 + x1 * tempt1, y0 + y1 * tempt1, z0 + z1 * tempt1);
			if (a[10] - THRESHOLD_RAY < interPoint1.x  &&
				interPoint1.x < a[11] + THRESHOLD_RAY &&
				a[12] - THRESHOLD_RAY < interPoint1.y &&
				interPoint1.y < a[13] + THRESHOLD_RAY &&
				a[14] - THRESHOLD_RAY < interPoint1.z &&
				interPoint1.z < a[15] + THRESHOLD_RAY)
			{
				t1 = tempt1;
				isOk1 = true;
			}
			else
			{
				isOk1 = false;
			}
		}
	}

	bool RayTracing::isInRestriction(const Vector3 & intersectionGlobal)
	{
		for (size_t i = 0; i < restrictio_vec.size(); i++)
		{
			const vector<double>& tempRestrictionData = restrictio_vec[i]->param;
			// 将世界坐标系转到限制条件的相对坐标系
			if (tempRestrictionData.size() < 4) continue;

			Vector3 intersectionLocal = R_translateMatrix[i + 1] *
				R_rotatMatrix[i + 1] * intersectionGlobal;
			if (intersectionLocal.z > tempRestrictionData[1] ||
				intersectionLocal.z < 0.0)
			{
				return false;
			}
			if (Restriction::judgeType(tempRestrictionData[2]) == Restriction::RES_CYLINDER)
			{
				double tempRadius = intersectionLocal.x * intersectionLocal.x +
					intersectionLocal.y * intersectionLocal.y;

				if (tempRadius > tempRestrictionData[0] * tempRestrictionData[0])
				{
					return false;
				}
			}
			else
			{
				if (abs(intersectionLocal.x) > tempRestrictionData[0] / 2 ||
					abs(intersectionLocal.y) > tempRestrictionData[0] / 2)
					return false;
			}

		}
		return true;
	}

	Vector3 RayTracing::reflectLight(const Vector3 & a, const Vector3 & n)
	{
		Vector3 tempN = n;
		if (a.Dot(n) > 0)
			tempN = Vector3(0, 0, 0) - n;
		//先单位化
		double absa = pow(a.Dot(a), 0.5);
		double absn = pow(tempN.Dot(tempN), 0.5);
		Vector3 tempa = a * (1 / absa);
		Vector3 tempn = tempN * (1 / absn);
		double I = 2 * tempn.Dot(tempa);
		if (I < 0)
			I = -I;
		else
			tempa = Vector3(0.0, 0.0, 0.0) - tempa;

		return tempn * I + tempa;
	}

	void RayTracing::prepare()
	{
		calcMatrix();
	}

	void RayTracing::switchRestriction()
	{
		restrictio_vec.clear();
		CompontParam* rest = restriction;
		while (rest)
		{
			restrictio_vec.push_back(rest);
			rest = rest->restriction;
		}
	}

	void RayTracing::calcMatrix()
	{
		int num = restrictio_vec.size() + 1;
		R_rotatMatrix.resize(num);
		R_translateMatrix.resize(num);
		rotatMatrix.resize(num);
		translateMatrix.resize(num);
		
		// 世界坐标系转到模型的相对坐标系矩阵（逆矩阵）先旋转后平移
		Vector3D RotateAsix(m_coor.rotate_axis.x, m_coor.rotate_axis.y, m_coor.rotate_axis.z);
		R_rotatMatrix[0] = Matrix4D::getRotateMatrix(-m_coor.rotate_theta, RotateAsix);
		Vector3D rotatTranslate(m_coor.pos.x, m_coor.pos.y, m_coor.pos.z);
		rotatTranslate = R_rotatMatrix[0] * rotatTranslate; // 先旋转在平移（把平移的坐标一起旋转）
		R_translateMatrix[0] = Matrix4D::getTranslateMatrix(rotatTranslate * (-1));

		// 模型的相对坐标系转到世界坐标矩阵
		rotatMatrix[0] = Matrix4D::getRotateMatrix(m_coor.rotate_theta, RotateAsix);
		translateMatrix[0] = Matrix4D::getTranslateMatrix(m_coor.pos.x, m_coor.pos.y, m_coor.pos.z);


		for (int i = 1; i < num; ++i)
		{
			const auto& coor = restrictio_vec[i-1]->coor;

			// 世界坐标系转到模型的相对坐标系矩阵（逆矩阵）先旋转后平移
			Vector3D RotateAsix(coor.rotate_axis.x, coor.rotate_axis.y, coor.rotate_axis.z);
			R_rotatMatrix[i] = Matrix4D::getRotateMatrix(-coor.rotate_theta, RotateAsix);
			Vector3D rotatTranslate(coor.pos.x, coor.pos.y, coor.pos.z);
			rotatTranslate = R_rotatMatrix[i] * rotatTranslate; // 先旋转在平移（把平移的坐标一起旋转）
			R_translateMatrix[i] = Matrix4D::getTranslateMatrix(rotatTranslate * (-1));

			// 模型的相对坐标系转到世界坐标矩阵
			rotatMatrix[i] = Matrix4D::getRotateMatrix(coor.rotate_theta, RotateAsix);
			translateMatrix[i] = Matrix4D::getTranslateMatrix(coor.pos.x, coor.pos.y, coor.pos.z);
		}

		m_calcmatrix_flag = true;
		return;
	}

	int RayTracing::checkData()
	{
		return 0;
	}

	void RayTracing::calcReflectByPolyDataIndex(
		const Vector3 & startPiont,
		const Vector3 & direction,
		Vector3 & reflex,
		Vector3 & intersection,
		bool & isIntersect)
	{


		
	}
}