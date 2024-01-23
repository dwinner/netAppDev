using System;
using System.IO;
using System.Linq;
using System.Reflection;
using MoreLinq;

namespace InheritanceRelationships
{
   internal static class Program
   {
      private const string FrameworkPath = @"c:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5";
      private const string AssemblyExt = "*.dll";
      private static readonly string[] Assemblies;

      static Program() => Assemblies = Directory.GetFiles(FrameworkPath, AssemblyExt);

      private static void Main()
      {
         var inheritanceRel =
            Assemblies.SelectMany(
                  dir =>
                     Assembly.LoadFrom(dir)
                        .GetTypes()
                        .Where(t => t.IsPublic && t.IsClass)
                        .Select(t => new
                        {
                           Parent = t.BaseType,
                           t.Name
                        }))
               .Where(arg => arg.Parent != null)
               .Select(arg => new
               {
                  Parent = arg.Parent.Name,
                  arg.Name
               })
               .ToLookup(arg => arg.Parent);
         inheritanceRel.ForEach(group => Console.WriteLine("{0}: {1}",
            group.Key,
            group.Count()));
      }
   }
}