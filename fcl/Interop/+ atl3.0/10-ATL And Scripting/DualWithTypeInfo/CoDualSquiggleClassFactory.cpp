// CoSquiggleClassFactory.cpp: implementation of the CoSquiggleClassFactory class.
//
//////////////////////////////////////////////////////////////////////

#include "CoDualSquiggleClassFactory.h"
#include "codualsquiggle.h"
extern DWORD g_lockCount;
extern DWORD g_objCount;
//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CoDualSquiggleClassFactory::CoDualSquiggleClassFactory() : m_refCount(0)
{
	++g_objCount;
}

CoDualSquiggleClassFactory::~CoDualSquiggleClassFactory() 
{
	--g_objCount;
}

// IUnknown
STDMETHODIMP_(ULONG) CoDualSquiggleClassFactory::AddRef()
{
	return ++m_refCount;
}

STDMETHODIMP_(ULONG) CoDualSquiggleClassFactory::Release()
{
	if(--m_refCount == 0)
	{
		delete this;
		return 0;
	}
	return m_refCount;
}

STDMETHODIMP CoDualSquiggleClassFactory::QueryInterface(REFIID riid, void** ppv)
{
	// Which aspect of me do they want?
	if(riid == IID_IUnknown)
	{
		*ppv = (IUnknown*)this;
	}
	else if(riid == IID_IClassFactory)
	{
		*ppv = (IClassFactory*)this;
	}	
	else
	{
		*ppv = NULL;
		return E_NOINTERFACE;
	}

	((IUnknown*)(*ppv))->AddRef();
	return S_OK;
}


// ICF
STDMETHODIMP CoDualSquiggleClassFactory::LockServer(BOOL fLock)
{
	if(fLock)
		++g_lockCount;
	else
		--g_lockCount;

	return S_OK;
}

STDMETHODIMP CoDualSquiggleClassFactory::CreateInstance(LPUNKNOWN pUnkOuter, REFIID riid, void** ppv)
{
	// We do not support aggregation in this class object.
	if(pUnkOuter != NULL)
		return CLASS_E_NOAGGREGATION;

	CoDualSquiggle* pSquig = NULL;
	HRESULT hr;

	// Create the object.
	pSquig = new CoDualSquiggle;
	
	// Ask object for an interface.
	hr = pSquig -> QueryInterface(riid, ppv);

	// Problem?  We must delete the memory we allocated.
	if (FAILED(hr))
		delete pSquig;

return hr;	// S_OK or E_NOINTERFACE.
	
}