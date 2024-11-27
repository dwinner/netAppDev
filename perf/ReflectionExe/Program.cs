using System;
using System.Diagnostics;
using System.Reflection;
using ReflectionInterface;

namespace ReflectionExe
{
   internal static class Program
   {
      private const int Iterations = 10_000;

      private static void Main()
      {
         const string extensionFile = @"ReflectionExtension";
         var assembly = Assembly.Load(extensionFile);

         var types = assembly.GetTypes();
         Type extensionType = null;
         foreach (var type in types)
         {
            var interfaceType = type.GetInterface("IExtension");
            if (interfaceType != null)
            {
               extensionType = type;
               break;
            }
         }

         object extensionObject = null;
         if (extensionType != null)
         {
            extensionObject = Activator.CreateInstance(extensionType);
         }

         var executeMethod = extensionType.GetMethod("Execute");
         var extensionViaInterface = extensionObject as IExtension;

         // Pre-execute to account for JIT
         executeMethod.Invoke(extensionObject, new object[] { 1, 2 });
         extensionViaInterface.Execute(1, 2);

         var watch = Stopwatch.StartNew();
         var actualResult = 0;

         // Execute via MethodInfo.Invoke
         for (var i = 0; i < Iterations; i++)
         {
            var result = executeMethod.Invoke(extensionObject, new object[] { 1, 2 });
            actualResult = (int)result;
         }

         watch.Stop();
         Console.WriteLine("{0}, {1} ticks", actualResult, watch.ElapsedTicks);

         // Execute via interface
         watch.Restart();
         for (var i = 0; i < Iterations; i++)
         {
            actualResult = extensionViaInterface.Execute(1, 2);
         }

         watch.Stop();
         Console.WriteLine("{0}, {1} ticks", actualResult, watch.ElapsedTicks);
      }
   }
}