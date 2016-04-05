/**
 * Простое рисование на форме
 */

using System;
using System.Windows.Forms;

namespace PaintEventApp
{
   internal static class Program
   {      
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new PaintForm());
      }
   }
}