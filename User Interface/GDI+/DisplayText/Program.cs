/**
 * Простой пример отображения текста
 */

using System;
using System.Windows.Forms;

namespace DisplayText
{
   internal static class Program
   {      
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new DisplayTextForm());
      }
   }
}