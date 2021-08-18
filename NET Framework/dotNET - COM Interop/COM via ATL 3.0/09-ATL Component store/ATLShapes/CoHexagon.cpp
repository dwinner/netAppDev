// CoHexagon.cpp : Implementation of CCoHexagon
#include "stdafx.h"
#include "ATLShapes.h"
#include "CoHexagon.h"
#include "osbuffer.h"

/////////////////////////////////////////////////////////////////////////////
// CCoHexagon


STDMETHODIMP CCoHexagon::Draw()
{
	// Draw the hexagon.
	MessageBox(NULL, "I am drawing a hexagon", "IDraw::Draw", 
			   MB_OK | MB_SETFOREGROUND);

	return S_OK;
}

STDMETHODIMP CCoHexagon::get_ShapeName(BSTR *pVal)
{
	// Return the shape's name.
	*pVal = m_name.Copy();

	return S_OK;
}

STDMETHODIMP CCoHexagon::put_ShapeName(BSTR newVal)
{
	// Assign the name.
	m_name = newVal;

	return S_OK;
}

STDMETHODIMP CCoHexagon::GetOSBuffer(IOSBuffer **pBuffer)
{
	// Create a brand new COSBuffer for client.
	HRESULT hr;
	CComObject<COSBuffer>* pBuf = NULL;
	hr = CComObject<COSBuffer>::CreateInstance(&pBuf);

	// If we created one, ask for IOSBuffer.
	if(SUCCEEDED(hr))
	{
		// Technically we are using a local copy so we should
		// AddRef and Release.
		pBuf->AddRef();
		hr = pBuf->QueryInterface(IID_IOSBuffer, (void**)pBuffer);
		pBuf->Release();
	}

	return hr;
}
