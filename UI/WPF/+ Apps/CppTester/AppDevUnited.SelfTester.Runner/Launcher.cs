using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using System.Windows;
using AppDevUnited.SelfTester.Model;
using AppDevUnited.SelfTester.Model.Utils;

namespace AppDevUnited.SelfTester.Runner
{
   /// <summary>
   ///    Запускающий
   /// </summary>
   internal sealed class Launcher : IDisposable
   {
      private const string RestoreBatFile = "restore_src.cmd";
      private static readonly string[] _ProcessesToKillOnStSessionEnd =
         ApplicationConfigReader.Instance.SessionEndProcessesToKill;
      private static readonly Regex _SolutionRegex =
         new Regex(@"\b(\w+).sln\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
      private readonly string[] _args;
      private readonly LaunchMode _launchMode;
      private FaultProcessMonitor _faultProcessMonitor;
      private bool _isDisposed;
      private string _restoreBatFile;

      internal Launcher(LaunchMode launchMode, string[] args)
      {
         _launchMode = launchMode;
         _args = args;
         Init();
      }

      public void Dispose()
      {
         Dispose(true);
      }

      private void Init()
      {
         ChangeConfigurationSettings();
         _restoreBatFile = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, RestoreBatFile);
         var currentProcessId = (uint)Process.GetCurrentProcess().Id;
         _faultProcessMonitor =
            new FaultProcessMonitor(currentProcessId, ApplicationConfigReader.Instance.FaultProcessNames);
         _faultProcessMonitor.Faulted += (o, faultedEventArgs) => TakeScreenshot(faultedEventArgs.FaultProcesses);
         _faultProcessMonitor.Hangout += (o, hangoutEventArgs) => TakeScreenshot(hangoutEventArgs.HangoutProcesses);
         _faultProcessMonitor.Start();
      }

      private static void TakeScreenshot(IEnumerable<Process> processes)
      {
         foreach (var solutionName in from staleProc in processes
                                      select staleProc.GetCommandLine()
                                         into commandLine
                                         select _SolutionRegex.Matches(commandLine)
                                            into slnMatch
                                            where slnMatch.Count > 0 && slnMatch[0].Groups.Count > 1
                                            select slnMatch[0].Groups[1].Value)
         {
            TakeScreenshot(solutionName);
         }
      }

      private static void TakeScreenshot(string solutionName)
      {
         var lastSessionDir = LastSessionDir;
         if (lastSessionDir != null)
         {
            AnalyzerUtilities.GetScreenSnapshot(Path.Combine(lastSessionDir.FullName, string.Format("{0}.png", solutionName)));
         }
      }

      private static DirectoryInfo LastSessionDir
      {
         get
         {
            return
               Directory.GetDirectories(ApplicationConfigReader.Instance.Viva64Logs)
                  .Select(path => new DirectoryInfo(path))
                  .OrderByDescending(info => info.CreationTime)
                  .FirstOrDefault();
         }
      }

      public int Start()
      {
         if (_isDisposed)
            throw new ObjectDisposedException(GetType().FullName);

         return InternalStart(_launchMode);
      }

      private int InternalStart(LaunchMode launchMode)
      {
         switch (launchMode)
         {
            case LaunchMode.CommandLine:
               return ShellRunner.Start(_args);

            case LaunchMode.Gui:
               Application selfTesterApplication = new SelfTesterApplication();
               selfTesterApplication.MainWindow.Show();
               Unmanaged.FreeConsole();
               return selfTesterApplication.Run();

            default:
               throw new InvalidEnumArgumentException("launchMode", (int)launchMode, typeof(LaunchMode));
         }
      }

      private static void ChangeConfigurationSettings()
      {
         PluginConfigurator.ConfigurePvsStudio();
      }

      private static void RestoreConfigurationSettings()
      {
         PluginConfigurator.RestorePvsStudio();
      }

      ~Launcher()
      {
         Dispose(false);
      }

      private void Dispose(bool disposing)
      {
         if (!_isDisposed)
         {
            if (disposing && _faultProcessMonitor != null)
            {
               _faultProcessMonitor.Stop();
               _ProcessesToKillOnStSessionEnd.SelectMany(Process.GetProcessesByName)
                  .ToList()
                  .ForEach(procToKill => ProcessUtils.MandatoryKill(procToKill));
            }

            BulletProofDispose();
         }

         _isDisposed = true;
      }

      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
      private void BulletProofDispose()
      {
         RestoreConfigurationSettings();
         if (File.Exists(_restoreBatFile))
         {
            var restoreStartInfo = new ProcessStartInfo(RestoreBatFile) { UseShellExecute = false, CreateNoWindow = true };
            Process.Start(restoreStartInfo);
         }
      }
   }
}