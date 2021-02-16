// CoSquiggleClassFactory.cpp: implementation of the CoSquiggleClassFactory class.
//
//////////////////////////////////////////////////////////////////////

#include "CoSquiggleClassFactory.h"
#include "cosquiggle.h"
extern DWORD g_lockCount;
extern DWORD g_objCount;
//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CoSquiggleClassFactory::CoSquiggleClassFactory() : m_refCount(0)
{
	++g_objCount;
}

CoSquiggleClassFactory::~CoSquiggleClassFactory()
{
	--g_objCount;
}

// IUnknown
STDMETHODIMP_(ULONG) CoSquiggleClassFactory::AddRef()
{
	return ++m_refCount;
}

STDMETHODIMP_(ULONG) CoSquiggleClassFactory::Release()
{
	--m_refCount;
	if(m_refCount == 0)
	{
		delete this;
		return 0;
	}
	return m_refCount;
}

STDMETHODIMP CoSquiggleClassFactory::QueryInterface(REFIID riid, void** ppv)
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
STDMETHODIMP CoSquiggleClassFactory::LockServer(BOOL fLock)
{
	if(fLock)
		++g_lockCount;
	else
		--g_lockCount;

	return S_OK;
}

STDMETHODIMP CoSquiggleClassFactory::CreateInstance(LPUNKNOWN pUnkOuter, REFIID riid, void** ppv)
{
	// We do not support aggregation in this class object.
	if(pUnkOuter != NULL)
		return CLASS_E_NOAGGREGATION;

	CoSquiggle* pSquig = NULL;
	HRESULT hr;

	// Create the object.
	pSquig = new CoSquiggle;
	
	// Ask object for an interface.
	hr = pSquig -> QueryInterface(riid, ppv);

	// Problem?  We must delete the memory we allocated.
	if (FAILED(hr))
		delete pSquig;

	return hr;	// S_OK or E_NOINTERFACE.
	
}