// Диагностика

using System;
using System.Linq;
using PostSharp.Extensibility;
using PostSharp.Patterns.Diagnostics;

namespace PsDiagnostics
{
   internal class Program
   {
      private static void Main()
      {
         try
         {
            var nameReverser = new NameReverser();
            var returnValue = nameReverser.For("Bobby");
            Console.WriteLine("Application Return: {0}", returnValue);
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.ToString());
         }

         Console.ReadKey();
      }
   }

   [Log("EntryExitLogging",
      AttributeTargetElements = MulticastTargets.Method,
      AttributeTargetMemberAttributes = MulticastAttributes.Public)]
   public class NameReverser
   {
      public string For(string aName)
      {
         return new string(aName.Reverse().ToArray());
      }
   }
}