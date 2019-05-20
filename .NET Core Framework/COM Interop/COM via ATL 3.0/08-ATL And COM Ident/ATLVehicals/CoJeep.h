// CoJeep.h : Declaration of the CCoJeep

#ifndef __COJEEP_H_
#define __COJEEP_H_

#include "resource.h"       // main symbols
#include "ATLTearOff.h"


/**********************************************************
	This ATL class illustrates COM blind auto-aggregation.
***********************************************************/

/////////////////////////////////////////////////////////////////////////////
// CCoJeep
class ATL_NO_VTABLE CCoJeep : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CCoJeep, &CLSID_CoJeep>,
	public IDragRace
{
public:
	CCoJeep() : pUnkInnerCar(NULL)
	{
	}

// Need this to forward the outer IUnknown to the aggregated object.
DECLARE_GET_CONTROLLING_UNKNOWN()

DECLARE_REGISTRY_RESOURCEID(IDR_COJEEP)

DECLARE_PROTECT_FINAL_CONSTRUCT()


	// Don't need to worry about calling CoCreateInstance ourselves.
	// COM_INTERFACE_ENTRY_AUTOAGGREGATE_BLIND does it for you!

	// Kill the inner object within FinalRelease()
	void FinalRelease()
	{
		MessageBox(NULL, "JEEP is dead!", 
				   "Message from Jeep", MB_OK | MB_SETFOREGROUND);
		
		if(pUnkInnerCar)
			pUnkInnerCar->Release();
	}

BEGIN_COM_MAP(CCoJeep)
	COM_INTERFACE_ENTRY(IDragRace)
    COM_INTERFACE_ENTRY_AUTOAGGREGATE_BLIND(pUnkInnerCar, CLSID_ATLCoCar)
END_COM_MAP()

// ICoJeep
public:
	STDMETHOD(DragRace)();
	IUnknown *pUnkInnerCar;
};

#endif //__COJEEP_H_
