/**
 * Compile and running entry point on the fly
 */

using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
// ReSharper disable AssignNullToNotNullAttribute

namespace CompileHelloWorld
{
   internal static class Program
   {
      private static void Main()
      {
         const string code = @"using System;

namespace HelloWorld
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Out.WriteLine(""Hello compiled world"");
		}
	}
}";

         var tree = SyntaxFactory.ParseSyntaxTree(code);
         var compilation = CSharpCompilation.Create(
            "HelloWorldCompiled.exe",
            options: new CSharpCompilationOptions(OutputKind.ConsoleApplication),
            syntaxTrees: new[] {tree},
            references: new[] {MetadataReference.CreateFromFile(typeof (object).Assembly.Location)});

         using (var stream = new MemoryStream())
         {
            /*var compileResult = */compilation.Emit(stream);
            var assembly = Assembly.Load(stream.GetBuffer());
            assembly.EntryPoint.Invoke(
               null, BindingFlags.NonPublic | BindingFlags.Static, null, new object[1], null);
         }
      }
   }
}