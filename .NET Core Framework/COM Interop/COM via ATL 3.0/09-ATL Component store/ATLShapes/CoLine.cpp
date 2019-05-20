// CoLine.cpp : Implementation of CCoLine
#include "stdafx.h"
#include "ATLShapes.h"
#include "CoLine.h"
#include "osbuffer.h"

/////////////////////////////////////////////////////////////////////////////
// CCoLine


STDMETHODIMP CCoLine::Draw()
{
	// Draw the line.
	MessageBox(NULL, "I am drawing a line", "IDraw::Draw", 
			   MB_OK | MB_SETFOREGROUND);

	return S_OK;
}

STDMETHODIMP CCoLine::get_ShapeName(BSTR *pVal)
{
	// Return the shape's name.
	*pVal = m_name.Copy();

	return S_OK;
}

STDMETHODIMP CCoLine::put_ShapeName(BSTR newVal)
{
	// Assign the name.
	m_name = newVal;
	
	return S_OK;
}

STDMETHODIMP CCoLine::GetOSBuffer(IOSBuffer **pBuffer)
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
