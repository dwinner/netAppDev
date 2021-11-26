#include "cocarclassfactory.h"
#include "carservertypeinfo_i.c"

ULONG g_lockCount = 0;
ULONG g_objCount = 0;

STDAPI DllCanUnloadNow()
{
	
	if(g_lockCount == 0 && g_objCount == 0)
	{
		MessageBox(NULL, "Server is dead", "Message!", MB_OK | MB_SETFOREGROUND);
		return S_OK;		// Unload me.	
	}
	else
		return S_FALSE;	// Keep me alive.
}


STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, void** ppv)
{
	HRESULT hr;
	CoCarClassFactory *pCFact = NULL;

	// We only know how to make CoHexagon objects in this house.
	if(rclsid != CLSID_CoCar)
		return CLASS_E_CLASSNOTAVAILABLE;
 
	// They want a CoCarClassFactory
	pCFact = new CoCarClassFactory;	

	// Go get the interface from the CoCarClassFactory
	hr = pCFact -> QueryInterface(riid, ppv);

	if(FAILED(hr))
		delete pCFact;
	
	return hr;
}