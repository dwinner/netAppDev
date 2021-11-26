// CoBulkObject.cpp : Implementation of CCoBulkObject
#include "stdafx.h"
#include "BulkServer.h"
#include "CoBulkObject.h"
#include "CoSub.h"

/////////////////////////////////////////////////////////////////////////////
// CCoBulkObject

STDMETHODIMP CCoBulkObject::UseTheseStrings(short size, BSTR names[])
{
	USES_CONVERSION;
	// Show each BSTR in a message box.
	for(int i = 0; i < size; i++)
	{
		MessageBox(NULL, W2A(names[i]), "Message from the client!", MB_OK | MB_SETFOREGROUND);
		SysReAllocString(&(names[i]), L"This is different");
	}
	return S_OK;
}


// Client gives us a batch of strings packages in a safe array, which we
// use as in our program.
//
STDMETHODIMP CCoBulkObject::UseThisSafeArrayOfStrings(VARIANT strings)
{
	USES_CONVERSION;
	
	// Be sure we have an array of strings.
	if((strings.vt & VT_ARRAY) && (strings.vt & VT_BSTR))
	{
		SAFEARRAY *pSA = strings.parray;
		BSTR *bstrArray;
		// Lock it down.
		SafeArrayAccessData(pSA, (void**)&bstrArray);
		
		// Read each item.
		for(int i = 0; i < pSA->rgsabound->cElements; i++)
		{
			CComBSTR temp = bstrArray[i];
			MessageBox(NULL, W2A(temp.m_str), "String says...", MB_OK | MB_SETFOREGROUND);
		}
		// Unlock it.
		SafeArrayUnaccessData(pSA);
	}
	return S_OK;
}


// Create a set of strings and give them back to the clients.
//
STDMETHODIMP CCoBulkObject::GiveMeASafeArrayOfString(VARIANT *pStrings)
{
	// Init and set the type of variant.
	VariantInit(pStrings);
	pStrings->vt = VT_ARRAY | VT_BSTR;

	SAFEARRAY *pSA;
	SAFEARRAYBOUND bounds = {4, 0};
	
	// Create the array.
	pSA = SafeArrayCreate(VT_BSTR, 1, &bounds);

	// Fill the array.
	BSTR *theStrings;
	SafeArrayAccessData(pSA, (void**)&theStrings);
		theStrings[0] = SysAllocString(L"Hello");
		theStrings[1] = SysAllocString(L"from");
		theStrings[2] = SysAllocString(L"the");
		theStrings[3] = SysAllocString(L"coclass!");
	SafeArrayUnaccessData(pSA);

	// Set return value.
	pStrings->parray = pSA;
	
	return S_OK;
}


// Use this struct!
//
STDMETHODIMP CCoBulkObject::WorkWithAPet(MyPet *p)
{
	USES_CONVERSION;
	
	// Show current Name of pet.
	MessageBox(NULL, W2A(p->Name), "Name of pet is", MB_OK);

	// Show current Age of pet.
	char buff[80] = {0};
	sprintf(buff, "%d", p->Age);
	MessageBox(NULL, buff, "Age of pet is", MB_OK);

	// Show current Type of pet.
	char* strType[3] = {"Dog", "Cat", "Tick"};
	MessageBox(NULL, strType[p->Type], "Type of pet is", MB_OK);

	// Now change everything.
	SysReAllocString(&(p->Name), L"Bubbles");
	p->Age = 200;
	p->Type = petTick;
	return S_OK;
}

STDMETHODIMP CCoBulkObject::GetSubObject(ICoSub **ppv)
{
	// Return the sub object.
	CComObject<CCoSub> *pSub;
	CComObject<CCoSub>::CreateInstance(&pSub);
	pSub->QueryInterface(IID_ICoSub, (void**)ppv);
	return S_OK;
}