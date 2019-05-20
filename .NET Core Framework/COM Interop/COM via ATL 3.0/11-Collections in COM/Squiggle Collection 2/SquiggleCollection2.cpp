// SquiggleCollection2.cpp : Implementation of CSquiggleCollection2
#include "stdafx.h"
#include "BetterSquiggleCollection.h"
#include "SquiggleCollection2.h"
#include <time.h>

/////////////////////////////////////////////////////////////////////////////
// CSquiggleCollection2

STDMETHODIMP CSquiggleCollection2::Item(VARIANT index, VARIANT *pItem)
{
	// Be sure we have a long.
	if(index.vt == VT_I2)
	{		
		// Be sure we are in range.
		if(index.lVal >=0 && index.lVal <= m_vecSquiggles.size())
		{
			// Find the correct squiggle.
			IDispatch* pDisp = m_vecSquiggles[index.lVal];
			
			// Add the ref!
			pDisp->AddRef();
			
			// Set the type of VARIANT and return
			pItem->vt = VT_DISPATCH;
			pItem->pdispVal = pDisp;
			return S_OK;
		}
	}

	return E_INVALIDARG;
}

STDMETHODIMP CSquiggleCollection2::get_Count(long *pVal)
{
	*pVal = m_vecSquiggles.size();
	return S_OK;
}

STDMETHODIMP CSquiggleCollection2::get__NewEnum(LPUNKNOWN *pVal)
{
	// This block of code allows VB-like iteration.
	//
	// Make a temp array of VARIANTS and fill with the 
	// current CoSquiggles.
	int size = m_vecSquiggles.size();

	VARIANT* pVar = new VARIANT[size];

	for(int i = 0; i < size; i++)
	{
		pVar[i].vt = VT_DISPATCH;
		pVar[i].pdispVal = m_vecSquiggles[i];
	}

	// Now make the enum.
	typedef CComObject< CComEnum< IEnumVARIANT, &IID_IEnumVARIANT, VARIANT, _Copy< VARIANT > > > enumVar;
	enumVar* pEnumVar = new enumVar;
	pEnumVar->Init(&pVar[0], &pVar[size], NULL, AtlFlagCopy);
	delete[] pVar;

	// return the enum.
	return pEnumVar->QueryInterface(IID_IUnknown, (void**)pVal);
}

STDMETHODIMP CSquiggleCollection2::Add(IDispatch *pnewSquiggle)
{
	// Here we are going to add a new squiggle the client gives us.	
	m_vecSquiggles.push_back(pnewSquiggle);
	pnewSquiggle->AddRef();

	return S_OK;
}

// Give me an index to remove.
//
STDMETHODIMP CSquiggleCollection2::Remove(long index)
{
	// Be sure we are in range.
	if(index >=0 && index <= m_vecSquiggles.size())
	{
		// Find the correct squiggle.
		IDispatch* pDisp = m_vecSquiggles[index];
		
		// Remove it.
		pDisp->Release();
		m_vecSquiggles.erase(m_vecSquiggles.begin() + index);
		return S_OK;
	}
	return E_FAIL;
}

