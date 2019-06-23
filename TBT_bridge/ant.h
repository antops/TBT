#pragma once
#include "interface_cs.h"
#include <vector>

namespace TBTBack {
	class NodeBase;
}
namespace TBTfront {
	public ref class Ant
	{

	public:
		Ant();
		~Ant();

		int clacLight(CompontList componts, RayLineCluster ^ input,
					  System::Collections::Generic::List<RayLineCluster^>^ output,
					  CalcOption^ option);

		int clacGaussian(CompontList componts, GaussianCluster ^ input,
			System::Collections::Generic::List<GaussianCluster^>^ output,
			CalcOption^ option);

		int clacField(CompontList componts, FieldBase ^input, FieldBase ^output);

		int getCount() { return count; };

		static void coordinateUV2Rotate(Coordinate ^coor);
		static void coordinateRotate2UV(Coordinate ^coor);

	private:
		int createCompont(CompontList compont_front, std::vector<TBTBack::NodeBase*>& compont_back);

	private:
		// ½ø¶ÈÌõ£¿
		int count;
		

	};
}