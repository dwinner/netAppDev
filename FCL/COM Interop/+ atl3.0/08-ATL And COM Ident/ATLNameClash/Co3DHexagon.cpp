// Co3DHexagon.cpp : Implementation of CCo3DHexagon
#include "stdafx.h"
#include "ATLNameClash.h"
#include "Co3DHexagon.h"

/////////////////////////////////////////////////////////////////////////////
// CCo3DHexagon


STDMETHODIMP CCo3DHexagon::Draw3DRect()
{
	MessageBox(NULL, " Stunning 3D!!", "Better!", MB_OK | MB_SETFOREGROUND);
	return S_OK;
}

STDMETHODIMP CCo3DHexagon::Draw2DRect()
{
	MessageBox(NULL, "Drawing a 2D Hex", "CoHex says", MB_OK | MB_SETFOREGROUND);
	return S_OK;
}

STDMETHODIMP CCo3DHexagon::AnotherIDraw()
{
	// TODO: Add your implementation code here
	MessageBox(NULL, "another method", "IDraw", MB_OK | MB_SETFOREGROUND);
	return S_OK;
}

STDMETHODIMP CCo3DHexagon::Another3dRender()
{
	// TODO: Add your implementation code here
	MessageBox(NULL, "another method", "I3DRender", MB_OK | MB_SETFOREGROUND);
	return S_OK;
}
