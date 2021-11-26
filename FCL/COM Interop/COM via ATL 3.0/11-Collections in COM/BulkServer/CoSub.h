// CoSub.h : Declaration of the CCoSub

#ifndef __COSUB_H_
#define __COSUB_H_

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// CCoSub
class ATL_NO_VTABLE CCoSub : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CCoSub, &CLSID_CoSub>,
	public ICoSub
{
public:
	CCoSub() : m_data(100)
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_COSUB)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CCoSub)
	COM_INTERFACE_ENTRY(ICoSub)
END_COM_MAP()

// ICoSub
public:
	STDMETHOD(get_DatumOne)(/*[out, retval]*/ short *pVal);
	STDMETHOD(put_DatumOne)(/*[in]*/ short newVal);
private:
	short m_data;
};

#endif //__COSUB_H_
