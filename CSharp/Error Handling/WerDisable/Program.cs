/*
 * Отключение окна Windows Error Reporting
 */

using System;

namespace WerDisable
{
   internal static class Program
   {
      [STAThread]
      private static void Main()
      {
         using (new ErrorModeManager(ErrorModes.SemNoGpFaultErrorBox))
         {
            throw new Exception("Oops");
         }
      }
   }
}