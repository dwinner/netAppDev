// CoCarClassFactory.cpp: implementation of the CoCarClassFactory class.
//
//////////////////////////////////////////////////////////////////////
#include "stdafx.h"
#include "cocar.h"
#include "CoCarClassFactory.h"
#include "locks.h"

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CoCarClassFactory::CoCarClassFactory()
{
	// Not adjusting obj count in EXEs!
	m_refCount = 0;
}

CoCarClassFactory::~CoCarClassFactory()
{
	// Not adjusting obj count in EXEs!
}


// IUnknown
STDMETHODIMP_(ULONG) CoCarClassFactory::AddRef()
{
	return 10; //++m_refCount;
}

STDMETHODIMP_(ULONG) CoCarClassFactory::Release()
{
	// We are not 'deleting this' here, as
	// the class object is scoped to WinMain!
	return 20; //--m_refCount;
}

STDMETHODIMP CoCarClassFactory::QueryInterface(REFIID riid, void** ppv)
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
STDMETHODIMP CoCarClassFactory::LockServer(BOOL fLock)
{
	if(fLock)
		Lock();
	else
		UnLock();
	return S_OK;
}

STDMETHODIMP CoCarClassFactory::CreateInstance(LPUNKNOWN pUnkOuter, REFIID riid, void** ppv)
{
	// We do not support aggregation in this class object.
	if(pUnkOuter != NULL)
		return CLASS_E_NOAGGREGATION;

	CoCar* pCarObj = NULL;
	HRESULT hr;
	
	// Create the object.
	pCarObj = new CoCar;

	// Ask object for an interface.
	hr = pCarObj -> QueryInterface(riid, ppv);

	// Problem?  We must delete the memory we allocated.
	if (FAILED(hr))
		delete pCarObj;

return hr;	// S_OK or E_NOINTERFACE.
	
}

