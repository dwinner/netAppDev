// CoCarClassFactory.cpp: implementation of the CoCarClassFactory class.
//
//////////////////////////////////////////////////////////////////////

#include "CoHexfactory.h"
#include "cohexagon.h"
extern ULONG g_lockCount;
extern ULONG g_objCount;

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CoHexFactory::CoHexFactory()
{
	m_refCount = 0;
	g_objCount++;
}

CoHexFactory::~CoHexFactory()
{
	g_objCount--;
}

// IUnknown
STDMETHODIMP_(ULONG) CoHexFactory::AddRef()
{
	return ++m_refCount;
}

STDMETHODIMP_(ULONG) CoHexFactory::Release()
{
	if(--m_refCount == 0)
	{
		delete this;
		return 0;
	}
	return m_refCount;
}

STDMETHODIMP CoHexFactory::QueryInterface(REFIID riid, void** ppv)
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
STDMETHODIMP CoHexFactory::LockServer(BOOL fLock)
{
	if(fLock)
		++g_lockCount;
	else
		--g_lockCount;

	return S_OK;
}

STDMETHODIMP CoHexFactory::CreateInstance(LPUNKNOWN pUnkOuter, REFIID riid, void** ppv)
{
	// We do not support aggregation in this class object.
	if(pUnkOuter != NULL)
		return CLASS_E_NOAGGREGATION;

	CoHexagon* pHexObj = NULL;
	HRESULT hr;

	// Create the object.
	pHexObj = new CoHexagon;
	
	// Ask object for an interface.
	hr = pHexObj -> QueryInterface(riid, ppv);

	// Problem?  We must delete the memory we allocated.
	if (FAILED(hr))
		delete pHexObj;

	return hr;	// S_OK or E_NOINTERFACE.
	
}
