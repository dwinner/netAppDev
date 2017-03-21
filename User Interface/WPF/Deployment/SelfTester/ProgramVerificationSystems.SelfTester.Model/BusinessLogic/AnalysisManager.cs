using ProgramVerificationSystems.SelfTester.Model.CommonTypes;
using ProgramVerificationSystems.SelfTester.Model.Poco;
using ProgramVerificationSystems.SelfTester.Model.Utils;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using PluginDiff = ProgramVerificationSystems.PVSStudio.PVSDiff;

namespace ProgramVerificationSystems.SelfTester.Model.BusinessLogic
{
   /// <summary>
   ///    Менеджер анализа
   /// </summary>
   public sealed class AnalysisManager
   {
      private const string SnapshotExt = ".png";
      private static readonly int HangTimeMilliseconds = 3600000 * ApplicationConfigReader.Instance.HangTime;

      private readonly string _htmlLog; // Текущий Html-лог
      private readonly string _logFileName;
      private readonly string _snapshotFilename;
      private AnalysisStatus _currentStatus; // Текущий статус анализа
      private DateTime _endTime;
      private DateTime _startTime;

      /// <summary>
      ///    Конструктор менеджера для тестового анализа
      /// </summary>
      /// <param name="slnInfoVm">Тестируемое решение</param>
      /// <param name="startupConfigurationMode">Версия Visual Studion, для которой нужно запустить решение</param>
      /// <param name="status">Начальный статус анализа</param>
      /// <param name="logFileName">Файл лога с результатами работы анализатора</param>
      /// <param name="htmlLog">Html-лог</param>
      public AnalysisManager(SolutionInfoViewModel slnInfoVm, StartupConfigurationMode startupConfigurationMode,
         AnalysisStatus status, string logFileName, string htmlLog)
      {
         SolutionInfo = slnInfoVm;
         StartupMode = startupConfigurationMode;
         _logFileName = logFileName;
         _snapshotFilename = string.Format("{0}{1}", _logFileName, SnapshotExt);
         _currentStatus = status;
         _htmlLog = htmlLog;
      }

      /// <summary>
      ///    Тестируемое решение
      /// </summary>
      public SolutionInfoViewModel SolutionInfo { get; private set; }

      private CrashReason Reason { get; set; }

      private int DevenvExitCode { get; set; }

      private AnalysisStatus CurrentStatus
      {
         get { return _currentStatus; }
         set
         {
            if (_currentStatus == value)
               return;

            _currentStatus = value;
            OnAnalysisStatusChanged(
               new TestAnalysisEventArgs(value, SolutionInfo, StartupMode, Reason, DevenvExitCode));
         }
      }

      /// <summary>
      ///    Файл лога с результатами работы анализатора
      /// </summary>
      public string LogFileName
      {
         get { return _logFileName; }
      }

      /// <summary>
      ///    Версия Visual Studion, для которой нужно запустить решение
      /// </summary>
      private StartupConfigurationMode StartupMode { get; set; }

      /// <summary>
      ///    Событие при смене статуса анализа
      /// </summary>
      public event EventHandler<TestAnalysisEventArgs> AnalysisStatusChanged;

      private void OnAnalysisStatusChanged(TestAnalysisEventArgs e)
      {
         var statusChanged = AnalysisStatusChanged;
         if (statusChanged != null)
         {
            statusChanged(this, e);
         }
      }

