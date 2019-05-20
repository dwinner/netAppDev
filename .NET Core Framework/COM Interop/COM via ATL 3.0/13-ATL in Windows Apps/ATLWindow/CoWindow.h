// CoWindow.h : Declaration of the CCoWindow

#ifndef __COWINDOW_H_
#define __COWINDOW_H_

#include "resource.h"       // main symbols
#include "mywindow.h"
/////////////////////////////////////////////////////////////////////////////
// CCoWindow
class ATL_NO_VTABLE CCoWindow : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CCoWindow, &CLSID_CoWindow>,
	public IDispatchImpl<ICoWindow, &IID_ICoWindow, &LIBID_ATLWINDOWEXAMPLELib>
{
public:
	CCoWindow()	: m_theWnd(NULL)
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_COWINDOW)
DECLARE_NOT_AGGREGATABLE(CCoWindow)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CCoWindow)
	COM_INTERFACE_ENTRY(ICoWindow)
	COM_INTERFACE_ENTRY(IDispatch)
END_COM_MAP()


public:

// ICoWindow
	STDMETHOD(KillMyWindow)();
	STDMETHOD(CreateMyWindow)();
	STDMETHOD(ChangeWindowText)(BSTR newText);
	STDMETHOD(ChangeTheColor)(/*[in]*/ OLE_COLOR newColor);
	STDMETHOD(DrawCircle)(/*[in]*/ int x, /*[in]*/ int y, /*[in]*/ int height, /*[in]*/int width);

private:
	CMyWindow *m_theWnd;
};

#endif //__COWINDOW_H_
