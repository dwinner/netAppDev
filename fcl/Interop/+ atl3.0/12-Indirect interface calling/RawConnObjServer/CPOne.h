// CPOne.h: interface for the CPOne class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CPONE_H__84D9AD03_6879_11D3_B929_0020781238D4__INCLUDED_)
#define AFX_CPONE_H__84D9AD03_6879_11D3_B929_0020781238D4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <windows.h>
#include <ocidl.h>
#include <olectl.h>
const int MAX = 10;

class CPOne : public IConnectionPoint  
{
public:
	HRESULT Fire_Test();
	CPOne(IConnectionPointContainer* pCont);
	virtual ~CPOne();

	// IUnknown 
	STDMETHODIMP_(ULONG) AddRef();
	STDMETHODIMP_(ULONG) Release();
	STDMETHODIMP QueryInterface(REFIID riid, void** ppv);
	
	// IConnectionPoint
    STDMETHODIMP GetConnectionInterface(IID *pIID);        
    STDMETHODIMP GetConnectionPointContainer(IConnectionPointContainer**ppCPC);        
    STDMETHODIMP Advise(IUnknown *pUnkSink, DWORD *pdwCookie);        
    STDMETHODIMP Unadvise(DWORD dwCookie);        
    STDMETHODIMP EnumConnections(IEnumConnections**ppEnum);

private:
	ULONG m_ref;
	ULONG m_cookie;
	IUnknown* m_unkArray[MAX];
	int m_position;

	// Our container.
	IConnectionPointContainer* m_pCont;
};

#endif // !defined(AFX_CPONE_H__84D9AD03_6879_11D3_B929_0020781238D4__INCLUDED_)
