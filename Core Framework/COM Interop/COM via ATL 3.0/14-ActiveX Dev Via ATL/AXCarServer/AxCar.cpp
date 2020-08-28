// AxCar.cpp : Implementation of CAxCar

#include "stdafx.h"
#include "AXCarServer.h"
#include "AxCar.h"
#include "resource.h"

/////////////////////////////////////////////////////////////////////////////
// CAxCar

STDMETHODIMP CAxCar::SpeedUp()
{
	// Speed up and check for problems...
	m_currSpeed += 10;
	ATLTRACE("New Speed = %d\n", m_currSpeed);

	// About to explode?
	if((m_maxSpeed - m_currSpeed) <= 10)
	{
		Fire_AboutToBlow();
		m_currMaxFrames = ABOUT_TO_BLOW;
	}

	// Maxed out?
	if(m_currSpeed >= m_maxSpeed)
	{
		m_currSpeed = m_maxSpeed;
		Fire_BlewUp();
		m_currMaxFrames = BLOWN_UP;
	}

	return S_OK;
}

STDMETHODIMP CAxCar::CreateCar(BSTR petName, short maxSp)
{
	// Set data.
	m_maxSpeed = maxSp;
	m_bstrPetName = petName;
	return S_OK;
}

HRESULT CAxCar::OnDraw(ATL_DRAWINFO& di)
{
	RECT& rc = *(RECT*)di.prcBounds;
	Rectangle(di.hdcDraw, rc.left, rc.top, rc.right, rc.bottom);
	
	BITMAP bmInfo;	
	HBITMAP oldBitmap;
	HBITMAP newBitmap;
	HDC memDC;
	SIZE size;

	ATLTRACE("New image = %d\n", m_currImage);
	// Get the stats on the current image.
	GetObject(m_carImage[m_currImage], sizeof(bmInfo), &bmInfo);
	size.cx = bmInfo.bmWidth;
	size.cy = bmInfo.bmHeight;

	// Grab the current image.
	newBitmap = m_carImage[m_currImage];

	// Create off screen DC & select object into it.
	memDC = CreateCompatibleDC(di.hdcDraw);
	oldBitmap = (HBITMAP)SelectObject(memDC, newBitmap);

	// Now transfer to screen.
	StretchBlt(di.hdcDraw, rc.left, rc.top, rc.right, rc.bottom, 
		       memDC, 0, 0, size.cx, size.cy, SRCCOPY);

	// clean up.
	SelectObject(di.hdcDraw, oldBitmap);
	DeleteDC(memDC);

	return S_OK;
}

STDMETHODIMP CAxCar::put_Animate(AnimVal newVal)
{
	if(FAILED(FireOnRequestEdit(3)))	// [id(3)] = Animate
		return S_FALSE;

	// Set the animation property.
	m_bAnimate = newVal;

	if(::IsWindow(m_hWnd) && (m_bAnimate == Yes)) 
	    SetTimer(1, 250);
	else if(::IsWindow(m_hWnd))
		KillTimer(1);

	// Set dirty flag.
	SetDirty(TRUE);

	// Tell container we have chagned the value.
	FireOnChanged(3);	// [id(3)] = Animate

	return S_OK;
}

STDMETHODIMP CAxCar::get_Animate(AnimVal *pVal)
{
	*pVal = m_bAnimate;
	return S_OK;
}

LRESULT CAxCar::OnTimer(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	// Increase the bitmap image.
	m_currImage = m_currImage + 1;
	if(m_currImage >= m_currMaxFrames)
		m_currImage = 0;
	ATLTRACE("curr image = %d\n", m_currImage);
	FireViewChange();
	return 0;
}

STDMETHODIMP CAxCar::get_MaxSpeed(short *pVal)
{
	*pVal = m_maxSpeed;
	return S_OK;
}

STDMETHODIMP CAxCar::put_MaxSpeed(short newVal)
{
	if(FAILED(FireOnRequestEdit(4)))	// [id(4)] = Speed
		return S_FALSE;

	m_maxSpeed = newVal;

	// Set dirty flag.
	SetDirty(TRUE);

	// Tell container we have chagned the value.
	FireOnChanged(4);	// [id(4)] = Speed

	return S_OK;
}

STDMETHODIMP CAxCar::AboutBox()
{
	// Show who made this lovely control.
	CSimpleDialog<IDD_ABOUTBOX> d;
	d.DoModal();

	return S_OK;
}

STDMETHODIMP CAxCar::get_PetName(BSTR *pVal)
{
	*pVal = m_bstrPetName.Copy();
	return S_OK;
}

STDMETHODIMP CAxCar::put_PetName(BSTR newVal)
{
	if(FAILED(FireOnRequestEdit(5)))	// [id(5)] = PetName
		return S_FALSE;

	m_bstrPetName = newVal;

	// Set dirty flag.
	SetDirty(TRUE);

	// Tell container we have chagned the value.
	FireOnChanged(5);	// [id(5)] = PetName

	return S_OK;
}


STDMETHODIMP CAxCar::get_Speed(short *pVal)
{
	*pVal = m_currSpeed;
	return S_OK;
}

STDMETHODIMP CAxCar::put_Speed(short newVal)
{
	if(FAILED(FireOnRequestEdit(6)))	// [id(6)] = Speed
		return S_FALSE;

	m_currSpeed = newVal;

	// Set dirty flag.
	SetDirty(TRUE);

	// Tell container we have chagned the value.
	FireOnChanged(6);	// [id(6)] = Speed

	return S_OK;
}
