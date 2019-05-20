// SquiggleCollection2.h : Declaration of the CSquiggleCollection2

#ifndef __SQUIGGLECOLLECTION2_H_
#define __SQUIGGLECOLLECTION2_H_

#include "resource.h"       // main symbols

// This time we are making use of the STL to hold our IDispatch interfaces.
#include <vector>
#include "cosquiggle.h"

/////////////////////////////////////////////////////////////////////////////
// CSquiggleCollection2
class ATL_NO_VTABLE CSquiggleCollection2 : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CSquiggleCollection2, &CLSID_SquiggleCollection2>,
	public IDispatchImpl<ISquiggleCollection2, &IID_ISquiggleCollection2, &LIBID_BETTERSQUIGGLECOLLECTIONLib>
{
public:
	CSquiggleCollection2()
	{
	}
	
	// Create 3 squiggles automatically.
	HRESULT FinalConstruct()
	{
		for(int i = 0; i < 3; i++)
		{
			// First make a squiggle.
			CComObject< CCoSquiggle > *pSquig = NULL;
			CComObject< CCoSquiggle >::CreateInstance(&pSquig);

			// Give the new squiggle a name.
			CComBSTR temp[3] = {L"Mandy", L"Whoppie", L"Eric"};
			pSquig->put_Name(temp[i]);
			
			// Get the IDispatch pointer and place in the STL vector.
			LPDISPATCH pDisp;
			if(SUCCEEDED(pSquig->QueryInterface(IID_IDispatch, (void**)&pDisp)))
			{
				m_vecSquiggles.push_back(pDisp);
			}
		}
		return S_OK;
	}

	void FinalRelease()
	{
		// Release all the squiggles from the vector.
		int size = m_vecSquiggles.size();
		for(int i = 0; i < size; i++)
		{
			LPDISPATCH pDisp;
			pDisp = m_vecSquiggles[i];
			pDisp -> Release();
		}
	}

DECLARE_REGISTRY_RESOURCEID(IDR_SQUIGGLECOLLECTION2)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CSquiggleCollection2)
	COM_INTERFACE_ENTRY(ISquiggleCollection2)
	COM_INTERFACE_ENTRY(IDispatch)
END_COM_MAP()

// ISquiggleCollection2
public:
	STDMETHOD(Remove)(/*[in]*/ long index);
	STDMETHOD(Add)(IDispatch *pnewSquiggle);
	STDMETHOD(get_Count)(/*[out, retval]*/ long *pVal);
	STDMETHOD(Item)(/*[in]*/ VARIANT index, /*[out, retval]*/ VARIANT* pItem);
	STDMETHOD(get__NewEnum)(/*[out, retval]*/ LPUNKNOWN *pVal);


private:
	std::vector< IDispatch* > m_vecSquiggles;
};

#endif //__SQUIGGLECOLLECTION2_H_
