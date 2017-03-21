using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppDevUnited.SelfTester.Model.Poco;
using AppDevUnited.SelfTester.Model.Utils;

namespace AppDevUnited.SelfTester.Model.BusinessLogic
{
   /// <summary>
   ///    Генератор запусков
   /// </summary>
   public sealed class AnalyzerController : IDisposable
   {
      private readonly ObservableCollection<AnalysisManager> _analysisManagers =
         new ObservableCollection<AnalysisManager>();

      private readonly int _concurrentTestRunNumber = ApplicationConfigReader.Instance.ConcurrentTestRunsNumber;
      private readonly IEnumerable<SolutionInfoViewModel> _solutionInfoViewModels;
      private readonly object _syncAnalysisManagers = new object();
      private string _currentHtmlLog;
      private DateTime _finishTestingTime;
      private bool _isDisposed;
      private string _launchPartyFolder; // Директория для групп запусков
      private DateTime _startTestingTime;
      private TimeSpan _totalElapsed; // Общее кол-во потраченного на тесты времени
      private int _totalFinishedCount;

      /// <summary>
      ///    Конструктор генератора
      /// </summary>
      /// <param name="solutionInfoViewModels">Набор объектов-моделей для запуска</param>
      public AnalyzerController(IEnumerable<SolutionInfoViewModel> solutionInfoViewModels)
      {
         _solutionInfoViewModels = solutionInfoViewModels;
         CreateTestFiles();
      }

      /// <summary>
      ///    Наблюдаемая коллекция объектов-анализаторов
      /// </summary>
      public ObservableCollection<AnalysisManager> AnalysisManagers
      {
         get { return _analysisManagers; }
      }

      /// <summary>
      ///    Неизменяемый набор объектов-моделей для запуска
      /// </summary>
      private IEnumerable<SolutionInfoViewModel> SlnInfoVms
      {
         get { return _solutionInfoViewModels.ToList().AsReadOnly(); }
      }

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      ~AnalyzerController()
      {
         Dispose(false);
      }

      private void Dispose(bool disposing)
      {
         if (!_isDisposed && disposing)
         {
            _totalElapsed = _finishTestingTime - _startTestingTime;
            using (
               TextWriter htmlLogWriter =
                  new StreamWriter(
                     new FileStream(_currentHtmlLog, FileMode.Append, FileAccess.Write, FileShare.ReadWrite),
                     Encoding.UTF8)
               )
            {
               htmlLogWriter.WriteLine("<hr />");
               htmlLogWriter.WriteLine("Process finished at {0}<br />", _finishTestingTime.ToString("F"));
               htmlLogWriter.WriteLine("<hr />");
               htmlLogWriter.WriteLine("Total testing time: {0}", _totalElapsed.ToString(@"d\.hh\:mm\:ss"));
            }
         }

         _isDisposed = true;
      }

      /// <summary>
      ///    Событие при завершении очередного тестового запуска
      /// </summary>
      public event EventHandler<CurrentAnalysisDoneEventArgs> CurrentAnalysisDone;

      private void OnCurrentAnalysisDone(CurrentAnalysisDoneEventArgs e)
      {
         var handler = CurrentAnalysisDone;
         if (handler != null)
         {
            handler(this, e);
         }
      }

      /// <summary>
      ///    Запуск анализатора для всех наборов
      /// </summary>
      /// <returns>Менеджеры для анализа</returns>
      public void RunAll(AnalysisStatus properInitialStatus = AnalysisStatus.NotStarted)
      {
         _startTestingTime = DateTime.Now;
         _totalFinishedCount = 0;
         var slnsByVersionMap = Utils.GroupSolutionsByVersion(SlnInfoVms);
         var startupConfigurationModes = slnsByVersionMap.Keys.ToArray();
         Array.Sort(startupConfigurationModes, (version1, version2) => (int)version1 - (int)version2);
         foreach (var startupConfigurationMode in startupConfigurationModes)
         {
            var testSolutionInfos = slnsByVersionMap[startupConfigurationMode];
            if (testSolutionInfos != null && testSolutionInfos.Count > 0)
            {
               RunAllByVersion(testSolutionInfos, startupConfigurationMode, properInitialStatus);
            }
         }

         _finishTestingTime = DateTime.Now;
      }

