/**
 * Приложение Windows Forms, созданное Visual Studio
 */

using System;
using System.Windows.Forms;

namespace SimpleVsWinFormsApp
{
   internal static class Program
   {      
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new MainForm());
      }
   }
}