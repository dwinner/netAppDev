// CoBulkObject.h : Declaration of the CCoBulkObject

#ifndef __COBULKOBJECT_H_
#define __COBULKOBJECT_H_

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// CCoBulkObject
class ATL_NO_VTABLE CCoBulkObject : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CCoBulkObject, &CLSID_CoBulkObject>,
	public ICoBulkObject
{
public:
	CCoBulkObject()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_COBULKOBJECT)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CCoBulkObject)
	COM_INTERFACE_ENTRY(ICoBulkObject)
END_COM_MAP()

// ICoBulkObject
public:
	STDMETHOD(GetSubObject)(/*[out, retval]*/ ICoSub** ppv);
	STDMETHOD(UseTheseStrings)(/*[in]*/ short size, /*[in, out, size_is(size)]*/ BSTR strings[]);
	STDMETHOD(UseThisSafeArrayOfStrings)(VARIANT strings);
	STDMETHOD(GiveMeASafeArrayOfString)(VARIANT *pStrings);
	STDMETHOD(WorkWithAPet)(MyPet *p);
};

#endif //__COBULKOBJECT_H_
