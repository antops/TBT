#include "mirror_quadric_surface.h"
#include "../../calculation/RayTracing.h"
#include "../../../TBT_common/MyLog.h"
TBTBack::MirrorQuadricSurface::MirrorQuadricSurface()
{
}

TBTBack::MirrorQuadricSurface::~MirrorQuadricSurface()
{
}

int TBTBack::MirrorQuadricSurface::init(CompontParam& param)
{
	int res = 0;
	res = NodeMirror::init(param);
	if (res != 0) return -1;

	if (cpt_param_.param.size() < 16) {
		LOG(ERROR) << "cpt_param less than 16, size:" << cpt_param_.param.size();
		return -1;
	}
	return 0;
}

int TBTBack::MirrorQuadricSurface::clacField(const FieldBase & in, FieldBase & out, int * count, bool is_end)
{
	return 0;
}

int TBTBack::MirrorQuadricSurface::clacLight(const RayLineCluster& in, const CalcOption opt, RayLineCluster& out)
{
	RayTracing ray_trace;
	ray_trace.setAnalysis(cpt_param_);
	return ray_trace.calcReflect(in, opt, out);
}

int TBTBack::MirrorQuadricSurface::clacGaussian(const GaussianCluster & in, const CalcOption opt, GaussianCluster & out)
{
	RayTracing ray_trace;
	ray_trace.setAnalysis(cpt_param_);
	ray_trace.calcReflect(in.rayline, opt, out.rayline);
	//调夏冬 生成其参数
	// 发散角
	out.angle = 20;
	// 起始半径
	out.start_radius = 0.1;
	// 是否汇聚
	out.is_foucs = false;
	//束腰
	out.foucs_radius = 0.001;

	out.distance = (in.rayline.ray_cluster[0].start_point - out.rayline.ray_cluster[0].start_point).Length();
	return 0;
}
/*
void TBTBack::MirrorQuadricSurface::sampling(vtkSmartPointer<vtkPolyData>& polyDataPtr, double ds)
{
	if (restrictions.empty())
	{
		double radius = 1;
		double temp = -4 * 1; // m_data[7] 表示焦距
		vtkSmartPointer<vtkQuadric>quadric = vtkSmartPointer<vtkQuadric>::New();
		quadric->SetCoefficients(m_data[0], m_data[1], m_data[2], m_data[3], m_data[4], m_data[5], m_data[6],
			m_data[7], m_data[8], m_data[9] + 1);

		//二次函数采样分辨率
		vtkSmartPointer<vtkSampleFunction>sample = vtkSmartPointer<vtkSampleFunction>::New();
		if (ds == 0)
			sample->SetSampleDimensions(60, 60, 30);
			//sample->SetSampleDimensions(200, 200, 200);
		else
			//sample->SetSampleDimensions(int(radius / ds) * 2,
			//	int(radius / ds) * 2, int(-temp / ds) * 2); // 采样点和ds有关
			sample->SetSampleDimensions(
				int((m_data[11] - m_data[10]) / ds),
				int((m_data[13] - m_data[12]) / ds),
				int((m_data[15] - m_data[14]) / ds)); // 采样点和ds有关
		sample->SetImplicitFunction(quadric);
		sample->SetModelBounds(m_data[10], m_data[11], m_data[12], m_data[13], m_data[14], m_data[15]);
		vtkSmartPointer<vtkContourFilter> contourFilter = vtkSmartPointer<vtkContourFilter>::New();
		contourFilter->SetInputConnection(sample->GetOutputPort());
		contourFilter->GenerateValues(1, 1, 1);
		contourFilter->Update();

		polyData = contourFilter->GetOutput();
		vtkSmartPointer<vtkTransform> transform = vtkSmartPointer<vtkTransform>::New();

		// 用户自定义平移旋转 (先移动后旋转)
		transform->Translate(graphTrans.getTrans_x(),
			graphTrans.getTrans_y(), graphTrans.getTrans_z());
		transform->RotateWXYZ(graphTrans.getRotate_theta(), graphTrans.getRotate_x(),
			graphTrans.getRotate_y(), graphTrans.getRotate_z());

		vtkSmartPointer<vtkTransformPolyDataFilter> TransFilter =
			vtkSmartPointer<vtkTransformPolyDataFilter>::New();
		TransFilter->SetInputData(polyData);
		TransFilter->SetTransform(transform); //use vtkTransform (or maybe vtkLinearTransform)
		TransFilter->Update();
		polyDataPtr =  TransFilter->GetOutput();
	}
	else
	{

		calcRestriction(polyDataPtr,ds);
	}
}



double MirrorQuadricSurface::calcZ(double x, double y)
{
	double res = 0;
	if (abs(m_data[2]) < 0.000000001)
	{
		double temp = m_data[4] * y + m_data[5] * x + m_data[8];
		if (abs(temp) > 0.000000001) // 被除数不等于0
		{
			res = -(m_data[0] * x*x + m_data[1] * y*y + m_data[3] * x*y + m_data[6] * x + m_data[7] * y + m_data[9])
				/ temp;
		}	
	}
	else
	{
		double A = m_data[2];
		double C = m_data[0] * x*x + m_data[1] * y*y + m_data[3] * x*y + m_data[6] * x + m_data[7] * y + m_data[9];
		double B = m_data[4] * y + m_data[5] * x + m_data[8];

		double temp = B * B - 4 * A * C;
		if (temp >= 0)
			temp = sqrt(temp);
		double res1 = (-B + temp) / 2 / A;
		double res2 = (-B - temp) / 2 / A;
		if (res2 < res1)
			return res2;
		else
			return res1;
	}
	return res;
}


void MirrorQuadricSurface::calcRestriction1()
{
	if (restrictions.empty())
		return;

	vtkSmartPointer<vtkPolyData> polyDataRestriction;

	polyDataRestriction = restrictions[0]->getPolyData();
		
	if (restrictions.size() > 1)
	{
		for (int i = 1; i < restrictions.size(); ++i)
		{

			vtkSmartPointer<vtkBooleanOperationPolyDataFilter> booleanOperation =
				vtkSmartPointer<vtkBooleanOperationPolyDataFilter>::New();

			booleanOperation->SetOperationToIntersection();

			booleanOperation->SetInputData(0, polyDataRestriction); // set the input m_data  
			booleanOperation->SetInputData(1, restrictions[i]->getPolyData());
			booleanOperation->Update();

			polyDataRestriction = booleanOperation->GetOutput();
		}	
	}	
	vtkSmartPointer<vtkBooleanOperationPolyDataFilter> booleanOperation =
		vtkSmartPointer<vtkBooleanOperationPolyDataFilter>::New();

	booleanOperation->SetOperationToIntersection();

	booleanOperation->SetInputData(0, polyData); // set the input m_data  
	booleanOperation->SetInputData(1, polyDataRestriction);
	booleanOperation->Update();

	polyData = booleanOperation->GetOutput();

	// 世界坐标系转到模型的相对坐标系矩阵（逆矩阵）先旋转后平移
	Vector3D RotateAsix(graphTrans.getRotate_x(),
		graphTrans.getRotate_y(), graphTrans.getRotate_z());
	Matrix4D R_rotatMatrix = Matrix4D::getRotateMatrix(
		-graphTrans.getRotate_theta(), RotateAsix);
	//Matrix4D R_translateMatrix = Matrix4D::getTranslateMatrix(-surfaceData[0], -surfaceData[1], -surfaceData[2]);

	Vector3D rotatTranslate(graphTrans.getTrans_x(),
		graphTrans.getTrans_y(), graphTrans.getTrans_z());
	rotatTranslate = R_rotatMatrix * rotatTranslate; // 先旋转在平移（把平移的坐标一起旋转）
	Matrix4D R_translateMatrix = Matrix4D::getTranslateMatrix(rotatTranslate * (-1));

	vtkSmartPointer<vtkPoints> points = vtkSmartPointer<vtkPoints>::New();
	vtkSmartPointer<vtkTriangle> p1 = vtkSmartPointer<vtkTriangle>::New();
	vtkSmartPointer<vtkCellArray>pLineCell = vtkCellArray::New();

	int EleNum = polyData->GetNumberOfCells();
	int couter = 0; // 计数器
	for (int i = 0; i < EleNum; i++)
	{
		vtkIdList * p = polyData->GetCell(i)->GetPointIds();
		bool ok = true;
		for (int j = 0; j < 3; j++)
		{
			double * temppoint = polyData->GetPoint(p->GetId(j));
			Vector3 point = R_translateMatrix*R_rotatMatrix*
				Vector3(temppoint[0], temppoint[1], temppoint[2]);
			double temp = m_data[0] * point.x * point.x + m_data[1] * point.y * point.y
				+ m_data[2] * point.z * point.z + m_data[3] * point.x * point.y
				+ m_data[4] * point.y * point.z + m_data[5] * point.x * point.z
				+ m_data[6] * point.x + m_data[7] * point.y + m_data[8] * point.z
				+ m_data[9];

			if (temp < -0.001 || temp > 0.001)
			{
				ok = false;
				break;
			}
		}
		if (ok)
		{
			points->InsertNextPoint(polyData->GetPoint(p->GetId(0)));
			points->InsertNextPoint(polyData->GetPoint(p->GetId(1)));
			points->InsertNextPoint(polyData->GetPoint(p->GetId(2)));
			p1->GetPointIds()->SetId(0, couter++);
			p1->GetPointIds()->SetId(1, couter++);
			p1->GetPointIds()->SetId(2, couter++);
			pLineCell->InsertNextCell(p1);
		}
	}

	vtkSmartPointer<vtkPolyData> polyDataNew = vtkSmartPointer<vtkPolyData>::New();
	polyDataNew->SetPoints(points); //获得网格模型中的几何数据：点集  
	polyDataNew->SetStrips(pLineCell);

	polyData = polyDataNew;
	//polyData = polyDataRestriction;
	
}
*/