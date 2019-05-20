// SalesEmployee.cpp: implementation of the CSalesEmployee class.
//
//////////////////////////////////////////////////////////////////////

#include "SalesEmployee.h"
#include <iostream.h>
#include <string.h>
//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CSalesEmployee::CSalesEmployee()
{
	m_numberOfSales = 0;
}

CSalesEmployee::~CSalesEmployee()
{

}


CSalesEmployee::CSalesEmployee(int ID, char* name, float startingPay, int sales)
{
	// See CFTEmployee for member init syntax.
	// Here we are making simple assignments.
	m_ID = ID;
	strcpy(m_name, name);
	m_currPay = startingPay;
	m_numberOfSales = sales;
}

char* CSalesEmployee::DisplayClassType(EMP_TYPE* eType)
{
	*eType = SP;
	return "I am a Sales Person";
}

float CSalesEmployee::GetPay()
{
	// Our pay is a result of curr pay + number of sales.
	return (m_currPay / 52) + (m_numberOfSales * 5);
}