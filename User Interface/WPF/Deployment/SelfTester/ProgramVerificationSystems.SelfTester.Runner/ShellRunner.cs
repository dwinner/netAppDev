using ProgramVerificationSystems.SelfTester.Model;
using ProgramVerificationSystems.SelfTester.Model.BusinessLogic;
using ProgramVerificationSystems.SelfTester.Model.Utils;
using ProgramVerificationSystems.SelfTester.UI.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading;

namespace ProgramVerificationSystems.SelfTester.Runner
{
   /// <summary>
   ///    Запуск из командной строки
   /// </summary>
   internal static class ShellRunner
   {
      private const string CisFormatString = "Mode:\t{0,10}.\t\tSolution {1,-35} received the status of\t\t{2,10}";
      private static readonly object SyncObj = new object();
      private static readonly TextWriter DefaultWriter = Console.Out;

      internal static int Start(IEnumerable<string> args)
      {
         var summaryWrapper = new SummaryWrapper();
         var selectionMode = ParseArgs(args);

         var testedViewModels = TestedSolutionsManager.RetrieveSlnInfoViewModels();

         var summaryResults = summaryWrapper.SummaryResults;

         switch (selectionMode)
         {
            case RunSelectionMode.All:
               using (var controller = new AnalyzerController(testedViewModels))
               {
                  controller.AnalysisManagers.CollectionChanged += (sender, changedEventArgs) =>
                  {
                     if (changedEventArgs.Action != NotifyCollectionChangedAction.Add)
                        return;

                     var newManager = changedEventArgs.NewItems.Cast<AnalysisManager>().FirstOrDefault();
                     if (newManager == null)
                        throw new InvalidOperationException("newManager can't be null");

                     newManager.AnalysisStatusChanged += (o, e) =>
                     {
                        var localSlnInfo = e.SolutionInfo;
                        var startupMode = e.StartupMode;
                        var newStatus = e.NewStatus;
                        var exitCode = e.DevenvExitCode;
                        var statDesc = newStatus.GetAnalysisStatusDescription();
                        var slnFileName = newManager.SolutionInfo.SolutionFileName;

                        StatWrapper summaryResult;
                        if (summaryResults.TryGetValue(startupMode, out summaryResult))
                        {
                           switch (newStatus)
                           {
                              case AnalysisStatus.OkFinished:
                                 Interlocked.Increment(ref summaryResult.OkCount);
                                 CisTsLog(() => { DefaultWriter.WriteLine(CisFormatString, startupMode, slnFileName, statDesc); });
                                 break;

                              case AnalysisStatus.DiffFinished:
                                 Interlocked.Increment(ref summaryResult.DiffCount);
                                 CisTsLog(() => { DefaultWriter.WriteLine(CisFormatString, startupMode, slnFileName, statDesc); });
                                 break;

                              case AnalysisStatus.PluginCrashed:
                                 Interlocked.Increment(ref summaryResult.FailCount);
                                 CisTsLog(() =>
                                 {
                                    DefaultWriter.WriteLine(CisFormatString, startupMode, slnFileName, statDesc);
                                    DefaultWriter.WriteLine(Resources.CisFailMsg, slnFileName, startupMode,
                                       e.CrashReason.GetCrashMessage());
                                    DefaultWriter.WriteLine(Resources.WatchActivityLog,
                                       newManager.LogFileName.GetDevenvActivityLog());
                                    if (exitCode != default(int))
                                    {
                                       DefaultWriter.WriteLine(Resources.ExitCodeMessage, exitCode);
                                    }
                                 });
                                 break;
                           }
                        }

                        localSlnInfo.SetStatusValue(startupMode, newStatus);
                     };
                  };

                  controller.RunAll();
               }
               break;

            default:
               DefaultWriter.WriteLine("Unknown option {0}. Press any key to exit...", selectionMode);
               Environment.Exit(0);
               break;
         }

         DefaultWriter.WriteLine(
            "----------------------------------------------------------Stat----------------------------------------------------------");
         foreach (var mode in summaryResults.Keys)
         {
            var oks = summaryResults[mode].OkCount;
            var diffs = summaryResults[mode].DiffCount;
            var fails = summaryResults[mode].FailCount;
            DefaultWriter.WriteLine("Mode: {0}. Oks: {1}. Diffs: {2}. Fails: {3}", mode, oks, diffs, fails);
         }

         return
            summaryResults.Keys.Sum(startMode => summaryResults[startMode].DiffCount) +   // Кол-во Diff'ов...
            summaryResults.Keys.Sum(startMode => summaryResults[startMode].FailCount);  // ... + Кол-во Fail'ов
      }

      private static void CisTsLog(Action logAction)
      {
         lock (SyncObj)
         {
            logAction();
         }
      }

      private static RunSelectionMode ParseArgs(IEnumerable<string> args)
      {
         var selection = RunSelectionMode.None;
         if (
            args.Any(
               arg =>
                  !string.IsNullOrWhiteSpace(arg) &&
                  arg.Trim().Equals("-all", StringComparison.InvariantCultureIgnoreCase)))
         {
            selection = RunSelectionMode.All;
         }

         return selection;
      }

      private class StatWrapper
      {
         public int DiffCount;
         public int FailCount;
         public int OkCount;
      }

      private class SummaryWrapper
      {
         internal readonly IDictionary<StartupConfigurationMode, StatWrapper> SummaryResults =
            new Dictionary<StartupConfigurationMode, StatWrapper>();

         public SummaryWrapper()
         {
            foreach (var mode in Enum.GetValues(typeof(StartupConfigurationMode))
               .Cast<StartupConfigurationMode>()
               .Where(mode => mode != StartupConfigurationMode.All)
               .ToArray())
            {
               SummaryResults.Add(mode, new StatWrapper());
            }
         }
      }
   }
}