      /// <summary>
      ///    Запуск анализа
      /// </summary>
      public void Run()
      {
         _startTime = DateTime.Now;

         var solutionFileName = SolutionInfo.TestSolutionInfo.AbsSolutionFileName;
         var visualStudioPath = string.Format("{0}{1}devenv.exe",
            InstallationInfo.RetrieveVisualStudioPath(StartupMode),
            Path.DirectorySeparatorChar);
         var selfTesterRootPath = ApplicationConfigReader.SelfTesterRootPath;
         var srcEtalonFolder = ApplicationConfigReader.Instance.SrcEtalonFolder;
         bool isHangout, success;

         #region Подготовка решения к анализу

         CurrentStatus = AnalysisStatus.InUpgrading;
         const int retryCount = 2; // Кол-во попыток обновить решение
         const int halfMinute = 30;
         var upgradeStatus = default(AnalysisStatus);
         Reason = default(CrashReason);

         for (var i = 0; i < retryCount; i++)
         {
            using (var upgradeSolutionWrapper =
               new UpgradeSolutionWrapper(
                  solutionFileName, visualStudioPath, selfTesterRootPath, srcEtalonFolder, _logFileName, _snapshotFilename))
            {
               int devenvExitCode;
               success = upgradeSolutionWrapper.Upgrade(out isHangout, out devenvExitCode);
               DevenvExitCode = devenvExitCode;
            }

            if (isHangout)
            {
               Reason = CrashReason.HangoutOnUpgrade;
               upgradeStatus = AnalysisStatus.PluginCrashed;
               Thread.Sleep(TimeSpan.FromSeconds(halfMinute));
               continue;
            }

            if (!success)
            {
               Reason = CrashReason.UpgradeFail;
               upgradeStatus = AnalysisStatus.PluginCrashed;
               Thread.Sleep(TimeSpan.FromSeconds(halfMinute));
               continue;
            }

            upgradeStatus = default(AnalysisStatus);
            break;
         }

         if (upgradeStatus != default(AnalysisStatus))
         {
            DevenvExitCode = default(int);
            CurrentStatus = upgradeStatus;
         }

         #endregion

         #region Анализ решения

         CurrentStatus = AnalysisStatus.InProgress;
         using (
            var analysisSolutionWrapper = new AnalysisSolutionWrapper(visualStudioPath, SolutionInfo.TestSolutionInfo,
               _logFileName))
         {
            int devenvExitCode;
            success = analysisSolutionWrapper.ProcessAnalysis(out isHangout, out devenvExitCode);
            DevenvExitCode = devenvExitCode;
         }

         if (isHangout)
         {
            Reason = CrashReason.HangoutOnAnalysis;
            CurrentStatus = AnalysisStatus.PluginCrashed;
         }

         #endregion

         Reason = success ? CrashReason.None : CrashReason.NotZeroExitCode;
         CurrentStatus = success ? AnalysisStatus.InProgress : AnalysisStatus.PluginCrashed;
         _endTime = DateTime.Now;

         if (CurrentStatus == AnalysisStatus.PluginCrashed)
         {
            var slnFileName = SolutionInfo.TestSolutionInfo.AbsSolutionFileName;
            var etalonLog = Utils.GetEtalonLog(slnFileName);
            var totalElapsed = _endTime.Subtract(_startTime).Duration();
            FlushHtml(
               string.Format("{0}{1}<br/><font color='red'><b>{2}</b></font><br/>",
               GetHeaderLine(slnFileName, etalonLog, totalElapsed),
               Reason.GetCrashMessage(),
               Reason.ToString().ToUpper()));
            return;
         }

         CurrentStatus = AnalysisStatus.СomparingLogs;
         CurrentStatus = CompareLogs();
      }

