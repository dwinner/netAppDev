// CoMiniVan.cpp : Implementation of CCoMiniVan
#include "stdafx.h"
#include "ATLVehicals.h"
#include "CoMiniVan.h"

/////////////////////////////////////////////////////////////////////////////
// CCoMiniVan


STDMETHODIMP CCoMiniVan::CreateMiniVan(BSTR petName, int maxSpeed, 
									   BSTR ownerName, BSTR ownerAddress, 
									   int numberOfDoors)
{
	// Here we will turn around and pass the params
	// into the contained ATLcocar.
	m_pCreate-> Create(ownerName, ownerAddress, petName, maxSpeed);

	m_numberOfDoors = numberOfDoors;
	return S_OK;
}

// The IEngine info.
STDMETHODIMP CCoMiniVan::SpeedUp()
{
	// Speed up the contained car.
	return m_pEngine->SpeedUp();
}

STDMETHODIMP CCoMiniVan::GetMaxSpeed(int *maxSpeed)
{
	return m_pEngine->GetMaxSpeed(maxSpeed); 
}

STDMETHODIMP CCoMiniVan::GetCurSpeed(int *curSpeed)
{
	return m_pEngine->GetCurSpeed(curSpeed);
}

STDMETHODIMP CCoMiniVan::GetPetName(BSTR* petName)
{
	return m_pStats->GetPetName(petName);
}

STDMETHODIMP CCoMiniVan::DisplayStats()
{
	return m_pStats->DisplayStats();
}

STDMETHODIMP CCoMiniVan::DragRace()
{
	// Slight insult.
	MessageBox(NULL, "Dude, You have a minivan.  You can't drag race.",
			   "Minivan message", MB_OK | MB_SETFOREGROUND);

	return S_OK;
}

STDMETHODIMP CCoMiniVan::get_NumberOfDoors(short *pVal)
{
	// TODO: Add your implementation code here
	*pVal = m_numberOfDoors;
	return S_OK;
}
