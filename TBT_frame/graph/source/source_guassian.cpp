#include "source_guassian.h"
#include "../../../TBT_common/Constant_Var.h"
#include "../../../TBT_common/Vector3.h"
#include "../../../TBT_common/Matrix4D.h"
#include "../../../TBT_common/Vector3D.h"
#include "../../../TBT_common/MyLog.h"
TBTBack::SourceGuassian::SourceGuassian()
{
}

TBTBack::SourceGuassian::~SourceGuassian()
{
}

int TBTBack::SourceGuassian::init(CompontParam & param)
{
	return NodeBase::init(param);
}

int TBTBack::SourceGuassian::clacField(const FieldBase& in, FieldBase& out, int* count, bool is_end)
{
	CreateGaussianGaussain(out);
	out.coordinate = cpt_param_.coor;
	out.ds_x = cpt_param_.param[2];
	out.ds_y = cpt_param_.param[2];
	return 0;
}

int TBTBack::SourceGuassian::clacLight(const RayLineCluster& in, const CalcOption opt, RayLineCluster& out)
{
	const auto& coor = cpt_param_.coor;
	const auto& param = cpt_param_.param;
	Vector3D RotateAsix(coor.rotate_axis.x, coor.rotate_axis.y, coor.rotate_axis.z);
	Matrix4D rotatMatrix = Matrix4D::getRotateMatrix(coor.rotate_theta, RotateAsix);
	Matrix4D translateMatrix = Matrix4D::getTranslateMatrix(coor.pos.x, coor.pos.y, coor.pos.z);

	double w0 = param[4];
	double fre = param[5] * 1e9;//单位是GHz,改为Hz

	double lamda = C_Speed / fre;
	float angle = lamda/(Pi*w0)*2*180.0/ Pi;//半张角乘以2，单位是弧度，变更为角度

	float angle_gap = 10;
	int N = 360 / angle_gap;

	int loop = 10;
	float tmp = angle / loop;
	for (int j = 0; j < loop - 1; j++) {
		double z = cos((angle - tmp * j) / 180 * Pi);
		double r = sin((angle - tmp * j)/ 180 * Pi);
		for (int i = 0; i < N; i++)
		{
			// 将坐标变换到世界坐标
			RayLine line;
			line.normal_line = rotatMatrix * Vector3(r*sin(angle_gap * i / 180 * Pi),
				r*cos(angle_gap * i / 180 * Pi), z);
			line.start_point = translateMatrix * rotatMatrix * Vector3(0, 0, 0);
			out.ray_cluster.push_back(line);
		}
	}
	return 0;
}

void TBTBack::SourceGuassian::CreateGaussianGaussain(FieldBase& out)
{
	const auto& param = cpt_param_.param;
	double ds = param[2];
	double z0 = param[3];//传播距离
	double w0 = param[4];
	double fre = param[5];
	double factor = 1;
	int N_width = param[0] / ds;
	int M_depth = param[1] / ds;
	out.Ex.resize(N_width);
	out.Ey.resize(N_width);
	for (int i = 0; i < N_width; i++)
	{
		out.Ex[i].resize(M_depth);
		out.Ey[i].resize(M_depth);
	}
	for (int i = 0; i < N_width; i++)
	{
		for (int j = 0; j < M_depth; j++)
		{
			double tempX = (i - (N_width - 1) / 2) * ds * factor;
			double tempY = (j - (M_depth - 1) / 2) * ds * factor;
			double tempr1;
			tempr1 = pow((tempX * tempX + tempY * tempY), 0.5);
			double temper, tempei;
			CreateGaussian(tempr1, w0 * factor, fre * 1e9, z0 * factor, temper, tempei);
			out.Ex[i][j] = complex<double>(0, 0);
			out.Ey[i][j] = complex<double>(temper, tempei);
		}
	}

}

void TBTBack::SourceGuassian::CreateGaussian(double r, double w0, double fre,
	double z0, double &Real, double &Imag) const {
	double lamda = C_Speed / fre;
	double k = 2 * Pi * fre / C_Speed;
	double w, Rdl, theta, r2, temp;

	w = w0 * pow((1 + pow(((lamda * z0) / (Pi * w0 * w0)), 2)), 0.5);
	Rdl = z0 / (z0 * z0 + pow((Pi * w0 * w0 / lamda), 2));
	theta = atan((lamda * z0) / (Pi * w0 * w0));
	r2 = r * r;
	temp = pow((2 / w / w / Pi), 0.5) * exp(-r2 / w / w);

	Real = temp * cos(theta - k * z0 - Pi * r2 / lamda * Rdl);
	Imag = temp * sin(theta - k * z0 - Pi * r2 / lamda * Rdl);
}

int TBTBack::SourceGuassian::clacGaussian(const GaussianCluster & in, const CalcOption opt, GaussianCluster & out)
{
	LOG(ERROR) << "out.angle" << 20;
	// test
	out.angle = 20;
	// 起始半径
	out.start_radius = 0.1;
	// 是否汇聚
	out.is_foucs = false;
	//束腰
	out.foucs_radius = 0.001;

	out.distance = 1;
	RayLine line;
	line.start_point = Vector3(0, 0, 0);
	line.normal_line = Vector3(0, 0, 1);
	out.rayline.ray_cluster.push_back(line);

	out.coordinate = cpt_param_.coor;
	LOG(ERROR) << "out.angle" << 20;
	return 0;
}