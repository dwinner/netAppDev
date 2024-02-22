// 3DRectEx.h: interface for the C3DRectEx class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_3DRECTEX_H__C058B644_D4ED_11D2_B8CF_0020781238D4__INCLUDED_)
#define AFX_3DRECTEX_H__C058B644_D4ED_11D2_B8CF_0020781238D4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "interfaces.h"

class C3DRectEx : public IDraw3  // Brings in layout of IDraw2 & IDraw
{
public:
	C3DRectEx();
	virtual ~C3DRectEx();
	
	// IDraw
	virtual void Draw();

	// IDraw2
	virtual	void DrawToMemory();
	virtual void DrawToPrinter();

	// IDraw3
	virtual void DrawToMetaFile();
};

#endif // !defined(AFX_3DRECTEX_H__C058B644_D4ED_11D2_B8CF_0020781238D4__INCLUDED_)
