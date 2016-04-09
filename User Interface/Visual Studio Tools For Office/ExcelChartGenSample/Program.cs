/**
 * Генерация Excel-графиков программным путем
 */

using System;
using ExcelChartGenSample.Xls;

namespace ExcelChartGenSample
{
   internal static class Program
   {
      private static void Main()
      {
         var testData = TrialData.GetTestData();
         var chartGenerator = new ExcelChartGenerator(testData);
         string errorMessage;
         if (!chartGenerator.Render(out errorMessage))
         {
            Console.WriteLine(errorMessage);
         }
      }
   }
}