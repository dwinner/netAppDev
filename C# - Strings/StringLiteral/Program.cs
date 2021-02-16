/**
 * Строковые литералы
 */

using System;

namespace _01_StringLiteral
{
   class Program
   {
      static void Main(string[] args)
      {
         const string literal1 = "c:\\windows\\system32";
         const string literal2 = @"c:\windows\system32";
         const string literal3 = @"
               Jack and Jill
               Went up the Hill...
         ";
         Console.WriteLine(literal3);
         Console.WriteLine("Object.ReferenceEquals(lit1, lit2): {0}", ReferenceEquals(literal1, literal2));
         if (args.Length > 0)
         {
            Console.WriteLine("Полученный параметр: {0}", args[0]);
            string strNew = string.Intern(args[0]);
            Console.WriteLine("Object.ReferenceEquals(lit1, strNew): {0}", ReferenceEquals(literal1, strNew));
         }

         Console.ReadKey();
      }
   }
}
