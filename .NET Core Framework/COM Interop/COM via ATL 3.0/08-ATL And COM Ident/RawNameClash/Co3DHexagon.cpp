// Co3DHexagon.cpp: implementation of the Co3DHexagon class.
//
//////////////////////////////////////////////////////////////////////

#include "Co3DHexagon.h"
extern DWORD g_objCount;
//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

Co3DHexagon::Co3DHexagon()
{
	m_refCount = 0;
	m_3dRenderImpl.m_pParent = this;
	g_objCount++;
}

Co3DHexagon::~Co3DHexagon()
{
	g_objCount--;
}

STDMETHODIMP_(ULONG) Co3DHexagon::AddRef()
{
	return ++m_refCount;
}

STDMETHODIMP_(ULONG) Co3DHexagon::Release() 
{
	if(--m_refCount == 0)
	{
		delete this;
		return 0;
	}
	return m_refCount;
}

STDMETHODIMP Co3DHexagon::QueryInterface(REFIID riid, void** ppv)
{
    if (riid == IID_IUnknown)
    {
        *ppv = (IUnknown*)(IDraw*)this;
    }
    else if (riid == IID_IDraw)
    {
        *ppv = (IDraw*)this;
    }
	else if(riid == IID_I3DRender)
	{
		*ppv = (I3DRender*)&m_3dRenderImpl;		
	}
	else
	{
		*ppv = NULL;
		return E_NOINTERFACE;
	}

	((IUnknown*)(*ppv))->AddRef();
	return S_OK;
}

STDMETHODIMP Co3DHexagon::Draw()
{
	MessageBox(NULL, "Drawing a boring 2D hexagon", "Outer Class", MB_OK);
	return S_OK;
}

//////////////////////////////////////////////////////////////////////
// I3DRenderImpl 
STDMETHODIMP_(ULONG) Co3DHexagon::I3DRenderImpl::AddRef()
{
	return m_pParent->AddRef();
}

STDMETHODIMP_(ULONG) Co3DHexagon::I3DRenderImpl::Release() 
{
	return m_pParent->Release();
}

STDMETHODIMP Co3DHexagon::I3DRenderImpl::QueryInterface(REFIID riid, void** ppv)
{
	return m_pParent->QueryInterface(riid, ppv);
}

STDMETHODIMP Co3DHexagon::I3DRenderImpl::Draw()
{
	MessageBox(NULL, "Drawing a lovely 3D hexagon", "Inner Class", MB_OK);
	return S_OK;
}