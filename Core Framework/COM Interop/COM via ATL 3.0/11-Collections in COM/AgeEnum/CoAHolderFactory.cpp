// CoCarClassFactory.cpp: implementation of the CoCarClassFactory class.
//
//////////////////////////////////////////////////////////////////////

#include "CoAholderFactory.h"
#include "coageholder.h"

extern DWORD g_lockCount;
extern DWORD g_objCount;

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CoAHolderFactory::CoAHolderFactory()
{
	m_refCount = 0;
	g_objCount++;
}

CoAHolderFactory::~CoAHolderFactory()
{
	g_objCount--;
}

// IUnknown
STDMETHODIMP_(ULONG) CoAHolderFactory::AddRef()
{
	return ++m_refCount;
}

STDMETHODIMP_(ULONG) CoAHolderFactory::Release()
{
	if(--m_refCount == 0)
	{
		delete this;
		return 0;
	}
	return m_refCount;
}

STDMETHODIMP CoAHolderFactory::QueryInterface(REFIID riid, void** ppv)
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
STDMETHODIMP CoAHolderFactory::LockServer(BOOL fLock)
{
	if(fLock)
		++g_lockCount;
	else
		--g_lockCount;

	return S_OK;
}

STDMETHODIMP CoAHolderFactory::CreateInstance(LPUNKNOWN pUnkOuter, REFIID riid, void** ppv)
{
	// We do not support aggregation in this class object.
	if(pUnkOuter != NULL)
		return CLASS_E_NOAGGREGATION;

	CoAgeHolder* pObj = NULL;
	HRESULT hr;

	// Create the car.
	pObj = new CoAgeHolder;
	
	// Ask car for an interface.
	hr = pObj -> QueryInterface(riid, ppv);

	// Problem?  We must delete the memory we allocated.
	if (FAILED(hr))
		delete pObj;

	return hr;	// S_OK or E_NOINTERFACE.
	
}
