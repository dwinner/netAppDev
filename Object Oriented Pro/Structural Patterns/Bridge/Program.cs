/**
 * Мост
 */

using System;

namespace Bridge
{
   class Program
   {
      static void Main()
      {
         IBaseListImpl implementation = new OrderedListImpl();
         var listOne = new BaseList { BaseListImplementor = implementation };
         listOne.Add("One");
         listOne.Add("Two");
         listOne.Add("Three");
         listOne.Add("Four");

         BaseList listTwo = new OrnamentedList { BaseListImplementor = implementation, ItemType = '+' };

         BaseList listThree = new NumberedList { BaseListImplementor = implementation };

         for (int i = 0; i < listOne.Count(); i++)
         {
            Console.WriteLine("\t{0}", listOne[i]);
         }

         for (int i = 0; i < listTwo.Count(); i++)
         {
            Console.WriteLine("\t{0}", listTwo[i]);
         }

         for (int i = 0; i < listThree.Count(); i++)
         {
            Console.WriteLine("\t{0}", listThree[i]);
         }

         Console.ReadKey();
      }
   }
}
