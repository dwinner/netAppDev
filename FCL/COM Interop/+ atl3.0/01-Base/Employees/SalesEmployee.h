// SalesEmployee.h: interface for the CSalesEmployee class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_SALESEMPLOYEE_H__961B0796_CE6C_11D2_AAED_00A0C9312D57__INCLUDED_)
#define AFX_SALESEMPLOYEE_H__961B0796_CE6C_11D2_AAED_00A0C9312D57__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "Employee.h"

class CSalesEmployee : public CEmployee  
{
public:
	CSalesEmployee();
	CSalesEmployee(int ID, char* name, float startingPay, int sales);
	virtual ~CSalesEmployee();


	// Inherited virtual functions
	virtual char* DisplayClassType(EMP_TYPE* eType);
	virtual float GetPay();

	// Sales specific functions.
	int GetNumberOfSales() { return m_numberOfSales;}
	void SetNumberOfSales(int numb) { m_numberOfSales = numb;}

private:
	int m_numberOfSales;

};

#endif // !defined(AFX_SALESEMPLOYEE_H__961B0796_CE6C_11D2_AAED_00A0C9312D57__INCLUDED_)
