// ConfigureCar.h : Declaration of the CConfigureCar

#ifndef __CONFIGURECAR_H_
#define __CONFIGURECAR_H_

#include "resource.h"       // main symbols

EXTERN_C const CLSID CLSID_ConfigureCar;

/////////////////////////////////////////////////////////////////////////////
// CConfigureCar
class ATL_NO_VTABLE CConfigureCar :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CConfigureCar, &CLSID_ConfigureCar>,
	public IPropertyPageImpl<CConfigureCar>,
	public CDialogImpl<CConfigureCar>
{
public:
	CConfigureCar() 
	{
		m_dwTitleID = IDS_TITLEConfigureCar;
		m_dwHelpFileID = IDS_HELPFILEConfigureCar;
		m_dwDocStringID = IDS_DOCSTRINGConfigureCar;
	}

	enum {IDD = IDD_CONFIGURECAR};

DECLARE_REGISTRY_RESOURCEID(IDR_CONFIGURECAR)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CConfigureCar) 
	COM_INTERFACE_ENTRY(IPropertyPage)
END_COM_MAP()

BEGIN_MSG_MAP(CConfigureCar)
	CHAIN_MSG_MAP(IPropertyPageImpl<CConfigureCar>)
	MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
	COMMAND_HANDLER(IDC_CHECKANIM, BN_CLICKED, OnClickedCheckanim)
	COMMAND_HANDLER(IDC_EDITNAME, EN_CHANGE, OnChangeEditname)
	COMMAND_HANDLER(IDC_EDITSPEED, EN_CHANGE, OnChangeEditspeed)
END_MSG_MAP()
// Handler prototypes:
//  LRESULT MessageHandler(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
//  LRESULT CommandHandler(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
//  LRESULT NotifyHandler(int idCtrl, LPNMHDR pnmh, BOOL& bHandled);

	STDMETHOD(Apply)(void)
	{
		ATLTRACE(_T("CConfigureCar::Apply\n"));
		for (UINT i = 0; i < m_nObjects; i++)
		{
			// First set the name.
			IAxCar* pCar = NULL;
			CComBSTR temp;
			GetDlgItemText( IDC_EDITNAME, temp.m_str);
			m_ppUnk[i]->QueryInterface(IID_IAxCar, (void**)&pCar);
			pCar->put_PetName(temp);

			// Now set the anim prop.
			pCar->put_Animate((AnimVal)IsDlgButtonChecked(IDC_CHECKANIM));

			// And the Max speed.
			pCar->put_MaxSpeed(GetDlgItemInt(IDC_EDITSPEED));

			pCar->Release();
		}
		m_bDirty = FALSE;
		return S_OK;
	}
	LRESULT OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
	{
		USES_CONVERSION;

		// Show the values from the first selected item
		if (m_nObjects > 0)
		{			
			IAxCar* pCar = NULL;
			short tempSP;
			CComBSTR temp;
		
			// Get the first item selected.
			m_ppUnk[0]->QueryInterface(IID_IAxCar, (void**)&pCar);
			
			// Set the Max Speed.
			pCar->get_MaxSpeed(&tempSP);
			SetDlgItemInt(IDC_EDITSPEED, tempSP);

			// Now check Animate selection.
			AnimVal curAnim;
			pCar->get_Animate(&curAnim);
			CheckDlgButton(IDC_CHECKANIM, (BOOL)curAnim);
			
			// Finally, set the name.
			pCar->get_PetName(&temp.m_str);
			SetDlgItemText(IDC_EDITNAME, W2A(temp.m_str));

			// All done!
			pCar->Release();
			SetDirty(FALSE);
		}
		return 0;
	}
	LRESULT OnClickedCheckanim(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
	{
		SetDirty(TRUE);
		return 0;
	}
	LRESULT OnChangeEditname(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
	{
		SetDirty(TRUE);
		return 0;
	}
	LRESULT OnChangeEditspeed(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
	{
		SetDirty(TRUE);
		return 0;
	}
};

#endif //__CONFIGURECAR_H_
