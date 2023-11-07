/**
 * Bridge
 */

using static System.Console;

namespace Bridge
{
   internal static class Program
   {
      private static void Main()
      {
         IBaseList implementation = new OrderedList();
         var listOne = new BaseList { BaseListImplementor = implementation };
         listOne.Add("One");
         listOne.Add("Two");
         listOne.Add("Three");
         listOne.Add("Four");

         BaseList listTwo = new OrnamentedList { BaseListImplementor = implementation, ItemType = '+' };

         BaseList listThree = new NumberedList { BaseListImplementor = implementation };

         for (var i = 0; i < listOne.Count(); i++)
         {
            WriteLine("\t{0}", listOne[i]);
         }

         for (var i = 0; i < listTwo.Count(); i++)
         {
            WriteLine("\t{0}", listTwo[i]);
         }

         for (var i = 0; i < listThree.Count(); i++)
         {
            WriteLine("\t{0}", listThree[i]);
         }

         ReadKey();
      }
   }
}