// CPPClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>

#include "atlvehicals_i.c"
#include "atlvehicals.h"

int main(int argc, char* argv[])
{
	CoInitialize(NULL);
	IDragRace* pDragRace[3] = {0};
	HRESULT hr;
	// Make some cars.
	hr = CoCreateInstance(CLSID_CoHotRod, NULL, CLSCTX_SERVER, IID_IDragRace, (void**)&(pDragRace[0]));
	hr = CoCreateInstance(CLSID_CoMiniVan, NULL, CLSCTX_SERVER, IID_IDragRace, (void**)&(pDragRace[1]));
	hr = CoCreateInstance(CLSID_CoJeep, NULL, CLSCTX_SERVER, IID_IDragRace, (void**)&(pDragRace[2]));

	for(int i = 0; i < 3; i++)
		pDragRace[i]->DragRace();

	for(i = 0; i < 3; i++)
		pDragRace[i]->Release();

	CoUninitialize();
	return 0;
}
