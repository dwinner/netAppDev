// AxCar.h : Declaration of the CAxCar

#ifndef __AXCAR_H_
#define __AXCAR_H_

#include "resource.h"       // main symbols
#include <atlctl.h>
#include "AXCarServerCP.h"
#include <ObjSafe.h>
// Some constants to refer to what the images are used for.
const int MAX_IMAGES = 5;
const int MAX_NORMAL = 3;
const int ABOUT_TO_BLOW = 4;
const int BLOWN_UP = 5;

/////////////////////////////////////////////////////////////////////////////
// CAxCar
class ATL_NO_VTABLE CAxCar : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public IDispatchImpl<IAxCar, &IID_IAxCar, &LIBID_AXCARSERVERLib>,
	public CComControl<CAxCar>,
	public IPersistStreamInitImpl<CAxCar>,
	public IOleControlImpl<CAxCar>,
	public IOleObjectImpl<CAxCar>,
	public IOleInPlaceActiveObjectImpl<CAxCar>,
	public IViewObjectExImpl<CAxCar>,
	public IOleInPlaceObjectWindowlessImpl<CAxCar>,
	public IConnectionPointContainerImpl<CAxCar>,
	public IPersistStorageImpl<CAxCar>,
	public ISpecifyPropertyPagesImpl<CAxCar>,
	public IQuickActivateImpl<CAxCar>,
	public IDataObjectImpl<CAxCar>,
	public IProvideClassInfo2Impl<&CLSID_AxCar, &DIID__IAxCarEvents, &LIBID_AXCARSERVERLib>,
	public IPropertyNotifySinkCP<CAxCar>,
	public CComCoClass<CAxCar, &CLSID_AxCar>,
	public CProxy_IAxCarEvents< CAxCar >,
	public IPersistPropertyBagImpl<CAxCar>
{
public:
	CAxCar()
	{
		m_bWindowOnly = TRUE;	// Must ensure a valid handle.
		m_maxSpeed = 0;
		m_bstrPetName = L"";
		m_currSpeed = 0;
		m_bAnimate = No;
		m_currImage = 0;
		m_currMaxFrames = MAX_NORMAL;
	}

	HRESULT FinalConstruct()
	{
		// Load up all bitmaps.
		for(int i = 0; i < MAX_IMAGES; i++)
		{
			m_carImage[i] = 
				LoadBitmap(_Module.m_hInst, MAKEINTRESOURCE(IDB_LEMON1 + i));
		}
		return S_OK;
	}

	void FinalRelease()
	{
		for(int i = 0; i < MAX_IMAGES; i++)
			DeleteObject(m_carImage[i]);
	}

DECLARE_REGISTRY_RESOURCEID(IDR_AXCAR)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CAxCar)
	COM_INTERFACE_ENTRY(IAxCar)
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

BEGIN_PROP_MAP(CAxCar)
	PROP_DATA_ENTRY("_cx", m_sizeExtent.cx, VT_UI4)
	PROP_DATA_ENTRY("_cy", m_sizeExtent.cy, VT_UI4)
	PROP_ENTRY("Animate", 3, CLSID_ConfigureCar)
	PROP_ENTRY("Speed", 4, CLSID_ConfigureCar)
	PROP_ENTRY("PetName", 5, CLSID_ConfigureCar)
	PROP_ENTRY("MaxSpeed", 6, CLSID_ConfigureCar)
	// Example entries
	// PROP_ENTRY("Property Description", dispid, clsid)
	// PROP_PAGE(CLSID_StockColorPage)
END_PROP_MAP()

BEGIN_CONNECTION_POINT_MAP(CAxCar)
	CONNECTION_POINT_ENTRY(IID_IPropertyNotifySink)
	CONNECTION_POINT_ENTRY(DIID__IAxCarEvents)
END_CONNECTION_POINT_MAP()

BEGIN_MSG_MAP(CAxCar)
	CHAIN_MSG_MAP(CComControl<CAxCar>)
	DEFAULT_REFLECTION_HANDLER()
	MESSAGE_HANDLER(WM_TIMER, OnTimer)
END_MSG_MAP()
// Handler prototypes:
//  LRESULT MessageHandler(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
//  LRESULT CommandHandler(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
//  LRESULT NotifyHandler(int idCtrl, LPNMHDR pnmh, BOOL& bHandled);

// IViewObjectEx
	DECLARE_VIEW_STATUS(VIEWSTATUS_SOLIDBKGND | VIEWSTATUS_OPAQUE)

// Category map.
BEGIN_CATEGORY_MAP(CShapesControl)
	IMPLEMENTED_CATEGORY(CATID_SafeForScripting)
	IMPLEMENTED_CATEGORY(CATID_SafeForInitializing)
END_CATEGORY_MAP()

// IAxCar
public:
	STDMETHOD(get_Speed)(/*[out, retval]*/ short *pVal);
	STDMETHOD(put_Speed)(/*[in]*/ short newVal);
	STDMETHOD(get_MaxSpeed)(/*[out, retval]*/ short *pVal);
	STDMETHOD(put_MaxSpeed)(/*[in]*/ short newVal);
	STDMETHOD(get_PetName)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_PetName)(/*[in]*/ BSTR newVal);
	STDMETHOD(AboutBox)();
	STDMETHOD(get_Animate)(/*[out, retval]*/ AnimVal *pVal);
	STDMETHOD(put_Animate)(/*[in]*/ AnimVal newVal);
	STDMETHOD(CreateCar)(/*[in]*/ BSTR petName, /*[in]*/ short maxSp);
	STDMETHOD(SpeedUp)();
	HRESULT OnDraw(ATL_DRAWINFO& di);
	LRESULT OnTimer(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	
	short m_maxSpeed;				// Holds the car's max speed.
	CComBSTR m_bstrPetName;			// Holds the car's pet name.
	short m_currSpeed;				// Holds the car's current speed.
	HBITMAP m_carImage[MAX_IMAGES];	// An array of bitmaps to render.
	AnimVal m_bAnimate;				// Holds animation flag (Yes or No)
	short m_currImage;				// Holds the current image in the array.
	short m_currMaxFrames;			// Holds the number of frames to cycle over.
};

#endif //__AXCAR_H_
