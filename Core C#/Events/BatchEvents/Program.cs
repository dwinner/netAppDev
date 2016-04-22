/**
 * Объединение нескольких событий в одно
 */

using System;
using System.Windows.Forms;

namespace BatchEvents
{
   static class Program
   {
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new BatchEventsForm());
      }
   }
}
