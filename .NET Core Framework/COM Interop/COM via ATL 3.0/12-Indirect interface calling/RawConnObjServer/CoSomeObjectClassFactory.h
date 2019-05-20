// CoCarClassFactory.h: interface for the CoCarClassFactory class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_COCARCLASSFACTORY_H__FFA3A107_E326_11D2_B8D0_0020781238D4__INCLUDED_)
#define AFX_COCARCLASSFACTORY_H__FFA3A107_E326_11D2_B8D0_0020781238D4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <windows.h>

class CoSomeObjectClassFactory : public IClassFactory  
{
public:
	CoSomeObjectClassFactory();
	virtual ~CoSomeObjectClassFactory();

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

#endif // !defined(AFX_COCARCLASSFACTORY_H__FFA3A107_E326_11D2_B8D0_0020781238D4__INCLUDED_)
