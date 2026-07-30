// CoCarEXE.cpp : Defines the entry point for the application.
//


/*  This file defines the component housing for
 *  a local version of CoCar.
 *  EXE COM servers do not export DllGetClassObject()
 *  or DllCanUnloadNow().  Therefore
 *  we must do the following in every EXE server:
 *
 *  1)  Use the COM library to register our class objects.
 *	2)  Monitor for the final relase and post a
 *		WM_QUIT message to exit WinMain.
 */

#include "stdafx.h"
#include "cocarEXEtypeinfo_i.c"		// MIDL file
#include "cocarclassfactory.h"		// MIDL file
#include <string.h>


// Our global counter for locks & active objects.
DWORD g_allLocks;


int APIENTRY WinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPSTR     lpCmdLine,
                     int       nCmdShow)
{
	// Init COM.
	CoInitialize(NULL); 	

	// Let's register the type lib.
	// to get the 'free' type library marsahler.
	ITypeLib* pTLib = NULL;
	LoadTypeLibEx(	L"CoCarEXETypeInfo.tlb", 
					REGKIND_REGISTER, &pTLib);
	
	pTLib->Release();
	
	// Let's see if we were started by SCM.
	if(strstr(lpCmdLine, "/Embedding") || strstr(lpCmdLine, "-Embedding"))
	{


		// Create the Car class object.
		CoCarClassFactory CarClassFactory;

		// Register the Car Factory.
		DWORD regID = 0;
		CoRegisterClassObject(CLSID_CoCar, 
							  (IClassFactory*)&CarClassFactory, 
							  CLSCTX_LOCAL_SERVER, 
							  REGCLS_MULTIPLEUSE, &regID);

		// Now just run until a quit message is sent,
		// in responce to the final release.
		MSG ms;
		while(GetMessage(&ms, 0, 0, 0))
		{
			TranslateMessage(&ms);
			DispatchMessage(&ms);
		}

		// All done, so remove class object.
		CoRevokeClassObject(regID);	

	}

	// Terminate COM.
	CoUninitialize();	
	MessageBox(NULL, "Server is dying", "EXE Message!", MB_OK | MB_SETFOREGROUND);
	return 0;
}



