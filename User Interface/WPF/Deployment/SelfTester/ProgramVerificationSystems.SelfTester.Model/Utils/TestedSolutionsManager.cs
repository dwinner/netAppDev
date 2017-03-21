using ProgramVerificationSystems.SelfTester.Model.Poco;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ProgramVerificationSystems.SelfTester.Model.Utils
{
   /// <summary>
   ///    Класс для чтения VS-решений для тестирования
   /// </summary>
   public static class TestedSolutionsManager
   {
      private const string TestedFilesListNodeName = "TestedFilesList";
      private const string TestedFileNodeName = "TestedFile";
      private const string SolutionFileNameNodeName = "solutionFileName";
      private const string ConfigNameNodeName = "ConfigName";
      private const string PlatformNodeName = "Platform";
      private const string PreprocessorNodeName = "Preprocessor";
      private const string FixedVsVersionNodeName = "FixedVSVersion";
      private static IEnumerable<TestSolutionInfo> _cachedTestedSolutions;

      private static readonly Lazy<IEnumerable<string>> DeferredLocalEtalons = new Lazy<IEnumerable<string>>(() =>
      {
         var etalonLogFolder = ApplicationConfigReader.Instance.EtalonLogFolder;
         var etalonLogFiles = Directory.EnumerateFiles(etalonLogFolder,
            string.Format("*{0}", CoreExtensions.EtalonLogExt), SearchOption.TopDirectoryOnly);
         return etalonLogFiles;
      });

      private static readonly Lazy<IEnumerable<string>> DeferredLocalEtalonFileNames =
         new Lazy<IEnumerable<string>>(() => DeferredLocalEtalons.Value.Select(Path.GetFileNameWithoutExtension));

      /// <summary>
      ///    Извлекает решения для тестовых запусков
      /// </summary>
      /// <param name="xmlSettingsFileName">Абсолютный путь к файлу конфигурации</param>
      /// <param name="selfTesterRootPath">Корневой путь к директории тестера</param>
      /// <returns>Список сущностей для тестируемых файлов решений</returns>
      /// <exception cref="InvalidSettingsException">Если узел [TestedFilesListNodeName] не был найден в конфигурации</exception>
      private static IEnumerable<TestSolutionInfo> RetrieveTestedSolutions(string xmlSettingsFileName,
         string selfTesterRootPath)
      {
         if (_cachedTestedSolutions == null)
         {
            if (!File.Exists(xmlSettingsFileName))
               throw new FileNotFoundException(string.Format("File {0} is not found", xmlSettingsFileName),
                  xmlSettingsFileName);

            var settingsDocument = XDocument.Load(xmlSettingsFileName);

            // Получим данные для тестов в "сыром" виде
            var testedFileListElement = settingsDocument.Descendants(TestedFilesListNodeName).FirstOrDefault();
            if (testedFileListElement == null)
               throw new InvalidSettingsException("TestedFilesListNodeName node is not exists");

            var rawTestedEntities = from testedSlnFile in testedFileListElement.Descendants(TestedFileNodeName)
                                    let slnFileName = testedSlnFile.Element(SolutionFileNameNodeName)
                                    let configMode = testedSlnFile.Element(ConfigNameNodeName)
                                    let platform = testedSlnFile.Element(PlatformNodeName)
                                    let preprocessor = testedSlnFile.Element(PreprocessorNodeName)
                                    let fixedVsVersion = testedSlnFile.Element(FixedVsVersionNodeName)
                                    select new
                                    {
                                       SolutionFileName = slnFileName == null ? string.Empty : slnFileName.Value,
                                       ConfigurationMode = configMode == null ? string.Empty : configMode.Value,
                                       PlatformType = platform == null ? string.Empty : platform.Value,
                                       PreprocessorType = preprocessor == null ? string.Empty : preprocessor.Value,
                                       FixedVsVersion = fixedVsVersion == null ? string.Empty : fixedVsVersion.Value
                                    };

            // Преобразуем "сырые" данные к формату данных модели
            var testSolutionInfoList = new List<TestSolutionInfo>();
            var readOrder = 0;
            foreach (var rawTestedEntity in rawTestedEntities)
            {
               ConfigurationMode configMode;
               if (!Enum.TryParse(rawTestedEntity.ConfigurationMode, true, out configMode))
                  configMode = TestSolutionInfo.DefaultConfigurationMode;

               PlatformType platformType;
               if (!Enum.TryParse(rawTestedEntity.PlatformType, true, out platformType))
                  platformType = TestSolutionInfo.DefaultPlatformType;

               PreprocessorType preprocessorType;
               if (!Enum.TryParse(rawTestedEntity.PreprocessorType, true, out preprocessorType))
                  preprocessorType = TestSolutionInfo.DefaultPreprocessorType;

               int vsVersionInt;
               var startupConfigurationMode =
                  !int.TryParse(rawTestedEntity.FixedVsVersion, out vsVersionInt)
                     ? TestSolutionInfo.DefaultVisualStudioVersions
                     : GetConfigurationModeByVsNumber(vsVersionInt);

               var testSolutionInfo = new TestSolutionInfo
               {
                  AbsSolutionFileName = selfTesterRootPath + rawTestedEntity.SolutionFileName,
                  ConfigurationMode = configMode,
                  Platform = platformType,
                  PreprocessorType = preprocessorType,
                  StartupMode = startupConfigurationMode,
                  ReadOrder = ++readOrder
               };

               testSolutionInfoList.Add(testSolutionInfo);
            }

            _cachedTestedSolutions = testSolutionInfoList;
         }

         return _cachedTestedSolutions;
      }

      /// <summary>
      ///    Извлекает модели решений для тестовых запусков
      /// </summary>
      /// <param name="xmlSettingsFileName">Абсолютный путь к файлу конфигурации</param>
      /// <param name="selfTesterRootPath">Корневой путь к директории тестера</param>
      /// <returns>Модели решений для тестовых запусков</returns>
      private static SolutionInfoViewModel[] RetrieveSlnInfoViewModels(string xmlSettingsFileName,
         string selfTesterRootPath)
      {
         var testedSolutions = RetrieveTestedSolutions(xmlSettingsFileName, selfTesterRootPath);
         return (from testedSolution in testedSolutions
                 select new SolutionInfoViewModel(testedSolution)).ToArray();
      }

      /// <summary>
      ///    Извлекает модели решений для тестовых запусков
      /// </summary>
      /// <param name="xmlSettingsFileName">Абсолютный путь к файлу конфигурации</param>
      /// <returns>Модели решений для тестовых запусков</returns>
      private static SolutionInfoViewModel[] RetrieveSlnInfoViewModels(string xmlSettingsFileName)
      {
         return RetrieveSlnInfoViewModels(xmlSettingsFileName, ApplicationConfigReader.SelfTesterRootPath);
      }

      /// <summary>
      ///    Извлекает модели решений для тестовых запусков
      /// </summary>
      /// <returns>Модели решений для тестовых запусков</returns>
      public static SolutionInfoViewModel[] RetrieveSlnInfoViewModels()
      {
         return RetrieveSlnInfoViewModels(ApplicationConfigReader.Instance.DefaultSettingsFileName);
      }

      /// <summary>
      ///    Очистка всех тестовых запусков
      /// </summary>
      public static void CleanExistingTestRuns()
      {
         var childDirs = Directory.EnumerateDirectories(ApplicationConfigReader.Instance.Viva64Logs);
         var dirs = childDirs as string[] ?? childDirs.ToArray();
         if (dirs.Length == 0)
            return;

         Array.ForEach(dirs, dirItem =>
         {
            try
            {
               Directory.Delete(dirItem);
            }
            catch (IOException ioEx)
            {
               AppEventLogger.Log(ioEx.Message, EventLogEntryType.Error);
            }
         });
      }

      /// <summary>
      ///    Получает текущие тестовые запуски
      /// </summary>
      /// <returns>Текущие тестовые запуски</returns>
      private static IEnumerable<string> GetExistingTestRuns(int topLimit)
      {
         var directoryInfos =
            Directory.EnumerateDirectories(ApplicationConfigReader.Instance.Viva64Logs)
               .Select(testRun => new DirectoryInfo(testRun))
               .ToList();

         return directoryInfos.Count == 0
            ? Enumerable.Empty<string>()
            : directoryInfos.OrderByDescending(info => info.CreationTime).Take(topLimit).Select(info => info.FullName);
      }

      /// <summary>
      ///    Восстановить значения тестового запуска
      /// </summary>
      /// <param name="testRun">Тестовый запуск</param>
      private static void RestoreTestRunState(Tuple<TestRunEntity, SolutionInfoViewModel[]> testRun)
      {
         var testRunEntity = testRun.Item1;
         var solutions = testRun.Item2;

         foreach (
            var startupConfigurationMode in
               Enum.GetValues(typeof(StartupConfigurationMode)).Cast<StartupConfigurationMode>())
         {
            if (startupConfigurationMode == StartupConfigurationMode.All)
               continue;

            var testRunDir = string.Format("{0}{1}{2}", testRunEntity.TestRunDirectory,
               Path.DirectorySeparatorChar,
               startupConfigurationMode);
            if (!Directory.Exists(testRunDir))
               continue;

            // Для этого режима конфигурации были тестовые запуски            
            var projectStateMap = GetProjectStateMap(testRunDir);
            SetSolutionsState(projectStateMap, solutions, startupConfigurationMode);
         }
      }

      /// <summary>
      ///    Инициализация словаря тестовых записей
      /// </summary>      
      /// <param name="testRunDir">Директория тестового запуска</param>
      /// <returns>Словарь тестовых записей</returns>
      private static IDictionary<string, AnalysisStatus> GetProjectStateMap(string testRunDir)
      {
         var projectStateMap = new Dictionary<string, AnalysisStatus>();
         const string plugExt = CoreExtensions.EtalonLogExt;

         var pvsLogs = Directory.GetFiles(testRunDir, string.Format("*{0}", plugExt),
            SearchOption.TopDirectoryOnly);
         var pvsLogFileNames = new HashSet<string>();
         foreach (var pvsLog in pvsLogs)
            pvsLogFileNames.Add(Path.GetFileNameWithoutExtension(pvsLog));

         var setToDetele = new HashSet<string>();
         var pvsDiffs = new HashSet<string>();
         foreach (
            var pvsLogFileName in
               pvsLogFileNames.Where(fileName => fileName.Contains(string.Format("{0}_Diffs", plugExt))))
         {
            pvsDiffs.Add(pvsLogFileName.Substring(0, pvsLogFileName.LastIndexOf('.')));
            setToDetele.Add(pvsLogFileName);
         }
         pvsLogFileNames.ExceptWith(setToDetele);

         foreach (var pvsLogFileName in pvsLogFileNames)
         {
            projectStateMap[string.Format("{0}.sln", pvsLogFileName)] = GetRemainingStatus(pvsDiffs,
               pvsLogFileName);
         }

         return projectStateMap;
      }

      private static AnalysisStatus GetRemainingStatus(ICollection<string> pvsDiffs, string pvsLogFileName)
      {
         var status = pvsDiffs.Contains(pvsLogFileName)
            ? AnalysisStatus.DiffFinished
            : (!DeferredLocalEtalonFileNames.Value.Contains(pvsLogFileName)
               ? AnalysisStatus.NoSuchEtalon
               : AnalysisStatus.OkFinished);

         return status;
      }

      /// <summary>
      ///    Установка состояния тестовых запусков в модели
      /// </summary>
      /// <param name="projectStateMap">Словарь [Имя файла решения, Статус анализа]</param>
      /// <param name="solutions">Модель решений</param>
      /// <param name="startupConfigurationMode">Версия VS</param>
      private static void SetSolutionsState(IDictionary<string, AnalysisStatus> projectStateMap,
         IEnumerable<SolutionInfoViewModel> solutions, StartupConfigurationMode startupConfigurationMode)
      {
         var testRunSlns = projectStateMap.Keys;
         foreach (var slnViewModel in solutions)
         {
            var solution = slnViewModel;
            if (!testRunSlns.Contains(solution.SolutionFileName))
               continue;

            var projectState = projectStateMap[solution.SolutionFileName];
            switch (startupConfigurationMode)
            {
               case StartupConfigurationMode.PvsVs2010:
                  slnViewModel.LaunchStatusOnPvsVs2010 = projectState;
                  break;
               case StartupConfigurationMode.PvsVs2012:
                  slnViewModel.LaunchStatusOnPvsVs2012 = projectState;
                  break;
               case StartupConfigurationMode.PvsVs2013:
                  slnViewModel.LaunchStatusOnPvsVs2013 = projectState;
                  break;
               case StartupConfigurationMode.PvsVs2015:
                  slnViewModel.LaunchStatusOnPvsVs2015 = projectState;
                  break;
            }
         }
      }

      public static IEnumerable<KeyValuePair<TestRunEntity, Lazy<IEnumerable<SolutionInfoViewModel>>>>
         DeferredInitializeTestingRuns(
         int topLimit)
      {
         var deferredTestRunners = new Dictionary<TestRunEntity, Lazy<IEnumerable<SolutionInfoViewModel>>>();

         try
         {
            foreach (var testRun in GetExistingTestRuns(topLimit))
            {
               var slnInfoViewModels = RetrieveSlnInfoViewModels();
               var runEntity = ObtainTestRunEntity(testRun);
               deferredTestRunners[runEntity] =
                  new Lazy<IEnumerable<SolutionInfoViewModel>>(() => RestoreTestRunState(runEntity, slnInfoViewModels));
            }
         }
         catch (ArgumentNullException)
         {
            return Enumerable.Empty<KeyValuePair<TestRunEntity, Lazy<IEnumerable<SolutionInfoViewModel>>>>();
         }

         return deferredTestRunners;
      }

      private static IEnumerable<SolutionInfoViewModel> RestoreTestRunState(TestRunEntity runEntity, SolutionInfoViewModel[] solutions)
      {
         foreach (var configMode in Enum.GetValues(typeof(StartupConfigurationMode)).Cast<StartupConfigurationMode>())
         {
            if (configMode == StartupConfigurationMode.All)
               continue;

            var testRunDir = Path.Combine(runEntity.TestRunDirectory, configMode.ToString());
            if (!Directory.Exists(testRunDir))
               continue;

            // Для этого режима конфигурации были тестовые запуски            
            var projectStateMap = GetProjectStateMap(testRunDir);
            SetSolutionsState(projectStateMap, solutions, configMode);
         }

         return solutions;
      }

      /// <summary>
      ///    Извлечение сущности тестового запуска
      /// </summary>
      /// <param name="existingTestRun">Абсолютный путь к директории тестового запуска</param>
      /// <returns>Cущность тестового запуска</returns>
      public static TestRunEntity ObtainTestRunEntity(string existingTestRun)
      {
         var fileName = Path.GetFileName(existingTestRun);
         string machineName;
         DateTime testingDateTime;
         AnalyzerUtilities.ExtractLaunchPartyComponents(fileName, out machineName, out testingDateTime);
         var runEntity = new TestRunEntity
         {
            TestRunDirectory = existingTestRun,
            MachineName = machineName,
            RunningDateTime = testingDateTime
         };

         return runEntity;
      }

      /// <summary>
      ///    Обновление модели для тестового запуска
      /// </summary>
      /// <param name="testingRunFolder">Директория тестового запуска</param>
      /// <returns>Актуальная версия модели тестового запуска</returns>
      public static IEnumerable<SolutionInfoViewModel> RefreshTestingRunModel(string testingRunFolder)
      {
         var testingRunModel = RetrieveSlnInfoViewModels();
         var testRunFolderName = Path.GetFileName(testingRunFolder);
         string machineName;
         DateTime testingDateTime;
         AnalyzerUtilities.ExtractLaunchPartyComponents(testRunFolderName, out machineName, out testingDateTime);
         var runEntity = new TestRunEntity
         {
            TestRunDirectory = testingRunFolder,
            MachineName = machineName,
            RunningDateTime = testingDateTime
         };
         RestoreTestRunState(Tuple.Create(runEntity, testingRunModel));

         return testingRunModel;
      }

      /// <summary>
      ///    Получение конфигурации запуска по номеру версии VS
      /// </summary>
      /// <param name="vsVersionInt">Номер версии VS</param>
      /// <returns>Конфигурация запуска</returns>
      private static StartupConfigurationMode GetConfigurationModeByVsNumber(int vsVersionInt)
      {
         switch (vsVersionInt)
         {
            case 14:
               return StartupConfigurationMode.PvsVs2015;
            case 12:
               return StartupConfigurationMode.PvsVs2013;
            case 11:
               return StartupConfigurationMode.PvsVs2012;
            case 10:
               return StartupConfigurationMode.PvsVs2010;
            default:
               return StartupConfigurationMode.All;
         }
      }
   }
}