using MyExtensionMethods;
using System;

namespace ExtensionMethods
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with Extension Methods *****\n");

         int myInt = 12345678;
         myInt.DisplayDefiningAssembly();

         System.Data.DataSet d = new System.Data.DataSet();
         d.DisplayDefiningAssembly();

         System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
         sp.DisplayDefiningAssembly();

         Console.WriteLine("Value of myInt: {0}", myInt);
         Console.WriteLine("Reversed digits of myInt: {0}", myInt.ReverseDigits());

         Console.ReadLine();
      }
   }
}