      /// <summary>
      ///    Шаблонный метод завершения анализа
      /// </summary>
      /// <returns>Статус завершения</returns>
      private AnalysisStatus CompareLogs()
      {
         var slnFileName = SolutionInfo.TestSolutionInfo.AbsSolutionFileName;
         var etalonLog = Utils.GetEtalonLog(slnFileName);
         var totalElapsed = _endTime.Subtract(_startTime).Duration();
         AppEventLogger.Log(totalElapsed, StartupMode, SolutionInfo.TestSolutionInfo);
         var headerLine = GetHeaderLine(slnFileName, etalonLog, totalElapsed);

         if (!File.Exists(_logFileName))
         {
            Reason = CrashReason.NoResults;
            var crashMessage = Reason.GetCrashMessage();
            FlushHtml(string.Format("{0}{1}<br/><font color='red'><b>{2}</b></font><br/>",
               headerLine,
               crashMessage,
               Reason.ToString().ToUpper()));
            return AnalysisStatus.PluginCrashed;
         }

         if (!File.Exists(etalonLog))
         {
            Reason = CrashReason.NoEtalon;
            var crashMessage = CrashReason.None.GetCrashMessage();
            FlushHtml(
               string.Format("{0}{1}<br/><font color='red'><b>{2}</b></font><br/>",
               headerLine,
               crashMessage,
               Reason.ToString().ToUpper()));
            return AnalysisStatus.NoSuchEtalon;
         }

         var pluginDiff = new PluginDiff();
         var fullSet = pluginDiff.GetDiff(etalonLog, _logFileName);
         var resultStatus = pluginDiff.Output.Succeeded ? AnalysisStatus.OkFinished : AnalysisStatus.DiffFinished;

         var logCmpBuilder = new StringBuilder(headerLine, 0x400);
         logCmpBuilder.Append("<i>Logs comparison result:</i> ");
         if (pluginDiff.Output.Succeeded)
         {
            logCmpBuilder.AppendLine("<font color='green'><B>OK</B></font><br/>");
         }
         else
         {
            logCmpBuilder
               .AppendLine("<font color='red'><b>HASDIFF</b></font><br/>")
               .AppendLine("<i>Comparison function output:</i><br/><br/>")
               .AppendLine("<font color='red'>");
            foreach (DataRow dataRow in pluginDiff.Output.Missings.Rows)
            {
               logCmpBuilder.AppendFormat("{0}<br/>", Utils.GenerateOutputWindowMessage(dataRow));
            }
            logCmpBuilder.AppendLine("</font>").AppendLine("<font color='green'>");
            foreach (DataRow dataRow in pluginDiff.Output.Additionals.Rows)
            {
               logCmpBuilder.AppendFormat("{0}<br/>", Utils.GenerateOutputWindowMessage(dataRow));
            }
            logCmpBuilder.AppendLine("</font>").AppendLine("<font color='blue'>");
            foreach (DataRow dataRow in pluginDiff.Output.Modifies.Rows)
            {
               logCmpBuilder.AppendFormat("{0}</br>", Utils.GenerateOutputWindowMessage(dataRow));
            }
            logCmpBuilder.AppendLine("</font>");
            logCmpBuilder.AppendFormat("<br/><i>The results are saved to:</i></br>{0}_Diffs.plog<br/>", _logFileName);

            fullSet.WriteXml(string.Format("{0}_Diffs.plog", _logFileName), XmlWriteMode.WriteSchema);
            fullSet.Tables.Clear();
         }

         var content = logCmpBuilder.ToString();
         FlushHtml(content);

         Reason = CrashReason.None;
         return resultStatus;
      }

      #region Локальные утилиты класса

      private static class Utils
      {
         /// <summary>
         ///    Получение команды запуска plugin'а для процесса Visual Studio, devenv.exe
         /// </summary>
         /// <param name="slnInfo">Информация о vs-решении</param>
         /// <param name="logFileName">Лог для записи диагностик</param>
         /// <returns>Команда запуска plugin'а для процесса Visual Studio, devenv.exe</returns>
         public static string GetPluginCommand(TestSolutionInfo slnInfo, string logFileName)
         {
            var slnFileName = slnInfo.AbsSolutionFileName;
            var platform = slnInfo.Platform.ToString();
            var configName = slnInfo.ConfigurationMode.ToString();
            var preprocessor = slnInfo.PreprocessorType.ToString();
            return string.Format("\"{0}\" /command \"PVSStudio.CheckSolution {1}|{2}|{3}||{4}\"", slnFileName, platform,
               configName, logFileName, preprocessor);
         }

