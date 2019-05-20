// CoHotRod.cpp : Implementation of CCoHotRod
#include "stdafx.h"
#include "ATLVehicals.h"
#include "CoHotRod.h"

/////////////////////////////////////////////////////////////////////////////
// CCoHotRod


STDMETHODIMP CCoHotRod::DragRace()
{
	MessageBox(NULL, "Dude!  Done. This is a fast car.",
			   "Hot Rod message", MB_OK | MB_SETFOREGROUND);

	return S_OK;
}
