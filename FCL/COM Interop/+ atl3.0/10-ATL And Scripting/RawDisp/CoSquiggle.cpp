// CoSquiggle.cpp: implementation of the CoSquiggle class.
//
//////////////////////////////////////////////////////////////////////

#include "CoSquiggle.h"
extern DWORD g_objCount;

// The DISPID constants
#define DISPID_DRAWSQUIG	1
#define DISPID_FLIPSQUIG	2
#define DISPID_ERASESQUIG	3
//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CoSquiggle::CoSquiggle() : m_ref(0)
{
	++g_objCount;
}

CoSquiggle::~CoSquiggle()
{
	--g_objCount;
}


STDMETHODIMP_(DWORD) CoSquiggle::AddRef()
{
	return ++m_ref;
}

STDMETHODIMP_(DWORD)CoSquiggle::Release()
{
	--m_ref;
	if(m_ref == 0)
	{
		delete this;
		return 0;
	}
	return m_ref;
}

STDMETHODIMP CoSquiggle::QueryInterface(REFIID riid, void** ppv)
{
	// We do not support any custom interfaces here.
	// All object functionality is given via IDispatch
	if(riid == IID_IUnknown)
		*ppv = (IUnknown*)this;
	else if(riid == IID_IDispatch)	// <-- This is for scripting clients!
		*ppv = (IDispatch*)this;
	else
	{
		*ppv = NULL;
		return E_NOINTERFACE;
	}
	((IUnknown*)(*ppv))->AddRef();
	return S_OK;
}


// IDispatch Impl.
STDMETHODIMP CoSquiggle::GetTypeInfoCount( UINT *pctinfo)
{
	// We do not support type information in this object.
	*pctinfo = 0;
	return S_OK;
}


STDMETHODIMP CoSquiggle::GetTypeInfo( 
    UINT iTInfo,
    LCID lcid,
    ITypeInfo **ppTInfo)
{
	// Return NULL pointer as we do not support type info.
	*ppTInfo = NULL;
	return E_NOTIMPL;
}


// Client gives us a string name, so do a look up
// between the string and the cookie.
STDMETHODIMP CoSquiggle::GetIDsOfNames( 
    REFIID riid,
    LPOLESTR *rgszNames,
    UINT cNames,
    LCID lcid,
    DISPID *rgDispId)
{
	// First off, we only support one name at a time.
	if(cNames > 1) 
		return E_INVALIDARG;

	// What method should I call in responce to the DISPID?
	//
	if(_wcsicmp(*rgszNames, L"DrawASquiggle") == 0)
	{
		*rgDispId = DISPID_DRAWSQUIG;
		return S_OK;
	}
	if(_wcsicmp(*rgszNames, L"FlipASquiggle") == 0)
	{
		*rgDispId = DISPID_FLIPSQUIG;
		return S_OK;
	}
	if(_wcsicmp(*rgszNames, L"EraseASquiggle") == 0)
	{
		*rgDispId = DISPID_ERASESQUIG;
		return S_OK;
	}
	else
		return DISP_E_UNKNOWNNAME;
}


// Now, based on the DISPID, call the correct helper function
STDMETHODIMP CoSquiggle::Invoke( 
    DISPID dispIdMember,
    REFIID riid,
    LCID lcid,
    WORD wFlags,
    DISPPARAMS *pDispParams,
    VARIANT *pVarResult,
    EXCEPINFO  *pExcepInfo,
    UINT *puArgErr)
{
	// We have no paramters for these functions, so we can just
	// ignore the DISPPARAMS.
	switch(dispIdMember)
	{
	case DISPID_DRAWSQUIG: 
		DrawASquiggle();
		return S_OK;	
		
	case DISPID_FLIPSQUIG: 
		FilpASquiggle();
		return S_OK;

	case DISPID_ERASESQUIG: 
		EraseASquiggle();
		return S_OK;

	default:
		return DISP_E_UNKNOWNINTERFACE;
	}
}

    
void CoSquiggle::DrawASquiggle()
{
	// Drawing a squiggle...
	MessageBox(NULL, "Drawing a squiggle", 
			   "CoSquiggle Invoke", MB_OK | MB_SETFOREGROUND);
}

void CoSquiggle::FilpASquiggle()
{
	// Flipping a squiggle...
	MessageBox(NULL, "Flipping a squiggle", 
			   "CoSquiggle Invoke", MB_OK | MB_SETFOREGROUND);
}

void CoSquiggle::EraseASquiggle()
{
	// Erasing a squiggle...
	MessageBox(NULL, "Erasing a squiggle", 
			   "CoSquiggle Invoke", MB_OK | MB_SETFOREGROUND);
}