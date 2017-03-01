/**
 * Стандартные элементы управления и компоненты
 */

using System;
using System.Windows.Forms;

namespace MainExample
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
