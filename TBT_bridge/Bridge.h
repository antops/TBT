#pragma once
#include "interface_cs.h"
#include "../TBT_common/interface_cpp.h"

namespace TBTfront {
	public ref class Bridge
	{

	public:
		static void transFieldBase(FieldBase^ left, TBTBack::FieldBase& right);
		static void transFieldBase(const TBTBack::FieldBase& left, FieldBase^&right);

		static void transVector2Complex(Vector2Complex left, vector<vector<complex<double>>>& right);
		static void transVector2Complex(const vector<vector<complex<double>>>& left, Vector2Complex&right);
		
		static void transCompontParam(CompontParam^ left, TBTBack::CompontParam& right);
		static void transCompontParam(const TBTBack::CompontParam& left, CompontParam^&right);

		static void transRayLineCluster(RayLineCluster^ left, TBTBack::RayLineCluster& right);
		static void transRayLineCluster(const TBTBack::RayLineCluster& left, RayLineCluster^&right);

		static void transVector3(Vector3^ left, TBTBack::Vector3& right);
		static void transVector3(const TBTBack::Vector3& left, Vector3^& right);

		static void transRayLine(RayLine^ left, TBTBack::RayLine& right);
		static void transRayLine(const TBTBack::RayLine& left, RayLine^&right);

		static void transCompontCoor(const TBTBack::Coordinate& left, Coordinate^&right);
		static void transCompontCoor(Coordinate^ left, TBTBack::Coordinate& right);

		static void transCalcOption(const TBTBack::CalcOption& left, CalcOption^&right);
		static void transCalcOption(CalcOption^ left, TBTBack::CalcOption& right);

		static void transGaussianCluster(const TBTBack::GaussianCluster& left, GaussianCluster^&right);
		static void transGaussianCluster(GaussianCluster^ left, TBTBack::GaussianCluster& right);
	private:
		Bridge(void);
		~Bridge(void);

	};
}