/**
 * Обращение к библиотекам Windows 7
 */

using System;
using System.Windows.Forms;

namespace Windows7Features
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
