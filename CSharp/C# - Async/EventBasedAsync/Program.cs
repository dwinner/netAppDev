/**
 * Асинхронный событийно-управляемый пэттерн (EAP)
 */

using System;
using System.Windows.Forms;

namespace _02_EventBasedAsync
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
