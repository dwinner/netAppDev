/*
 * Способ определения, когда процесс начал показывать окно
 */

using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcWndShowDetector
{
   internal static class Program
   {
      private static void Main()
      {
         var procArgumentBuilder = new StringBuilder();
         procArgumentBuilder
             .Append("\"D:\\SelfTester\\src\\teststlport\\teststlport.sln\"")
             .Append(" /command ")
             .Append(
                 "\"PVSStudio.OpenAnalysisReport D:\\SelfTester\\Logs\\Viva64Logs\\VINEVCEV-PC@2015-08-14#09_34_38\\PvsVs2015\\teststlport.plog_Diffs.plog\"");

         var vsStartInfo = new ProcessStartInfo
         {
            FileName = @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\\devenv.exe",
            Arguments = procArgumentBuilder.ToString()
         };
         var vsProc = Process.Start(vsStartInfo);
         if (vsProc == null)
            return;

         vsProc.WaitForInputIdle();

         var tokenSource = new CancellationTokenSource();
         Task.Factory.StartNew(() =>
         {
            var showBoxOnce = false;

            while (!tokenSource.IsCancellationRequested)
            {
               var vsWindowHandle = vsProc.MainWindowHandle;
               if (vsWindowHandle == IntPtr.Zero)
               {
                  Console.WriteLine("devenv is not ready to start");
               }
               else
               {
                  var vsInVisibleState = NativeMethods.IsWindowVisible(vsWindowHandle);
                  if (!showBoxOnce)
                  {
                     MessageBox.Show("Ok");
                     showBoxOnce = true;
                  }

                  Console.WriteLine("devenv is visible: {0}", (vsInVisibleState ? "Yes" : "No"));
               }

               Thread.Sleep(TimeSpan.FromMilliseconds(300));
            }
         }, tokenSource.Token);

         Console.WriteLine("Press enter to exit");
         Console.ReadLine();
         tokenSource.Cancel();
      }
   }
}