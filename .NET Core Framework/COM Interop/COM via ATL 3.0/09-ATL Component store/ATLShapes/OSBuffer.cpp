// OSBuffer.cpp : Implementation of COSBuffer
#include "stdafx.h"
#include "ATLShapes.h"
#include "OSBuffer.h"

/////////////////////////////////////////////////////////////////////////////
// COSBuffer


STDMETHODIMP COSBuffer::RenderToMemory()
{
	// Simulate bitblitting to memory.
	MessageBox(NULL, "I am rendering to the screen", "Off Screen Buffer Says...",
			   MB_OK | MB_SETFOREGROUND);
	return S_OK;
}
