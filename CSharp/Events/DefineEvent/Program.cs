/**
 * Публикация событий
 */

using System;
using System.Windows.Forms;

namespace DefineEvent
{
   static class Program
   {
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new MainForm());
      }
   }
}
