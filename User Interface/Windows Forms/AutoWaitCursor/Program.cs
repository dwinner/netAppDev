/**
 * Автоматический сброс индикатора ожидания
 */

using System;
using System.Windows.Forms;

namespace AutoWaitCursor
{
   internal static class Program
   {
      [STAThread]
      private static void Main()
      {
         Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new Form1());
      }
   }
}