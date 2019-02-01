// Прогнозирование успеха операции, требующей много памяти

using System;
using System.Runtime;

namespace _11_Forecasting
{
   internal static class Program
   {
      private static void Main()
      {
         try
         {
            // Логически резервируем 1.5 Гбайт памяти
            using (/*var failPoint = */new MemoryFailPoint(3000))
            {
               // Выполняем алгоритм, требующий много памяти
            } // Здесь логически освобождается 1.5 Гбайт
         }
         catch (InsufficientMemoryException e)
         {
            // Память не может быть зарезервирована
            Console.WriteLine(e);
         }
      }
   }
}