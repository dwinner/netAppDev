// ShapesControl.cpp : Implementation of CShapesControl

#include "stdafx.h"
#include "AXShapesServer.h"
#include "ShapesControl.h"
#include "resource.h"

/////////////////////////////////////////////////////////////////////////////
// CShapesControl


STDMETHODIMP CShapesControl::get_ShapeType(CURRENTSHAPE *pVal)
{
	// return the value to container.
	*pVal = m_shapeType;

	return S_OK;
}

STDMETHODIMP CShapesControl::put_ShapeType(CURRENTSHAPE newVal)
{
	// Is this change OK with the container?
	if(FAILED(FireOnRequestEdit(1)))	// [id(1)] = ShapeType
		return S_FALSE;
	
	// Check bounds.
	if(newVal >= shapeCIRCLE && newVal <= shapeRECTANGLE)
		m_shapeType = newVal;
	
	// Set dirty flag.
	SetDirty(TRUE);

	// Redraw the view (i.e. cal OnDraw())
	FireViewChange();

	// Tell container we have chagned the value.
	FireOnChanged(1);	// [id(1)] = ShapeType
	return S_OK;
}

HRESULT CShapesControl::OnDraw(ATL_DRAWINFO& di)
{
	USES_CONVERSION;
	COLORREF colBack;
	HBRUSH oldBrush, newBrush;
	HPEN oldPen, newPen;
	HFONT oldFont, newFont;
	HDC hdc = di.hdcDraw;
	
	// Get the size of this view.
	RECT& rc = *(RECT*)di.prcBounds;
	Rectangle(hdc, rc.left, rc.top, rc.right, rc.bottom);

	// Fill the background using the BackColor stock prop.
	OleTranslateColor(m_clrBackColor, NULL, &colBack);
	newBrush = (HBRUSH) CreateSolidBrush(colBack);
	oldBrush = (HBRUSH) SelectObject(hdc, newBrush);
	FillRect(hdc, &rc, newBrush);

	// Draw the correct shape with a big blue pen.
	newPen = CreatePen(PS_SOLID, 10, RGB(0, 0, 255));
	oldPen = (HPEN) SelectObject(hdc, newPen);
	
	switch(m_shapeType)
	{
	case shapeCIRCLE:
		Ellipse(hdc, rc.left, rc.top, rc.right, rc.bottom);
		break;
	case shapeSQUARE:
		Rectangle(hdc, rc.left, rc.top, rc.right, rc.bottom);
		break;
	case shapeRECTANGLE:
		Rectangle(hdc, rc.left + 20, rc.top + 30, 
				  rc.right - 20, rc.bottom - 30);
		break;
	}

	// Draw the Caption stock property with the correct font.
	CComQIPtr<IFont, &IID_IFont> pFont(m_pFont);
	if (pFont != NULL)
	{
		pFont->get_hFont(&newFont);
		pFont->AddRefHfont(newFont);
		oldFont = (HFONT) SelectObject(hdc, newFont);
	}
	
	SetTextAlign(hdc, TA_CENTER|TA_BASELINE);
	SetBkMode(hdc, TRANSPARENT);
	TextOut(hdc, (rc.left + rc.right) / 2, (rc.top + rc.bottom) / 2, 
			W2A(m_bstrCaption), m_bstrCaption.Length());

	// Reset DC & clean up.
	SelectObject(hdc, oldPen);
	SelectObject(hdc, oldBrush);
	SelectObject(hdc, oldFont);
	pFont->ReleaseHfont(newFont);

	return S_OK;
}


STDMETHODIMP CShapesControl::AboutBox()
{
	// Show the about box.
	CSimpleDialog<IDD_ABOUT> dlg;
	dlg.DoModal();
	return S_OK;
}
