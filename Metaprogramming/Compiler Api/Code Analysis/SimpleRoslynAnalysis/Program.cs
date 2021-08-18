// Простое использование Roslyn-анализатора

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System.Diagnostics;
using System.Linq;
using static System.Console;

namespace SimpleRoslynAnalysis
{
   internal static class Program
   {
      // Путь к решению
      private const string PathToSolution =
          @"D:\Compiler Services\Roslyn.Samples\+ SolutionsToAnalyze\SampleToAnalyze\SampleToAnalyze.sln";

      // Имя проекта
      private const string ProjectName = "SampleToAnalyze";

      // Имя класса
      private const string ClassName = "SampleToAnalyze.SimpleClassToAnalyze";

      private static void Main()
      {
         // Рабочее пространство Roslyn            
         var workspace = MSBuildWorkspace.Create();

         // Откроем решение, которое хотим анализировать
         var solutionToAnalyze = workspace.OpenSolutionAsync(PathToSolution).Result;

         // Получим проект, который хотим анализировать
         var projectToAnalyze = solutionToAnalyze.Projects.FirstOrDefault(project => project.Name == ProjectName);

         // Получим объект компиляции проекта
         Debug.Assert(projectToAnalyze != null, "projectToAnalyze != null");
         var compilationToAnalyze = projectToAnalyze.GetCompilationAsync().Result;
         //var typeNames = compilationToAnalyze.Assembly.TypeNames;
         //var classToAnalyze = typeNames.Where(typeName => typeName == ClassName);            

         var classToAnalyze = compilationToAnalyze.GetTypeByMetadataName(ClassName);
         Debug.Assert(classToAnalyze != null);

         var fullNamespace = classToAnalyze.GetFullNamespace();
         WriteLine("Full namespace: {0}", fullNamespace);

         var propertySymbol = classToAnalyze.GetMembers("MySimpleProperty").FirstOrDefault() as IPropertySymbol;
         WriteLine("Fetched property: {0}", propertySymbol?.Name ?? "None");

         var eventSymbol = classToAnalyze.GetMembers("MySimpleEvent").FirstOrDefault() as IEventSymbol;
         WriteLine("Event name: {0}", eventSymbol?.Name ?? "None");

         var methodSymbol = classToAnalyze.GetMembers("MySimpleMethod").FirstOrDefault() as IMethodSymbol;
         var methodSignature = methodSymbol.GetMethodSignature();
         WriteLine(methodSignature);

         var attrData = classToAnalyze.GetAttributes().FirstOrDefault();
         var intProperty = attrData.GetAttributeConstructorValueByParameterName("intProp");
         WriteLine("Attribute's intProp = {0}", intProperty);
         var stringProperty = attrData.GetAttributeConstructorValueByParameterName("stringProp");
         WriteLine("Attrubute's string property = {0}", stringProperty);
      }
   }
}