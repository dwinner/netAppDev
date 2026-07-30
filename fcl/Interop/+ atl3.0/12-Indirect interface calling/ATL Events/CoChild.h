// CoStats.h : Declaration of the CCoChild

#ifndef __COSTATS_H_
#define __COSTATS_H_

#include "resource.h"       // main symbols
#include "ATLEventsCP.h"

/////////////////////////////////////////////////////////////////////////////
// CCoChild
class ATL_NO_VTABLE CCoChild : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CCoChild, &CLSID_CoChild>,
	public IConnectionPointContainerImpl<CCoChild>,
	public IDispatchImpl<IChild, &IID_IChild, &LIBID_ATLEVENTSLib>,
	public CProxy_IChildEvents< CCoChild >
{
public:
	CCoChild()
	{
		// Init the private data.
		m_bstrName = L"";
		m_bstrMessage = L"";
		m_Age = 1;
	}

DECLARE_REGISTRY_RESOURCEID(IDR_COSTATS)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CCoChild)
	COM_INTERFACE_ENTRY(IChild)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(IConnectionPointContainer)
	COM_INTERFACE_ENTRY_IMPL(IConnectionPointContainer)
END_COM_MAP()


BEGIN_CONNECTION_POINT_MAP(CCoChild)
// This is incorrect... 
// CONNECTION_POINT_ENTRY(IID__IChildEvents)
CONNECTION_POINT_ENTRY(DIID__IChildEvents)
END_CONNECTION_POINT_MAP()


// IChild
public:
	STDMETHOD(AskChildQuestion)();
	STDMETHOD(get_Age)(/*[out, retval]*/ short *pVal);
	STDMETHOD(put_Age)(/*[in]*/ short newVal);
	STDMETHOD(get_Name)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Name)(/*[in]*/ BSTR newVal);

private:
	void GetAMessage();
	CComBSTR m_bstrMessage;
	short m_Age;
	CComBSTR m_bstrName;
};

#endif //__COSTATS_H_
