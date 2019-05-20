// CoCar.h: interface for the CoCar class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_COCAR_H__86A1A88D_D36B_11D2_B8CF_0020781238D4__INCLUDED_)
#define AFX_COCAR_H__86A1A88D_D36B_11D2_B8CF_0020781238D4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

const int MAX_SPEED = 500;
const int MAX_NAME_LENGTH = 20;

#include "carcallback.h"


class CoCarCallBack : 
	public IEngine, 
	public ICreateCar, 
	public IStats,
	public IEstablishCommunications

{
public:
	CoCarCallBack();
	virtual ~CoCarCallBack();
 
	// IUnknown
	STDMETHODIMP QueryInterface(REFIID riid, void** pIFace);
	STDMETHODIMP_(DWORD)AddRef();
	STDMETHODIMP_(DWORD)Release();

	// IEngine
	STDMETHODIMP SpeedUp();
	STDMETHODIMP GetMaxSpeed(int* maxSpeed);
	STDMETHODIMP GetCurSpeed(int* curSpeed);
	
	// IStats
	STDMETHODIMP DisplayStats();
	STDMETHODIMP GetPetName(BSTR* petName);

	// ICreateCar
	STDMETHODIMP SetPetName(BSTR petName);
	STDMETHODIMP SetMaxSpeed(int maxSp);

	// IEstablishCommunications
	STDMETHODIMP Advise(IEngineEvents *pCallMe);
	STDMETHODIMP Unadvise();

private:
	DWORD	m_refCount;
	BSTR	m_petName;
	int		m_maxSpeed;
	int		m_currSpeed;

	// Used to talk back to client.
	IEngineEvents* m_ClientsImpl;
};

#endif // !defined(AFX_COCAR_H__86A1A88D_D36B_11D2_B8CF_0020781238D4__INCLUDED_)
