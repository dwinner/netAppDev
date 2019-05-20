// ATLCoCar.h : Declaration of the CATLCoCar

#ifndef __ATLCOCAR_H_
#define __ATLCOCAR_H_

#include "resource.h"       // main symbols

const int MAX_SPEED = 500;
const int MAX_NAME_LENGTH = 20;
/////////////////////////////////////////////////////////////////////////////
// CATLCoCar
class ATL_NO_VTABLE CATLCoCar : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CATLCoCar, &CLSID_ATLCoCar>,
	public ICreateCar,
	public IStats,
	public IEngine,
	public IOwnerInfo
{
public:
	CATLCoCar() : m_maxSpeed(0), m_currSpeed(0)
	{
		// Set the BSTRs to an empty string.
		m_petName = L"";
		m_ownerName = L"";
		m_ownerAddress = L"";
	}

DECLARE_REGISTRY_RESOURCEID(IDR_ATLCOCAR)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CATLCoCar)	
	COM_INTERFACE_ENTRY(ICreateCar)
	COM_INTERFACE_ENTRY(IStats)
	COM_INTERFACE_ENTRY(IEngine)
	COM_INTERFACE_ENTRY(IOwnerInfo)
END_COM_MAP()

public:
	// IOwnerInfo
	STDMETHOD(get_Address)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Address)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_Name)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Name)(/*[in]*/ BSTR newVal);

	// IEngine
	STDMETHOD(GetCurSpeed)(/*[out, retval]*/ int* curSpeed);
	STDMETHOD(GetMaxSpeed)(/*[out, retval]*/ int* maxSpeed);
	STDMETHOD(SpeedUp)();

	// IStats
	STDMETHOD(GetPetName)(/*[out, retval]*/ BSTR* petName);
	STDMETHOD(DisplayStats)();

	// ICreateCar
	STDMETHOD(SetMaxSpeed)(/*[in]*/ int maxSp);
	STDMETHOD(SetPetName)(/*[in]*/ BSTR petName);


	// We don't need to override these, but it is useful
	// to see the car come to life and pass on...
	void FinalRelease()
	{
		MessageBox(NULL, "ATLCoCar is dead...",
				   "Message from ATL CoCar.", MB_OK | MB_SETFOREGROUND);
	}

	STDMETHOD(FinalConstruct)()
	{
		MessageBox(NULL, "ATLCoCar is ALIVE...",
				   "Message from ATL CoCar.", MB_OK | MB_SETFOREGROUND);
		
		return S_OK;
	}

private:
	CComBSTR	m_petName;
	int			m_maxSpeed;
	int			m_currSpeed;
	CComBSTR	m_ownerName;
	CComBSTR	m_ownerAddress;
};

#endif //__ATLCOCAR_H_
