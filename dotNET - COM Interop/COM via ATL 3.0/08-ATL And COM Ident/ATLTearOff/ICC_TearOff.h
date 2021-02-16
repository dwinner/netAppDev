#include "atltearoff.h"

// Need to forward declare to keep the compiler hushed up.
class CATLCoCar;

#ifndef _ICC_TearOff 
#define _ICC_TearOff 

class ATL_NO_VTABLE CCreateCar_TearOff : 
	public CComTearOffObjectBase<CATLCoCar, CComSingleThreadModel>, 
	public ICreateCar
{
public:
	// There is no requirement that a tear-off
	// worries about FinalConstruct and FinalRelease.
	// This is just for diagnostics...
	HRESULT FinalConstruct()
	{	
		MessageBox(NULL, "Created tear-off", "Notice!", 
			       MB_OK | MB_SETFOREGROUND);
		return S_OK;	
	}
		
	void FinalRelease() 
	{   
		MessageBox(NULL, "The tear-off is destroyed", "Notice!", 
				   MB_OK | MB_SETFOREGROUND);
	}

	// Methods of outdated ICreateCar.
	STDMETHOD(SetMaxSpeed)(int maxSp);
	STDMETHOD(SetPetName)(BSTR petName);

	// Recall that a tear offs COM map 
	// adds an entry for the interface it 
	// supports.
	BEGIN_COM_MAP(CCreateCar_TearOff)
		COM_INTERFACE_ENTRY(ICreateCar)
	END_COM_MAP()
};

#endif // _ICC_TearOff