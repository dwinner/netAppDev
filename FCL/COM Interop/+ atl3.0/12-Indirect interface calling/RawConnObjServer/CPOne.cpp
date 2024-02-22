// CPOne.cpp: implementation of the CPOne class.
//
//////////////////////////////////////////////////////////////////////

#include "CPOne.h"
#include "rawconnobjserver_i.c"
#include "rawconnobjserver.h"

extern DWORD g_objCount;
//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CPOne::CPOne(IConnectionPointContainer* pCont)
{
	// Prep work...
	m_ref = 0;
	m_cookie = 0;
	m_position = 0;
	//g_objCount++;
	m_pCont = pCont;
	//m_pCont->AddRef();

	// Fill the array of IUnknown pointers with NULL.
	for(int i = 0; i < MAX; i++)
		m_unkArray[i] = NULL;
}

CPOne::~CPOne()
{	
	//g_objCount--;
	// clean up pointers.
	for(int i = 0; i < MAX; i++)
	{
		if(m_unkArray[i] != NULL)
			m_unkArray[i]->Release();
	}
	//m_pCont->Release();

}

// IUnknown 
STDMETHODIMP_(ULONG) CPOne::AddRef()
{
	return ++m_ref;
}

STDMETHODIMP_(ULONG) CPOne::Release()
{
	--m_ref;
	if(m_ref == 0)
	{
		delete this;
		return 0;
	}
	return m_ref;
}

STDMETHODIMP CPOne::QueryInterface(REFIID riid, void** ppv)
{
	if(riid == IID_IUnknown)
		*ppv = (IUnknown*)this;
	if(riid == IID_IConnectionPoint)
		*ppv = (IConnectionPoint*)this;

	if(*ppv)
	{
		((IUnknown*)(*ppv))->AddRef();
		return S_OK;	
	}
	return E_NOINTERFACE;
}

// IConnectionPoint
STDMETHODIMP CPOne::GetConnectionInterface(IID *pIID)
{
	// They want to know our IID.
	*pIID = IID__IOutBound;
	return S_OK;
}

STDMETHODIMP CPOne::GetConnectionPointContainer(IConnectionPointContainer**ppCPC)
{
	*ppCPC = m_pCont;
	(*ppCPC)->AddRef();
	return S_OK;
}


STDMETHODIMP CPOne::Advise(IUnknown *pUnkSink, DWORD *pdwCookie)
{	
	// Query the client for the IOutBound interface and 
	// store it for our use.
	IUnknown *pOutBound;

	if(m_position < MAX)
	{
		if(SUCCEEDED(pUnkSink->QueryInterface(IID__IOutBound, (void**)&pOutBound)))
		{
			m_unkArray[m_position] = pOutBound;
			m_cookie = m_position;

			// A client is interested in what we have to say.
			*pdwCookie = m_cookie;
			
			m_position++;
			return S_OK;
		}
		else
		{
			// Some client sent me a sink I can't use!  Bozo!
			pOutBound->Release();
			return CONNECT_E_NOCONNECTION;
		}
	}
	return CONNECT_E_ADVISELIMIT;
}

STDMETHODIMP CPOne::Unadvise(DWORD dwCookie)
{
	m_unkArray[dwCookie]->Release();
	m_unkArray[dwCookie] = NULL;
	return S_OK;
} 

STDMETHODIMP CPOne::EnumConnections(IEnumConnections**ppEnum)
{
	return E_NOTIMPL;
}

HRESULT CPOne::Fire_Test()
{
	// For each connection...
	for (int nConnectionIndex = 0; nConnectionIndex < MAX; nConnectionIndex++)
	{
		// Grab an IUnk fromthe array.
		IUnknown* pUnk = m_unkArray[nConnectionIndex];
		
		// Dynamically change it into a _IOutBound...
		_IOutBound* pOut = reinterpret_cast<_IOutBound*>(pUnk);
		if (pOut != NULL)
		{
			// Call client's impl of test.
			pOut->Test();
		}
	}
	return S_OK;
}
