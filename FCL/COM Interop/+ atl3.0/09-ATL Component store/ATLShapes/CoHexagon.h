// CoHexagon.h : Declaration of the CCoHexagon

#ifndef __COHEXAGON_H_
#define __COHEXAGON_H_

#include "resource.h"       // main symbols
#include "catids.h"
		   
/////////////////////////////////////////////////////////////////////////////
// CCoHexagon
class ATL_NO_VTABLE CCoHexagon : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CCoHexagon, &CLSID_CoHexagon>,
	public IDraw,
	public IShapeID
{
public:
	CCoHexagon()
	{
		m_name = "";
	}

//DECLARE_REGISTRY_RESOURCEID(IDR_COHEXAGON)

static HRESULT WINAPI UpdateRegistry(BOOL b)
{
	// We will add the time that this class was installed
	// as part of our custom registration.
	USES_CONVERSION;

	char time[50];
	SYSTEMTIME sysTime;
	GetLocalTime(&sysTime);
	
	// Format the time into the char array.
	sprintf(time, "Hour %.2d : Min %.2d : Second %.2d : MS %.2d", 
			  sysTime.wHour, sysTime.wMinute, 
			  sysTime.wSecond, sysTime.wMilliseconds);

	OLECHAR* name = OLESTR("Your name goes here...");
	
	// Now we need to create a special tag to use in the RGS script.
	_ATL_REGMAP_ENTRY regMap[] = { { OLESTR("INSTALLTIME"), A2W(time)},
					               { OLESTR("DEVELOPER"), name},
								   {0, 0} };
	
	return _Module.UpdateRegistryFromResource(IDR_COHEXAGON, b, regMap);
}

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CCoHexagon)
	COM_INTERFACE_ENTRY(IDraw)
	COM_INTERFACE_ENTRY(IShapeID)
END_COM_MAP()

// The Category Map
BEGIN_CATEGORY_MAP(CCoHexagon)
	IMPLEMENTED_CATEGORY(CATID_NamedShape)
END_CATEGORY_MAP()


// IDraw
public:
	STDMETHOD(GetOSBuffer)(/*[out, retval]*/ IOSBuffer** pBuffer);
	STDMETHOD(get_ShapeName)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_ShapeName)(/*[in]*/ BSTR newVal);
	STDMETHOD(Draw)();
// IShapeID

private:
	CComBSTR m_name;
};

#endif //__COHEXAGON_H_
