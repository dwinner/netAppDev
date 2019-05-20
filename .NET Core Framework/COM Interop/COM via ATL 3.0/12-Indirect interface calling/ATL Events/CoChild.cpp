// CoStats.cpp : Implementation of CCoChild
#include "stdafx.h"
#include "ATLEvents.h"
#include "CoChild.h"

#include <stdlib.h>		// for random number.
#include <time.h>		// for seeding number.

/////////////////////////////////////////////////////////////////////////////
// CCoChild


STDMETHODIMP CCoChild::get_Name(BSTR *pVal)
{
	// return the name.
	*pVal = m_bstrName.Copy(); 

	return S_OK;
}

STDMETHODIMP CCoChild::put_Name(BSTR newVal)
{
	// Set new value.
	m_bstrName = newVal;

	return S_OK;
}

STDMETHODIMP CCoChild::get_Age(short *pVal)
{
	// Hand out age.
	*pVal = m_Age;

	return S_OK;
}

STDMETHODIMP CCoChild::put_Age(short newVal)
{
	// Set the new age.
	m_Age = newVal;

	return S_OK;
}

STDMETHODIMP CCoChild::AskChildQuestion()
{
	// Go get a new message.
	GetAMessage();
	// Fire the message.
	Fire_ChildSays(m_bstrMessage);

	return S_OK;
}

void CCoChild::GetAMessage()
{
	// Here are the possible messages.
	CComBSTR babyMsg[3] = {L"MommaDatta", 
						   L"Why sky is blue?" , 
						   L"Poppa is yucks"};

	CComBSTR youngMsg[3] = {L"I love my parents", 
							L"Can I have some candy please?" , 
							L"Read me a bedtime story"};

	CComBSTR teenMsg[3] = {L"You don't know what it is like to be ME!", 
						   L"I want an eyebrow ring" , 
						   L"My world is falling APART"};

	CComBSTR adultMsg[3] = {L"I love COM", 
						    L"I want more COM" , 
						    L"Life is COM.  I love VARIANTS."};

	// Now generate a random number.
   srand( (unsigned)clock() );   
   
   int item = rand() % 3;
	
	// How old is the child?
	if (m_Age >= 1 && m_Age <= 3)
		m_bstrMessage = babyMsg[item];

	else if (m_Age >= 4 && m_Age <= 12)
		m_bstrMessage = youngMsg[item];

	else if (m_Age >= 13 && m_Age <= 21)
		m_bstrMessage = teenMsg[item];

	else if (m_Age >= 22)
		m_bstrMessage = adultMsg[item];

}
