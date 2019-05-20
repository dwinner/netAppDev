// CoCarClassFactory.cpp: implementation of the CoCarClassFactory class.
//
//////////////////////////////////////////////////////////////////////

#include "co3dhexfactory.h"
#include "co3dhexagon.h"

// #include "raw.h"
extern DWORD g_lockCount;
extern DWORD g_objCount;

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

Co3DHexFactory::Co3DHexFactory()
{
	m_refCount = 0;
	g_objCount++;
}

Co3DHexFactory::~Co3DHexFactory()
{
	g_objCount--;
}


// IUnknown
STDMETHODIMP_(ULONG) Co3DHexFactory::AddRef()
{
	return ++m_refCount;
}

STDMETHODIMP_(ULONG) Co3DHexFactory::Release()
{
	if(--m_refCount == 0)
	{
		delete this;
		return 0;
	}
	return m_refCount;
}

STDMETHODIMP Co3DHexFactory::QueryInterface(REFIID riid, void** ppv)
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
STDMETHODIMP Co3DHexFactory::LockServer(BOOL fLock)
{
	if(fLock)
		++g_lockCount;
	else
		--g_lockCount;

	return S_OK;
}

STDMETHODIMP Co3DHexFactory::CreateInstance(LPUNKNOWN pUnkOuter, REFIID riid, void** ppv)
{
	// We do not support aggregation in this class object.
	if(pUnkOuter != NULL)
		return CLASS_E_NOAGGREGATION;

	Co3DHexagon* pHexObj = NULL;
	HRESULT hr;

	// Create the object.
	pHexObj = new Co3DHexagon;
	
	// Ask object for an interface.
	hr = pHexObj -> QueryInterface(riid, ppv);

	// Problem?  We must delete the memory we allocated.
	if (FAILED(hr))
		delete pHexObj;

return hr;	// S_OK or E_NOINTERFACE.
	
}
