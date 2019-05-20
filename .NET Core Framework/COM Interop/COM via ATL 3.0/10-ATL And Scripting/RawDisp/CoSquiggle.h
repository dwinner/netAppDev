// CoSquiggle.h: interface for the CoSquiggle class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_COSQUIGGLE_H__C3444A88_4526_11D3_B915_0020781238D4__INCLUDED_)
#define AFX_COSQUIGGLE_H__C3444A88_4526_11D3_B915_0020781238D4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
#include <windows.h>

// This coclass only supports IDispatch and thus late binding.
class CoSquiggle : public IDispatch
{
public:
	CoSquiggle();
	virtual ~CoSquiggle();

	// IUnknown
	STDMETHODIMP_(DWORD)AddRef();
	STDMETHODIMP_(DWORD)Release();
	STDMETHODIMP QueryInterface(REFIID riid, void** ppv);

	// IDispatch
    STDMETHODIMP GetTypeInfoCount( UINT *pctinfo);
    STDMETHODIMP GetTypeInfo( UINT iTInfo, LCID lcid, ITypeInfo **ppTInfo);
    STDMETHODIMP GetIDsOfNames( REFIID riid, LPOLESTR *rgszNames, UINT cNames,
								LCID lcid, DISPID *rgDispId);
    STDMETHODIMP Invoke( DISPID dispIdMember, REFIID riid, LCID lcid, WORD wFlags,
						 DISPPARAMS *pDispParams, VARIANT *pVarResult,
						 EXCEPINFO  *pExcepInfo, UINT *puArgErr); 
private:
	void DrawASquiggle();
	void FilpASquiggle();
	void EraseASquiggle();
	ULONG m_ref;
};

#endif // !defined(AFX_COSQUIGGLE_H__C3444A88_4526_11D3_B915_0020781238D4__INCLUDED_)
