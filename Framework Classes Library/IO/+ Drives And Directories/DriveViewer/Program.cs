/**
 * Чтение информации об устройствах
 */

using System;
using System.Windows.Forms;

namespace DriveViewer
{
   internal static class Program
   {
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new Form1());
      }
   }
}