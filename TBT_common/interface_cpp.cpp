#include "interface_cpp.h"
#include "Matrix4D.h"
#include "Vector3D.h"
#include "eigen/Eigen/Dense"
#include "eigen/Eigen/eigenvalues"
using namespace Eigen;
const static double Pi = 3.1415926;
namespace TBTBack {
	bool Coordinate::setUV(const Vector3& u, const Vector3& v)
	{
		Vector3 tmpU = u;
		Vector3 tmpV = v;
		tmpU.Normalization();
		tmpV.Normalization();
		U = tmpU;
		V = tmpV;
		N = U.Cross(V);
		N.Normalization();
		Matrix3d A;
		A << U.x, V.x, N.x, U.y, V.y, N.y, U.z, V.z, N.z;
		EigenSolver<Matrix3d> es(A);
		Matrix3d DM = es.pseudoEigenvalueMatrix();
		Matrix3d VM = es.pseudoEigenvectors();
		Vector3 rotate_axis;
		for (int i = 0; i < 3; i++) {
			double eigenvalue = DM(i, i);
			if (eigenvalue > 0.999 && eigenvalue < 1.001) {
				rotate_axis.set(VM(0, i), VM(1, i), VM(2, i));
			}
		}
		rotate_axis.Normalization();
		//update Rotation Angle:
		double theta;
		Vector3 tempz(0, 0, 1);
		Vector3 tempx(1, 0, 0);
		Vector3 tempy(0, 1, 0);
		double rotate_theta;

		if (abs(tempz.Dot(rotate_axis)) < 0.9) {
			Vector3 v = tempz.Cross(rotate_axis);
			v.Normalization();
			Vector3 rv = Vector3(A(0, 0)*v.x + A(0, 1)*v.y + A(0, 2)*v.z,
				A(1, 0)*v.x + A(1, 1)*v.y + A(1, 2)*v.z,
				A(2, 0)*v.x + A(2, 1)*v.y + A(2, 2)*v.z);
			rotate_theta = acos(v.Dot(rv) / v.Length() / rv.Length());
			rotate_theta = rotate_theta * 180 / Pi;
			updateRotate(rotate_axis, rotate_theta);

			Matrix4D CheckM;
			CheckM = Matrix4D::getRotateMatrix(rotate_theta, rotate_axis.x, rotate_axis.y, rotate_axis.z);
			Vector3 Uc, Vc, Nc;
			Uc = CheckM*Vector3(1, 0, 0);	Vc = CheckM*Vector3(0, 1, 0);	Nc = CheckM*Vector3(0, 0, 1);
			if ((Uc.Dot(U) > 0.999) && (Vc.Dot(V) > 0.999) && (Nc.Dot(N) > 0.999))	return true;
			else {
				rotate_axis = rotate_axis*(-1);
				v = tempz.Cross(rotate_axis);
				v.Normalization();
				rv = Vector3(A(0, 0)*v.x + A(0, 1)*v.y + A(0, 2)*v.z,
					A(1, 0)*v.x + A(1, 1)*v.y + A(1, 2)*v.z,
					A(2, 0)*v.x + A(2, 1)*v.y + A(2, 2)*v.z);
				rotate_theta = acos(v.Dot(rv) / v.Length() / rv.Length());
				rotate_theta = rotate_theta * 180 / Pi;
				//if (rotate_theta > 180) rotate_theta = rotate_theta - 360;
				//if (rotate_theta < -180) rotate_theta = rotate_theta + 360;
				updateRotate(rotate_axis, rotate_theta);
				return true;
			}
		}
		else if (abs(tempx.Dot(rotate_axis)) < 0.9) {
			Vector3 v = tempx.Cross(rotate_axis);
			v.Normalization();
			Vector3 rv = Vector3(A(0, 0)*v.x + A(0, 1)*v.y + A(0, 2)*v.z,
				A(1, 0)*v.x + A(1, 1)*v.y + A(1, 2)*v.z,
				A(2, 0)*v.x + A(2, 1)*v.y + A(2, 2)*v.z);
			rotate_theta = acos(v.Dot(rv) / v.Length() / rv.Length());
			rotate_theta = rotate_theta * 180 / Pi;
			//if (rotate_theta > 180) rotate_theta = rotate_theta - 360;
			//if (rotate_theta < -180) rotate_theta = rotate_theta + 360;
			updateRotate(rotate_axis, rotate_theta);
			//updateRotate(Vector3(1, 0, 0), 180);
			Matrix4D CheckM;
			CheckM = Matrix4D::getRotateMatrix(rotate_theta, rotate_axis.x, rotate_axis.y, rotate_axis.z);
			Vector3 Uc, Vc, Nc;
			Uc = CheckM*Vector3(1, 0, 0);	Vc = CheckM*Vector3(0, 1, 0);	Nc = CheckM*Vector3(0, 0, 1);
			if ((Uc.Dot(U) > 0.999) && (Vc.Dot(V) > 0.999) && (Nc.Dot(N) > 0.999))	return true;
			else {
				rotate_axis = rotate_axis*(-1);
				v = tempx.Cross(rotate_axis);
				v.Normalization();
				rv = Vector3(A(0, 0)*v.x + A(0, 1)*v.y + A(0, 2)*v.z,
					A(1, 0)*v.x + A(1, 1)*v.y + A(1, 2)*v.z,
					A(2, 0)*v.x + A(2, 1)*v.y + A(2, 2)*v.z);
				rotate_theta = acos(v.Dot(rv) / v.Length() / rv.Length());
				rotate_theta = rotate_theta * 180 / Pi;
				updateRotate(rotate_axis, rotate_theta);
				return true;
			}
		}
		else if (abs(tempy.Dot(rotate_axis)) < 0.9) {
			Vector3 v = tempy.Cross(rotate_axis);
			v.Normalization();
			Vector3 rv = Vector3(A(0, 0)*v.x + A(0, 1)*v.y + A(0, 2)*v.z,
				A(1, 0)*v.x + A(1, 1)*v.y + A(1, 2)*v.z,
				A(2, 0)*v.x + A(2, 1)*v.y + A(2, 2)*v.z);
			rotate_theta = acos(v.Dot(rv) / v.Length() / rv.Length());
			rotate_theta = rotate_theta * 180 / Pi;
			updateRotate(rotate_axis, rotate_theta);
			Matrix4D CheckM;
			CheckM = Matrix4D::getRotateMatrix(rotate_theta, rotate_axis.x, rotate_axis.y, rotate_axis.z);
			Vector3 Uc, Vc, Nc;
			Uc = CheckM*Vector3(1, 0, 0);	Vc = CheckM*Vector3(0, 1, 0);	Nc = CheckM*Vector3(0, 0, 1);
			if ((Uc.Dot(U) > 0.999) && (Vc.Dot(V) > 0.999) && (Nc.Dot(N) > 0.999))	return true;
			else {
				rotate_axis = rotate_axis*(-1);
				v = tempy.Cross(rotate_axis);
				v.Normalization();
				rv = Vector3(A(0, 0)*v.x + A(0, 1)*v.y + A(0, 2)*v.z,
					A(1, 0)*v.x + A(1, 1)*v.y + A(1, 2)*v.z,
					A(2, 0)*v.x + A(2, 1)*v.y + A(2, 2)*v.z);
				rotate_theta = acos(v.Dot(rv) / v.Length() / rv.Length());
				rotate_theta = rotate_theta * 180 / Pi;
				updateRotate(rotate_axis, rotate_theta);
				return true;
			}
		}

		return false;
	}

	void Coordinate::setRotate(const Vector3& axis, double theta)
	{
		Vector3D RotateAsix(axis.x, axis.y, axis.z);
		Matrix4D matrix = Matrix4D::getRotateMatrix(rotate_theta, RotateAsix) *
			Matrix4D::getTranslateMatrix(pos.x, pos.y, pos.z);
		U = matrix * Vector3(1, 0, 0);
		V = matrix * Vector3(0, 1, 0);
		N = matrix * Vector3(0, 0, 1);
	}

	void Coordinate::updateRotate(const Vector3& rotate, double theta)
	{
		rotate_axis = rotate;
		rotate_theta = theta;
	}
	CompontParam::~CompontParam()
	{
		if (restriction != nullptr) {
			delete restriction;
		}
	}
	void CompontParam::release()
	{
		restriction = nullptr;
	}
	CompontParam & CompontParam::operator=(const CompontParam & right)
	{
		if (this == &right) {
			return *this;
		}
		this->name = right.name;
		this->coor = right.coor;
		this->param.assign(right.param.begin(), right.param.end());
		this->param_str = right.param_str;
		this->restriction = right.restriction;
		return *this;
	}
}