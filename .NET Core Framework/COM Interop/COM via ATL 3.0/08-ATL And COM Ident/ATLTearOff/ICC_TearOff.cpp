#include "stdafx.h"
#include "icc_tearoff.h"

// annoyingly so, we need to include the header of the
// owner in our IMPL file or else the compiler
// cannot see the public sector of the owner.
#include "atlcocar.h"

// Recall that CComTearOffObjectBase maintains a pointer to 
// the owner class (m_pOwner)
// Use this when you need to access data or methods
// during your interface impl.

STDMETHODIMP CCreateCar_TearOff::SetMaxSpeed(int maxSp)
{
	if(maxSp < MAX_SPEED)
		m_pOwner->m_maxSpeed = maxSp;
	
	return S_OK;
}

STDMETHODIMP CCreateCar_TearOff::SetPetName(BSTR petName)
{
	return m_pOwner->SetPetName(petName);
}