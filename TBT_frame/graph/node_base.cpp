#include "node_base.h"
#include "mirror/mirror_quadric_surface.h"
#include "mirror/Restriction.h"
#include "mirror/mirror_stl.h"
#include "source/source_guassian.h"
#include "../../TBT_common/MyLog.h"
namespace TBTBack {
	NodeBase::NodeBase()
	{
	}

	NodeBase::~NodeBase()
	{
	}

	int NodeBase::init(CompontParam & param)
	{
		cpt_param_ = param;
		// 需要将 param指针release
		param.release();
		return 0;
	}

	NodeBase * NodeBaseFactory::genCompont(CompontParam & param)
	{
		NodeBase* item = nullptr;
		if (param.name == "guassian") {
			item = new SourceGuassian();
		}
		else if (param.name == "quadricSurface") {
			item = new MirrorQuadricSurface();
		}
		else if (param.name == "restriction")
		{
			item = new Restriction();
		}
		else if (param.name == "stl")
		{
			item = new MirrorSTL();
		}
		else {
			LOG(ERROR) << "unknown name:" << param.name;
			return nullptr;
		}
		int res = item->init(param);
		if (res != 0) {
			LOG(ERROR) << "init failed, name:" << param.name;
			delete item;
			return nullptr;
		}
		return item;
	}
}
