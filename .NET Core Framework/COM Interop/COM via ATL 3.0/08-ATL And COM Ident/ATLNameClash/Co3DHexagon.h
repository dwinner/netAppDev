// Co3DHexagon.h : Declaration of the CCo3DHexagon

#ifndef __CO3DHEXAGON_H_
#define __CO3DHEXAGON_H_

#include "resource.h"       // main symbols
#include "wrappers.h"

/////////////////////////////////////////////////////////////////////////////
// CCo3DHexagon
class ATL_NO_VTABLE CCo3DHexagon : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CCo3DHexagon, &CLSID_Co3DHexagon>,
	// public IDraw,
	// public I3DRender,
	public IDrawImpl<CCo3DHexagon>,
	public I3DRenderImpl
{
public:
	CCo3DHexagon()
	{
	}



DECLARE_REGISTRY_RESOURCEID(IDR_CO3DHEXAGON)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CCo3DHexagon)
	COM_INTERFACE_ENTRY(IDraw)
	COM_INTERFACE_ENTRY(I3DRender)
END_COM_MAP()


public:
	STDMETHOD(Another3dRender)();
	STDMETHOD(AnotherIDraw)();

	STDMETHOD(Draw2DRect)();
	STDMETHOD(Draw3DRect)();
// I3DRender
};

#endif //__CO3DHEXAGON_H_
