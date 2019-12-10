using System;
using System.IO;
using System.Runtime.Loader;

namespace AssemblyLoadContextSample
{
   internal static class Program
   {
      private static void Main()
      {
         LoadAssembly(6);

         // cleaning up
         GC.Collect();
         GC.WaitForPendingFinalizers();

         Array.ForEach(AppDomain.CurrentDomain.GetAssemblies(), assembly => Console.WriteLine(assembly.GetName().Name));

         Console.ReadKey(true);
      }

      private static void LoadAssembly(int number)
      {
         AssemblyLoadContext context = new CustomAssemblyLoadContext();

         // unloading handler
         context.Unloading += Context_Unloading;

         // Path to MyApp
         var assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "MyApp.dll");

         // load the assembly
         var assembly = context.LoadFromAssemblyPath(assemblyPath);

         // Get the type 'SomeType'
         var type = assembly.GetType("MyApp.SomeType");

         // Get 'Factorial' method
         var factMethod = type.GetMethod("Factorial");

         // Call the method
         var instance = Activator.CreateInstance(type);
         var result = (int) factMethod.Invoke(instance, new object[] {number});

         Console.WriteLine($"Fact({number}) = {result}");

         // See loaded assemblies
         Array.ForEach(AppDomain.CurrentDomain.GetAssemblies(), asm => Console.WriteLine(asm.GetName().Name));

         // Unload the context
         context.Unload();
      }

      private static void Context_Unloading(AssemblyLoadContext asmLoadCtx)
      {
         Console.WriteLine("MyApp's been unloaded");
      }
   }
}