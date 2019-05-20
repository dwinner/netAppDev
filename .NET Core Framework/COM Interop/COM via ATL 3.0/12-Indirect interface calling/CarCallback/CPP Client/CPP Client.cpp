// CPP Client.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "carcallback.h"
#include "carcallback_i.c"
#include "CoTheCallBack.h"

CCoSink g_sink;

int main(int argc, char* argv[])
{
	CoInitialize(NULL);

	HRESULT hr;
	IEstablishCommunications* pEC;

	hr = CoCreateInstance(CLSID_CoCarCallBack, NULL, 
		 CLSCTX_SERVER, IID_IEstablishCommunications, (void**)&pEC);

	// Set up the advise...
	pEC->Advise((IEngineEvents*)&g_sink);

	// Create the car.
	ICreateCar* pCC;
	pEC->QueryInterface(IID_ICreateCar, (void**)&pCC);
	pCC->SetMaxSpeed(50);

	// Grab the engine.
	IEngine* pE;
	pEC->QueryInterface(IID_IEngine, (void**)&pE);
	
	// Speed things up...
	for(int i = 0; i < 5; i++)
		pE->SpeedUp();

	// See ya.
	pEC->Unadvise();
	pEC->Release();
	pE->Release();
	pCC->Release();

	CoUninitialize();
	return 0;
}
