﻿/**
 * Трансформации
 */

using System;
using System.Windows.Forms;

namespace Transforms
{
   internal static class Program
   { 
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new TansformForm());
      }
   }
}