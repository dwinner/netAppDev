// DualCoCar.h : Declaration of the CDualCoCar

#ifndef __DUALCOCAR_H_
#define __DUALCOCAR_H_

#include "resource.h"       // main symbols

const int MAX_SPEED = 500;
const int MAX_NAME_LENGTH = 20;

// Contains the CATIDs.
#include <ObjSafe.h>
/////////////////////////////////////////////////////////////////////////////
// CDualCoCar
class ATL_NO_VTABLE CDualCoCar : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CDualCoCar, &CLSID_DualCoCar>,
	public IDispatchImpl<IDualCoCar, &IID_IDualCoCar, &LIBID_SCRIPTIBLECOCARLib>,
	public ICreateCar,
	public IStats,
	public IEngine,
	public IOwnerInfo
{
public:
	CDualCoCar(): m_maxSpeed(0), m_currSpeed(0)
	{
		// Set the BSTRs to an empty string.
		m_petName = "";
		m_ownerName = "";
		m_ownerAddress = "";
	}

	HRESULT FinalConstruct()
	{
		MessageBox(NULL, "Made the car", "Hello!", MB_OK | MB_SETFOREGROUND);
		return S_OK;
	}

DECLARE_REGISTRY_RESOURCEID(IDR_DUALCOCAR)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CDualCoCar)
	COM_INTERFACE_ENTRY(IDualCoCar)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(ICreateCar)
	COM_INTERFACE_ENTRY(IStats)
	COM_INTERFACE_ENTRY(IEngine)
	COM_INTERFACE_ENTRY(IOwnerInfo)
END_COM_MAP()

BEGIN_CATEGORY_MAP(CDualCoCar)
	IMPLEMENTED_CATEGORY(CATID_SafeForScripting)
	IMPLEMENTED_CATEGORY(CATID_SafeForInitializing)
END_CATEGORY_MAP()

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

private:
	CComBSTR	m_petName;
	int			m_maxSpeed;
	int			m_currSpeed;
	CComBSTR	m_ownerName;
	CComBSTR	m_ownerAddress;
};


#endif //__DUALCOCAR_H_
