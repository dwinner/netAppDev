using System;

namespace DynamicAssembly
{
   public class CodeDriverInAppDomain
   {
      public string CompileAndRun(string code, out bool hasError)
      {
         AppDomain codeDomain = AppDomain.CreateDomain("CodeDriver");
         var codeDriver =
            codeDomain.CreateInstanceAndUnwrap("DynamicAssembly", "DynamicAssembly.CodeDriver") as CodeDriver;
         string result = null;
         hasError = false;
         if (codeDriver != null)
         {
            result = codeDriver.CompileAndRun(code, out hasError);
         }
         AppDomain.Unload(codeDomain);

         return result;
      }
   }
}