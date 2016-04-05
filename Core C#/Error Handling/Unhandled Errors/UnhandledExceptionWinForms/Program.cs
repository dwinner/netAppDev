/**
 * Перехват необработанных исключений в приложениях Windows Forms
 */
using System;
using System.Windows.Forms;

namespace HowToCSharp.Ch04.UnhandledExceptionWinForms
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new MainForm());
      }
   }
}
