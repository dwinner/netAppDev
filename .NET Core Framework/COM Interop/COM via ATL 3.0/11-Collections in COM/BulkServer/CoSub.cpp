// CoSub.cpp : Implementation of CCoSub
#include "stdafx.h"
#include "BulkServer.h"
#include "CoSub.h"

/////////////////////////////////////////////////////////////////////////////
// CCoSub


STDMETHODIMP CCoSub::get_DatumOne(short *pVal)
{
	*pVal = m_data;
	return S_OK;
}

STDMETHODIMP CCoSub::put_DatumOne(short newVal)
{
	m_data = newVal;
	return S_OK;
}
