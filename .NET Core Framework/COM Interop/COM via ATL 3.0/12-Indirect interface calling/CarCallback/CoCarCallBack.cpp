// CoCar.cpp: implementation of the CoCar class.
//
//////////////////////////////////////////////////////////////////////

#include "CoCarCallBack.h"
#include "carcallback_i.c"
#include <stdio.h> 
extern DWORD g_objCount;

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CoCarCallBack::CoCarCallBack() : m_refCount(0), m_currSpeed(0), m_maxSpeed(0),
								 m_ClientsImpl(NULL)
{
	m_petName = SysAllocString(OLESTR(""));
	++g_objCount;
}

CoCarCallBack::~CoCarCallBack()
{
	--g_objCount;
	if(m_petName)
		SysFreeString(m_petName);
}


// IUnknown
STDMETHODIMP CoCarCallBack::QueryInterface(REFIID riid, void** pIFace)
{
	// Which aspect of me do they want?
	if(riid == IID_IUnknown)
	{
		*pIFace = (IUnknown*)(IEngine*)this;
	}
	
	else if(riid == IID_IEngine)
	{
		*pIFace = (IEngine*)this;
	}
	
	else if(riid == IID_IStats)
	{
		*pIFace = (IStats*)this;
	}
	
	else if(riid == IID_ICreateCar)
	{
		*pIFace = (ICreateCar*)this;
	}
	else if (riid == IID_IEstablishCommunications)
	{
		*pIFace = (IEstablishCommunications*)this;
	}
	else
	{
		*pIFace = NULL;
		return E_NOINTERFACE;
	}

	((IUnknown*)(*pIFace))->AddRef();
	return S_OK;
}

STDMETHODIMP_(DWORD) CoCarCallBack::AddRef()
{
	++m_refCount;
	return m_refCount;
}

STDMETHODIMP_(DWORD) CoCarCallBack::Release()
{
	if(--m_refCount == 0)
	{
		delete this;
		return 0;
	}
	else
		return m_refCount;
}

// IEngine
STDMETHODIMP CoCarCallBack::SpeedUp()
{
	m_currSpeed += 10;
	
	// Only send events using a non-null pointer & if
	// speed logic is just so.
	if((m_maxSpeed - m_currSpeed) == 10 && (m_ClientsImpl != NULL))
	{
		// Fire the event!
		m_ClientsImpl->AboutToExplode();
	}
	
	if((m_currSpeed >= m_maxSpeed) && (m_ClientsImpl != NULL))
	{
		// Fire the event!
		m_ClientsImpl->Exploded();
	}	
	
	return S_OK;
}

STDMETHODIMP CoCarCallBack::GetMaxSpeed(int* maxSpeed)
{
	*maxSpeed = m_maxSpeed;
	return S_OK;
}

STDMETHODIMP CoCarCallBack::GetCurSpeed(int* curSpeed)
{
	*curSpeed = m_currSpeed;
	return S_OK;
}


// IStats
STDMETHODIMP CoCarCallBack::DisplayStats()
{
	// Need to transfer a BSTR to a char array.
	char buff[MAX_NAME_LENGTH];
	WideCharToMultiByte(CP_ACP, NULL, m_petName, -1, buff, 
						MAX_NAME_LENGTH, NULL, NULL);

	MessageBox(NULL, buff, "Pet Name",MB_OK|MB_SETFOREGROUND);
	memset(buff, 0, sizeof(buff));
	sprintf(buff, "%d", m_maxSpeed);
	MessageBox(NULL, buff, "Max Speed", MB_OK|MB_SETFOREGROUND);
	return S_OK;
}

STDMETHODIMP CoCarCallBack::GetPetName(BSTR* petName)
{
	*petName = SysAllocString(m_petName);
	return S_OK;
}

// ICreateCar
STDMETHODIMP CoCarCallBack::SetPetName(BSTR petName)
{
	SysReAllocString(&m_petName, petName);
	return S_OK;
}

STDMETHODIMP CoCarCallBack::SetMaxSpeed(int maxSp)
{
	if(maxSp < MAX_SPEED)
		m_maxSpeed = maxSp;
	return S_OK;
}

// IEstablishCommunications
STDMETHODIMP CoCarCallBack::Advise(IEngineEvents *pCallMe)
{	
	// Store the client sink for future use.
	m_ClientsImpl = pCallMe;
	m_ClientsImpl->AddRef();
	return S_OK;
}

STDMETHODIMP CoCarCallBack::Unadvise()
{
	// Set to null.
	m_ClientsImpl->Release();
	m_ClientsImpl = NULL;
	return S_OK;
}
