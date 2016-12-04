using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis.MSBuild;
using static System.Console;

namespace ProgrammaticallyCompileSolutionSample
{
   internal static class CompileViaRoslynApiEntry
   {
      private static void Main(string[] args)
      {
         if (args == null || args.Length != 2)
         {
            WriteLine("Usage: CompileViaRoslynApiEntry \"Path to solution\" \"Output directory\"");
            return;
         }

         var solutionPath = args[0];
         var outputPath = args[1];

         if (!File.Exists(solutionPath))
         {
            WriteLine("Solution '{0}' doesn't exist", solutionPath);
            return;
         }

         if (!Directory.Exists(outputPath))
         {
            WriteLine("Directory '{0}' doesn't exist", outputPath);
            return;
         }

         var successfullyCompiled = CompileSolution(solutionPath, outputPath);
         if (successfullyCompiled)
         {
            WriteLine("Compilation completed successfully.");
            WriteLine("Output directory: {0}", outputPath);
         }         
      }

      private static bool CompileSolution(string solutionPath, string outputPath)
      {         
         var workspace = MSBuildWorkspace.Create();                  
         var solution = workspace.OpenSolutionAsync(solutionPath).Result;
         var projectDependencyGraph = solution.GetProjectDependencyGraph();         

         foreach (
            var projectCompilation in
               projectDependencyGraph.GetTopologicallySortedProjects()
                  .Select(projectId => solution.GetProject(projectId).GetCompilationAsync().Result))
         {
            if (!string.IsNullOrEmpty(projectCompilation?.AssemblyName))
            {               
               using (var emitStream = new MemoryStream())
               {
                  var emitResult = projectCompilation.Emit(emitStream);
                  if (emitResult.Success)
                  {
                     var assemblyFileName = $"{projectCompilation.AssemblyName}.dll";
                     using (
                        var assemblyFileStream = File.Create(
                           $"{outputPath}{Path.DirectorySeparatorChar}{assemblyFileName}")
                        )
                     {
                        emitStream.Seek(0, SeekOrigin.Begin);
                        emitStream.CopyTo(assemblyFileStream);
                     }
                  }
                  else
                  {
                     return false;
                  }
               }
            }
            else
            {
               return false;
            }
         }

         return true;
      }
   }
}