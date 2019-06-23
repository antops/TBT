#pragma once
#include <complex>
#include <vector>
#include <string>
#include <memory>
#include "Vector3.h"

using std::vector;
using std::complex;
using std::string;

namespace TBTBack {

	// ����ֲ�����ϵ
	struct Coordinate {
		Vector3 U;
		Vector3 V;
		Vector3 N;

		Vector3 pos;
		double scale;

		double rotate_theta;
		Vector3 rotate_axis;
		bool setUV(const Vector3& u, const Vector3& v);
		void setRotate(const Vector3& axis, double theta);
		void updateRotate(const Vector3& rotate, double theta);
	};

	struct FieldBase {
		vector<vector<complex<double>>> Ex;
		vector<vector<complex<double>>> Ey;
		vector<vector<complex<double>>> Ez;
		vector<vector<complex<double>>> Hx;
		vector<vector<complex<double>>> Hy;
		vector<vector<complex<double>>> Hz;

		Coordinate coordinate;

		// Ĭ��ds_x = ds_y
		double ds_x;
		double ds_y;

		void clear() {
			Ex.clear();
			Ey.clear();
			Ez.clear();
			Hx.clear();
			Hy.clear();
			Hz.clear();
		}
	};

	// �߶λ�����
	struct RayLine
	{
		Vector3 start_point; // ���
		Vector3 normal_line; // ���߻����յ� 
	};

	// ������
	struct RayLineCluster {
		vector<RayLine> ray_cluster;

		void clear() {
			ray_cluster.clear();
		}
	};

	// ��˹Բ̨
	struct GaussianCluster
	{
		Coordinate coordinate;
		double start_radius;
		double distance;
		double angle;
		bool is_foucs;
		double foucs_radius;
		RayLineCluster rayline;
		void clear() {
			rayline.clear();
		}
	};

	struct CompontParam {
		CompontParam() :
			restriction(nullptr) {};

		~CompontParam();
		void release();
		CompontParam& operator =(const CompontParam& right);

		string name;
		vector<double> param;
		string param_str;
		Coordinate coor;
		CompontParam* restriction;
		
	};

	struct SourceParam {
		string name;
		vector<double> param;

	};

	struct CalcOption
	{
		CalcOption() {};
		// �Ƿ����û�н���Ĺ���
		bool is_ign_non_intersection = false;

		// �Ƿ����restriction
		bool is_ign_restriction = false;

		// �Ƿ�ʹ�����������㷨
		bool is_use_index_reflect = false;
	};

}