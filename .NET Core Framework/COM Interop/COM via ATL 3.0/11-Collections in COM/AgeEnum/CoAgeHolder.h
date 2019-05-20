// CoRawPeopleHolder.h: Definition of the CoRawPeopleHolder class
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CORAWPEOPLEHOLDER_H__ACD72FDB_60E2_11D3_B929_0020781238D4__INCLUDED_)
#define AFX_CORAWPEOPLEHOLDER_H__ACD72FDB_60E2_11D3_B929_0020781238D4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

const ULONG MAX = 10000;

/////////////////////////////////////////////////////////////////////////////
// CoAgeHolder
#include "ageenum.h"

class CoAgeHolder : public IEnumAge
{
public:
	CoAgeHolder();
	~CoAgeHolder();

	// IUnknown.
	STDMETHODIMP QueryInterface(REFIID riid, void** pIFace);
	STDMETHODIMP_(DWORD)AddRef();
	STDMETHODIMP_(DWORD)Release();
	
	// IEnumAge
	STDMETHODIMP Next(ULONG celt, ULONG* rgVar, ULONG * pCeltFetched);
	STDMETHODIMP Skip(ULONG celt);
	STDMETHODIMP Reset();
	STDMETHODIMP Clone(IEnumAge ** ppEnum);

	// Helper for Clone().
	SetIndex(ULONG pos);

private:
	ULONG m_currentAge;
	ULONG m_theAges[MAX];
	ULONG m_refCount;
};

#endif // !defined(AFX_CORAWAHOLDER_H__ACD72FDB_60E2_11D3_B929_0020781238D4__INCLUDED_)
