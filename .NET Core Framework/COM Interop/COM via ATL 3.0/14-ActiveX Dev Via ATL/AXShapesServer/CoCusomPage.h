// CoCusomPage.h : Declaration of the CCoCusomPage

#ifndef __COCUSOMPAGE_H_
#define __COCUSOMPAGE_H_

#include "resource.h"       // main symbols

EXTERN_C const CLSID CLSID_CoCusomPage;

#include "shapescontrol.h"

/////////////////////////////////////////////////////////////////////////////
// CCoCusomPage
class ATL_NO_VTABLE CCoCusomPage :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CCoCusomPage, &CLSID_CoCusomPage>,
	public IPropertyPageImpl<CCoCusomPage>,
	public CDialogImpl<CCoCusomPage>
{
public:
	CCoCusomPage() 
	{
		m_dwTitleID = IDS_TITLECoCusomPage;
		m_dwHelpFileID = IDS_HELPFILECoCusomPage;
		m_dwDocStringID = IDS_DOCSTRINGCoCusomPage;
	}

	enum {IDD = IDD_COCUSOMPAGE};

DECLARE_REGISTRY_RESOURCEID(IDR_COCUSOMPAGE)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CCoCusomPage) 
	COM_INTERFACE_ENTRY(IPropertyPage)
END_COM_MAP()

BEGIN_MSG_MAP(CCoCusomPage)
	CHAIN_MSG_MAP(IPropertyPageImpl<CCoCusomPage>)
	COMMAND_HANDLER(IDC_RADIOCIRCLE, BN_CLICKED, OnClickedRadiocircle)
	COMMAND_HANDLER(IDC_RADIORECT, BN_CLICKED, OnClickedRadiorect)
	COMMAND_HANDLER(IDC_RADIOSQUARE, BN_CLICKED, OnClickedRadiosquare)
	COMMAND_HANDLER(IDC_EDITCAPTION, EN_CHANGE, OnChangeEditcaption)
	MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
END_MSG_MAP()

	STDMETHOD(Apply)(void)
	{
		ATLTRACE(_T("CCoCusomPage::Apply\n"));
		for (UINT i = 0; i < m_nObjects; i++)
		{
			// First set the caption.
			IShapesControl* pSC = NULL;
			CComBSTR temp;
			GetDlgItemText( IDC_EDITCAPTION, temp.m_str);
			m_ppUnk[i]->QueryInterface(IID_IShapesControl, (void**)&pSC);
			pSC->put_Caption(temp);

			// Now set the shape.
			pSC->put_ShapeType(m_currShape);
			pSC->Release();
		}
		m_bDirty = FALSE;
		return S_OK;
	}
	LRESULT OnClickedRadiocircle(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
	{
		m_currShape = shapeCIRCLE;
		SetDirty(TRUE);
		return 0;
	}
	LRESULT OnClickedRadiorect(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
	{
		m_currShape = shapeRECTANGLE;
		SetDirty(TRUE);
		return 0;
	}
	LRESULT OnClickedRadiosquare(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
	{
		m_currShape = shapeSQUARE;
		SetDirty(TRUE);
		return 0;
	}
	LRESULT OnChangeEditcaption(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
	{
		SetDirty(TRUE);
		return 0;
	}
	LRESULT OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
	{
		USES_CONVERSION;
		// Show the values from the first selected item
		if (m_nObjects > 0)
		{
			// Get the first item selected.
			IShapesControl* pSC = NULL;
			CComBSTR temp;
			m_ppUnk[0]->QueryInterface(IID_IShapesControl, (void**)&pSC);
			
			// Set the caption.
			pSC->get_Caption(&temp.m_str);
			SetDlgItemText(IDC_EDITCAPTION, W2A(temp.m_str));

			// Now set focus to the correct radio item.
			CURRENTSHAPE curShape;
			pSC->get_ShapeType(&curShape);
			CheckRadioButton(IDC_RADIOCIRCLE, IDC_RADIORECT, 
							 IDC_RADIOCIRCLE + curShape);

			// All done!
			pSC->Release();
			SetDirty(FALSE);
		}

		return 0;
	}
private:
	CURRENTSHAPE m_currShape;


};

#endif //__COCUSOMPAGE_H_
