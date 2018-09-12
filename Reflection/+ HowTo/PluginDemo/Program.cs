/**
 * Поиск и загрузка добавляемых модулей
 */

using System;
using System.Windows.Forms;

namespace PluginDemo
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