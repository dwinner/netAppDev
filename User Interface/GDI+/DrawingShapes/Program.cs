/**
 * Рисование контуров
 */

using System;
using System.Windows.Forms;

namespace DrawingShapes
{
   internal static class Program
   { 
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new DrawingShapes());
      }
   }
}