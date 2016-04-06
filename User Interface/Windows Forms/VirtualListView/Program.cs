/**
 * Виртуализация списка
 */

using System;
using System.Windows.Forms;

namespace VirtualListViewSort
{
   static class Program
   {      
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new Form1());
      }
   }
}
