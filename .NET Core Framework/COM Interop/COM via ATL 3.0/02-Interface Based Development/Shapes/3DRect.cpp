// 3DRect.cpp: implementation of the C3DRect class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "3DRect.h"
#include <iostream.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

C3DRect::C3DRect()
{
	cout << "Created a 3DRect" << endl;
}

C3DRect::~C3DRect()
{
	cout << "Destroyed a 3DRect" << endl;
}

// IDraw
void C3DRect::Draw()
{
	cout << "C3DRect::Draw" << endl;
}

// IShapeEdit
void C3DRect::Fill (FILLTYPE fType)
{
	char* FillString[] = {"Hatch", "Solid", "Polka dot"};
	cout << "Filling a 3D Rect as: " << FillString[fType] << endl;
}

void C3DRect::Inverse()
{
	cout << "Inverting a 3D Rect" << endl;
}

void C3DRect::Stretch(int factor)
{
	cout << "Streching a 3D Rect by a factor of: " << factor << endl;
}
