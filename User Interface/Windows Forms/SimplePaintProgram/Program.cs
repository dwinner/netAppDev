/**
 * Простейшая программа для рисования
 */

using System;
using System.Windows.Forms;

namespace SimplePaintProgram
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