// PeopleHolder.cpp : Implementation of CPeopleHolder
#include "stdafx.h"
#include "ATLEnumServer.h"
#include "PeopleHolder.h"

/////////////////////////////////////////////////////////////////////////////
// CPeopleHolder

STDMETHODIMP CPeopleHolder::GetPersonEnum(IEnumPerson **ppEnumPerson)
{
	// This method is in charge of returning the IEnumPerson interface
	// to the client.
	
	typedef CComObject< CComEnum<IEnumPerson, &IID_IEnumPerson, 
					    IPerson*, _CopyInterface<IPerson> > > EnumPerson;
	EnumPerson* pNewEnum = NULL;
	EnumPerson::CreateInstance(&pNewEnum);
	
	// Now fill the enumerator with all the IPerson objects.
	pNewEnum->Init(&m_theFolks[0], &m_theFolks[9], NULL, AtlFlagCopy);

	// Now return the enumerator.
	return pNewEnum->QueryInterface(IID_IEnumPerson, (void**)ppEnumPerson);
}
