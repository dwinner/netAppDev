// ATLCoCar.cpp : Implementation of CATLCoCar
#include "stdafx.h"
#include "ATLTearOff.h"
#include "ATLCoCar.h"

/////////////////////////////////////////////////////////////////////////////
// CATLCoCar

STDMETHODIMP CATLCoCar::SetPetName(BSTR petName)
{
	// operator= is a good thing!
	m_petName = petName;

	return S_OK;
}

STDMETHODIMP CATLCoCar::SetMaxSpeed(int maxSp)
{
	// Assume MAX_SPEED is a class level const, 
	// i.e. const int MAX_SPEED = 500;
	if(maxSp < MAX_SPEED)
		m_maxSpeed = maxSp;
	return S_OK;
}

STDMETHODIMP CATLCoCar::DisplayStats()
{
	// Use conversion macros.
	USES_CONVERSION;

	MessageBox(NULL, W2A(m_petName), "Pet Name",
			   MB_OK|MB_SETFOREGROUND);

	char buff[MAX_NAME_LENGTH];
	memset(buff, 0, sizeof(buff));
	sprintf(buff, "%d", m_maxSpeed);
	
	MessageBox(NULL, buff, "Max Speed", 
			   MB_OK|MB_SETFOREGROUND);

	return S_OK;
}

STDMETHODIMP CATLCoCar::GetPetName(BSTR *petName)
{
	// Copy().
	*petName = m_petName.Copy();

	return S_OK;
}

STDMETHODIMP CATLCoCar::SpeedUp()
{
	m_currSpeed += 10;
	return S_OK;
}

STDMETHODIMP CATLCoCar::GetMaxSpeed(int *maxSpeed)
{
	*maxSpeed = m_maxSpeed;
	return S_OK;
}

STDMETHODIMP CATLCoCar::GetCurSpeed(int *curSpeed)
{
	*curSpeed = m_currSpeed;	
	return S_OK;
}

STDMETHODIMP CATLCoCar::get_Name(BSTR *pVal)
{
	*pVal = m_ownerName.Copy();
	return S_OK;
}

STDMETHODIMP CATLCoCar::put_Name(BSTR newVal)
{
	m_ownerName = newVal;
	return S_OK;
}

STDMETHODIMP CATLCoCar::get_Address(BSTR *pVal)
{
	*pVal = m_ownerAddress.Copy();
	return S_OK;
}

STDMETHODIMP CATLCoCar::put_Address(BSTR newVal)
{
	m_ownerAddress = newVal;
	return S_OK;
}

STDMETHODIMP CATLCoCar::Create(BSTR ownerName, BSTR ownerAddress, BSTR petName, int maxSp)
{
	// This one call does it all.
	m_petName = petName;
	m_ownerName = ownerName;
	m_ownerAddress = ownerAddress;

	if(maxSp < MAX_SPEED)
		m_maxSpeed = maxSp;
	
	return S_OK;
}
