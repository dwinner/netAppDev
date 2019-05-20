// CoHexagon.h : Declaration of the CCoHexagon

#ifndef __COHEXAGON_H_
#define __COHEXAGON_H_

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// CCoHexagon
class ATL_NO_VTABLE CCoHexagon : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CCoHexagon, &CLSID_CoHexagon>,
	public IDraw,
	public IShapeEdit,
	public IErase,
	public IShapeID
{
public:
	CCoHexagon() : m_name("")
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_COHEXAGON)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CCoHexagon)
	COM_INTERFACE_ENTRY(IDraw)
	COM_INTERFACE_ENTRY(IShapeEdit)
	COM_INTERFACE_ENTRY(IErase)
	COM_INTERFACE_ENTRY(IShapeID)
END_COM_MAP()

public:
	STDMETHOD(get_Name)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Name)(/*[in]*/ BSTR newVal);
	STDMETHOD(Erase)();
	STDMETHOD(Fill)(/*[in]*/ FILLTYPE fType);
	STDMETHOD(Stretch)(/*[in]*/ int factor);
	STDMETHOD(Inverse)();
	STDMETHOD(Draw)();
private:
	CComBSTR m_name;
};

#endif //__COHEXAGON_H_
