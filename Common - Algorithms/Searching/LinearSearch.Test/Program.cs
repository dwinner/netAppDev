using System;
using SearchAlgs.Lib;
using static System.Environment;

namespace LinearSearch.Test
{
   internal static class Program
   {
      private static void Main()
      {
         SearchViaIface();
         SearchViaFunc();
      }

      private static void SearchViaIface()
      {
         var generator = new Random();
         var data = new int[10];

         for (var i = 0; i < data.Length; i++) data[i] = generator.Next(10, 100);

         Console.WriteLine(string.Join(" ", data) + NewLine);

         Console.Write("Enter an integer value (-1 to quit): ");
         var searchInt = int.Parse(Console.ReadLine());

         while (searchInt != -1)
         {
            var position = Searching.LinearSearch(data, searchInt);

            Console.WriteLine(position != -1
               ? $"The integer {searchInt} was found in position {position}.{NewLine}"
               : $"The integer {searchInt} was not found.{NewLine}");

            Console.Write("Enter an integer value (-1 to quit): ");
            searchInt = int.Parse(Console.ReadLine());
         }
      }

      private static void SearchViaFunc()
      {
         var generator = new Random();
         var data = new int[10];

         for (var i = 0; i < data.Length; i++) data[i] = generator.Next(10, 100);

         Console.WriteLine(string.Join(" ", data) + NewLine);

         Console.Write("Enter an integer value (-1 to quit): ");
         var searchInt = int.Parse(Console.ReadLine());

         while (searchInt != -1)
         {
            var position = Searching.LinearSearch(data, searchInt, (v1, v2) => v1.CompareTo(v2) == 0);

            Console.WriteLine(position != -1
               ? $"The integer {searchInt} was found in position {position}.{NewLine}"
               : $"The integer {searchInt} was not found.{NewLine}");

            Console.Write("Enter an integer value (-1 to quit): ");
            searchInt = int.Parse(Console.ReadLine());
         }
      }
   }
}