      private void RunAllByVersion(IEnumerable<SolutionInfoViewModel> testSolutionInfos,
         StartupConfigurationMode startupConfigurationMode, AnalysisStatus properInitialStatus)
      {
         var actualModels = testSolutionInfos.ToList();
         var needToRun = Utils.NeedToRun(actualModels, startupConfigurationMode, properInitialStatus);
         if (!needToRun)
         {
            return;
         }

         // Сконфигурируем имя директории
         var currentTestingDir = CreateTestFiles(startupConfigurationMode);
         var factory = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(_concurrentTestRunNumber));
         try
         {
            Task.WaitAll(
               actualModels.OrderBy(model => model.TestSolutionInfo.ReadOrder)
                  .Select(
                     solution =>
                        factory.StartNew(
                           () => Execute(startupConfigurationMode, properInitialStatus, solution, currentTestingDir)))
                  .ToArray());
         }
         catch (AggregateException aggrEx)
         {
            aggrEx.Handle(innerEx =>
            {
               AppEventLogger.Log(
                  string.Format("{0}: {1}", innerEx.Message, innerEx.StackTrace), EventLogEntryType.Error);
               return true;
            });
         }
      }

      private void Execute(StartupConfigurationMode startupConfigurationMode, AnalysisStatus properInitialStatus,
         SolutionInfoViewModel solutionInfo, string currentTestingDir)
      {
         var testSolutionInfo = solutionInfo.TestSolutionInfo;
         var initialStatus = Utils.GetLaunchStatus(startupConfigurationMode, solutionInfo);
         if (initialStatus == AnalysisStatus.None)
         {
            throw new InvalidOperationException("Unable to determine the analysis status");
         }

         if (initialStatus != properInitialStatus)
         {
            return;
         }

         var logFileName = Utils.GetLogFileName(currentTestingDir, testSolutionInfo);
         var analysisManager = new AnalysisManager(
            solutionInfo, startupConfigurationMode, initialStatus, logFileName, _currentHtmlLog);

         lock (_syncAnalysisManagers)
         {
            AnalysisManagers.Add(analysisManager);
         }

         analysisManager.Run();
         OnCurrentAnalysisDone(
            new CurrentAnalysisDoneEventArgs(Interlocked.Increment(ref _totalFinishedCount)));
      }

      private void CreateTestFiles()
      {
         _launchPartyFolder = string.Format("{0}{1}{2}", ApplicationConfigReader.Instance.Viva64Logs,
            Path.DirectorySeparatorChar, Utils.GenerateNewLaunchParty());
         if (!Directory.Exists(_launchPartyFolder))
         {
            Directory.CreateDirectory(_launchPartyFolder);
         }

         _currentHtmlLog = string.Format("{0}{1}{2}.html", _launchPartyFolder, Path.DirectorySeparatorChar,
            Path.GetFileName(_launchPartyFolder));
         var createdNew = Utils.CreateHtmlLog(_currentHtmlLog);
         if (!createdNew)
         {
            throw new InvalidOperationException(string.Format("Log file {0} already exists", _currentHtmlLog));
         }
      }

      private string CreateTestFiles(StartupConfigurationMode startupConfigurationMode)
      {
         var currentTestingDir = string.Format(
            "{0}{1}{2}", _launchPartyFolder, Path.DirectorySeparatorChar, startupConfigurationMode);
         if (!Directory.Exists(currentTestingDir))
         {
            Directory.CreateDirectory(currentTestingDir);
         }

         return currentTestingDir;
      }

      #region Локальные утилиты класса

      private static class Utils
      {
         /// <summary>
         ///    Получает строку статуса для версии Visual Studio
         /// </summary>
         /// <param name="vsVersion">Версия Visual Studio</param>
         /// <param name="solutionInfo">Информация о VS-решении</param>
         /// <returns>Строка статуса для версии Visual Studio</returns>
         public static AnalysisStatus GetLaunchStatus(StartupConfigurationMode vsVersion,
            SolutionInfoViewModel solutionInfo)
         {
            AnalysisStatus launchStatusOn;
            switch (vsVersion)
            {
               case StartupConfigurationMode.PvsVs2010:
                  launchStatusOn = solutionInfo.LaunchStatusOnPvsVs2010;
                  break;
               case StartupConfigurationMode.PvsVs2012:
                  launchStatusOn = solutionInfo.LaunchStatusOnPvsVs2012;
                  break;
               case StartupConfigurationMode.PvsVs2013:
                  launchStatusOn = solutionInfo.LaunchStatusOnPvsVs2013;
                  break;
               case StartupConfigurationMode.PvsVs2015:
                  launchStatusOn = solutionInfo.LaunchStatusOnPvsVs2015;
                  break;

               default:
                  throw new InvalidOperationException(string.Format("Unsupported Visual Studio version: {0}",
                     vsVersion));
            }

            return launchStatusOn;
         }

