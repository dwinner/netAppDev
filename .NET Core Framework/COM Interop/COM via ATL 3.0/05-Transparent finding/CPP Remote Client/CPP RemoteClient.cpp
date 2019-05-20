// CPP RemoteClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#define _WIN32_DCOM
#include <windows.h>

#include "CarServerEXETypeInfo_i.c"
#include "CarServerEXETypeInfo.h"

int main(int argc, char* argv[])
{
	CoInitialize(NULL);		// Init COM.
	HRESULT hr;
	COSERVERINFO csi = {0};	// null out all fields.

	// Set the name of the remote server.
	csi.pwszName = (L"BIGMANU"); /* ADD YOUR SERVER NAME HERE!!!!!!!! */

	// Here are the interfaces I want.
	MULTI_QI qi[3] = {0};
	qi[0].pIID = &IID_IEngine;
	qi[1].pIID = &IID_IStats;
	qi[2].pIID = &IID_ICreateCar;

	// Create the remote CoCar.
	hr = CoCreateInstanceEx(CLSID_CoCar, NULL, CLSCTX_REMOTE_SERVER, &csi, 3, qi);

	// Assign interface pointers to fetched results.
	IEngine* pEngine = (IEngine*)qi[0].pItf;
	IStats* pStats = (IStats*)qi[1].pItf;
	ICreateCar* pCCar = (ICreateCar*)qi[2].pItf;

	// Use car.
	BSTR name;
	name = SysAllocString(L"Cindy");
	pCCar->SetPetName(name);
	pCCar->SetMaxSpeed(40);
	pStats->DisplayStats();
	
	SysFreeString(name);
	pCCar->Release();
	pStats->Release();
	pEngine->Release();

	CoUninitialize();		// Terminate COM.
	return 0;
}
