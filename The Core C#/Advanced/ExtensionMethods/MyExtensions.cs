using System;
using System.Reflection;

namespace MyExtensionMethods
{
   static class MyExtensions
   {
      // Данный метод расширения позволяет любому объекту отображать сборку, в которой он определен
      public static void DisplayDefiningAssembly(this object obj)
      {
         Console.WriteLine("{0} lives here: => {1}\n",
            obj.GetType().Name,
            Assembly.GetAssembly(obj.GetType()).GetName().Name);
      }

      // Данный метод расширения позволяет любому целому числу переворачивать себя: 56 <=> 65
      public static int ReverseDigits(this int i)
      {
         char[] digits = i.ToString().ToCharArray();
         Array.Reverse(digits);
         string newDigits = new string(digits);
         return int.Parse(newDigits);
      }
   }
}
