/**
 * Методы вывода изображений
 */

using System;
using System.Windows.Forms;

namespace DrawImages
{
   internal static class Program
   {      
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new DrawImagesForm());
      }
   }
}