         public static string GenerateOutputWindowMessage(DataRow row)
         {
            if ((uint)row[TableIndexes.Level] >= 1 && (uint)row[TableIndexes.Level] <= 3)
            {
               var msgFormat = string.Format("{{0}}({{1}}): error {{2}}: {{3}}{0}", Environment.NewLine);
               var fileNameFull = row[TableIndexes.File] as string;
               if (fileNameFull != null && fileNameFull.StartsWith(ApplicationSettings.SourceTreeRootMarker))
               {
                  fileNameFull = fileNameFull.Replace(ApplicationSettings.SourceTreeRootMarker,
                     ApplicationConfigReader.SelfTesterRootPath);
               }

               var msg = string.Format(msgFormat, fileNameFull,
                  (bool)row[TableIndexes.Trial] ? Resources.TrialLineNumber : row[TableIndexes.Line].ToString(),
                  row[TableIndexes.ErrorCode], row[TableIndexes.Message]);

               return msg;
            }

            return (string)row[TableIndexes.Message];
         }

         public static string GetEtalonLog(string slnFileName)
         {
            return string.Format("{0}{1}{2}{3}", ApplicationConfigReader.Instance.EtalonLogFolder,
               Path.DirectorySeparatorChar, Path.GetFileNameWithoutExtension(slnFileName),
               ApplicationConfigReader.Instance.DefaultEtalonLogFileExtension);
         }
      }

      #endregion

      #region Служебные методы

      private void FlushHtml(string content)
      {
         if (_snapshotFilename != null && File.Exists(_snapshotFilename))
         {
            var relativeToHtmlLog = Path.Combine(Directory.GetParent(_snapshotFilename).Name,
               Path.GetFileName(_snapshotFilename));
            content += string.Format(" (<a href='{0}'>See Snapshot</a>)", relativeToHtmlLog);
         }

         using (
            TextWriter outputTextWriter =
               new StreamWriter(new FileStream(_htmlLog, FileMode.Append, FileAccess.Write, FileShare.ReadWrite)))
         {
            outputTextWriter.Write(content);
            // NOTE: Здесь возможна гонка, однако операция выполняется достаточно быстро
         }
      }

      private string GetHeaderLine(string slnFileName, string etalonLog, TimeSpan totalElapsed)
      {
         return
            string.Format("<hr />{0}{1}{2}{3}",
               string.Format("<b>---=== Checked file: {0} at {1} ===----</b><br/>", slnFileName,
                  DateTime.Now.ToString("D")),
               string.Format("<i>Viva64 log saved to:</i> {0}<br/>",
                  (File.Exists(_logFileName) ? _logFileName : " No log")),
               string.Format("<i>Etalon log:</i> {0}<br/>", (File.Exists(etalonLog) ? etalonLog : "No etalon")),
               string.Format("<u><b>Start time: {0}. End time: {1}. Elapsed time: {2}</b></u><br />",
                  _startTime.ToString("T"), _endTime.ToString("T"), totalElapsed.ToString(@"hh\:mm\:ss")));
      }

      #endregion

      #region Служебные классы

      /// <summary>
      ///    Оболочка для запуска процесса обновления решения
      /// </summary>
      private sealed class UpgradeSolutionWrapper : IDisposable
      {
         private readonly string _logFileName;
         private readonly string _selfTesterRootPath;
         private readonly string _snapshot;
         private readonly string _solutionFileName;
         private readonly string _srcEtalonFolder;
         private readonly string _visualStudioPath;

         private Process _upgradeSlnProcess;

         public UpgradeSolutionWrapper(string solutionFileName, string visualStudioPath, string selfTesterRootPath,
            string srcEtalonFolder, string logFileName, string snapshot)
         {
            _solutionFileName = solutionFileName;
            _selfTesterRootPath = selfTesterRootPath;
            _srcEtalonFolder = srcEtalonFolder;
            _visualStudioPath = visualStudioPath;
            _logFileName = logFileName;
            _snapshot = snapshot;
         }

         public void Dispose()
         {
         }

