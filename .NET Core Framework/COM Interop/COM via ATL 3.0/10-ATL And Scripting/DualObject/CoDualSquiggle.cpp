// CoSquiggle.cpp: implementation of the CoSquiggle class.
//
//////////////////////////////////////////////////////////////////////

#include "CoDualSquiggle.h"
extern DWORD g_objCount;
#include "dualobj_i.c"

// The DISPID constants
#define DISPID_DRAWSQUIG	1
#define DISPID_FLIPSQUIG	2
#define DISPID_ERASESQUIG	3
//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CoDualSquiggle::CoDualSquiggle() : m_ref(0)
{
	++g_objCount;
}

CoDualSquiggle::~CoDualSquiggle()
{
	--g_objCount;
}


STDMETHODIMP_(DWORD) CoDualSquiggle::AddRef()
{
	return ++m_ref;
}

STDMETHODIMP_(DWORD)CoDualSquiggle::Release()
{
	--m_ref;
	if(m_ref == 0)
	{
		delete this;
		return 0;
	}
	return m_ref;
}

STDMETHODIMP CoDualSquiggle::QueryInterface(REFIID riid, void** ppv)
{
	if(riid == IID_IUnknown)
		*ppv = (IUnknown*)this;
	else if(riid == IID_IDispatch)	// <-- This is for scripting clients!
	{
		*ppv = (IDispatch*)this;
		MessageBox(NULL, "Using IDispatch", 
				   "Late binding", MB_OK | MB_SETFOREGROUND);
	}
	else if(riid == IID_ISquiggle)	// <-- For early clients!
	{
		*ppv = (ISquiggle*)this;
		MessageBox(NULL, "Using ISquiggle", 
				   "Early binding", MB_OK | MB_SETFOREGROUND);
	}
	else
	{
		*ppv = NULL;
		return E_NOINTERFACE;
	}
	((IUnknown*)(*ppv))->AddRef();
	return S_OK;
}


// IDispatch Impl.
STDMETHODIMP CoDualSquiggle::GetTypeInfoCount( UINT *pctinfo)
{
	// We do not support type information in this object.
	*pctinfo = 0;
	return S_OK;
}


STDMETHODIMP CoDualSquiggle::GetTypeInfo( 
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
STDMETHODIMP CoDualSquiggle::GetIDsOfNames( 
    REFIID riid,
    LPOLESTR *rgszNames,
    UINT cNames,
    LCID lcid,
    DISPID *rgDispId)
{
	// First off, we only support one name at a time.
	if(cNames > 1) 
		return E_INVALIDARG;

	// Are they asking for the DISPID for DrawSquiggle?
	if(_wcsicmp(*rgszNames, OLESTR("DrawASquiggle")) == 0)
	{
		*rgDispId = DISPID_DRAWSQUIG;
		return S_OK;
	}
	if(_wcsicmp(*rgszNames, OLESTR("FlipASquiggle")) == 0)
	{
		*rgDispId = DISPID_FLIPSQUIG;
		return S_OK;
	}
	if(_wcsicmp(*rgszNames, OLESTR("EraseASquiggle")) == 0)
	{
		*rgDispId = DISPID_ERASESQUIG;
		return S_OK;
	}
	else
		return DISP_E_UNKNOWNNAME;
}


// Now, based on the DISPID, call the correct helper function
STDMETHODIMP CoDualSquiggle::Invoke( 
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
		FlipASquiggle();
		return S_OK;

	case DISPID_ERASESQUIG: 
		EraseASquiggle();
		return S_OK;

	default:
		return DISP_E_UNKNOWNINTERFACE;
	}
}

// ISquiggle    
STDMETHODIMP CoDualSquiggle::DrawASquiggle()
{
	// Drawing a squiggle...
	MessageBox(NULL, "Drawing a squiggle", 
			   "CoDualSquiggle", MB_OK | MB_SETFOREGROUND);
	return S_OK;
}

STDMETHODIMP CoDualSquiggle::FlipASquiggle()
{
	// Drawing a squiggle...
	MessageBox(NULL, "Flipping a squiggle", 
			   "CoDualSquiggle", MB_OK | MB_SETFOREGROUND);
	return S_OK;

}

STDMETHODIMP CoDualSquiggle::EraseASquiggle()
{
	// Drawing a squiggle...
	MessageBox(NULL, "Erasing a squiggle", 
			   "CoDualSquiggle", MB_OK | MB_SETFOREGROUND);
	return S_OK;
}