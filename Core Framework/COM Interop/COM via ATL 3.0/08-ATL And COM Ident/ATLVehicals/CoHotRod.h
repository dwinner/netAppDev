// CoHotRod.h : Declaration of the CCoHotRod

#ifndef __COHOTROD_H_
#define __COHOTROD_H_

#include "resource.h"       // main symbols
#include "ATLTearOff.h"

/////////////////////////////////////////////////////////////////////////////
// CCoHotRod

/************************************************
	This ATL class illustrates COM aggregation.
************************************************/

class ATL_NO_VTABLE CCoHotRod : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CCoHotRod, &CLSID_CoHotRod>,
	public IDragRace
{
public:
	CCoHotRod() : pUnkInnerCar(NULL)
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_COHOTROD)

DECLARE_PROTECT_FINAL_CONSTRUCT()

// Need this to forward the outer IUnknown to the aggregated object.
DECLARE_GET_CONTROLLING_UNKNOWN()

	// Create the aggregate.
	HRESULT FinalConstruct()
	{

		MessageBox(NULL, "Created a ATLCoCar using aggregation!", 
				   "Message from hotrod", MB_OK | MB_SETFOREGROUND);

		HRESULT hr;
		hr = CoCreateInstance(CLSID_ATLCoCar, GetControllingUnknown(), CLSCTX_SERVER,
							  IID_IUnknown, (void**)&pUnkInnerCar);
		return S_OK;
	}

	// Kill the inner object within FinalRelease()
	void FinalRelease()
	{
		pUnkInnerCar->Release();
		MessageBox(NULL, "The HOTROD is dead!", 
				   "Message from hotrod", MB_OK | MB_SETFOREGROUND);

	}


BEGIN_COM_MAP(CCoHotRod)
	COM_INTERFACE_ENTRY(IDragRace)
	COM_INTERFACE_ENTRY_AGGREGATE(IID_ICreateCar, pUnkInnerCar)
	COM_INTERFACE_ENTRY_AGGREGATE(IID_IStats, pUnkInnerCar)
	COM_INTERFACE_ENTRY_AGGREGATE(IID_IEngine, pUnkInnerCar)
	COM_INTERFACE_ENTRY_AGGREGATE(IID_IOwnerInfo, pUnkInnerCar)
END_COM_MAP()

// ICoHotRod
public:
	STDMETHOD(DragRace)();
	IUnknown *pUnkInnerCar;
};

#endif //__COHOTROD_H_
