// CoSquiggle.cpp: implementation of the CoSquiggle class.
//
//////////////////////////////////////////////////////////////////////

#include "CoDualSquiggle.h"
extern DWORD g_objCount;
#include "dualobjwithti_i.c"

// The DISPID constants
// #define DISPID_DRAWSQUIG	1
// #define DISPID_FLIPSQUIG	2
// #define DISPID_ERASESQUIG	3
//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CoDualSquiggle::CoDualSquiggle() : m_ref(0), m_ptypeInfo(NULL)
{
	++g_objCount;
	
	// When our object is constructed, we are going to 
	// load up the *tlb file and store it in our ITypeInfo pointer.
	ITypeLib* pTypeLibrary = NULL;
	HRESULT hr;
	hr = LoadRegTypeLib(LIBID_DualWithTypeInfo, 1, 0, 
					    LANG_NEUTRAL, &pTypeLibrary);
	
	if(SUCCEEDED(hr))
	{
		MessageBox(NULL, "Got type info", "Good news", MB_OK | MB_SETFOREGROUND);
		pTypeLibrary->GetTypeInfoOfGuid(IID_ISquiggleTI, &m_ptypeInfo);
		pTypeLibrary->Release();
	}
}

CoDualSquiggle::~CoDualSquiggle()
{
	--g_objCount;
	m_ptypeInfo->Release();
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
	// We do not support any custom interfaces here.
	// All object functionality is given via IDispatch
	if(riid == IID_IUnknown)
		*ppv = (IUnknown*)this;
	else if(riid == IID_IDispatch)	// <-- This is for scripting clients!
	{
		*ppv = (IDispatch*)this;
		MessageBox(NULL, "Using IDispatch", 
				   "Late binding", MB_OK | MB_SETFOREGROUND);
	}
	else if(riid == IID_ISquiggleTI)	// <-- For early clients!
	{
		*ppv = (ISquiggleTI*)this;
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
	// We DO support type information in this object.
	*pctinfo = 1;
	return S_OK;
}


STDMETHODIMP CoDualSquiggle::GetTypeInfo( 
    UINT iTInfo,
    LCID lcid,
    ITypeInfo **ppTInfo)
{
	// Return pointer as we DO support type info.
	*ppTInfo = m_ptypeInfo;
	m_ptypeInfo->AddRef();
	return S_OK;
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
	
	// Now we just delegate the work of the look-up to
	// our type library.
	return DispGetIDsOfNames(m_ptypeInfo, rgszNames, cNames, rgDispId);
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
	// Again, delegate to the type info.
	return DispInvoke(this, m_ptypeInfo, dispIdMember, wFlags, pDispParams,
					  pVarResult, pExcepInfo, puArgErr);

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

STDMETHODIMP CoDualSquiggle::Explode()
{
	// Oh rats!
	MessageBox(NULL, "OH NO!", 
			   "CoDualSquiggle has EXPLODED", MB_OK | MB_SETFOREGROUND);
	return S_OK;
}