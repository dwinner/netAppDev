// CPP Client.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "atlshapesserver.h"
#include "atlshapesserver_i.c"

int main(int argc, char* argv[])
{
	CoInitialize(NULL);
	
	IDraw* pDraw;
	CoCreateInstance(CLSID_CoHexagon, NULL, CLSCTX_SERVER, IID_IDraw, (void**)&pDraw);
	pDraw->Draw();

	CoUninitialize();
	return 0;
}
