// CoSquiggle.h : Declaration of the CCoSquiggle

#ifndef __COSQUIGGLE_H_
#define __COSQUIGGLE_H_

#include "resource.h"       // main symbols


/////////////////////////////////////////////////////////////////////////////
// CCoSquiggle
class ATL_NO_VTABLE CCoSquiggle : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CCoSquiggle, &CLSID_CoSquiggle>,
	public IDispatchImpl<ICoSquiggle, &IID_ICoSquiggle, &LIBID_SQUIGGLECOLLECTIONLib>
{
public:
	CCoSquiggle() : m_bstrName("")
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_COSQUIGGLE)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CCoSquiggle)
	COM_INTERFACE_ENTRY(ICoSquiggle)
	COM_INTERFACE_ENTRY(IDispatch)
END_COM_MAP()

// ICoSquiggle
public:
	STDMETHOD(Draw)();
	STDMETHOD(get_Name)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Name)(/*[in]*/ BSTR newVal);
	// Kill the squiggles!
	void FinalRelease()
	{
		ATLTRACE("Killed a squiggle...\n");
	}
private:
	CComBSTR m_bstrName;

};


#endif