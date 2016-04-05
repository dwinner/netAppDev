/**
 * Координаты и размеры
 */

using System;
using System.Drawing;

namespace PointsAndSizes
{
   internal static class Program
   {
      private static void Main()
      {
         var topLeft = new Point(10, 10);
         var rectangleSize = new Size(50, 50);
         var bottomRight = topLeft + rectangleSize;
         Console.WriteLine("topLeft = {0}", topLeft);
         Console.WriteLine("bottomRight = {0}", bottomRight);
         Console.WriteLine("Size = {0}", rectangleSize);
      }
   }
}