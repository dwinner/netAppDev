/**
 * Добавление строки меню и панели инструментов
 */

using System;
using System.Windows.Forms;

namespace ToolAndStatusAndSplit
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
