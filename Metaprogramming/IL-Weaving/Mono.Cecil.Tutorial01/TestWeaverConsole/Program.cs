using System;
using ILCodeWeaving;
using TestLibrary;

namespace TestWeaverConsole
{
   internal static class Program
   {
      private const string AssemblyPath = @"..\..\..\TestDlls\TestLibrary.dll";

      private static void Main()
      {
         var ilCodeWeaver = new IlCodeWeaver(AssemblyPath);

         //Example 1. Faking return value - primitives
         ilCodeWeaver.Setup(() => HelloMessages.GetHelloWorld())
            .Returns("All your bases are belong to us");

         ilCodeWeaver.Setup(() => HelloMessages.GetSumMessage(5, 11))
            .Returns("the sum of x and y is none of your concern");

         //Example 2. Throwing exception in code
         var person1 = new Person("Chris", new DateTime(1972, 9, 15));
         ilCodeWeaver.Setup(() => person1.GetAge()).Throws();

         var person2 = new Person("Jane", new DateTime(1988, 5, 12));
         ilCodeWeaver.Setup(() => person2.GetIntro())
            .Throws<MySillyException>(person2.Name, person2.GetAge());

         //Example 3. Reweaving properties
         ilCodeWeaver.SetupProp(() => person2.Name)
            .Returns("Bond, James Bond");

         ilCodeWeaver.SetupProp(() => person2.Occupation)
            .Sets("agent 007");

         ilCodeWeaver.Reweave();
      }
   }
}