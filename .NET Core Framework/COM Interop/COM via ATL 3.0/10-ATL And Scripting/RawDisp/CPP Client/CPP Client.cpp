// CPP Client.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include <iostream.h>

int main(int argc, char* argv[])
{
	CoInitialize(0);

	IDispatch* pDisp = NULL;
	CLSID clsid;
	DISPID dispid;

	CLSIDFromProgID(OLESTR("RawDisp.CoSquiggle"),&clsid);
	LPOLESTR str = OLESTR("FlipASquiggle");
	
	// Create the CoSquiggle.
	CoCreateInstance(clsid, NULL, 
					 CLSCTX_SERVER, IID_IDispatch, (void**)&pDisp);
	
	// See if the object supports a method named 'DrawSquiggle'
	pDisp->GetIDsOfNames(IID_NULL, &str,1, 
						 GetUserDefaultLCID(), &dispid);
	
	// Call Invoke. We have no parameters, but must still 
	// supply an DISPPARAMS structure.
	DISPPARAMS params = {0, 0, 0, 0};
	pDisp->Invoke(dispid, IID_NULL, LOCALE_SYSTEM_DEFAULT, 
				  DISPATCH_METHOD, &params, NULL, NULL, NULL);

	// See ya!
	pDisp->Release();

	CoUninitialize();
	return 0;
}

