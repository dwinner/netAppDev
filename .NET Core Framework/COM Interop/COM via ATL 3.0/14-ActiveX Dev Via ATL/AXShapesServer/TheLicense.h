// TheLicense.h: interface for the CTheLicense class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_THELICENSE_H__E1A461D6_741A_11D3_B92E_0020781238D4__INCLUDED_)
#define AFX_THELICENSE_H__E1A461D6_741A_11D3_B92E_0020781238D4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CTheLicense  
{
public:
	CTheLicense();
	virtual ~CTheLicense();

	static BOOL VerifyLicenseKey(BSTR bstr)
	{
		return wcscmp(bstr, L"YouGottaHaveThis") == 0;
	}

	static BOOL GetLicenseKey(DWORD reserved, BSTR* pBSTR)
	{
		*pBSTR = SysAllocString(OLESTR("YouGottaHaveThis"));
		return TRUE;
	}

	static BOOL IsLicenseValid()
	{
		DWORD result;
		result = GetFileAttributes("C:\\ATL\\Labs\\Chapter 14\\AXShapesServer\\Test.lic") == (DWORD)-1;
		if(!result)
		{
			MessageBox(NULL, "All systems go", "Creating object", 
					   MB_OK | MB_SETFOREGROUND);
			return TRUE;
		}
		else
		{
			MessageBox(NULL, "Uh oh", "Can't make object", 
					   MB_OK | MB_SETFOREGROUND);
			return FALSE;
		}
	}
};

#endif // !defined(AFX_THELICENSE_H__E1A461D6_741A_11D3_B92E_0020781238D4__INCLUDED_)
