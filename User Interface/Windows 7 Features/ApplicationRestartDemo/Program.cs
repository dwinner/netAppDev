using MS.WindowsAPICodePack.Internal;
using System;
using System.Windows.Forms;

namespace ApplicationRestartDemo
{
   internal static class Program
   {
      /// <summary>
      ///     The main entry point for the application.
      /// </summary>
      [STAThread]
      private static void Main()
      {
         CoreHelpers.ThrowIfNotVista();
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new Form1());
      }
   }
}