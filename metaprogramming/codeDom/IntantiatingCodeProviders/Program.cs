using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CSharp;

namespace IntantiatingCodeProviders
{
   internal static class Program
   {
      private const string Source =
         @"namespace V3Features
{
  class Program {
    static void Main() {
      var name = ""Kevin"";
      System.Console.WriteLine(name);
    }
  }
}";

      private static void Main()
      {
         var providerOptions = new Dictionary<string, string> {{"CompilerVersion", "v4.0"}};
         var cSharpCodeProvider = new CSharpCodeProvider(providerOptions);
         var compilerParameters = new CompilerParameters(new string[] {});
         var results = cSharpCodeProvider.CompileAssemblyFromSource(compilerParameters, Source);
         Console.WriteLine(results.Errors.HasErrors ? "Compilation failed" : "Compilation successed");
      }
   }
}