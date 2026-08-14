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
	MessageBox(NULL, "sink is ALIVE...", "Event from CCoSink...",
			   MB_OK | MB_SETFOREGROUND);
}

CCoSink::~CCoSink()
{
	MessageBox(NULL, "sink is DEAD...", "Event from CCoSink...",
			   MB_OK | MB_SETFOREGROUND);
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
	if(riid == IID_IUnknown || riid == IID_IEngineEvents)
	{
		*ppv = (IEngineEvents*)this;
		return S_OK;
	}
	else
		return E_NOINTERFACE;
}
