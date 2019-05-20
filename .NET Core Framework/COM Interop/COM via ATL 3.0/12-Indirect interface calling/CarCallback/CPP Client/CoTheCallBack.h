// CoTheCallBack.h: interface for the CCoTheCallBack class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_COTHECALLBACK_H__84D9AD01_6879_11D3_B929_0020781238D4__INCLUDED_)
#define AFX_COTHECALLBACK_H__84D9AD01_6879_11D3_B929_0020781238D4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "carcallback.h"

class CCoSink : public IEngineEvents  
{
public:
	CCoSink();
	virtual ~CCoSink();
	
	// IUnknown
	STDMETHODIMP_(ULONG) AddRef();
	STDMETHODIMP_(ULONG) Release();
	STDMETHODIMP QueryInterface(REFIID riid, void** ppv);
	
	// IEngineEvents
	STDMETHODIMP AboutToExplode()
	{
		MessageBox(NULL, "Prepare to meet thy doom...", "Event from coclass...",
			       MB_OK | MB_SETFOREGROUND);
		return S_OK;
	}

	STDMETHODIMP Exploded()
	{
		MessageBox(NULL, "Your car is dead...", "Event from coclass...",
			       MB_OK | MB_SETFOREGROUND);
		return S_OK;
	}
};

#endif // !defined(AFX_COTHECALLBACK_H__84D9AD01_6879_11D3_B929_0020781238D4__INCLUDED_)
