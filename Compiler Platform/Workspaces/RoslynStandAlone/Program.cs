using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using static System.Console;

namespace RoslynStandAlone
{
   internal static class Program
   {
      private static void Main()
      {
         GenerateCode();
         ReadLine();
      }

      private static void GenerateCode()
      {
         // Generate a syntax tree from source text
         var syntaxTree = CSharpSyntaxTree.ParseText(@"
using System;
using System.IO;

namespace RoslynSuccinctly
{
  public class Helper
  {
      public void PrintTextFromFile(string fileName)
      {
          if (File.Exists(fileName) == false)
          {
              Console.WriteLine(""File does not exist"");
              return;
          }

          using (StreamReader str = new StreamReader(fileName))
          {
              Console.WriteLine(str.ReadToEnd());
          }
      }
   }
}
");
         // Get a random file name for the output assembly
         var outputAssemblyName = Path.GetRandomFileName();

         // Add a list of references from assemblies by a type name, get the assembly ref
         MetadataReference[] references =
         {
            MetadataReference.CreateFromFile(typeof (object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof (File).Assembly.Location)
         };

         // Single invocation to the compiler, create an assembly with the specified syntax trees,
         // references, and options
         var compilation = CSharpCompilation.Create(outputAssemblyName, new[] {syntaxTree}, references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

         // Create a stream
         using (var memoryStream = new MemoryStream())
         {
            // Emit the IL code into the stream
            var result = compilation.Emit(memoryStream);

            // If emit fails,
            if (!result.Success)
            {
               // Query the list of diagnistics in the source code
               var diagnostics =
                  result.Diagnostics.Where(
                     diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);

               // Write id and message for each diagnistic
               foreach (var diagnostic in diagnostics)
               {
                  Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
               }
            }
            else
            {
               // If emit succeeds, move to the beginning of the assembly
               memoryStream.Seek(0, SeekOrigin.Begin);

               // Load the generated assembly
               var inputAssembly = Assembly.Load(memoryStream.ToArray());

               // Get a reference to the type defined in the syntax tree
               var typeInstance = inputAssembly.GetType("RoslynSuccinctly.Helper");

               // Create an instance of the type
               var obj = Activator.CreateInstance(typeInstance);

               // Invoke the method
               typeInstance.InvokeMember(
                  "PrintTextFromFile", BindingFlags.Default | BindingFlags.InvokeMethod, null, obj,
                  new object[] { "Test.txt" });
            }
         }
      }
   }
}