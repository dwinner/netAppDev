using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Emitting
{
   internal static class Program
   {
      private static void Main()
      {
         var tree = CSharpSyntaxTree.ParseText(@"class Program 
{
	static void Main() => System.Console.WriteLine (""Hello"");
}");

         var trustedAssemblies = (string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES");
         var trustedAssemblyPaths = trustedAssemblies.Split(Path.PathSeparator);
         var references = trustedAssemblyPaths.Select(path => MetadataReference.CreateFromFile(path));

         var compilation = CSharpCompilation
            .Create("test")
            .WithOptions(new CSharpCompilationOptions(OutputKind.ConsoleApplication))
            .AddSyntaxTrees(tree)
            .AddReferences(references);

         var outputPath = "test.dll";
         var result = compilation.Emit(outputPath);
         Console.WriteLine(result.Success);

         File.WriteAllText("test.runtimeconfig.json", $@"{{
	""runtimeOptions"": {{
		""tfm"": ""net{Environment.Version.Major}.{Environment.Version.Minor}"",
		""framework"": {{
			""name"": ""Microsoft.NETCore.App"",
			""version"": ""{Environment.Version.Major}.{Environment.Version.Minor}.{Environment.Version.Build}""
		}}
	}}
}}");

         // Execute the program we just compiled.
         //Util.Cmd(@"dotnet.exe", "\"" + Path.GetFullPath(outputPath) + "\"");
      }
   }
}