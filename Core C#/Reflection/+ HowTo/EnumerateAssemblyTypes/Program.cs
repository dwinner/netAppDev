/**
 * Перечисление типов в сборке
 */

using System;
using System.Windows.Forms;

namespace EnumerateAssemblyTypes
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
