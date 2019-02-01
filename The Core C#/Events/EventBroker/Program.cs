/**
 * Брокер событий
 */

using System;
using System.Windows.Forms;

namespace EventBrokerDemo
{
   static class Program
   {
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new EventBlokerForm());
      }
   }
}
