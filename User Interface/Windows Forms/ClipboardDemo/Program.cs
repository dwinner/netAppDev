/**
 * Реализация Cut и Paste для одного типа данных
 */

using System;
using System.Windows.Forms;

namespace ClipboardDemo
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