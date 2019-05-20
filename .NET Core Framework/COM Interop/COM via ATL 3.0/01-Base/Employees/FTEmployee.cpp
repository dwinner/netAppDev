// FTEmployee.cpp: implementation of the CFTEmployee class.
//
//////////////////////////////////////////////////////////////////////

#include "FTEmployee.h"
#include <iostream.h>
#include <string.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CFTEmployee::CFTEmployee()
{
	// Base class inherited inits data.
}


CFTEmployee::CFTEmployee(int ID, char* name, float startingPay)
	:CEmployee(ID, name, startingPay)
{

}

CFTEmployee::~CFTEmployee()
{

}


// We return a string specifying we are a full time worker.
char* CFTEmployee::DisplayClassType(EMP_TYPE* eType)
{
	*eType = FT;
	return "I am a Full Timer worker";
}

float CFTEmployee::GetPay()
{
	// Calculate the pay.
	return m_currPay / 52;
}