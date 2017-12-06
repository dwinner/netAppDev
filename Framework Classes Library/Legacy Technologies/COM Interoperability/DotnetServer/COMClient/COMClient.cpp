// COMClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#import "..\DotnetServer\bin\Debug\DotnetServer.tlb" named_guids

using namespace std;
using namespace DotnetServer;

class CEventHandler : public IDispEventImpl<4, CEventHandler, &DIID_IMathEvents, &LIBID_DotnetServer, 1, 0>
{
public:
   virtual ~CEventHandler() = default;
   BEGIN_SINK_MAP(CEventHandler)
            SINK_ENTRY_EX(4, DIID_IMathEvents, 46200, OnCalcCompleted)
   END_SINK_MAP()

   static HRESULT __stdcall OnCalcCompleted()
   {
      cout << "calculation completed" << endl;
      return S_OK;
   }
};

int _tmain(int argc, _TCHAR* argv[])
{
   auto hr = CoInitialize(nullptr);

   try
   {
      IWelcomePtr spWelcome;

      // CoCreateInstance()
      hr = spWelcome.CreateInstance("Wrox.DotnetComponent");
      const IUnknownPtr spUnknown = spWelcome;

      cout << spWelcome->Greeting("Bill") << endl;
      auto eventHandler = new CEventHandler();
      hr = eventHandler->DispEventAdvise(spUnknown);

      IMathPtr spMath = spWelcome; // QueryInterface()
      const auto result = spMath->Add(4, 5);
      cout << "result:" << result << endl;

      eventHandler->DispEventUnadvise(spWelcome.GetInterfacePtr());
      delete eventHandler;
   }
   catch (_com_error& e)
   {
      cout << e.ErrorMessage() << endl;
   }

   CoUninitialize();

   return 0;
}
