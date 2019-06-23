#include "Restriction.h"
#include "../../../TBT_common/Constant_Var.h"
#include "../../../TBT_common/MyLog.h"
#include "../../../TBT_common/Vector3D.h"
#include "../../../TBT_common/Matrix4D.h"
#include "../../../TBT_common/Position3D.h"

TBTBack::Restriction::Restriction()
{
}

TBTBack::Restriction::~Restriction()
{
}

int TBTBack::Restriction::init(CompontParam & param)
{
	return NodeBase::init(param);
}

int TBTBack::Restriction::clacField(const FieldBase& in, FieldBase& out, int* count, bool is_end)
{
	return 0;
}

int TBTBack::Restriction::clacLight(const RayLineCluster& in, const CalcOption opt, RayLineCluster& out)
{
	const auto& param = cpt_param_.param;
	const auto& coor = cpt_param_.coor;
	int n = 0;
	int m = 0;
	double ds = param[3];

	if (ds < 0.0) // ÏÔÊ¾
	{
		n = 51;
		m = 51;
	}
	else
	{
		m = n = int(param[0] / ds);
	}

	if (Restriction::judgeType(param[2]) == RES_CYLINDER)
	{
		double gapR = param[0] / (n - 1);
		double gapCyl = Pi / (m - 1);

		Vector3D RotateAsix(coor.rotate_axis.x, coor.rotate_axis.y, coor.rotate_axis.z);
		Matrix4D rotatMatrix = Matrix4D::getRotateMatrix(coor.rotate_theta, RotateAsix);
		Matrix4D translateMatrix = Matrix4D::getTranslateMatrix(coor.pos.x, coor.pos.y, coor.pos.z);

		Matrix4D R_Matrix = translateMatrix * rotatMatrix;

		double x, y;
		double startR = 0;
		double startCyl = 0;
		double eachGapCyl = 0;

		Vector3 tempStar;

		//startR += gapR * 50;

		Vector3 to = rotatMatrix * Vector3(0, 0, 1);
		for (int i = 0; i < n; ++i)
		{
			if (startR < 0.0000001)
			{
				eachGapCyl = 2 * Pi;
			}
			else
			{
				eachGapCyl = param[0] / startR * gapCyl;
			}
			startCyl = 0;
			while (startCyl < 2 * Pi)
			{
				RayLine line;
				line.normal_line = to;
				x = cos(startCyl) * startR;
				y = sin(startCyl) * startR;
				line.start_point = R_Matrix * Vector3(x, y, 0);
				out.ray_cluster.push_back(line);

				startCyl += eachGapCyl;		
			}
			startR += gapR;
		}

	}
	return 0;
}

TBTBack::Restriction::RestrictionType TBTBack::Restriction::judgeType(double data)
{
	if (data < 0.0) return RES_CYLINDER;
	return RES_CUBE;
}
