#pragma once

namespace TBTfront{
	// ��ά��
	public ref struct Vector3
	{
		Vector3() {
			x = 0;
			y = 0;
			z = 0;
		}
		Vector3(double x_,
			double y_,
			double z_) {
			x = x_;
			y = y_;
			z = z_;
		}
		double x;
		double y;
		double z;
		double Dot(const Vector3^ v)
		{
			return x*v->x + y*v->y + z*v->z;
		};

		// ���
		Vector3^ Cross(const Vector3^ v)
		{
			return gcnew Vector3(
				y * v->z - z * v->y,
				z * v->x - x * v->z,
				x * v->y - y * v->x);
		}
	};

	public ref struct Coordinate
	{
		Vector3^ U;
		Vector3^ V;
		Vector3^ N;
		Vector3^ pos;
		double scale;

		double rotate_theta;
		Vector3^ rotate_axis;
	};

	// ���
	public ref struct CompontParam
	{
		// ���������
		System::String^ name;

		// ����1
		System::Collections::Generic::List<double>^ param;
		// ����2
		System::String^ param_str;

		Coordinate^ coor;
		// �ض�
		CompontParam^ restriction;
	};

	// ����
	public ref struct Complex
	{
		double real;
		double imag;
	};

	typedef System::Collections::Generic::List<System::Collections::Generic::List<Complex^>^>^ Vector2Complex;

	// ��
	public ref struct FieldBase
	{
		Vector2Complex Ex;
		Vector2Complex Ey;
		Vector2Complex Ez;
		Vector2Complex Hx;
		Vector2Complex Hy;
		Vector2Complex Hz;

		Coordinate^ coordinate;

		// Ĭ��ds_x = ds_y
		double ds_x;
		double ds_y;
	};

	// �߶λ�����
	public ref struct RayLine
	{
		Vector3^ start_point; // ���
		Vector3^ normal_line; // ���߻����յ� 
	};

	public ref struct RayLineCluster
	{
		System::Collections::Generic::List<RayLine^>^ ray_cluster;
	};

	public ref struct GaussianCluster
	{
		Coordinate^ coordinate;
		double start_radius;
		double distance;
		double angle;
		bool is_foucs;
		double foucs_radius;
		RayLineCluster^ rayline;
	};

	typedef System::Collections::Generic::List<CompontParam^>^ CompontList;

	public ref struct CalcOption
	{
		CalcOption()
			:is_ign_non_intersection(false),
			is_ign_restriction(false)
		{};
		// �Ƿ����û�н���Ĺ���
		bool is_ign_non_intersection;

		// �Ƿ����restriction
		bool is_ign_restriction;
	};

}


