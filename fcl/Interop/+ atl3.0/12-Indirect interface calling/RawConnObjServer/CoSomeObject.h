// CoHello.h: interface for the CoHello class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_COHELLO_H__84D9ACF3_6879_11D3_B929_0020781238D4__INCLUDED_)
#define AFX_COHELLO_H__84D9ACF3_6879_11D3_B929_0020781238D4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <windows.h>
#include <ocidl.h>
#include "rawconnobjserver.h"
#include "cpone.h"

class CoSomeObject : public ISomeInterface, public IConnectionPointContainer  
{
public:
	CoSomeObject();
	virtual ~CoSomeObject();

	// IUnknown 
	STDMETHODIMP_(ULONG) AddRef();
	STDMETHODIMP_(ULONG) Release();
	STDMETHODIMP QueryInterface(REFIID riid, void** ppv);

	// IConnectionPointContainer
    STDMETHODIMP EnumConnectionPoints(IEnumConnectionPoints **ppEnum);     
    STDMETHODIMP FindConnectionPoint(REFIID riid, IConnectionPoint **ppCP);

	// ISomeInterface
	STDMETHODIMP TriggerEvent();

private:
	ULONG m_ref;

	// The connections!
	CPOne* m_pconnOne;
};

#endif // !defined(AFX_COHELLO_H__84D9ACF3_6879_11D3_B929_0020781238D4__INCLUDED_)
