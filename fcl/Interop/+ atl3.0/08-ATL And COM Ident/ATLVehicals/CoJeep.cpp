// CoJeep.cpp : Implementation of CCoJeep
#include "stdafx.h"
#include "ATLVehicals.h"
#include "CoJeep.h"

/////////////////////////////////////////////////////////////////////////////
// CCoJeep


STDMETHODIMP CCoJeep::DragRace()
{
	// Slight insult.
	MessageBox(NULL, "Dude!  I'm goin' offroadin'.",
			   "Jeep message", MB_OK | MB_SETFOREGROUND);

	return S_OK;
}
