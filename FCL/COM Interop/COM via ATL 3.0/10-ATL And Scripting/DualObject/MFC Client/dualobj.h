// Machine generated IDispatch wrapper class(es) created with ClassWizard
/////////////////////////////////////////////////////////////////////////////
// ISquiggle wrapper class

class ISquiggle : public COleDispatchDriver
{
public:
	ISquiggle() {}		// Calls COleDispatchDriver default constructor
	ISquiggle(LPDISPATCH pDispatch) : COleDispatchDriver(pDispatch) {}
	ISquiggle(const ISquiggle& dispatchSrc) : COleDispatchDriver(dispatchSrc) {}

// Attributes
public:

// Operations
public:
	void DrawASquiggle();
	void FlipASquiggle();
	void EraseASquiggle();
};
