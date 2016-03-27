/**
 * Обобщенные методы
 */

using System;

namespace CustomGenericMethods
{
   class Program
   {
      static void Main()
      {
         Console.WriteLine("***** Fun with Custom Generic Methods *****\n");

         // Поменяем два целых числа
         int a = 10, b = 90;
         Console.WriteLine("Before swap: {0}, {1}", a, b);
         MyGenericMethods.Swap(ref a, ref b);
         Console.WriteLine("After swap: {0}, {1}", a, b);
         Console.WriteLine();

         // Поменяем местами две строки
         string s1 = "Hello", s2 = "There";
         Console.WriteLine("Before swap: {0} {1}!", s1, s2);
         MyGenericMethods.Swap(ref s1, ref s2);
         Console.WriteLine("After swap: {0} {1}!", s1, s2);
         Console.WriteLine();

         // Компилятор выведет тип автоматически по контексту
         bool b1 = true, b2 = false;
         Console.WriteLine("Before swap: {0}, {1}", b1, b2);
         MyGenericMethods.Swap(ref b1, ref b2);
         Console.WriteLine("After swap: {0}, {1}", b1, b2);
         Console.WriteLine();
                 
         // Здесь нужно указать параметр типа явно, потому что метод не принимает параметров
         MyGenericMethods.DisplayBaseClass<int>();
         MyGenericMethods.DisplayBaseClass<string>();         

         Console.ReadLine();
      }      
   }
}
