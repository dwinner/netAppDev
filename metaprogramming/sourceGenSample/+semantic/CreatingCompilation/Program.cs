using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CreatingCompilation
{
   internal static class Program
   {
      private static void Main()
      {
         var tree = CSharpSyntaxTree.ParseText(@"class Program 
{
	static void Main() => System.Console.WriteLine (""Hello"");
}");
         /*
         var compilation = CSharpCompilation.Create("test");
         compilation = compilation.WithOptions(
               new CSharpCompilationOptions(OutputKind.ConsoleApplication));
         compilation = compilation.AddSyntaxTrees(tree);

         var trustedAssemblies = (string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES");
         var trustedAssemblyPaths = trustedAssemblies.Split(Path.PathSeparator);
         var references = trustedAssemblyPaths.Select(path => MetadataReference.CreateFromFile(path));

         compilation = compilation.AddReferences(references);
         */

         // Or, in one step:

         var compilation = CSharpCompilation
               .Create("test")
               .WithOptions(new CSharpCompilationOptions(OutputKind.ConsoleApplication))
               .AddSyntaxTrees(tree)
            /*.AddReferences(references)*/;

         foreach (var diagnostic in compilation.GetDiagnostics())
         {
            Console.WriteLine(diagnostic);
         }
      }
   }
}