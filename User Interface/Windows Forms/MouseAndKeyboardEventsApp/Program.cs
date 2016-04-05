/**
 * События мыши и клавиатуры
 */

using System;
using System.Windows.Forms;

namespace MouseAndKeyboardEventsApp
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