﻿/**
 * Захват экрана
 */

using System;
using System.Windows.Forms;

namespace ScreenCapture
{
   internal static class Program
   {      
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new ScreenCaptureForm());
      }
   }
}