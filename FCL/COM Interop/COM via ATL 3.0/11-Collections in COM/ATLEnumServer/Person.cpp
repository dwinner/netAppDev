// Person.cpp : Implementation of CPerson
#include "stdafx.h"
#include "ATLEnumServer.h"
#include "Person.h"

/////////////////////////////////////////////////////////////////////////////
// CPerson


STDMETHODIMP CPerson::get_Name(BSTR *pVal)
{
	*pVal = m_bstrName.Copy();
	return S_OK;
}

STDMETHODIMP CPerson::put_Name(BSTR newVal)
{
	m_bstrName = newVal;
	return S_OK;
}

STDMETHODIMP CPerson::get_ID(short *pVal)
{
	*pVal = m_ID;
	return S_OK;
}

STDMETHODIMP CPerson::put_ID(short newVal)
{
	m_ID = newVal;
	return S_OK;
}
