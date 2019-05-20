/*  This file implements the minimal and complete 
 *	DLL exports for an in-process server.
 */

#include "coaholderfactory.h"
#include "ageenum.h"
#include "ageenum_i.c"

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
		MessageBox(NULL, "DllCanUnloadNow", "Message!", MB_OK | MB_SETFOREGROUND);
		return S_OK;		// Unload me.
	}
	else
		return S_FALSE;		// Keep me alive.
}


// Called by SCM in responce to CoGetClassObject() or
// CoCreateInstance().  Creates a given class factory for
// the client based on the CLSID of the coclass.
STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, void** ppv)
{
	HRESULT hr;
	CoAHolderFactory *pCFact = NULL;
	
	if(rclsid != CLSID_CoRawAgeHolder)
		return CLASS_E_CLASSNOTAVAILABLE;
 
	// They want a CoCarClassFactory
	pCFact = new CoAHolderFactory;	

	// Go get the interface from the CoCarClassFactory
	hr = pCFact -> QueryInterface(riid, ppv);

	if(FAILED(hr))
		delete pCFact;
	
	return hr;
}