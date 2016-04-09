/**
 * Более развитый пример рисования
 */

using System;
using System.Windows.Forms;

namespace ScrollMoreShapes
{
   internal static class Program
   {
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new MoreShapesForm());
      }
   }
}