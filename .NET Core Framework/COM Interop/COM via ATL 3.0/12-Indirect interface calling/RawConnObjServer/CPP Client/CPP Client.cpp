// CPP Client.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include <ocidl.h>

#include "rawconnobjserver.h"
#include "rawconnobjserver_i.c"
#include "CoTheCallBack.h"

CCoSink g_sink;

const int MAX_CONN = 3;

int main(int argc, char* argv[])
{
	CoInitialize(NULL);
	IConnectionPointContainer* pICPC = NULL;
	CoCreateInstance(CLSID_CoSomeObject, NULL, CLSCTX_SERVER, IID_IConnectionPointContainer, (void**)&pICPC);

	// Find the connection point.
	IConnectionPoint* pCP = NULL;
	pICPC->FindConnectionPoint(IID__IOutBound, &pCP);

	// Make 3 connections to the same sink.
	ULONG cookie[MAX_CONN];
	for(int i = 0; i < MAX_CONN; i++)
		pCP->Advise(&g_sink, &cookie[i]);

	// Get ISomeInterface.
	ISomeInterface* pISomeIntf = NULL;
	pICPC->QueryInterface(IID_ISomeInterface, (void**)&pISomeIntf);

	// Trigger event.
	pISomeIntf->TriggerEvent();
	
	// Clean up.
	for(i = 0; i < MAX_CONN; i++)
		pCP->Unadvise(cookie[i]);

	pISomeIntf->Release();
	pICPC->Release();
	pCP->Release();

	CoUninitialize();
	return 0;
}