         public static bool CreateHtmlLog(string currentHtmlLog)
         {
            if (File.Exists(currentHtmlLog))
            {
               return false;
            }

            using (TextWriter outputWriter =
               new StreamWriter(
                  new FileStream(currentHtmlLog, FileMode.CreateNew, FileAccess.Write, FileShare.Read), Encoding.UTF8))
            {
               outputWriter.WriteLine("--== Viva64 self-testing log file ==--<br />");
               outputWriter.WriteLine("Process started at {0}<br />", DateTime.Now.ToString("F"));
            }

            return true;
         }

         /// <summary>
         ///    Группировка решений по версиям VS
         /// </summary>
         /// <param name="slnViewModels">Модели решений</param>
         /// <returns>Карта версия => набор решений</returns>
         public static IDictionary<StartupConfigurationMode, IList<SolutionInfoViewModel>> GroupSolutionsByVersion(
            IEnumerable<SolutionInfoViewModel> slnViewModels)
         {
            var slnsByVersionMap = new Dictionary<StartupConfigurationMode, IList<SolutionInfoViewModel>>();
            var vsVersions = Enum.GetValues(typeof(StartupConfigurationMode));

            foreach (
               var vsVersion in
                  vsVersions.Cast<StartupConfigurationMode>()
                     .Where(vsVersion => vsVersion != StartupConfigurationMode.All))
               slnsByVersionMap[vsVersion] = new List<SolutionInfoViewModel>();

            var solutions = slnViewModels.ToArray();
            foreach (StartupConfigurationMode vsVersion in vsVersions)
            {
               if (vsVersion == StartupConfigurationMode.All)
               {
                  continue;
               }

               foreach (var slnVm in solutions)
               {
                  slnsByVersionMap[vsVersion].Add(slnVm);
               }
            }

            return slnsByVersionMap;
         }

         /// <summary>
         ///    Генерирование нового имени папки для группы тестовых запусков
         /// </summary>
         /// <returns>Новое имя папки для группы тестовых запусков</returns>
         public static string GenerateNewLaunchParty()
         {
            return
               string.Format("{0}@{1}", Environment.MachineName, DateTime.Now.ToString("s"))
                  .Replace(':', '_')
                  .Replace('T', '#');
         }

         /// <summary>
         ///    Нужно ли создавать конфигурацию для запуска под конкретную версию
         /// </summary>
         /// <param name="testSolutionInfos">VS-решения</param>
         /// <param name="vsVersion">Версия VS</param>
         /// <param name="properInitialStatus">Статус, под которым нужно звапускать анализ</param>
         /// <returns>true, если есть хотя бы один проект для запуска под конкретную версию, false - в противном случае</returns>
         public static bool NeedToRun(IEnumerable<SolutionInfoViewModel> testSolutionInfos,
            StartupConfigurationMode vsVersion, AnalysisStatus properInitialStatus)
         {
            var needToRun = false;
            switch (vsVersion)
            {
               case StartupConfigurationMode.PvsVs2010:
                  needToRun =
                     testSolutionInfos.Any(slnInfo => slnInfo.LaunchStatusOnPvsVs2010 == properInitialStatus);
                  break;
               case StartupConfigurationMode.PvsVs2012:
                  needToRun =
                     testSolutionInfos.Any(slnInfo => slnInfo.LaunchStatusOnPvsVs2012 == properInitialStatus);
                  break;
               case StartupConfigurationMode.PvsVs2013:
                  needToRun =
                     testSolutionInfos.Any(slnInfo => slnInfo.LaunchStatusOnPvsVs2013 == properInitialStatus);
                  break;
               case StartupConfigurationMode.PvsVs2015:
                  needToRun =
                     testSolutionInfos.Any(slnInfo => slnInfo.LaunchStatusOnPvsVs2015 == properInitialStatus);
                  break;
            }

            return needToRun;
         }

         /// <summary>
         ///    Получение файла-лога
         /// </summary>
         /// <param name="currentTestingDir">Текущая директория запуска</param>
         /// <param name="testSolutionInfo">Тестируемое решение</param>
         /// <returns>Файл-лога</returns>
         public static string GetLogFileName(string currentTestingDir, TestSolutionInfo testSolutionInfo)
         {
            return currentTestingDir + Path.DirectorySeparatorChar +
                   Path.GetFileNameWithoutExtension(testSolutionInfo.AbsSolutionFileName) +
                   CoreExtensions.EtalonLogExt;
         }
      }

      #endregion
   }
}