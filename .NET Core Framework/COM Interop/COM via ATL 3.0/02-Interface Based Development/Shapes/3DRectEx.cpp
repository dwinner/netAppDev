// 3DRectEx.cpp: implementation of the C3DRectEx class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "3DRectEx.h"
#include <iostream.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

C3DRectEx::C3DRectEx()
{
	cout << "Created a C3DRectEx" << endl;
}

C3DRectEx::~C3DRectEx()
{
	cout << "Destroyed a C3DRectEx" << endl;
}


void C3DRectEx::Draw()
{
	cout << "C3DRectEx::Draw" << endl;
}

void C3DRectEx::DrawToMemory()
{
	cout << "C3DRectEx::DrawToMemory" << endl;
}

void C3DRectEx::DrawToPrinter()
{
	cout << "C3DRectEx::DrawToPrinter" << endl;
}

void C3DRectEx::DrawToMetaFile()
{
	cout << "C3DRectEx::DrawToMetaFile" << endl;
}
