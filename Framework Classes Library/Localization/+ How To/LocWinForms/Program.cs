/**
 * Локализация в Windows Forms
 */

using System;
using System.Windows.Forms;

namespace LocWinForms
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
