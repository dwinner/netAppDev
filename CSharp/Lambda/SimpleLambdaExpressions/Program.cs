using System;
using System.Collections.Generic;

namespace SimpleLambdaExpressions
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with Lambdas *****\n");
         TraditionalDelegateSyntax();
         AnonymousMethodSyntax();
         Console.WriteLine();
         LambdaExpressionSyntax();
         Console.ReadLine();
      }

      #region Синтаксис анонимных методов

      private static void AnonymousMethodSyntax()
      {
         var list = new List<int>();
         list.AddRange(new[] {20, 1, 4, 8, 9, 44});

         // Теперь используем анонимные методы
         var evenNumbers = list.FindAll(delegate(int i) { return (i%2) == 0; });

         Console.WriteLine("Here are your even numbers:");
         foreach (var evenNumber in evenNumbers)
         {
            Console.Write("{0}\t", evenNumber);
         }
         Console.WriteLine();
      }

      #endregion

      #region Лямбда-выражение

      private static void LambdaExpressionSyntax()
      {
         var list = new List<int>();
         list.AddRange(new[] {20, 1, 4, 8, 9, 44});

         // Теперь обрабатываем каждый аргумент своим блоком кода обратного вызова         
         var evenNumbers = list.FindAll(i =>
         {
            Console.WriteLine("value of i is currently: {0}", i);
            var isEven = ((i%2) == 0);
            return isEven;
         });

         Console.WriteLine("Here are your even numbers:");
         foreach (var evenNumber in evenNumbers)
         {
            Console.Write("{0}\t", evenNumber);
         }
         Console.WriteLine();
      }

      #endregion

      #region Традиционный синтаксис делегата

      private static void TraditionalDelegateSyntax()
      {
         // Создаем список целых значений.
         var list = new List<int>();
         list.AddRange(new[] {20, 1, 4, 8, 9, 44});

         // Вызываем FindAll(), используя традиционный синтаксис делегата.
         Predicate<int> callback = IsEvenNumber;
         var evenNumbers = list.FindAll(callback);

         Console.WriteLine("Here are your even numbers:");
         foreach (var evenNumber in evenNumbers)
         {
            Console.Write("{0}\t", evenNumber);
         }
         Console.WriteLine();
      }

      // Назначение для делегата Predicate<>.
      private static bool IsEvenNumber(int i)
      {
         return (i%2) == 0;
      }

      #endregion
   }
}