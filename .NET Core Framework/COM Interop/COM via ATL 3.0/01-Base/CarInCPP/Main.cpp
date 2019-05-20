
#include <iostream.h>
#include "ccar.h"

void main(void)
{

	cout << "***********************************" << endl;
	cout << "The Amazing Car Application via CPP"<< endl;
	cout << "***********************************"<< endl;

	// Create a car.
	CCar myCar;
	myCar.CreateACar();

	// Show stats.
	myCar.DisplayCarStats();

	// Speed up until engine block cracks
	while(myCar.GetCurrSpeed() <= myCar.GetMaxSpeed())
		myCar.SpeedUp();
	
	cout << myCar.GetPetName() << " has blown up!  Lead foot!" << endl;

}




