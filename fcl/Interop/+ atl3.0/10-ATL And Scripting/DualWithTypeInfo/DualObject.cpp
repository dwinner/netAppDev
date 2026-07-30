#include "CoDualSquiggleClassFactory.h"

ULONG g_lockCount = 0;
ULONG g_objCount = 0;

STDAPI DllCanUnloadNow()
{
	
	if(g_lockCount == 0 && g_objCount == 0)
	{
		MessageBox(NULL, "Server dead", "Message", MB_OK | MB_SETFOREGROUND);
		return S_OK;		// Unload me.	
	}
	else
		return S_FALSE;	// Keep me alive.
}


STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, void** ppv)
{
	HRESULT hr;
	CoDualSquiggleClassFactory *pCFact = NULL;

	// They want a CoSquiggleClassFactory
	pCFact = new CoDualSquiggleClassFactory;	

	// Go get the interface from the CoSquiggleClassFactory
	hr = pCFact -> QueryInterface(riid, ppv);

	if(FAILED(hr))
		delete pCFact;
	
	return hr;
}
