// EnumTester.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include <iostream.h>
#import "C:\ATL\Labs\Chapter 11\ATLEnumServer\ATLEnumServer.tlb" no_namespace named_guids

int main(int argc, char* argv[])
{
	CoInitialize(NULL);

	IPeopleHolderPtr pPeople(CLSID_PeopleHolder);
	IEnumPersonPtr pEnum;
	pPeople->GetPersonEnum(&pEnum);

	HRESULT hr;
	ULONG uElementsRead;


	IPersonPtr pPerson[20];
	hr = pEnum->Next(90, (IPerson**)&pPerson, &uElementsRead);

	for(int i = 0; i < uElementsRead; i++)
	{
		_bstr_t name;
		name = pPerson[i]->GetName();
		cout << "Name is: " << name << endl;
		cout << "ID is: " << pPerson[i]->GetID() << endl;
		pPerson[i] = NULL;
	}
		
	pPeople = NULL;
	pEnum = NULL;
	CoUninitialize();
	return 0;	
}
