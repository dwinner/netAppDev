// CoSquiggleClassFactory.h: interface for the CoSquiggleClassFactory class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_COSQUIGGLECLASSFACTORY_H__C3444A89_4526_11D3_B915_0020781238D4__INCLUDED_)
#define AFX_COSQUIGGLECLASSFACTORY_H__C3444A89_4526_11D3_B915_0020781238D4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
#include <windows.h>

class CoDualSquiggleClassFactory : public IClassFactory
{
public:
	CoDualSquiggleClassFactory();
	virtual ~CoDualSquiggleClassFactory();

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

#endif // !defined(AFX_COSQUIGGLECLASSFACTORY_H__C3444A89_4526_11D3_B915_0020781238D4__INCLUDED_)