         /// <summary>
         ///    Обновление решения
         /// </summary>
         /// <param name="isHangout">
         ///    true, если время на обновление вышло, а процесс все еще не завершен, false - если процесс
         ///    завершился
         /// </param>
         /// <param name="devenvExitCode"></param>
         /// <returns>true, если обновление прошло успешно, false - в противном случае</returns>
         public bool Upgrade(out bool isHangout, out int devenvExitCode)
         {
            var restoreResult = AnalyzerUtilities.IndividualRestore(_solutionFileName, _selfTesterRootPath,
               _srcEtalonFolder);
            if (!restoreResult.Item1)
            {
               isHangout = false;
               devenvExitCode = default(int);
               AppEventLogger.Log(restoreResult.Item2, EventLogEntryType.Error);
               return false;
            }

            try
            {
               ConfigureUpgradeProcess();
               _upgradeSlnProcess.Start();
               var isSelfExit = IsSelfExit();
               if (!isSelfExit) // Если сам не завершился, значит завис
               {
                  ProcessUtils.MandatoryKill(_upgradeSlnProcess);
                  isHangout = true;
                  if (_upgradeSlnProcess.HasExited)
                  {
                     devenvExitCode = _upgradeSlnProcess.ExitCode;
                     return false;
                  }
               }

               isHangout = false;
               devenvExitCode = _upgradeSlnProcess.HasExited ? _upgradeSlnProcess.ExitCode : default(int);
               return devenvExitCode == 0;
            }
            catch (Exception ex)
            {
               AppEventLogger.Log(
                  string.Format("Error message: {0}{1}{2}", ex.Message, Environment.NewLine, ToString()),
                  EventLogEntryType.Error);
               isHangout = false;
               devenvExitCode = default(int);
               return false;
            }
         }

         private bool IsSelfExit()
         {
            var isSelfExit = false;

            try
            {
               var startTime = _upgradeSlnProcess.StartTime;
               isSelfExit = _upgradeSlnProcess.WaitForExit(HangTimeMilliseconds);
               var workingTime = (_upgradeSlnProcess.ExitTime - startTime).Duration();
               var staleDuration = TimeSpan.FromMilliseconds(HangTimeMilliseconds).Duration();
               if (workingTime >= staleDuration)
               {
                  try
                  {
                     AnalyzerUtilities.GetScreenSnapshot(_snapshot);
                  }
                  catch (Win32Exception win32Ex)
                  {
                     AppEventLogger.Log(
                        string.Format("Message {0}. Error code: {1}", win32Ex.Message, win32Ex.NativeErrorCode),
                        EventLogEntryType.Warning);
                  }
               }
            }
            catch (InvalidOperationException invOpEx)
            {
               AppEventLogger.Log(invOpEx.Message, EventLogEntryType.Warning);
            }

            return isSelfExit;
         }

         private void ConfigureUpgradeProcess()
         {
            _upgradeSlnProcess = new Process
            {
               StartInfo = new ProcessStartInfo
               {
                  FileName = _visualStudioPath,
                  Arguments =
                     string.Format("/Upgrade \"{0}\" /Log \"{1}\"", _solutionFileName,
                        _logFileName.GetDevenvActivityLog()),
                  WindowStyle = ProcessWindowStyle.Hidden
               }
            };
         }

         public override string ToString()
         {
            return
               string.Format("SolutionFileName: {0}, LogFileName: {1}", _solutionFileName, _logFileName);
         }
      }

      /// <summary>
      ///    Оболочка для процесса анализа решения
      /// </summary>
      private sealed class AnalysisSolutionWrapper : IDisposable
      {
         private readonly string _plog;
         private readonly TestSolutionInfo _solutionInfo;
         private readonly string _visualStudioPath;
         private Process _analyzerProcess;

         public AnalysisSolutionWrapper(string visualStudioPath, TestSolutionInfo solutionInfo, string plog)
         {
            _plog = plog;
            _solutionInfo = solutionInfo;
            _visualStudioPath = visualStudioPath;
         }

         public void Dispose()
         {
         }

