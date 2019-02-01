/**
 * Анонимные методы
 */

using System;

namespace AnonymousMethods
{
   static class Program
   {
      static void Main()
      {
         const string mid = ", middle part,";

         Func<string, string> anonDel = delegate(string param)
         {
            param += mid;
            param += " and this was added to the string.";
            return param;
         };

         Console.WriteLine(anonDel("Start of string"));
         Console.ReadLine();
      }
   }
}
