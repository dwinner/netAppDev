// CoSquiggles.cpp : Implementation of CCoSquiggles
#include "stdafx.h"
#include "SquiggleCollection.h"
#include "CoSquiggles.h"
#include <time.h>

/////////////////////////////////////////////////////////////////////////////
// CCoSquiggles


STDMETHODIMP CCoSquiggles::Item(VARIANT index, VARIANT *pItem)
{
	// Get a squiggle.

	// Be sure we have a long.
	if(index.vt == VT_I2)
	{
		
		// Be sure we are in range.
		if(index.lVal >=0 && index.lVal <= MAX_SQUIGGLES)
		{
			// Find the correct squiggle.
			IDispatch* pDisp = m_pArrayOfSquigs[index.lVal];
			
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

STDMETHODIMP CCoSquiggles::get_Count(long *pVal)
{
	*pVal = MAX_SQUIGGLES - 1;
	return S_OK;
}

STDMETHODIMP CCoSquiggles::get__NewEnum(LPUNKNOWN *pVal)
{
	// This block of code allows VB-like iteration.
	//
	// Make a temp array of VARIANTS and fill with the 
	// current CoSquiggles.
	VARIANT* pVar = new VARIANT[MAX_SQUIGGLES];

	for(int i = 0; i < MAX_SQUIGGLES; i++)
	{
		pVar[i].vt = VT_DISPATCH;
		pVar[i].pdispVal = m_pArrayOfSquigs[i];
	}

	// Now make the enum.
	typedef CComObject< CComEnum< IEnumVARIANT, &IID_IEnumVARIANT, VARIANT, _Copy< VARIANT > > > enumVar;
	enumVar* pEnumVar = new enumVar;
	pEnumVar->Init(&pVar[0], &pVar[MAX_SQUIGGLES], NULL, AtlFlagCopy);
	delete[] pVar;

	// return the enum.
	return pEnumVar->QueryInterface(IID_IUnknown, (void**)pVal);
}

