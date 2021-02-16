// CoTheCallBack.h: interface for the CCoTheCallBack class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_COTHECALLBACK_H__84D9AD01_6879_11D3_B929_0020781238D4__INCLUDED_)
#define AFX_COTHECALLBACK_H__84D9AD01_6879_11D3_B929_0020781238D4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "rawconnobjserver.h"

class CCoSink : public _IOutBound  
{
public:
	CCoSink();
	virtual ~CCoSink();
	
	// IUnknown
	STDMETHODIMP_(ULONG) AddRef();
	STDMETHODIMP_(ULONG) Release();
	STDMETHODIMP QueryInterface(REFIID riid, void** ppv);
	
	// _IOutBound
	STDMETHODIMP Test();
};

#endif // !defined(AFX_COTHECALLBACK_H__84D9AD01_6879_11D3_B929_0020781238D4__INCLUDED_)
