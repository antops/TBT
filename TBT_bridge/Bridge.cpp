#include "Bridge.h"
#include <string>
#include <msclr\marshal_cppstd.h>  

TBTfront::Bridge::Bridge(void)
{
	return;
}

TBTfront::Bridge::~Bridge(void)
{
	return;
}

void TBTfront::Bridge::transFieldBase(FieldBase ^ left, TBTBack::FieldBase & right)
{
	if (left == nullptr) return;
	transVector2Complex(left->Ex, right.Ex);
	transVector2Complex(left->Ey, right.Ey);
	transVector2Complex(left->Ez, right.Ez);
	transVector2Complex(left->Hx, right.Hx);
	transVector2Complex(left->Hy, right.Hy);
	transVector2Complex(left->Hz, right.Hz);
	right.ds_x = left->ds_x;
	right.ds_y = left->ds_y;
	transCompontCoor(left->coordinate, right.coordinate);
	return;
}

void TBTfront::Bridge::transFieldBase(const TBTBack::FieldBase & left, FieldBase ^& right)
{
	//right = gcnew FieldBase();
	Vector2Complex tmp;
	transVector2Complex(left.Ex, tmp);
	right->Ex = tmp;
	transVector2Complex(left.Ey, tmp);
	right->Ey = tmp;
	transVector2Complex(left.Ez, tmp);
	right->Ez = tmp;
	transVector2Complex(left.Hx, tmp);
	right->Hx = tmp;
	transVector2Complex(left.Hy, tmp);
	right->Hy = tmp;
	transVector2Complex(left.Hz, tmp);
	right->Hz = tmp;

	right->ds_x = left.ds_x;
	right->ds_y = left.ds_y;
	Coordinate^ coor;
	transCompontCoor(left.coordinate, coor);
	right->coordinate = coor;
	return;
}

void TBTfront::Bridge::transVector2Complex(Vector2Complex left, vector<vector<complex<double>>>& right)
{
	if (left == nullptr) return;
	size_t count = left->Count;
	right.resize(count);
	for (size_t i = 0; i < count; i++) {
		System::Collections::Generic::List<Complex^>^ left_list = left[i];
		if (left_list == nullptr) continue;
		size_t count_list = left_list->Count;
		right[i].reserve(count_list);
		for (size_t j = 0; j < count_list; j++) {
			if (left_list[j] == nullptr) continue;
			double real = left_list[j]->real;
			double imag = left_list[j]->imag;
			complex<double> data(real, imag);
			right[i].push_back(data);
		}
	}
	
	return;
}

void TBTfront::Bridge::transVector2Complex(const vector<vector<complex<double>>>& left, Vector2Complex & right)
{
	size_t count = left.size();
	right = gcnew System::Collections::Generic::List<System::Collections::Generic::List<Complex^>^>();
	for (size_t i = 0; i < left.size(); i++) {
		auto right_list = gcnew System::Collections::Generic::List<Complex^>();
		for (size_t j = 0; j < left[i].size(); j++) {
			auto data = gcnew Complex();
			double real = left[i][j].real();
			double imag = left[i][j].imag();
			data->real = real;
			data->imag = imag;
			right_list->Add(data);
		}
		right->Add(right_list);
	}
	return;
}

void TBTfront::Bridge::transCompontParam(CompontParam ^ left, TBTBack::CompontParam & right)
{
	if (left == nullptr) return;
	if (left->name != nullptr) {
		System::String^ strNew = left->name;
		right.name = msclr::interop::marshal_as<std::string>(strNew);
	}
	
	if (left->param_str != nullptr) {
		System::String^ strNew = left->param_str;
		right.param_str = msclr::interop::marshal_as<std::string>(strNew);
	}

	if (left->param != nullptr) {
		for (int i = 0; i < left->param->Count; i++) {
			right.param.push_back(left->param[i]);
		}
	}

	transCompontCoor(left->coor, right.coor);
	if (left->restriction != nullptr) {
		right.restriction = new TBTBack::CompontParam;
		transCompontParam(left->restriction, *right.restriction);
	}

	return;
}

void TBTfront::Bridge::transCompontParam(const TBTBack::CompontParam & left, CompontParam ^& right)
{
	right->name = msclr::interop::marshal_as<System::String^>(left.name);
	right->param_str = msclr::interop::marshal_as<System::String^>(left.param_str);
	right->param = gcnew System::Collections::Generic::List<double>();
	for (int i = 0; i < left.param.size(); i++)
	{
		right->param->Add(left.param[i]);
	}
	Coordinate^ coor;
	transCompontCoor(left.coor, coor);
	right->coor = coor;
	if (left.restriction != nullptr) {
		CompontParam^ tmp = gcnew CompontParam();
		transCompontParam(*left.restriction, tmp);
		right->restriction = tmp;
	}
	return;
}

