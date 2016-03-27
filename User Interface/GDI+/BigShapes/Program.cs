/**
 * Рисование прокручиваемых окон
 */

using System;
using System.Windows.Forms;

namespace BigShapes
{
   internal static class Program
   { 
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new BigShapesForm());
      }
   }
}