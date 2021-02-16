
#include "employee.h"
#include "FTemployee.h"
#include "Salesemployee.h"
#include <iostream.h>


// Upper limit in program.
const int MAX_EMPS = 4;

void main(void)
{
	// A cookie to see if we have a salesperson.
	EMP_TYPE e;

	// Make an array of Employee pointers.
	CEmployee* theStaff[MAX_EMPS];
	theStaff[0] = new CFTEmployee(30, "Fred", 30000);
	theStaff[1] = new CSalesEmployee(40, "Mary", 40000, 120);
	theStaff[2] = new CFTEmployee(15, "JoJo", 11000);
	theStaff[3] = new CSalesEmployee(1, "Jimmy Jones", 100000, 19);


	// Now go through each member in the array and 
	// pull out the stats.
	for(int i = 0; i < MAX_EMPS; i++)
	{
		// Which class is here?
		cout << theStaff[i]->DisplayClassType(&e) << endl;
		
		// Get name.
		cout << "Name is: " << theStaff[i]->GetName() << endl;

		// Get ID.
		cout << "ID is: " << theStaff[i]->GetID() << endl;
		
		// Get pay.
		cout << "Pay is: " << theStaff[i]->GetPay() << " a paycheck before taxes." << endl;

		// Let's see if we have a salesperson.
		if(e == SP)
		{
			cout << "-->My Sales are " 
				 << ((CSalesEmployee*)theStaff[i])->GetNumberOfSales() 
				 << endl << flush;
		}

		// Just to make it look nice.
		cout << endl;	
		cout << "*************************" << endl;
	}

	// clean up.
	delete [] *theStaff;
}

