// Client.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include "iids.h"
#include "interfaces.h"
#include <iostream.h>

int main(int argc, char* argv[])
{
	CoInitialize(NULL);

	HRESULT hr;
	IText* pText;
	hr = CoCreateInstance(CLSID_CoText, NULL, CLSCTX_SERVER, IID_IText, (void**)&pText);

	// First, create a BSTR and send it into the object.
	//

	BSTR bstr;
	bstr  = SysAllocString(L"Is anybody out there?!");
	pText-> Speak (bstr);		// Server pops up a message box.
	SysFreeString(bstr);		// I made it, I free it.

	// Now, we are asking the object for a new BSTR, which is allocated for us.
	BSTR bstr2;
	pText -> GiveMeABSTR(&bstr2);

	// As the BSTR is Unicode, we can convert to ANSI using wcstombs().
	char buff[80];
	wcstombs(buff, bstr2, 80);	// Convert to ANSI for cout.
	cout << "\'" << buff << "\' " << " is what the server said!" << endl;

	SysFreeString(bstr2); 	// It was made for me, I free it.
	pText->Release();		// Done with interface.

	CoUninitialize();

	return 0;
}
