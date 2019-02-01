using System;
using System.IO;
using System.Linq;
using System.Reflection;
using MoreLinq;

namespace SizeOfTypes
{
   internal static class Program
   {
      private const string FrameworkPath = @"c:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5";
      private const string AssemblyExt = "*.dll";

      private static void Main()
      {
         Func<MethodInfo, bool> methodSelector =
            info => info.IsPublic && !info.Name.StartsWith("get_") && !info.Name.StartsWith("set_");

         Func<Type, bool> typeSelector = type => type.IsClass && type.IsPublic;

         var typeSizes =
            Directory.GetFiles(FrameworkPath, AssemblyExt)
               .SelectMany(
                  dir =>
                     Assembly.LoadFrom(dir)
                        .GetTypes()
                        .Where(typeSelector)
                        .Select(
                           type => new { TypeName = type.FullName, MethodCount = type.GetMethods().Count(methodSelector) }))
               .OrderByDescending(arg => arg.MethodCount);
         typeSizes.ForEach(obj => Console.WriteLine("{0}: {1}", obj.TypeName, obj.MethodCount));
      }
   }
}