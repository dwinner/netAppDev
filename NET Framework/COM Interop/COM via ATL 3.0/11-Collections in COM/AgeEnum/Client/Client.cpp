// Client.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include "ageenum_i.c"
#include "ageenum.h"
#include <iostream.h>

int main(int argc, char* argv[])
{
	CoInitialize(NULL);
	
	ULONG uElementsRead;
	ULONG pAges[5000];
	IEnumAge* pEAge;

	// Create the age holder & get the eunmertor.
	CoCreateInstance(CLSID_CoRawAgeHolder, NULL, 
					 CLSCTX_SERVER, IID_IEnumAge, (void**)&pEAge);
	
	// Go get 400 ages at once.
	pEAge->Next(400, pAges, &uElementsRead);
	
	// Show value for each of them.
	for(ULONG i = 0; i < uElementsRead; i++)
	{	
		cout << "Age " << i << " is " << pAges[i] << endl;
	}

	// Skip 27 in the sequence.
	cout << endl << "-->Skipping 27 Ages..." << endl;
	pEAge->Skip(27);


	// Get next age (i.e. #428).
	cout << endl << "-->Getting next age..." << endl;
	ULONG newAge[1];
	pEAge->Next(1, newAge, &uElementsRead);
	cout << "Age #428 is " << newAge[0] << endl;


	// Clone the current enumerator.
	IEnumAge* pClone = NULL;
	cout << endl << "-->Cloaned first enumerator..." << endl;
	pEAge->Clone((IEnumAge**)&pClone);

	// Reset first enum.
	pEAge->Reset();
	cout << endl << "-->First enumerator has been reset..." << endl;
	
	// Get next of first enum.
	ULONG anotherAge[1];
	cout << endl << "-->Getting Next from first enumerator..." << endl;
	pEAge->Next(1, anotherAge, &uElementsRead);
	cout << "Next of first is " << anotherAge[0] << endl;

	// Done with first guy.
	if(pEAge)
		pEAge->Release();

		
	// Get next from clone.
	ULONG finalAge[1];
	cout << endl << "-->Getting Next from Cloned enumerator..." << endl;
	pClone->Next(1, finalAge, &uElementsRead);
	cout << "Next clone is " << finalAge[0] << endl;
	
	// Done with clone.
	if(pClone)
		pClone->Release();

	CoUninitialize();
	return 0;	
}
