/**
 * Запоминание местоположения на экране
 */

using System;
using System.Windows.Forms;

namespace SaveScreenLocation
{
   static class Program
   {
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new SaveScreenForm());
      }
   }
}