void TBTfront::Bridge::transRayLineCluster(RayLineCluster ^ left, TBTBack::RayLineCluster & right)
{
	if (left == nullptr || left->ray_cluster == nullptr) return;
	right.ray_cluster.resize(left->ray_cluster->Count);
	for (int i = 0; i < left->ray_cluster->Count; i++)
	{
		transRayLine(left->ray_cluster[i], right.ray_cluster[i]);
	}
	return;
}

void TBTfront::Bridge::transRayLineCluster(const TBTBack::RayLineCluster & left, RayLineCluster ^& right)
{
	right = gcnew RayLineCluster();
	right->ray_cluster = gcnew System::Collections::Generic::List<RayLine^>();
	
	for (int i = 0; i < left.ray_cluster.size(); i++)
	{
		RayLine^ line;
		transRayLine(left.ray_cluster[i], line);
		right->ray_cluster->Add(line);
	}
	return;
}

void TBTfront::Bridge::transVector3(Vector3 ^ left, TBTBack::Vector3 & right)
{
	if (left == nullptr) {
		return;
	}
	right.x = left->x;
	right.y = left->y;
	right.z = left->z;
	return;
}

void TBTfront::Bridge::transVector3(const TBTBack::Vector3 & left, Vector3^& right)
{
	right = gcnew Vector3();
	right->x = left.x;
	right->y = left.y;
	right->z = left.z;
	return;
}

void TBTfront::Bridge::transRayLine(RayLine ^ left, TBTBack::RayLine & right)
{
	if (left == nullptr) return;
	transVector3(left->start_point, right.start_point);
	transVector3(left->normal_line, right.normal_line);
	return;
}

void TBTfront::Bridge::transRayLine(const TBTBack::RayLine & left, RayLine^& right)
{
	right = gcnew RayLine();

	Vector3^ start_point;
	Vector3^ normal_line;
	transVector3(left.start_point, start_point);
	transVector3(left.normal_line, normal_line);
	right->start_point = start_point;
	right->normal_line = normal_line;
	return;
}

void TBTfront::Bridge::transCompontCoor(const TBTBack::Coordinate& left, Coordinate^&right)
{
	right = gcnew Coordinate();
	Vector3^ tmp;
	transVector3(left.pos, tmp);
	right->pos = tmp;
	transVector3(left.U, tmp);
	right->U = tmp;
	transVector3(left.V, tmp);
	right->V = tmp;
	transVector3(left.rotate_axis, tmp);
	right->rotate_axis = tmp;
	right->rotate_theta = left.rotate_theta;
}

void TBTfront::Bridge::transCompontCoor(Coordinate^ left, TBTBack::Coordinate& right)
{
	if (left == nullptr) return;
	transVector3(left->pos, right.pos);
	transVector3(left->U, right.U);
	transVector3(left->V, right.V);
	transVector3(left->rotate_axis, right.rotate_axis);
	right.rotate_theta = left->rotate_theta;
}

void TBTfront::Bridge::transCalcOption(const TBTBack::CalcOption& left, CalcOption^&right)
{
	right = gcnew CalcOption();
	right->is_ign_non_intersection = left.is_ign_non_intersection;
	right->is_ign_restriction = left.is_ign_restriction;
}

void TBTfront::Bridge::transCalcOption(CalcOption^ left, TBTBack::CalcOption& right)
{
	if (left == nullptr) return;
	right.is_ign_non_intersection = left->is_ign_non_intersection;
	right.is_ign_restriction = left->is_ign_restriction;
}

void TBTfront::Bridge::transGaussianCluster(const TBTBack::GaussianCluster & left, GaussianCluster ^& right)
{
	right = gcnew GaussianCluster();
	right->angle = left.angle;
	right->start_radius = left.start_radius;
	right->foucs_radius = left.foucs_radius;
	right->is_foucs = left.is_foucs;
	Coordinate^ coor;
	transCompontCoor(left.coordinate, coor);
	RayLineCluster^ rayline;
	transRayLineCluster(left.rayline, rayline);
	right->coordinate = coor;
	right->rayline = rayline;
}

void TBTfront::Bridge::transGaussianCluster(GaussianCluster ^ left, TBTBack::GaussianCluster & right)
{
	right.angle = left->angle;
	right.start_radius = left->start_radius;
	right.foucs_radius = left->foucs_radius;
	right.is_foucs = left->is_foucs;
	transCompontCoor(left->coordinate, right.coordinate);
	RayLineCluster^ rayline;
	transRayLineCluster(left->rayline, right.rayline);
}

