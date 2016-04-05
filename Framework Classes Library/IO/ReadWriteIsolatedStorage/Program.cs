/**
 * Чтение и запись в изолированное хранилище
 */

using System;
using System.Windows.Forms;

namespace SelfPlacingWindow
{
   internal static class Program
   {
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new Form1());
      }
   }
}