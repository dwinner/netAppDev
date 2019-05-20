// TextFactory.h: interface for the TextFactory class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_TEXTFACTORY_H__466300A4_99B5_11D3_A7DE_0000E885A202__INCLUDED_)
#define AFX_TEXTFACTORY_H__466300A4_99B5_11D3_A7DE_0000E885A202__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <windows.h>

class TextFactory : public IClassFactory  
{

public:
	TextFactory();
	~TextFactory();

	// IUnknown methods.
	STDMETHODIMP QueryInterface(REFIID riid, void** pIFace);
	STDMETHODIMP_(ULONG)AddRef();
	STDMETHODIMP_(ULONG)Release();
	
// ICF methods.
	STDMETHODIMP CreateInstance(LPUNKNOWN pUnk, REFIID riid, void** pIFace);
	STDMETHODIMP LockServer(BOOL fLock);
private:
	ULONG m_refCount;
};


#endif // !defined(AFX_TEXTFACTORY_H__466300A4_99B5_11D3_A7DE_0000E885A202__INCLUDED_)
