/**
 * Binary literals
 */

using System;
using static System.Console;

class Program
{
   static void Main(string[] args)
   {
      int[] numbers = { 0b1, 0b10, 0b100 };
      foreach (var item in numbers)
      {
         WriteLine(item);
      }
   }
}