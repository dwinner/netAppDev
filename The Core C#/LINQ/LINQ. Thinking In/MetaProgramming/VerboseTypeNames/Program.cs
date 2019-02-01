using System;
using System.IO;
using System.Linq;
using System.Reflection;
using MoreLinq;

namespace VerboseTypeNames
{
   internal static class Program
   {
      private const string FrameworkPath = @"c:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5";
      private const string AssemblyExt = "*.dll";

      private static void Main()
      {
         var verboseTypeNames = Directory.GetFiles(FrameworkPath, AssemblyExt)
            .SelectMany(
               dir =>
                  Assembly.LoadFrom(dir)
                     .GetTypes()
                     .Where(type => type.IsClass && type.IsPublic)
                     .Select(type => new { type.Namespace, type.Name, type.Name.Length }))
            .ToLookup(arg => arg.Length)
            .OrderByDescending(group => group.Key)
            .Select(group => group.ElementAt(0));
         verboseTypeNames.ForEach(obj => Console.WriteLine(obj.Name));
      }
   }
}