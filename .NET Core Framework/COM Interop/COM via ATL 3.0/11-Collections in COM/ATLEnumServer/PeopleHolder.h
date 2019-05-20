// PeopleHolder.h : Declaration of the CPeopleHolder

#ifndef __PEOPLEHOLDER_H_
#define __PEOPLEHOLDER_H_

#include "resource.h"       // main symbols
#include "person.h"

const int MAX = 10;

/////////////////////////////////////////////////////////////////////////////
// CPeopleHolder
class ATL_NO_VTABLE CPeopleHolder : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CPeopleHolder, &CLSID_PeopleHolder>,
	public IPeopleHolder
{
public:
	CPeopleHolder()
	{
	}

	HRESULT FinalConstruct()
	{
		ATLTRACE("Creating the people holder\n");
		// Fill the vector with some people.
		CComBSTR name[MAX] = {L"Fred", L"Alice", L"Joe", L"Mitch", L"Beth",
							  L"Mikey", L"Bart", L"Mary", L"Wally", L"Pete" };
		
		for(int i = 0; i < MAX; i++)
		{
			CComObject<CPerson>* pNewPerson = NULL;
			CComObject<CPerson>::CreateInstance(&pNewPerson);
			pNewPerson->put_ID(i);
			pNewPerson->put_Name(name[i].Copy());
			
			// Get IPerson from the new object.
			IPerson* pPerson = NULL;
			pNewPerson->QueryInterface(IID_IPerson, (void**)&pPerson);

			m_theFolks[i] = pPerson;
		}
	
		return S_OK;
	}

	void FinalRelease()
	{
		ATLTRACE("Killed the people holder\n");
		for(int i = 0; i < MAX; i++)
		{
			m_theFolks[i]->Release();
		}		
	}


DECLARE_REGISTRY_RESOURCEID(IDR_PEOPLEHOLDER)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CPeopleHolder)
	COM_INTERFACE_ENTRY(IPeopleHolder)
END_COM_MAP()

// IPeopleHolder
public:
	STDMETHOD(GetPersonEnum)(/*[out]*/ IEnumPerson** ppEnumPerson);

private:
	IPerson* m_theFolks[MAX];
};

#endif //__PEOPLEHOLDER_H_
