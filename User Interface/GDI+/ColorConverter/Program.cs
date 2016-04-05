/**
 * Преобразование цветов разных форматов
 */

using System;
using System.Windows.Forms;

namespace ColorConverter
{
   internal static class Program
   {      
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new ColorConverterForm());
      }
   }
}