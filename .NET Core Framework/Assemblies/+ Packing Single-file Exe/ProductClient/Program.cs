using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ProductLib;

namespace ProductClient
{
   internal static class Program
   {
      private static readonly Dictionary<string, Assembly> _Libs = new Dictionary<string, Assembly>();

      private static void Main()
      {
         AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolved;
         var product = new Product {Id = 1, FirstName = "Joe", LastName = "Doe"};
         Console.WriteLine(product);
      }

      private static Assembly OnAssemblyResolved(object sender, ResolveEventArgs args)
      {
         var shortName = new AssemblyName(args.Name).Name;
         if (_Libs.ContainsKey(shortName))
            return _Libs[shortName];

         using (var resourceStream =
            Assembly.GetExecutingAssembly().GetManifestResourceStream($"{nameof(ProductClient)}.{shortName}.dll"))
         using (var binReader =
            new BinaryReader(resourceStream ?? throw new ArgumentNullException(nameof(resourceStream))))
         {
            var data = binReader.ReadBytes((int) resourceStream.Length);
            var assembly = Assembly.Load(data);
            _Libs[shortName] = assembly;
            return assembly;
         }
      }
   }
}