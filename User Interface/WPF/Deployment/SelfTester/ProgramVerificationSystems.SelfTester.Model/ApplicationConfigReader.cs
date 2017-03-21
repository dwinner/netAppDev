using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace ProgramVerificationSystems.SelfTester.Model
{
   /// <summary>
   ///    Оболочка для чтения параметров конфигурации
   /// </summary>
   public class ApplicationConfigReader
   {
      public const int DefaultTopRecords = 15;
      private static ApplicationConfigReader _singleInstance;
      private readonly AppSettingsReader _appSettingsReader = new AppSettingsReader();
      private readonly string[] _faultProcessNames;
      private readonly string[] _sessionEndProcessesToKill;

      private ApplicationConfigReader()
      {
         _faultProcessNames = new[] { "DW20.EXE", "DWWIN.EXE", "WerFault.exe" };
         _sessionEndProcessesToKill = new[] { "DW20", "PVS-Studio", "VsHub", "vstest.discoveryengine.x86", "DWWIN" };
      }

      /// <summary>
      ///    Время зависания devenv.exe (в минутах)
      /// </summary>
      public int HangTime
      {
         get { return Get<int>("HangTime"); }
      }

      /// <summary>
      ///    Рекомендуемое минимальное число свободного места на диске
      ///    <remarks>В гигабайтах</remarks>
      /// </summary>
      public int MinRecommendedWorkingDiskSpace
      {
         get { return Get<int>("MinRecommendedWorkingDiskSpace"); }
      }

      /// <summary>
      ///    Корневая директория тестера
      /// </summary>
      public static string SelfTesterRootPath
      {
         get { return Environment.CurrentDirectory; }
      }

      /// <summary>
      ///    Директория с настройками тестера
      /// </summary>
      private string TestSuitesDir
      {
         get
         {
            return Path.Combine(
               SelfTesterRootPath,
               Get<string>("TestSuitesDir"));
         }
      }

      /// <summary>
      ///    Файл настроек по умолчанию
      /// </summary>
      public string DefaultSettingsFileName
      {
         get
         {
            return Path.Combine(
               TestSuitesDir,
               Get<string>("DefaultSettingsFileName"));
         }
      }

      /// <summary>
      ///    Директория с логами
      /// </summary>
      private string LogFolder
      {
         get
         {
            return Path.Combine(
               SelfTesterRootPath,
               Get<string>("LogFolder"));
         }
      }

      /// <summary>
      ///    Директория с эталонными логами
      /// </summary>
      public string EtalonLogFolder
      {
         get
         {
            return Path.Combine(
               LogFolder,
               Get<string>("EtalonLogFolder"));
         }
      }

      /// <summary>
      ///    Директория с результатами тестовых запусков
      /// </summary>
      public string Viva64Logs
      {
         get
         {
            var viva64Logs = Path.Combine(LogFolder, Get<string>("Viva64Logs"));
            if (!Directory.Exists(viva64Logs))
            {
               Directory.CreateDirectory(viva64Logs);
            }

            return viva64Logs;
         }
      }

      /// <summary>
      ///    Имя директории с эталонными исходными кодами
      /// </summary>
      public string SrcEtalonFolder
      {
         get { return Get<string>("SrcEtalon"); }
      }

      /// <summary>
      ///    Файл настроек PVS-Studio для тестера
      /// </summary>
      public string EtalonPvsSettingsFile
      {
         get
         {
            return Path.Combine(
               SelfTesterRootPath,
               Get<string>("EtalonPvsSettingsFile"));
         }
      }

      /// <summary>
      ///    Файл лицензии для PVS-Studio
      /// </summary>
      private string PvsStudioLicenseFile
      {
         get { return Get<string>("PvsStudioLicenseFile"); }
      }

      /// <summary>
      ///    Файл лицензии для эталоннных исходников
      /// </summary>
      public string EtalonPvsStudioLicenseFile
      {
         get
         {
            return Path.Combine(
               SelfTesterRootPath,
               Get<string>("PvsStudioLicenseFile"));
         }
      }

      /// <summary>
      ///    Имя директории PVS-Studio в Application Data
      /// </summary>
      private string PvsStudioSettingsDir
      {
         get { return Get<string>("PvsStudioSettingsDir"); }
      }

      /// <summary>
      ///    Backup'файла лицензии PVS-Studio
      /// </summary>
      private string BackupPvsLicenseFileName
      {
         get { return Get<string>("BackupPvsLicenseFileName"); }
      }

      /// <summary>
      ///    Текущий файл настроек PVS-Studio
      /// </summary>
      private string CurrentPvsSettingsFileName
      {
         get { return Get<string>("CurrentPvsSettingsFileName"); }
      }

      /// <summary>
      ///    Backup файла настроек для PVS-Studio
      /// </summary>
      private string BackUpPvsSettingsFileName
      {
         get { return Get<string>("BackUpPvsSettingsFileName"); }
      }

      /// <summary>
      ///    Путь к текущему backup'у файла настроек PVS-Studio
      /// </summary>
      public string BackupPvsSettings
      {
         get
         {
            return Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
               PvsStudioSettingsDir,
               BackUpPvsSettingsFileName);
         }
      }

      /// <summary>
      ///    Путь к текущему файлу настроек PVS-Studio
      /// </summary>
      public string CurrentPvsSettingsFile
      {
         get
         {
            return Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
               PvsStudioSettingsDir,
               CurrentPvsSettingsFileName);
         }
      }

      /// <summary>
      ///    Текущий файл лицензии а Application Data
      /// </summary>
      public string CurrentPvsLicenseFile
      {
         get
         {
            return Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
               PvsStudioSettingsDir,
               PvsStudioLicenseFile);
         }
      }

      /// <summary>
      ///    Backup файла лицензии а Application Data
      /// </summary>
      public string BackupPvsLicenseFile
      {
         get
         {
            return Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
               PvsStudioSettingsDir,
               BackupPvsLicenseFileName);
         }
      }

      /// <summary>
      ///    Расширение файла эталонного лога
      /// </summary>
      public string DefaultEtalonLogFileExtension
      {
         get { return Get<string>("DefaultEtalonLogFileExtension"); }
      }

      /// <summary>
      ///    Путь к папке с эталонными логами на Build-сервере
      /// </summary>
      public string BldSrvEtalonsFolder
      {
         get { return Get<string>("BldSrvEtalonsFolder"); }
      }

      /// <summary>
      ///    Степень параллелизма
      /// </summary>
      public int ConcurrentTestRunsNumber
      {
         get
         {
            var parallelDegree = Get<int>("ConcurrentTestRunsNumber");
            if (parallelDegree <= 0 || parallelDegree > Environment.ProcessorCount)
            {
               parallelDegree = Environment.ProcessorCount;
            }

            return parallelDegree;
         }
      }

      /// <summary>
      ///   Процессы, которые могут свидетельствовать о возникновении ошибки в devenv.exe
      /// </summary>
      public string[] FaultProcessNames
      {
         get { return _faultProcessNames; }
      }

      /// <summary>
      ///   Суррогатные процессы, которые могут остаться в IDLE-статусе после сеанса анализа
      /// </summary>
      public string[] SessionEndProcessesToKill
      {
         get { return _sessionEndProcessesToKill; }
      }

      /// <summary>
      ///    "Одиночка"
      /// </summary>
      public static ApplicationConfigReader Instance
      {
         get
         {
            if (_singleInstance != null)
            {
               return _singleInstance;
            }

            var tempSingleton = new ApplicationConfigReader();
            Interlocked.CompareExchange(ref _singleInstance, tempSingleton, null);

            return _singleInstance;
         }
      }

      private T Get<T>(string key)
      {
         try
         {
            return (T)_appSettingsReader.GetValue(key, typeof(T));
         }
         catch (InvalidOperationException invalidOperationEx)
         {
            throw new InvalidSettingsException(
               string.Format("Key {0} does not exists in App.config or incompatible type", key),
               invalidOperationEx);
         }
         catch (ArgumentNullException argNullEx)
         {
            throw new InvalidSettingsException(
               string.Format("Key {0} is not found", key),
               argNullEx);
         }
      }
   }
}