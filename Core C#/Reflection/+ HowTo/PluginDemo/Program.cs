/**
 * Поиск и загрузка добавляемых модулей
 */

using System;
using System.Windows.Forms;

namespace PluginDemo
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
