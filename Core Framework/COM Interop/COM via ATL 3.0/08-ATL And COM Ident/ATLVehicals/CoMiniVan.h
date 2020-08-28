// CoMiniVan.h : Declaration of the CCoMiniVan

#ifndef __COMINIVAN_H_
#define __COMINIVAN_H_

#include "resource.h"       // main symbols
#include "ATLTearOff.h"
 
/////////////////////////////////////////////////////////////////////////////
// CCoMiniVan

/************************************************
	This ATL class illustrates COM containment.
************************************************/

class ATL_NO_VTABLE CCoMiniVan : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CCoMiniVan, &CLSID_CoMiniVan>,
	public IDragRace,
	public ICreateMiniVan,
	public IEngine,
	public IStats
{
public:
	CCoMiniVan() :	m_pCarUnk(NULL), m_pCreate(NULL), 
					m_pEngine(NULL), m_pStats(NULL), m_numberOfDoors(0)
	{
	}

	// Best place to create a contained object
	// is within the FinalConstruct() method.
	HRESULT FinalConstruct()
	{
		HRESULT hr;
		hr = CoCreateInstance(CLSID_ATLCoCar, NULL, CLSCTX_SERVER,
							  IID_IUnknown, (void**)&m_pCarUnk);

		if(SUCCEEDED(hr))
		{
			MessageBox(NULL, "Created a ATLCoCar using containment!", 
					   "Message from Minivan", MB_OK | MB_SETFOREGROUND);
			
			// Lets get the interfaces right up front.
			m_pCarUnk->QueryInterface(IID_ICreate, (void**)&m_pCreate);
			m_pCarUnk->QueryInterface(IID_IEngine, (void**)&m_pEngine);
			m_pCarUnk->QueryInterface(IID_IStats, (void**)&m_pStats);
		}	
		else
			return E_FAIL;	// Don't let them make minivan
							// if we can't make a car.
		return S_OK;
	}

	// Kill the inner object within FinalRelease()
	void FinalRelease()
	{
		m_pCarUnk->Release();
		m_pCreate->Release();
		m_pEngine->Release();
		m_pStats->Release();

		MessageBox(NULL, "The Minivan is dead...", 
					   "Message from Minivan", MB_OK | MB_SETFOREGROUND);

	}

DECLARE_REGISTRY_RESOURCEID(IDR_COMINIVAN)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CCoMiniVan)
	COM_INTERFACE_ENTRY(IDragRace)
	COM_INTERFACE_ENTRY(IEngine)
	COM_INTERFACE_ENTRY(IStats)
	COM_INTERFACE_ENTRY(ICreateMiniVan)
END_COM_MAP()

public:
	STDMETHOD(get_NumberOfDoors)(/*[out, retval]*/ short *pVal);
	// IDragRace
	STDMETHOD(DragRace)();
	
	// IEngine.
	STDMETHOD(GetCurSpeed)(/*[out, retval]*/ int* curSpeed);
	STDMETHOD(GetMaxSpeed)(/*[out, retval]*/ int* maxSpeed);
	STDMETHOD(SpeedUp)();

	// IStats
	STDMETHOD(GetPetName)(/*[out, retval]*/ BSTR* petName);
	STDMETHOD(DisplayStats)();

	// IMiniVan
	STDMETHOD(CreateMiniVan)(BSTR petName, int maxSpeed, BSTR ownerName, 
							 BSTR ownerAddress, int numberOfDoors);

private:
	// These are private interfaces on the 'inner' object.
	IUnknown* m_pCarUnk;
	ICreate* m_pCreate;
	IEngine* m_pEngine;
	IStats* m_pStats;

	// State data.
	int m_numberOfDoors;
};

#endif //__COMINIVAN_H_
