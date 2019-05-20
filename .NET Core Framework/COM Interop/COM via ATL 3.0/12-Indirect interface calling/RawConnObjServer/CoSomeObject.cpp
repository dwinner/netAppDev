// CoHello.cpp: implementation of the CoHello class.
//
//////////////////////////////////////////////////////////////////////

#include "cosomeobject.h"
#include "rawconnobjserver_i.c"
extern DWORD g_objCount;
//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CoSomeObject::CoSomeObject() : m_ref(0)
{
	g_objCount++;	
	
	m_pconnOne = new CPOne((IConnectionPointContainer*)this);
	m_pconnOne->AddRef();
} 

CoSomeObject::~CoSomeObject()
{
	g_objCount--;
	m_pconnOne->Release();
}

// IUnknown 
STDMETHODIMP_(ULONG) CoSomeObject::AddRef()
{
	return ++m_ref;
}

STDMETHODIMP_(ULONG) CoSomeObject::Release()
{
	--m_ref;
	if(m_ref == 0)
	{
		delete this;
		return 0;
	}
	return m_ref;
}

STDMETHODIMP CoSomeObject::QueryInterface(REFIID riid, void** ppv)
{
	if(riid == IID_IUnknown)
		*ppv = (IUnknown*)(ISomeInterface*)this;
	if(riid == IID_ISomeInterface)
		*ppv = (ISomeInterface*)this;
	if(riid == IID_IConnectionPointContainer)
		*ppv = (IConnectionPointContainer*)this;
	
	if(*ppv)
	{
		((IUnknown*)(*ppv))->AddRef();
		return S_OK;
	}
	return E_NOINTERFACE;
}

// ISomeInterface
STDMETHODIMP CoSomeObject::TriggerEvent()
{
	MessageBox(NULL, "About to send event",
			   "Incoming call", MB_OK | MB_SETFOREGROUND);
	
	// Here we go baby!
	m_pconnOne->Fire_Test();
	return S_OK;
}

// IConnectionPointContainer
STDMETHODIMP CoSomeObject::EnumConnectionPoints(IEnumConnectionPoints **ppEnum)    
{
	// No use of enumeration...sorry!
	return E_NOTIMPL;
}

STDMETHODIMP CoSomeObject::FindConnectionPoint(REFIID riid, IConnectionPoint **ppCP)
{
	// Which CP do you want?
	if(riid == IID__IOutBound)
	{
		*ppCP = m_pconnOne;
	}
	else
	{
		*ppCP = NULL;
		return E_FAIL;
	}
	(*ppCP)->AddRef();
	return S_OK;
}