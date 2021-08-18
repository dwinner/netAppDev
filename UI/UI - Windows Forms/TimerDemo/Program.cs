/**
 * Применение таймера
 */

using System;
using System.Windows.Forms;

namespace TimerDemo
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
