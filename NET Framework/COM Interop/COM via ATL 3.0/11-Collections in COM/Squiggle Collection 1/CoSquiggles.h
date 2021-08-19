// CoSquiggles.h : Declaration of the CCoSquiggles

#ifndef __COSQUIGGLES_H_
#define __COSQUIGGLES_H_

#include "resource.h"       // main symbols
#include "cosquiggle.h"
const int MAX_SQUIGGLES = 10;

/////////////////////////////////////////////////////////////////////////////
// CCoSquiggles
class ATL_NO_VTABLE CCoSquiggles : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CCoSquiggles, &CLSID_CoSquiggles>,
	public IDispatchImpl<ICoSquiggles, &IID_ICoSquiggles, &LIBID_SQUIGGLECOLLECTIONLib>
{
public:
	CCoSquiggles()
	{
	}

	// Create the squiggles!
	HRESULT FinalConstruct()
	{
		CComBSTR names[MAX_SQUIGGLES] = 
					{L"Amy", L"Freda", L"Siddhartha Govinda", L"Tom",
					 L"Tim", L"Amanda", L"Max", L"Danny", L"Beth", L"Joe"};
	
		for(int i = 0; i < MAX_SQUIGGLES; i++)
		{
			CComObject<CCoSquiggle> *pSquig = NULL;
			CComObject<CCoSquiggle>::CreateInstance(&pSquig);
			pSquig->put_Name(names[i].Copy());

			IDispatch* pDisp;
			pSquig->QueryInterface(IID_IDispatch, (void**)&pDisp);
			m_pArrayOfSquigs[i] = pDisp;
		}
		return S_OK;
	}

	// Kill the squiggles!
	void FinalRelease()
	{
		for(int i = 0; i < 10; i++)
		{
			m_pArrayOfSquigs[i]->Release();
		}
	}

DECLARE_REGISTRY_RESOURCEID(IDR_COSQUIGGLES)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CCoSquiggles)
	COM_INTERFACE_ENTRY(ICoSquiggles)
	COM_INTERFACE_ENTRY(IDispatch)
END_COM_MAP()

// ICoSquiggles
public:
	STDMETHOD(get_Count)(/*[out, retval]*/ long *pVal);
	STDMETHOD(Item)(/*[in]*/ VARIANT index, /*[out, retval]*/ VARIANT* pItem);
	STDMETHOD(get__NewEnum)(/*[out, retval]*/ LPUNKNOWN *pVal);

private:
	IDispatch* m_pArrayOfSquigs[MAX_SQUIGGLES];
};

#endif //__COSQUIGGLES_H_
