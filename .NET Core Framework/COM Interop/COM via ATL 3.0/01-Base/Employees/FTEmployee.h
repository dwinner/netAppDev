// FTEmployee.h: interface for the CFTEmployee class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_FTEMPLOYEE_H__961B0797_CE6C_11D2_AAED_00A0C9312D57__INCLUDED_)
#define AFX_FTEMPLOYEE_H__961B0797_CE6C_11D2_AAED_00A0C9312D57__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "Employee.h"

class CFTEmployee : public CEmployee  
{
public:
	CFTEmployee();
	CFTEmployee(int ID, char* name, float startingPay);
	virtual ~CFTEmployee();

	// Inherited pure virtual functions.  FTEmp must
	// specify an implementation of these methods,
	// or else it is also abstract!
	virtual  char* DisplayClassType(EMP_TYPE* eType);
	virtual float GetPay();

};

#endif // !defined(AFX_FTEMPLOYEE_H__961B0797_CE6C_11D2_AAED_00A0C9312D57__INCLUDED_)
