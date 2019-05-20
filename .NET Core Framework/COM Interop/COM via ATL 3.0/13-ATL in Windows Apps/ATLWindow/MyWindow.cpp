#include "stdafx.h"
#include "mywindow.h"
#include "resource.h"

CMyWindow::CMyWindow()
{
	// Set the BSTR
	m_Text = "Default String";
	m_Rect.left = 10;
	m_Rect.right = 70;
	m_Rect.top = 10;
	m_Rect.bottom = 70;
		
	// Set the custom icon.
	HICON hCustomIcon = LoadIcon(_Module.GetResourceInstance(), 
		                         MAKEINTRESOURCE(IDI_MAINICON));
	GetWndClassInfo().m_wc.hIcon = hCustomIcon;
	
	// Load the custom menu.
	HMENU hMenu = LoadMenu(_Module.GetResourceInstance(), 
		                   MAKEINTRESOURCE(IDR_MAINMENU));

	// Call inherited Create method to get window going.
	RECT r = {200, 200, 500, 500};
	Create(NULL, r, "My ATL Window", 0, 0, (UINT)hMenu);

	// Set color 
	colFore = RGB(0, 255, 0);
}

CMyWindow::~CMyWindow()
{
	// Destroy this window.
	if(m_hWnd)
		DestroyWindow();
}


LRESULT CMyWindow::OnPaint(UINT msg, WPARAM wparam, LPARAM lparam, BOOL& handled)
{
	PAINTSTRUCT ps;
	HDC hdc = GetDC();
	HBRUSH hOldBrush, hBrush;

	BeginPaint(&ps);

	// Draw Text.
	USES_CONVERSION;
	TextOut(hdc, 70, 70, OLE2A(m_Text.m_str), m_Text.Length());

	// Make a filled circle.
	OleTranslateColor(m_clrFillColor, NULL, &colFore);
	hBrush = CreateSolidBrush(colFore);   
	hOldBrush = (HBRUSH)SelectObject(hdc, hBrush);
	Ellipse(hdc,m_Rect.left,m_Rect.top,m_Rect.right,m_Rect.bottom);
	
	SelectObject(hdc, hOldBrush);
	DeleteObject(hBrush);
	
	EndPaint(&ps);
	ReleaseDC(hdc);
	return 0;
}


void CMyWindow::ChangeTheColor(OLE_COLOR newColor)
{
	// Set the OLE_COLOR.
	m_clrFillColor = newColor;
	Invalidate();
}

void CMyWindow::DrawACircle(int top, int left, int bottom, int right)
{
	m_Rect.left		= left;
	m_Rect.right	= right;
	m_Rect.top		= top;
	m_Rect.bottom	= bottom;
	Invalidate();
}

void CMyWindow::ChangeText(BSTR newString)
{
	m_Text = newString;
	Invalidate();
}