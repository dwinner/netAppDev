using System;

namespace InterfaceNameClash
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with Interface Name Clashes *****\n");
         var oct = new Octagon();

         // We now must use casting to access the Draw()
         // members.
         IDrawToForm itfForm = oct;
         itfForm.Draw();

         // Shorthand notation if you don't need
         // the interface variable for later use.
         ((IDrawToPrinter)oct).Draw();

         // Could also use the "as" keyword.
         ((IDrawToMemory)oct).Draw();

         Console.ReadLine();
      }
   }
}