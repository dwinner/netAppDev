/*  This file implements the minimal and complete 
 *	DLL exports for an in-process server.
 */
#include <windows.h>
#include "CoSomeObjectClassFactory.h"
#include "rawconnobjserver_i.c"

// Need these global data points to figure
// out when the DLL can get unloaded.
ULONG g_lockCount = 0;
ULONG g_objCount = 0;


// Called by SCM and CoFreeUnusedLibraries() to see
// if this DLL can be removed from memory.
STDAPI DllCanUnloadNow()
{
	if(g_lockCount == 0 && g_objCount == 0)
	{	
		MessageBox(NULL, "Server dead...", "Message", MB_OK | MB_SETFOREGROUND);
		return S_OK;		// Unload me.	
	}
	else
	{
		return S_FALSE;		// Keep me alive.
	}
}


// Called by SCM in responce to CoGetClassObject() or
// CoCreateInstance().  Creates a given class factory for
// the client based on the CLSID of the coclass.
STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, void** ppv)
{
	HRESULT hr;
	CoSomeObjectClassFactory *pCFact = NULL;
	
	// We only know how to make CoHexagon objects in this house.
	if(rclsid != CLSID_CoSomeObject)
		return CLASS_E_CLASSNOTAVAILABLE;
 
	pCFact = new CoSomeObjectClassFactory;	
	hr = pCFact -> QueryInterface(riid, ppv);

	if(FAILED(hr))
		delete pCFact;
	
	return hr;
}
