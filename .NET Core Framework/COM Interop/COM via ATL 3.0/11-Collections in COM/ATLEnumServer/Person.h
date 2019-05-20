// Person.h : Declaration of the CPerson

#ifndef __PERSON_H_
#define __PERSON_H_

#include "resource.h"       // main symbols
/////////////////////////////////////////////////////////////////////////////
// CPerson
class ATL_NO_VTABLE CPerson : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CPerson, &CLSID_Person>,
	public IPerson
{
public:
	CPerson()
	{
		m_bstrName = "";
		m_ID = 0;
	}

DECLARE_REGISTRY_RESOURCEID(IDR_PERSON)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CPerson)
	COM_INTERFACE_ENTRY(IPerson)
END_COM_MAP()

// IPerson
public:
	STDMETHOD(get_ID)(/*[out, retval]*/ short *pVal);
	STDMETHOD(put_ID)(/*[in]*/ short newVal);
	STDMETHOD(get_Name)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Name)(/*[in]*/ BSTR newVal);

private:
	CComBSTR m_bstrName;
	short m_ID;
};

#endif //__PERSON_H_
