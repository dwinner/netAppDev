using System;
using System.IO;
using System.Linq;
using System.Reflection;
using MoreLinq;

namespace NumberOfOverloads
{
   internal static class Program
   {
      private const string FrameworkPath = @"c:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5";
      private const string AssemblyExt = "*.dll";

      private static void Main()
      {
         Func<MethodInfo, bool> methodFilter =
            methodInf =>
               methodInf.DeclaringType != null && methodInf.IsPublic &&
               methodInf.DeclaringType.Namespace == typeof(Enumerable).Namespace && !methodInf.Name.StartsWith("get_") &&
               !methodInf.Name.StartsWith("set_");
         var overloads =
            Directory.GetFiles(FrameworkPath, AssemblyExt)
               .SelectMany(dir => Assembly.LoadFrom(dir).GetTypes().SelectMany(type => type.GetMethods()))
               .Where(methodFilter)
               .ToLookup(info => info.Name)
               .Select(group => new { MethodName = group.Key, Overloads = group.Count() })
               .Where(var => var.Overloads >= 2)
               .OrderByDescending(arg => arg.Overloads);
         overloads.ForEach(@var => Console.WriteLine("{0}: {1}", @var.MethodName, @var.Overloads));
      }
   }
}