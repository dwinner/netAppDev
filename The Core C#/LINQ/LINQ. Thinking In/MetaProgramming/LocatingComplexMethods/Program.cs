using System;
using System.IO;
using System.Linq;
using System.Reflection;
using MoreLinq;

namespace LocatingComplexMethods
{
   internal static class Program
   {
      private const string FrameworkPath = @"c:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5";
      private const string AssemblyExt = "*.dll";
      private static readonly string[] Assemblies;

      static Program()
      {
         Assemblies = Directory.GetFiles(FrameworkPath, AssemblyExt);
      }

      private static void Main()
      {
         var badMethods =
            Assemblies.SelectMany(dir => Assembly.LoadFrom(dir).GetTypes().SelectMany(type => type.GetMethods()))
               .Where(methodInfo => !methodInfo.Name.StartsWith("get_") && !methodInfo.Name.StartsWith("set_"))
               .Select(
                  methodInfo =>
                     new
                     {
                        Method = methodInfo.Name,
                        NameSpace = methodInfo.DeclaringType.Namespace,
                        Class = methodInfo.DeclaringType.FullName,
                        ParameterNumber = methodInfo.GetParameters().Length
                     })
               .Where(arg => arg.NameSpace == typeof (Enumerable).Namespace)
               .OrderByDescending(arg => arg.ParameterNumber);
         badMethods.ForEach(Console.WriteLine);
      }
   }
}