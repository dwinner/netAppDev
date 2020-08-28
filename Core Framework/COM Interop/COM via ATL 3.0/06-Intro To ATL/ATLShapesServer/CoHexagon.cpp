// CoHexagon.cpp : Implementation of CCoHexagon
#include "stdafx.h"
#include "ATLShapesServer.h"
#include "CoHexagon.h"

/////////////////////////////////////////////////////////////////////////////
// CCoHexagon


STDMETHODIMP CCoHexagon::Draw()
{
	MessageBox(NULL, "Drawing!", "CoHex Says", MB_OK | MB_SETFOREGROUND);
	return S_OK;
}

STDMETHODIMP CCoHexagon::Inverse()
{
	MessageBox(NULL, "Inversing!", "CoHex Says", MB_OK | MB_SETFOREGROUND);
	return S_OK;
}

STDMETHODIMP CCoHexagon::Stretch(int factor)
{
	for(int i = 0; i < factor; i++)
		MessageBox(NULL, "Stretching!", "CoHex Says", MB_OK | MB_SETFOREGROUND);
	return S_OK;
}

STDMETHODIMP CCoHexagon::Fill(FILLTYPE fType)
{
	ATLTRACE("Client is using fill type number: %d\n", fType);
	MessageBox(NULL, "Filling!", "CoHex Says", MB_OK | MB_SETFOREGROUND);
	return S_OK;
}

STDMETHODIMP CCoHexagon::Erase()
{
	MessageBox(NULL, "Erasing!", "CoHex Says", MB_OK | MB_SETFOREGROUND);
	return S_OK;
}

STDMETHODIMP CCoHexagon::get_Name(BSTR *pVal)
{
	*pVal = m_name.Copy();
	return S_OK;
}

STDMETHODIMP CCoHexagon::put_Name(BSTR newVal)
{
	m_name = newVal;
	return S_OK;
}
