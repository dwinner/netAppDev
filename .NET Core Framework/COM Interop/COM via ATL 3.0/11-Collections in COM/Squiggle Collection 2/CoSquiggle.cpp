// CoSquiggle.cpp : Implementation of CCoSquiggle
#include "stdafx.h"
#include "BetterSquiggleCollection.h"
#include "CoSquiggle.h"

/////////////////////////////////////////////////////////////////////////////
// CCoSquiggle


STDMETHODIMP CCoSquiggle::get_Name(BSTR *pVal)
{
	*pVal = m_bstrName.Copy();
	return S_OK;
}

STDMETHODIMP CCoSquiggle::put_Name(BSTR newVal)
{
	m_bstrName = newVal;
	return S_OK;
}

STDMETHODIMP CCoSquiggle::Draw()
{
	USES_CONVERSION;
	CComBSTR temp;
	temp = "Drawing a squiggle named ";
	temp += m_bstrName;
	MessageBox(NULL, W2A(temp.m_str), 
			   "Information about this squiggle", MB_OK | MB_SETFOREGROUND);
	return S_OK;
}
