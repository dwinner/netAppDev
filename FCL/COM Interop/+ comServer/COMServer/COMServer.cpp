// COMServer.cpp : Implementation of DLL Exports.


#include "stdafx.h"
#include "COMServer_i.h"
#include "dllmain.h"

using namespace ATL;

// Used to determine whether the DLL can be unloaded by OLE.
STDAPI DllCanUnloadNow(void)
{
   return _AtlModule.DllCanUnloadNow();
}

// Returns a class factory to create an object of the requested type.
STDAPI DllGetClassObject(_In_ REFCLSID rclsid, _In_ REFIID riid, _Outptr_ LPVOID* ppv)
{
   return _AtlModule.DllGetClassObject(rclsid, riid, ppv);
}

// DllRegisterServer - Adds entries to the system registry.
STDAPI DllRegisterServer(void)
{
   // registers object, typelib and all interfaces in typelib
   HRESULT hr = _AtlModule.DllRegisterServer();
   return hr;
}

// DllUnregisterServer - Removes entries from the system registry.
STDAPI DllUnregisterServer(void)
{
   HRESULT hr = _AtlModule.DllUnregisterServer();
   return hr;
}

// DllInstall - Adds/Removes entries to the system registry per user per machine.
STDAPI DllInstall(BOOL bInstall, _In_opt_  LPCWSTR pszCmdLine)
{
   HRESULT hr = E_FAIL;
   static const wchar_t szUserSwitch[] = L"user";

   if (pszCmdLine != nullptr)
   {
      if (_wcsnicmp(pszCmdLine, szUserSwitch, _countof(szUserSwitch)) == 0)
      {
         AtlSetPerUserRegistration(true);
      }
   }

   if (bInstall)
   {
      hr = DllRegisterServer();
      if (FAILED(hr))
      {
         DllUnregisterServer();
      }
   }
   else
   {
      hr = DllUnregisterServer();
   }

   return hr;
}
