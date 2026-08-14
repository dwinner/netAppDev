#include "atlnameclash.h"

// This IMPL class wraps the offending 
// Draw method of the I3DRender interface.
struct I3DRenderImpl : I3DRender
{
	STDMETHODIMP Draw()
	{
		return Draw3DRect();
	}
	STDMETHOD(Draw3DRect)() = 0;
};

// This IMPL class wraps the offending 
// Draw method of the IDraw interface
// using C++ templates.
// This is different that the lab, but 
// shows an alternative.
template<class owner>
struct IDrawImpl : IDraw
{
	STDMETHODIMP Draw()
	{
		return ((owner*)(this)) -> Draw2DRect();
	}
};

