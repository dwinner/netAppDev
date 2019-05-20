// CPP Client.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include "rawerrors_i.c"
#include "rawerrors.h"
#include <iostream.h>

// Error handler.
void ParseError(IUnknown* pInterface, REFIID riid);

int main(int argc, char* argv[])
{
	CoInitialize(0);

	IEngine *pEngine = NULL;
	ICreateCar *pCCar = NULL;
	
	// Make the car.
	CoCreateInstance(CLSID_CoCarErrors, NULL, CLSCTX_SERVER, IID_ICreateCar, (void**)&pCCar);
	pCCar->SetMaxSpeed(20);
	pCCar->QueryInterface(IID_IEngine, (void**)&pEngine);
	pCCar->Release();

	// Speed up to cause error
	if(FAILED(pEngine->SpeedUp()))
		ParseError(pEngine, IID_IEngine);

	if(FAILED(pEngine->SpeedUp()))
		ParseError(pEngine, IID_IEngine);

	if(FAILED(pEngine->SpeedUp()))
		ParseError(pEngine, IID_IEngine);

	pEngine->Release();

	CoUninitialize();
	return 0;
}

void ParseError(IUnknown* pInterface, REFIID riid)
{
	pInterface->AddRef();

	ISupportErrorInfo *pISER = NULL;
	HRESULT hr;
	// Does this object have COM exceptions for me?
	hr = pInterface->QueryInterface(IID_ISupportErrorInfo, (void**)&pISER);
	
	if(SUCCEEDED(hr))
	{
		// Does this interface have COM exception support?
		if(SUCCEEDED(pISER->InterfaceSupportsErrorInfo(riid)))
		{
			// Great! Get the exception.
			IErrorInfo *pEI = NULL;
			if(SUCCEEDED(GetErrorInfo(NULL, &pEI)))
			{
				BSTR desc;
				pEI->GetDescription(&desc);
				char buff [80];
				WideCharToMultiByte(CP_ACP, NULL, desc, -1, buff, 
									80, NULL, NULL);
				cout << buff << endl;
				SysFreeString(desc);
				pEI->Release();
			}	
		}
		pISER->Release();
	}
	pInterface->Release();	
}
