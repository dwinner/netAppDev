// CCar.cpp: implementation of the CCar class.
//
//////////////////////////////////////////////////////////////////////

#include "CCar.h"

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////


CCar::CCar() :m_currSpeed(0), m_maxSpeed(0)
{
	strcpy(m_petName, "");
}

CCar::~CCar()
{
	// Nothing special...
}


void CCar::DisplayCarStats()
{
	// Show what we got.
	cout << "***********************************" << endl;
	cout << "PetName is: " << m_petName << endl;
	cout << "Max Speed is: " << m_maxSpeed << endl;
	cout << "***********************************" << endl << endl;
}

void CCar::CreateACar()
{
	char	buffer[MAX_LENGTH];
	int		spd = 0;

	cout << "Please enter a pet-name for your car: " << flush;

	gets(buffer);
	strcpy(m_petName, buffer);

	// Be sure speed isn't beyond reality...
	do 
	{		
		cout << endl << "Enter the max speed of this car: " << flush;
		cin >> spd;
		cout << endl;
	}while(spd > MAX_SPEED);

	m_maxSpeed = spd;
}

void CCar::SpeedUp()
{
	if(m_currSpeed <= m_maxSpeed)
	{
		m_currSpeed = m_currSpeed + 10;
		cout << "Speed is: " << m_currSpeed << endl;
	}
}
