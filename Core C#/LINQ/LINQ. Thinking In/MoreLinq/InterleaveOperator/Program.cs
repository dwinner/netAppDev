// Поэелементное соединение последовательностей

using System;
using MoreLinq;

namespace InterleaveOperator
{
   internal static class Program
   {
      private static void Main()
      {
         // Первый вариант
         int[] n1 = { 1, 3, 4, 5 };
         int[] n2 = { 4, 6 };
         var total = new int[n1.Length + n2.Length];
         int first = 0, second = 0, index = 0;

         for (; index < n1.Length + n2.Length; index++)
         {
            if (index % 2 == 0)
            {
               if (first != n1.Length)
               {
                  total[index] = n1[first];
                  first++;
               }
               else
               {
                  if (second != n2.Length)
                  {
                     total[index] = n2[second];
                     second++;
                  }
               }
            }
            else
            {
               if (second != n2.Length)
               {
                  total[index] = n2[second];
                  second++;
               }
               else
               {
                  if (first != n1.Length)
                  {
                     total[index] = n1[first];
                     first++;
                  }
               }
            }
         }
         
         total.ForEach(Console.WriteLine);         
      }
   }
}