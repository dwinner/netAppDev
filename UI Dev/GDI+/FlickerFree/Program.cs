/**
 * Двойная буферизация
 */

using System;
using System.Windows.Forms;

namespace FlickerFree
{
   internal static class Program
   {      
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new DoubleBufferedForm());
      }
   }
}