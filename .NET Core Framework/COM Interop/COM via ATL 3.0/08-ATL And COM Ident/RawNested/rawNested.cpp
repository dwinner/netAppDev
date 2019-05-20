
#include "rawnested.h"
#include "cohexfactory.h"
#include "rawnested_i.c"

ULONG g_lockCount = 0;
ULONG g_objCount = 0;

STDAPI DllCanUnloadNow()
{	
	if(g_lockCount == 0 && g_objCount == 0)
	{
		MessageBox(NULL, "DllCanUnloadNow", "Server Dying", MB_OK | MB_SETFOREGROUND);
		return S_OK;		// Unload me.	
	}
	else
		return S_FALSE;		// Keep me alive.
}


STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, void** ppv)
{

	HRESULT hr;
	CoHexFactory *pCFact = NULL;
	
	// We only know how to make CoHexagon objects in this house.
	if(rclsid == CLSID_CoHexagon)
	{	
		// They want a CoHexFactory
		pCFact = new CoHexFactory;	

		// Go get the interface from the CoHexFactory
		hr = pCFact -> QueryInterface(riid, ppv);
		if(FAILED(hr))
			delete pCFact;
	}
	else 
		return CLASS_E_CLASSNOTAVAILABLE;
	
	return hr;
}