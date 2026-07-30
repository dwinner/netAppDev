// CoAgeHolder.cpp 

#include "ageenum.h"
#include "CoageHolder.h"
extern DWORD g_objCount;
#include <windows.h>
/////////////////////////////////////////////////////////////////////////////
//
CoAgeHolder::CoAgeHolder()
{
	g_objCount++;
	m_currentAge = 0;
	m_refCount = 0;

	// Fill the array of ages.
	for(int i = 0; i < MAX; i++)
		m_theAges[i] = i * 10;
}


CoAgeHolder::~CoAgeHolder()
{	
	g_objCount--;
}

CoAgeHolder::SetIndex(ULONG pos)
{
	m_currentAge = pos;	
}

STDMETHODIMP CoAgeHolder::QueryInterface(REFIID riid, void** pIFace)
{
	// Which aspect of me do they want?
	if(riid == IID_IUnknown)
	{
		*pIFace = (IUnknown*)(IEnumAge*)this;
	}
	
	else if(riid == IID_IEnumAge)
	{
		*pIFace = (IEnumAge*)this;
	}
	
	else
	{
		*pIFace = NULL;
		return E_NOINTERFACE;
	}

	((IUnknown*)(*pIFace))->AddRef();
	return S_OK;
}

STDMETHODIMP_(DWORD) CoAgeHolder::AddRef()
{
	++m_refCount;
	return m_refCount;
}

STDMETHODIMP_(DWORD) CoAgeHolder::Release()
{
	--m_refCount;
	if( m_refCount== 0)
	{
		delete this;
		return 0;
	}
	else
		return m_refCount;
}

STDMETHODIMP CoAgeHolder::Next(ULONG celt, ULONG* rgelt, ULONG* pceltFetched)
{
	
	ULONG cFetched = 0;	// How many we have given out.

	// While the number grabbed is less than the number requested
	// AND the current age is still less (or equal to) than MAX.
	while(cFetched < celt && m_currentAge <= MAX)
	{
		rgelt[cFetched] = m_theAges[m_currentAge];
		m_currentAge++;
		cFetched++;
	}

	*pceltFetched = cFetched;
	return cFetched == celt ? S_OK : S_FALSE;
}

STDMETHODIMP CoAgeHolder::Skip(ULONG celt)
{
	// Move the cursor ahead by celt amount.
	// Check for overflow...
	ULONG cSkipped = 0;		// How many we have skipped.

	while(cSkipped < celt && m_currentAge <= MAX)
	{
		m_currentAge++;
		cSkipped++;
	}

	if(cSkipped == celt)
		return S_OK;
	else
		return S_FALSE;
}

STDMETHODIMP CoAgeHolder::Reset()
{
	// Reset the internal cursor of the array
	m_currentAge = 0;
	return S_OK;
}

STDMETHODIMP CoAgeHolder::Clone(IEnumAge** ppEnum)
{
	IEnumAge* pEnum = NULL;
	
	// Make new AgeHolder & set state.
	CoAgeHolder *pNew = new CoAgeHolder;
	pNew->SetIndex(m_currentAge);

	// Hand out IEnumAge (QI calls AddRef).
	pNew->QueryInterface(IID_IEnumAge, (void**)&pEnum);
	*ppEnum = pEnum;
	return S_OK;
}



