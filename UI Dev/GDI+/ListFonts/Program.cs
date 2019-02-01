/**
 * Перечисление семейств шрифтов
 */

using System;
using System.Windows.Forms;

namespace ListFonts
{
   internal static class Program
   {      
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new EnumFontsForm());
      }
   }
}