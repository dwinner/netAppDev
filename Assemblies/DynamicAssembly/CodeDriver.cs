using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace DynamicAssembly
{
   public class CodeDriver : MarshalByRefObject
   {
      private const string Prefix = "using System; public static class Driver { public static void Run() { ";
      private const string Postfix = " } } ";

      public string CompileAndRun(string input, out bool hasError)
      {
         hasError = false;
         string returnData;

         CompilerResults results;
         using (var provider = new CSharpCodeProvider())
         {
            var compilerParameters = new CompilerParameters { GenerateInMemory = true };
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(Prefix).Append(input).Append(Postfix);
            results = provider.CompileAssemblyFromSource(compilerParameters, stringBuilder.ToString());
         }

         if (results.Errors.HasErrors)
         {
            hasError = true;
            var errorMessage = new StringBuilder();
            foreach (CompilerError compilerError in results.Errors)
            {
               errorMessage.AppendFormat("{0} {1}", compilerError.Line, compilerError.ErrorText);
            }
            returnData = errorMessage.ToString();
         }
         else
         {
            TextWriter textWriter = Console.Out;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            Type driverType = results.CompiledAssembly.GetType("Driver");
            driverType.InvokeMember("Run", BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public, null,
               null, null);
            Console.SetOut(textWriter);
            returnData = stringWriter.ToString();
         }

         return returnData;
      }
   }
}