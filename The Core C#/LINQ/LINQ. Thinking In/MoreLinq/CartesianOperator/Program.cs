using System;
using System.Collections.Generic;
using System.Linq;

namespace CartesianOperator
{
   internal static class Program
   {
      private static void Main()
      {
         int[] lengths = { 1, 2, 3, 4, 5, 6, 7 };
         int[] breadths = { 1, 1, 2, 3, 1, 3 };
         int[] heights = { 2, 1, 3, 1, 4 };

         var volumes = new List<int>();
         foreach (var i in lengths)
         {
            foreach (var j in breadths)
            {
               volumes.AddRange(heights.Select(t => i * j * t));
            }
         }

         volumes.ForEach(i => Console.Write("{0} ", i));
      }
   }
}