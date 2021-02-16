// CoWindow.cpp : Implementation of CCoWindow
#include "stdafx.h"
#include "ATLWindowExample.h"
#include "CoWindow.h"

/////////////////////////////////////////////////////////////////////////////
// CCoWindow


STDMETHODIMP CCoWindow::CreateMyWindow()
{
	// User wants a new window.
	m_theWnd = new CMyWindow;
	m_theWnd->CenterWindow();
	return S_OK;
}

STDMETHODIMP CCoWindow::KillMyWindow()
{
	// Kill window.
	if(m_theWnd)
		delete m_theWnd;

	return S_OK;
}

STDMETHODIMP CCoWindow::ChangeWindowText(BSTR newText)
{
	// Change the text.
	if(m_theWnd)	
		m_theWnd ->ChangeText(newText);
	else
		return E_FAIL;

	return S_OK;
}

STDMETHODIMP CCoWindow::DrawCircle(int top, int left, int bottom, int right)
{
	// Draw!
	if(m_theWnd)	
		m_theWnd -> DrawACircle(top, left, bottom, right);
	else
		return E_FAIL;

	return S_OK;
}

STDMETHODIMP CCoWindow::ChangeTheColor(OLE_COLOR newColor)
{
	// Chnage the color.
	if(m_theWnd)
		m_theWnd -> ChangeTheColor(newColor);	
	else
		return E_FAIL;
	
	return S_OK;
}
