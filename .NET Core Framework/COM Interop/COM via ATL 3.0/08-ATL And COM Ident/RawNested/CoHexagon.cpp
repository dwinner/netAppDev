// CoHexagon.cpp: implementation of the CoHexagon class.
//
//////////////////////////////////////////////////////////////////////

#include "CoHexagon.h"
extern ULONG g_objCount;
#include <stdio.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CoHexagon::CoHexagon()
{
	// Set ref count to zero.
	m_refCount = 0;
	m_ID = 0;
	g_objCount++;

	// Tell nested classes who poppa is.
	m_drawImpl.m_pParent = this;
	m_shapeIDImpl.m_pParent = this;
}

CoHexagon::~CoHexagon()
{
	g_objCount--;
}

//////////////////////////////////////////////////////////////////////
// IShapeIDImpl IUnknown.
STDMETHODIMP_(ULONG) CoHexagon::AddRef()
{
	return ++m_refCount;
}

STDMETHODIMP_(ULONG) CoHexagon::Release() 
{
	if(--m_refCount == 0)
	{
		delete this;
		return 0;
	}
	return m_refCount;
}

STDMETHODIMP CoHexagon::QueryInterface(REFIID riid, void** ppv)
{

    if (riid == IID_IUnknown)
    {
        *ppv = (IUnknown*)this;
    }
    else if (riid == IID_IDraw)
    {
        *ppv = (IDraw*)&m_drawImpl;
    }
	else if(riid == IID_IShapeID)
    {
        *ppv = (IShapeID*)&m_shapeIDImpl;
    }
	else
	{
		*ppv = NULL;
		return E_NOINTERFACE;
	}

	((IUnknown*)(*ppv))->AddRef();
	return S_OK;
}


//////////////////////////////////////////////////////////////////////
// IDrawImpl 
STDMETHODIMP_(ULONG) CoHexagon::IDrawImpl::AddRef()
{
	return m_pParent->AddRef();
}

STDMETHODIMP_(ULONG) CoHexagon::IDrawImpl::Release() 
{
	return m_pParent->Release();
}

STDMETHODIMP CoHexagon::IDrawImpl::QueryInterface(REFIID riid, void** ppv)
{
	return m_pParent->QueryInterface(riid, ppv);
}

STDMETHODIMP CoHexagon::IDrawImpl::Draw()
{
	MessageBox(NULL, "CoHexagon::IDrawImpl::Draw", "Inner Class", 
			   MB_OK | MB_SETFOREGROUND );
	return S_OK;
}

//////////////////////////////////////////////////////////////////////
// IShapeIDImpl IUnknown.
STDMETHODIMP_(ULONG) CoHexagon::IShapeIDImpl::AddRef()
{
	return m_pParent->AddRef();
}

STDMETHODIMP_(ULONG) CoHexagon::IShapeIDImpl::Release() 
{
	return m_pParent->Release();
}

STDMETHODIMP CoHexagon::IShapeIDImpl::QueryInterface(REFIID riid, void** ppv)
{
	return m_pParent->QueryInterface(riid, ppv);
}

STDMETHODIMP CoHexagon::IShapeIDImpl::put_ID(int shapeID)
{
	// Set parents m_ID.
	m_pParent->m_ID = shapeID;

	return S_OK;
}

STDMETHODIMP CoHexagon::IShapeIDImpl::get_ID(int* shapeID)
{	
	// Set parents m_ID.
	*shapeID = m_pParent->m_ID;

	return S_OK;
}
