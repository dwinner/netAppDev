// Извлечение каждого n-ого элемента из коллекции

using System;
using System.Collections.Generic;
using System.Linq;

namespace PickNthElement
{
   internal static class Program
   {
      private static void Main()
      {
         const int n = 20; // Извлекаем каждый 20-й элемент
         var numbers = Enumerable.Range(1, 100).ToList();
         var nthElements = new List<int>();
         Enumerable.Range(0, numbers.Count / n).ToList().ForEach(k => nthElements.Add(numbers.Skip(k * n).First()));
         nthElements.ForEach(element => Console.Write("{0} ", element));
      }
   }
}