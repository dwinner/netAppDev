using System;
using System.IO;
using System.Linq;
using System.Reflection;
using MoreLinq;

namespace NamespaceSize
{
   internal static class Program
   {
      private const string FrameworkPath = @"c:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5";
      private const string AssemblyExt = "*.dll";

      private static void Main()
      {
         var namespaces =
            Directory.GetFiles(FrameworkPath, AssemblyExt)
               .SelectMany(dir => Assembly.LoadFrom(dir).GetTypes().Where(type => type.IsClass && type.IsPublic))
               .ToLookup(type => type.Namespace)
               .ToDictionary(group => group.Key, group => group.Count())
               .OrderByDescending(pair => pair.Value);
         namespaces.ForEach(pair => Console.WriteLine("{0} {1}", pair.Key, pair.Value));
      }
   }
}