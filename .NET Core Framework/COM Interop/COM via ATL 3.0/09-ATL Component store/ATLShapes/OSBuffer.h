// OSBuffer.h : Declaration of the COSBuffer

#ifndef __OSBUFFER_H_
#define __OSBUFFER_H_

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// COSBuffer
class ATL_NO_VTABLE COSBuffer : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<COSBuffer, &CLSID_OSBuffer>,
	public IOSBuffer
{
public:
	COSBuffer()
	{
	}

// DECLARE_REGISTRY_RESOURCEID(IDR_OSBUFFER)
DECLARE_NO_REGISTRY()

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(COSBuffer)
	COM_INTERFACE_ENTRY(IOSBuffer)
END_COM_MAP()

// IOSBuffer
public:
	STDMETHOD(RenderToMemory)();

	// For validation of creation / destruction
	STDMETHOD(FinalConstruct)()
	{
		MessageBox(NULL, "Constructed!", "Off Screen Buffer Says...",
				   MB_OK | MB_SETFOREGROUND);

		return S_OK;
	}

	void FinalRelease() 
	{
		MessageBox(NULL, "Destroyed!", "Off Screen Buffer Says...",
				   MB_OK | MB_SETFOREGROUND);
		
	}
};

#endif //__OSBUFFER_H_
