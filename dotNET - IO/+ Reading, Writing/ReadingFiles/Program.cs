/**
 * Простейший способ чтения файла
 */

using System;
using System.Windows.Forms;

namespace ReadingFiles
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