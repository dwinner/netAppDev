// ShapesControl.h : Declaration of the CShapesControl

#ifndef __SHAPESCONTROL_H_
#define __SHAPESCONTROL_H_

#include "resource.h"       // main symbols
#include <atlctl.h>

// Contains the CATIDs.
#include <ObjSafe.h>
#include "AXShapesServerCP.h"
#include "thelicense.h"

/////////////////////////////////////////////////////////////////////////////
// CShapesControl
class ATL_NO_VTABLE CShapesControl : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CStockPropImpl<CShapesControl, IShapesControl, &IID_IShapesControl, &LIBID_AXSHAPESSERVERLib>,
	public CComControl<CShapesControl>,
	public IPersistStreamInitImpl<CShapesControl>,
	public IOleControlImpl<CShapesControl>,
	public IOleObjectImpl<CShapesControl>,
	public IOleInPlaceActiveObjectImpl<CShapesControl>,
	public IViewObjectExImpl<CShapesControl>,
	public IOleInPlaceObjectWindowlessImpl<CShapesControl>,
	public IConnectionPointContainerImpl<CShapesControl>,
	public IPersistStorageImpl<CShapesControl>,
	public ISpecifyPropertyPagesImpl<CShapesControl>,
	public IQuickActivateImpl<CShapesControl>,
	public IDataObjectImpl<CShapesControl>,
	public IProvideClassInfo2Impl<&CLSID_ShapesControl, &DIID__IShapesControlEvents, &LIBID_AXSHAPESSERVERLib>,
	public IPropertyNotifySinkCP<CShapesControl>,
	public CComCoClass<CShapesControl, &CLSID_ShapesControl>,
	public CProxy_IShapesControlEvents< CShapesControl >,
	public IPersistPropertyBagImpl< CShapesControl >
{
public:
	CShapesControl()
	{
		m_bWindowOnly = TRUE;
		m_shapeType = shapeCIRCLE;
		m_bstrCaption = "Yo!";	
		m_clrBackColor = RGB(255, 255, 0);

		// Create a default font to use with this control.
		static FONTDESC _fontDesc = { sizeof(FONTDESC), OLESTR("Times New Roman"), 
						FONTSIZE(12), FW_BOLD, ANSI_CHARSET, FALSE, FALSE, FALSE };
		OleCreateFontIndirect( &_fontDesc, IID_IFontDisp, (void **)&m_pFont);
	}

DECLARE_REGISTRY_RESOURCEID(IDR_SHAPESCONTROL)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CShapesControl)
	COM_INTERFACE_ENTRY(IShapesControl)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(IViewObjectEx)
	COM_INTERFACE_ENTRY(IViewObject2)
	COM_INTERFACE_ENTRY(IViewObject)
	COM_INTERFACE_ENTRY(IOleInPlaceObjectWindowless)
	COM_INTERFACE_ENTRY(IOleInPlaceObject)
	COM_INTERFACE_ENTRY2(IOleWindow, IOleInPlaceObjectWindowless)
	COM_INTERFACE_ENTRY(IOleInPlaceActiveObject)
	COM_INTERFACE_ENTRY(IOleControl)
	COM_INTERFACE_ENTRY(IOleObject)
	COM_INTERFACE_ENTRY(IPersistStreamInit)
	COM_INTERFACE_ENTRY2(IPersist, IPersistStreamInit)
	COM_INTERFACE_ENTRY(IConnectionPointContainer)
	COM_INTERFACE_ENTRY(ISpecifyPropertyPages)
	COM_INTERFACE_ENTRY(IQuickActivate)
	COM_INTERFACE_ENTRY(IPersistStorage)
	COM_INTERFACE_ENTRY(IDataObject)
	COM_INTERFACE_ENTRY(IProvideClassInfo)
	COM_INTERFACE_ENTRY(IProvideClassInfo2)
	COM_INTERFACE_ENTRY_IMPL(IConnectionPointContainer)
	COM_INTERFACE_ENTRY(IPersistPropertyBag)
END_COM_MAP()

BEGIN_PROP_MAP(CShapesControl)
	PROP_DATA_ENTRY("_cx", m_sizeExtent.cx, VT_UI4)
	PROP_DATA_ENTRY("_cy", m_sizeExtent.cy, VT_UI4)
	PROP_ENTRY("BackColor", DISPID_BACKCOLOR, CLSID_StockColorPage)
	PROP_ENTRY("Caption", DISPID_CAPTION, CLSID_CoCusomPage)
	PROP_ENTRY("Font", DISPID_FONT, CLSID_StockFontPage)
	PROP_ENTRY("ShapeType", 1, CLSID_CoCusomPage)
END_PROP_MAP()

BEGIN_CONNECTION_POINT_MAP(CShapesControl)
	CONNECTION_POINT_ENTRY(IID_IPropertyNotifySink)
	CONNECTION_POINT_ENTRY(DIID__IShapesControlEvents)
END_CONNECTION_POINT_MAP()

BEGIN_MSG_MAP(CShapesControl)
	CHAIN_MSG_MAP(CComControl<CShapesControl>)
	DEFAULT_REFLECTION_HANDLER()
	MESSAGE_HANDLER(WM_LBUTTONDOWN, OnLButtonDown)
END_MSG_MAP()
// Handler prototypes:
//  LRESULT MessageHandler(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
//  LRESULT CommandHandler(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
//  LRESULT NotifyHandler(int idCtrl, LPNMHDR pnmh, BOOL& bHandled);


// Category map.
BEGIN_CATEGORY_MAP(CShapesControl)
	IMPLEMENTED_CATEGORY(CATID_SafeForScripting)
	IMPLEMENTED_CATEGORY(CATID_SafeForInitializing)
END_CATEGORY_MAP()

// IClassFactory2
// Uncomment to enable license check...
// DECLARE_CLASSFACTORY2(CTheLicense)


// IViewObjectEx
	DECLARE_VIEW_STATUS(VIEWSTATUS_SOLIDBKGND | VIEWSTATUS_OPAQUE)

// IShapesControl
public:
	STDMETHOD(AboutBox)();
	STDMETHOD(get_ShapeType)(/*[out, retval]*/ CURRENTSHAPE *pVal);
	STDMETHOD(put_ShapeType)(/*[in]*/ CURRENTSHAPE newVal);
	HRESULT OnDraw(ATL_DRAWINFO& di);
	
	OLE_COLOR m_clrBackColor;
	CComBSTR m_bstrCaption;
	CComPtr<IFontDisp> m_pFont;
	CURRENTSHAPE m_shapeType;

	LRESULT OnLButtonDown(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
	{
		short xPos = LOWORD(lParam);
		short yPos = HIWORD(lParam);
		Fire_ClickedControl(xPos, yPos);
		return 0;
	}

	// Called when the container changes an ambient property.
	STDMETHOD(OnAmbientPropertyChange)(DISPID dispid)
	{
		if(dispid == DISPID_AMBIENT_BACKCOLOR)
		{
			ATLTRACE("The client changed the backgound color again...\n");
		}
		return S_OK;
	}
};

#endif //__SHAPESCONTROL_H_
