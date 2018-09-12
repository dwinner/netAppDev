/**
 * Удаление тегов из HTML-кода
 */

using System;
using System.Windows.Forms;

namespace StripHtml
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
