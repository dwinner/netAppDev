/**
 * Приложение для тестирования поведения анализаторов PVS-Studio и CppCat
 * ----------------------------------------------------------------------
 * UI-режим: SelfTester.exe
 * CMD-режим с запуском всех проверок: SelfTester.exe -all
 */

using System;
using System.Runtime.CompilerServices;
using System.Windows;
using AppDevUnited.SelfTester.Model;

namespace AppDevUnited.SelfTester.Runner
{
   public static class EntryPoint
   {
      private static LaunchMode _launchMode;

      [STAThread]
      public static int Main(string[] args)
      {
         try
         {
            _launchMode = args != null && args.Length != 0
               ? LaunchMode.CommandLine
               : LaunchMode.Gui;

            RuntimeHelpers.PrepareConstrainedRegions();
            using (var stLauncher = new Launcher(_launchMode, args))
            {
               return stLauncher.Start();
            }
         }
         catch (Exception commonEx)
         {
            var exceptionExpert = new ErrorHandler(commonEx);
            var errorMessage = exceptionExpert.GetErrorMessage();

            switch (_launchMode)
            {
               case LaunchMode.CommandLine:
                  Console.WriteLine(@"{0}: {1}", errorMessage.Item1, errorMessage.Item2);
                  break;
               case LaunchMode.Gui:
                  MessageBox.Show(string.Format("{1}: {0}", errorMessage.Item2, errorMessage.Item1), "Error",
                     MessageBoxButton.OK, MessageBoxImage.Error);
                  break;
               default:
                  goto case LaunchMode.CommandLine;
            }
         }

         return -1;
      }
   }
}