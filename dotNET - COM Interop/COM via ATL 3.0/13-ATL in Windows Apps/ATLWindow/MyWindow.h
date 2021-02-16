// This class represents a window.
#ifndef _MYWINDOW
#define _MYWINDOW
#include "resource.h"

class CMyWindow : public CWindowImpl<CMyWindow, CWindow,
	  CWinTraits<WS_CAPTION | WS_POPUPWINDOW | WS_VISIBLE, 0> >
{
public:
	// Helper functions.
	void ChangeText(BSTR newString);
	void DrawACircle(int x, int y, int width, int height);
	void ChangeTheColor(OLE_COLOR newColor);

	CMyWindow();
	~CMyWindow();

BEGIN_MSG_MAP(CMyWindow)
	MESSAGE_HANDLER(WM_PAINT, OnPaint)
	COMMAND_HANDLER(IDM_ABOUT, 0, OnAbout)
END_MSG_MAP()

// Message handlers.
LRESULT OnPaint(UINT msg, WPARAM wparam, LPARAM lparam, BOOL& handled);
LRESULT OnAbout(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	CSimpleDialog<IDD_ABOUTBOX> d;
	d.DoModal();
	return 0;
}

// Private Data members.
private:
	RECT m_Rect;
	CComBSTR m_Text;
	OLE_COLOR m_clrFillColor;
	COLORREF colFore;
};

#endif // _MYWINDOW