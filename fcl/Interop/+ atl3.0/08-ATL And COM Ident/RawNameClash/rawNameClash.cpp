#include "rawnameclash.h"

#include "co3dhexfactory.h"
#include "rawnameclash_i.c"
ULONG g_lockCount = 0;
ULONG g_objCount = 0;

STDAPI DllCanUnloadNow()
{
	
	if(g_lockCount == 0 && g_objCount == 0)
	{
		MessageBox(NULL, "Server Dead", "Message", MB_OK | MB_SETFOREGROUND);
		return S_OK;		// Unload me.	
	}
	else
		return S_FALSE;	// Keep me alive.
}


STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, void** ppv)
{

	HRESULT hr;
	Co3DHexFactory *p3DFact = NULL;

	// We only know how to make Co3DHexagon objects in this house.
	if (rclsid == CLSID_Co3DHexagon)
	{
		// They want a Co3DHexFactory
		p3DFact = new Co3DHexFactory;	

		// Go get the interface from the Co3DHexFactory
		hr = p3DFact -> QueryInterface(riid, ppv);

		if(FAILED(hr))
			delete p3DFact;
	}
	else 
		return CLASS_E_CLASSNOTAVAILABLE;
	
	return hr;
}