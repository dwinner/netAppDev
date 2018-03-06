/**
 * Сохранение изображения в буфере обмена
 */

using System;
using System.Windows.Forms;

namespace DrawToBitmap
{
   internal static class Program
   {      
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new DrawToBitmapForm());
      }
   }
}