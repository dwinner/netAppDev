using System;
using System.Windows.Forms;

namespace AmbientSensorDemo
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