         /// <summary>
         ///    Анализ решения
         /// </summary>
         /// <param name="isHangout">
         ///    true, если время, отведенное для анализа вышло, а процесс все еще не завершен, false - если
         ///    процесс завершился
         /// </param>
         /// <param name="devenvExitCode"></param>
         /// <returns>true, если процесс анализа прошел успешно, false - в противном случае</returns>
         public bool ProcessAnalysis(out bool isHangout, out int devenvExitCode)
         {
            const string errorDetectedSuffix = ".plog.exception_messages.txt";

            try
            {
               ConfigureAnalisysProcess();
               var start = DateTime.Now;
               _analyzerProcess.Start();
               var runningQuota = TimeSpan.FromMinutes(ApplicationConfigReader.Instance.HangTime).Duration();

               var plogPath = Path.GetDirectoryName(_plog);
               Debug.Assert(plogPath != null, "plogPath != null");

               var destinationPlog = Path.GetFileName(_plog);
               var plogCreatedEvent = new ManualResetEventSlim();
               var plogWatcher = new FileSystemWatcher(plogPath, "*.plog")
               {
                  EnableRaisingEvents = true,
                  IncludeSubdirectories = false,
                  NotifyFilter = NotifyFilters.FileName
               };
               plogWatcher.Created += (sender, args) =>
               {
                  var createdName = args.Name;
                  if (string.Equals(createdName, destinationPlog))
                  {
                     plogCreatedEvent.Set();
                  }
               };

               var destinationErrorPlog = string.Format("{0}{1}", Path.GetFileNameWithoutExtension(_plog), errorDetectedSuffix);
               var errorCreatedEvent = new ManualResetEventSlim();
               var errorWatcher = new FileSystemWatcher(plogPath, string.Format("*{0}", errorDetectedSuffix))
               {
                  EnableRaisingEvents = true,
                  IncludeSubdirectories = false,
                  NotifyFilter = NotifyFilters.FileName
               };
               errorWatcher.Created += (sender, args) =>
               {
                  var createdName = args.Name;
                  if (string.Equals(destinationErrorPlog, createdName))
                  {
                     errorCreatedEvent.Set();
                  }
               };

               var waitingIndex = WaitHandle.WaitAny(
                  new[]
                  {
                     plogCreatedEvent.WaitHandle,
                     errorCreatedEvent.WaitHandle
                  }, TimeSpan.FromMinutes(ApplicationConfigReader.Instance.HangTime));

               var plogAppeared = waitingIndex == 0;
               if (plogAppeared)
               {
                  plogAppeared = AnalyzerUtilities.WaitForFileReleased(_plog, start, runningQuota);
                  AppEventLogger.Log(
                     string.Format("File {0} is {1}", _plog, (plogAppeared ? "Released" : "Time out")),
                     plogAppeared ? EventLogEntryType.SuccessAudit : EventLogEntryType.FailureAudit);
               }

               isHangout = !plogAppeared;
               ProcessUtils.MandatoryKill(_analyzerProcess);
               devenvExitCode = default(int);
               return !isHangout;
            }
            catch (Exception ex)
            {
               AppEventLogger.Log(
                  string.Format("Error message: {0}. Stack trace: {1}{2}{3}", ex.Message, ex.StackTrace,
                     Environment.NewLine, ToString()), EventLogEntryType.Error);
               devenvExitCode = default(int);
               return (isHangout = !File.Exists(_plog));
            }
         }

         private void ConfigureAnalisysProcess()
         {
            _analyzerProcess = new Process
            {
               StartInfo =
               {
                  FileName = _visualStudioPath,
                  Arguments = Utils.GetPluginCommand(_solutionInfo, _plog),
                  WindowStyle = ProcessWindowStyle.Hidden
               }
            };
         }

         public override string ToString()
         {
            return
               string.Format("LogFileName: {0}, SolutionInfo: {1}", _plog, _solutionInfo);
         }
      }

      #endregion
   }
}