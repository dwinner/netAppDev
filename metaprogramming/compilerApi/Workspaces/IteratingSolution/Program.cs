using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using static System.Console;

namespace IteratingSolution
{
   internal static class Program
   {
      private static void Main()
      {
         // Path to an existing solution
         const string solutionPath = "../../../../CompilerApi.sln";

         // Create a workspace
         var workspace = MSBuildWorkspace.Create();

         // Open a solution
         var solution = workspace.OpenSolutionAsync(solutionPath).Result;

         // Invoke code to iterate items
         IterateSolution(solution, solutionPath);
      }

      private static void IterateSolution(Solution solution, string solutionPath)
      {
         // Print solution's pathname and version
         WriteLine($"Solution {Path.GetFileName(solutionPath)}, version: {solution.Version}");

         // For each project...
         foreach (var project in solution.Projects)
         {
            // Print the name and version
            WriteLine($"Project name: {project.Name}, version: {project.Version}");

            // Then print the number of code files
            WriteLine($" {project.Documents.Count()} code files:");

            // For each code file, print the file name
            foreach (var codeFile in project.Documents)
            {
               WriteLine($"    {codeFile.Name}");
            }

            WriteLine(" References:");

            // For each reference in the project print the name
            foreach (var reference in project.MetadataReferences)
            {
               WriteLine($"    {Path.GetFileName(reference.Display)}");
            }
         }
      }
   }
}