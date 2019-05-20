// CoHexagon.h: interface for the CoHexagon class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_COHEXAGON_H__8945C9B9_2C00_11D3_B901_0020781238D4__INCLUDED_)
#define AFX_COHEXAGON_H__8945C9B9_2C00_11D3_B901_0020781238D4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "rawnested.h"

// CoHexagon using nested classes as opposed to MI.
// 
class CoHexagon : public IUnknown
{
public:
	CoHexagon();
	virtual ~CoHexagon();

	class IDrawImpl : public IDraw
	{
	public:

		// IUnknown methods.
		STDMETHODIMP_(ULONG) AddRef();
		STDMETHODIMP_(ULONG) Release();
		STDMETHODIMP QueryInterface(REFIID riid, void** ppv);

		// IDraw methods.
		STDMETHODIMP Draw();

		// Pointer to parent.
		CoHexagon* m_pParent;
	};

	class IShapeIDImpl : public IShapeID
	{
	public:

		// IUnknown methods.
		STDMETHODIMP_(ULONG) AddRef();
		STDMETHODIMP_(ULONG) Release();
		STDMETHODIMP QueryInterface(REFIID riid, void** ppv);

		// IShapeID methods.
		STDMETHODIMP put_ID(int id);
		STDMETHODIMP get_ID(int* id);

		// Pointer to parent.
		CoHexagon* m_pParent;
	};

	// Master IUnknown impl.
	STDMETHODIMP_(ULONG) AddRef();
	STDMETHODIMP_(ULONG) Release();
	STDMETHODIMP QueryInterface(REFIID riid, void** ppv);

	int m_ID;

private:
	ULONG m_refCount;		
	IDrawImpl m_drawImpl;
	IShapeIDImpl m_shapeIDImpl;
};

#endif // !defined(AFX_COHEXAGON_H__8945C9B9_2C00_11D3_B901_0020781238D4__INCLUDED_)
