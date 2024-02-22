// ATLCoCar.h : Declaration of the CATLCoCar

#ifndef __ATLCOCAR_H_
#define __ATLCOCAR_H_

#include "resource.h"       // main symbols

// Remove the line below to see 12 really fun compiler errors...
#include "icc_tearoff.h"

const int MAX_SPEED = 500;
const int MAX_NAME_LENGTH = 20;

/////////////////////////////////////////////////////////////////////////////
// CATLCoCar
class ATL_NO_VTABLE CATLCoCar : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CATLCoCar, &CLSID_ATLCoCar>,
	//public ICreateCar, 
	public IStats,
	public IEngine,
	public IOwnerInfo,
	public ICreate
{
public:
	CATLCoCar() : m_maxSpeed(0), m_currSpeed(0), m_pUnk(0)
	{
		// Set the BSTRs to an empty string
		m_petName = "";
		m_ownerName = "";
		m_ownerAddress = "";
	}

DECLARE_REGISTRY_RESOURCEID(IDR_ATLCOCAR)
DECLARE_GET_CONTROLLING_UNKNOWN()
DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CATLCoCar)	
	//COM_INTERFACE_ENTRY(ICreateCar)
	COM_INTERFACE_ENTRY(IStats)
	COM_INTERFACE_ENTRY(IEngine)
	COM_INTERFACE_ENTRY(IOwnerInfo)
	// Non-cached version
	//COM_INTERFACE_ENTRY_TEAR_OFF(IID_ICreateCar, CCreateCar_TearOff)
	COM_INTERFACE_ENTRY(ICreate)
	COM_INTERFACE_ENTRY_CACHED_TEAR_OFF(IID_ICreateCar, CCreateCar_TearOff, m_pUnk)
END_COM_MAP()


public:
	// ICreate
	STDMETHOD(Create)(/*[in]*/ BSTR ownerName, /*[in]*/ BSTR ownerAddress, /*[in]*/ BSTR petName, /*[in]*/ int maxSp);
	
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

	
	void FinalRelease()
	{
		MessageBox(NULL, "Owner class is dead...",
				   "Message from ATL CoCar.", MB_OK | MB_SETFOREGROUND);

		if(m_pUnk)
			m_pUnk->Release();
	}

	STDMETHOD(FinalConstruct)()
	{
		MessageBox(NULL, "Owner class is alive...",
				   "Message from ATL CoCar.", MB_OK | MB_SETFOREGROUND);
		return S_OK;
	}

	// We need to create a friendship because our data is 
	// private.  If we created public data we would not need
	// the following line.
	friend class CCreateCar_TearOff;

private:
	CComBSTR	m_petName;
	int			m_maxSpeed;
	int			m_currSpeed;
	CComBSTR	m_ownerName;
	CComBSTR	m_ownerAddress;
public:
	IUnknown*   m_pUnk;		// For cached tear off.
};

#endif //__ATLCOCAR_H_
