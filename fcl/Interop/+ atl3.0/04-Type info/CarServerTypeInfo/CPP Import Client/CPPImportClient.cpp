// CPPImportClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream.h>

#import "C:\ATL\Labs\Chapter 04\CarServerTypeInfo\Debug\CarServerTypeInfo.tlb" no_namespace named_guids


int main(int argc, char* argv[])
{
	CoInitialize(NULL);
	
	cout << "*********************************" << endl;
	cout << "The Amazing CoCar Import Client"   << endl;
	cout << "*********************************" << endl;
	
	// Make some smart pointers
	ICreateCarPtr spCCar(__uuidof(CoCar));
	IEnginePtr spEng = spCCar;
	IStatsPtr spStats = spCCar;

	spCCar->SetPetName(L"Greased Lightin!");
	spCCar->SetMaxSpeed(50);

	spStats->DisplayStats();
	
	int curSp = 0;
	int maxSp = 0;
	
	maxSp = spEng->GetMaxSpeed();

	do	// Zoom!
	{	
		spEng->SpeedUp();
		curSp = spEng->GetCurSpeed();
		cout << "Speed is: " << curSp << endl;

	}while(curSp <= maxSp);

	// Blown up!
	_bstr_t bstrName;
	bstrName = spStats->GetPetName();
	cout << bstrName << " has blown up! Lead Foot!" << endl << endl;

	spCCar = NULL;
	spStats= NULL;
	spEng =  NULL;

	CoUninitialize();
	return 0;
}
