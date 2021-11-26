// CCar.h: interface for the CCar class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CCAR_H__ECEE7FD6_C488_11D2_B8C3_0020781238D4__INCLUDED_)
#define AFX_CCAR_H__ECEE7FD6_C488_11D2_B8C3_0020781238D4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

const	int MAX_LENGTH = 100;
const	int MAX_SPEED = 500;

#include <iostream.h>
#include <stdio.h>
#include <string.h>

class CCar  
{
public:
	CCar();
	virtual ~CCar();

	// Public interface to the class.
	void	DisplayCarStats();
	void	SpeedUp();
	int		GetCurrSpeed() { return m_currSpeed;}
	int		GetMaxSpeed() { return m_maxSpeed;}
	char*	GetPetName() { return m_petName;}
	void	CreateACar();

private:
	// Instance data.
	char	m_petName[MAX_LENGTH];
	int		m_maxSpeed;
	int		m_currSpeed;

};

#endif // !defined(AFX_CCAR_H__ECEE7FD6_C488_11D2_B8C3_0020781238D4__INCLUDED_)
