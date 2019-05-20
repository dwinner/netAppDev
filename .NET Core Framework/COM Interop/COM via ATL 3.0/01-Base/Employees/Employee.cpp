// Employee.cpp: implementation of the CEmployee class.
//
//////////////////////////////////////////////////////////////////////

#include "Employee.h"
#include "string.h"

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CEmployee::CEmployee()
{
	// Set all state data to default values.
	m_currPay = 0.0F;
	strcpy(m_name, "");
	m_ID = 0;
}

CEmployee::CEmployee(int ID, char* name, float startingPay)
{
	m_ID = ID;
	strcpy(m_name, name);
	m_currPay = startingPay;
}

CEmployee::~CEmployee()
{
	// Does nothing...
}

void CEmployee::IncreasePay(float amt)
{
	// Add amount to current pay.
	m_currPay += amt;
}
