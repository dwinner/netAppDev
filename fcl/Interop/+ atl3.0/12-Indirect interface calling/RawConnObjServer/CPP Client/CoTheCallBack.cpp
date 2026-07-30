// CoTheCallBack.cpp: implementation of the CCoTheCallBack class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "CoTheCallBack.h"


//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CCoSink::CCoSink() 
{
}

CCoSink::~CCoSink()
{
}

STDMETHODIMP_(ULONG) CCoSink::AddRef()
{
	return 1;
}

STDMETHODIMP_(ULONG) CCoSink::Release()
{
	return 2;
}

STDMETHODIMP CCoSink::QueryInterface(REFIID riid, void** ppv)
{
	if(riid == IID_IUnknown || riid == IID__IOutBound)
	{
		*ppv = (_IOutBound*)this;
		return S_OK;
	}
	else
		return E_NOINTERFACE;
}

// _IOutBound
STDMETHODIMP CCoSink::Test()
{ 
	MessageBox(NULL, "Testing...", "Message from object", MB_OK | MB_SETFOREGROUND);
	return S_OK;
}