// Co3DHexagon.h: interface for the Co3DHexagon class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CO3DHEXAGON_H__8945C9B8_2C00_11D3_B901_0020781238D4__INCLUDED_)
#define AFX_CO3DHEXAGON_H__8945C9B8_2C00_11D3_B901_0020781238D4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "rawnameclash.h"

class Co3DHexagon : public IDraw
{
public:
	Co3DHexagon();
	virtual ~Co3DHexagon();

	// IUnknown
	STDMETHODIMP_(ULONG) AddRef();
	STDMETHODIMP_(ULONG) Release();
	STDMETHODIMP QueryInterface(REFIID riid, void** ppv);

	// IDraw::Draw
	STDMETHODIMP Draw();
	
	// IShapeID
	STDMETHODIMP ID(int id);
	
	// I3DRender
	struct I3DRenderImpl : public I3DRender
	{
		// IUnknown methods.
		STDMETHODIMP_(ULONG) AddRef();
		STDMETHODIMP_(ULONG) Release();
		STDMETHODIMP QueryInterface(REFIID riid, void** ppv);

		// IDraw methods.
		STDMETHODIMP Draw();

		// Pointer to parent.
		Co3DHexagon* m_pParent;
	};

private:
	I3DRenderImpl m_3dRenderImpl;
	ULONG m_refCount;
};

#endif // !defined(AFX_CO3DHEXAGON_H__8945C9B8_2C00_11D3_B901_0020781238D4__INCLUDED_)
