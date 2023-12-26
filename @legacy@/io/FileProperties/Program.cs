/**
 * Пример чтения свойств для файлов и каталогов
 */

using System;
using System.Windows.Forms;

namespace FileProperties
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