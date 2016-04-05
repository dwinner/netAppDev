/**
 * Прямое обращение к пикселям
 */

using System;
using System.Windows.Forms;

namespace BitmapDirect
{
   internal static class Program
   {      
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new BitmapDirectForm());
      }
   }
}