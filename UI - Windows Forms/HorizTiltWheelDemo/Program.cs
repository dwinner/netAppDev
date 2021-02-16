/**
 * Горизонтальная прокрутка
 */

using System;
using System.Windows.Forms;

namespace HorizTiltWheelDemo
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
