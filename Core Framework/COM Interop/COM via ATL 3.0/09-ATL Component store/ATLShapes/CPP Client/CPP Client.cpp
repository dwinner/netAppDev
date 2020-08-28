// CPP Client.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include <comcat.h>
#include "catids.h"

#include "atlshapes.h"
#include "atlshapes_i.c"
#include <iostream.h>

int main(int argc, char* argv[])
{
	CoInitialize(NULL);

	// We want to get all classes that are members of the 
	// Named Drawing Objects COM category.
	ICatInformation *pCatInfo = NULL;
	HRESULT hr;

	// Get the categories manager.
	hr = CoCreateInstance(CLSID_StdComponentCategoriesMgr, 0,
						  CLSCTX_SERVER, IID_ICatInformation, (void**)&pCatInfo);

	// Now get em!
	if(SUCCEEDED(hr))
	{
		IEnumCLSID *pCLSIDS = NULL;
		CATID catids[1];
		catids[0] = CATID_NamedShape;

		hr = pCatInfo->EnumClassesOfCategories(1, catids, -1, 0, &pCLSIDS);
		CLSID clsid[20];
		LPOLESTR progID;
		
		cout << "Here are the proud members of " << endl  
			 <<	"the Named Drawing Objects category" << endl << endl;
		do
		{
			DWORD numb = 0;
			hr = pCLSIDS->Next(20, clsid, &numb);
			if(SUCCEEDED(hr))
			{
				for(DWORD i = 0; i < numb; i++)
				{
					ProgIDFromCLSID(clsid[i], &progID);
					char buff[30];
					WideCharToMultiByte(CP_ACP, NULL, progID, -1, buff, 30,
										NULL, NULL);
					cout << buff << endl;

				}
				cout << endl;
			}
					
		}while(hr == S_OK);
		pCLSIDS->Release();

	}
	pCatInfo->Release();
	CoUninitialize();
	MessageBox(NULL, "All done", "Note!", MB_OK | MB_SETFOREGROUND);
	return 0;
}
