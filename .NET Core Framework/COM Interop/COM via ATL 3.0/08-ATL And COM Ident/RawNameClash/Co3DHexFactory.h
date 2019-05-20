// CoCarClassFactory.h: interface for the CoCarClassFactory class.
//
//////////////////////////////////////////////////////////////////////

#ifndef _3DFACT
#define _3DFACT

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <windows.h>


class Co3DHexFactory : public IClassFactory  
{
public:
	Co3DHexFactory();
	virtual ~Co3DHexFactory();

	// IUnknown
	STDMETHODIMP QueryInterface(REFIID riid, void** pIFace);
	STDMETHODIMP_(DWORD)AddRef();
	STDMETHODIMP_(DWORD)Release();

	// ICF
	STDMETHODIMP LockServer(BOOL fLock);
	STDMETHODIMP CreateInstance(LPUNKNOWN pUnkOuter, REFIID riid, void** ppv);

private:
	DWORD	m_refCount;
};

#endif // 3DFACT
