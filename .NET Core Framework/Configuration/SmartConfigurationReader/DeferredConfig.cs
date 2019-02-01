using System;
using System.Configuration;
using System.Threading;
using static System.Threading.LazyThreadSafetyMode;

namespace SmartConfigurationReader
{
   /// <summary>
   ///    Оболочка для отложенного чтения параметров конфигурации
   /// </summary>
   /// <remarks>Имена открытых свойств должны в точности соответствовать именам в App.config-файле</remarks>
   public sealed class DeferredConfig
   {
      private static DeferredConfig _oneInstance;
      private static readonly AppSettingsReader AppSettingsReader = new AppSettingsReader();

      #region Поля для отложенного чтения      

      private readonly Lazy<string> _backupPvsLicenseFileName =
         new Lazy<string>(() => Get<string>(nameof(BackupPvsLicenseFileName)), PublicationOnly);

      private readonly Lazy<string> _backupPvsSettings = new Lazy<string>(() => Get<string>(nameof(BackupPvsSettings)),
         PublicationOnly);      

      private readonly Lazy<string> _bldSrvEtalonsFolder =
         new Lazy<string>(() => Get<string>(nameof(BldSrvEtalonsFolder)), PublicationOnly);

      private readonly Lazy<int> _concurrentTestRunsNumber =
         new Lazy<int>(() => Get<int>(nameof(ConcurrentTestRunsNumber)), PublicationOnly);

      private readonly Lazy<string> _currentPvsLicenseFile =
         new Lazy<string>(() => Get<string>(nameof(CurrentPvsLicenseFile)), PublicationOnly);

      private readonly Lazy<string> _currentPvsSettingsFile =
         new Lazy<string>(() => Get<string>(nameof(CurrentPvsSettingsFile)), PublicationOnly);      

      private readonly Lazy<string> _defaultEtalonLogFileExtension =
         new Lazy<string>(() => Get<string>(nameof(DefaultEtalonLogFileExtension)), PublicationOnly);

      private readonly Lazy<string> _defaultSettingsFileName =
         new Lazy<string>(() => Get<string>(nameof(DefaultSettingsFileName)), PublicationOnly);

      private readonly Lazy<string> _etalonLogFolder = new Lazy<string>(() => Get<string>(nameof(EtalonLogFolder)),
         PublicationOnly);

      private readonly Lazy<string> _etalonPvsSettingsFile =
         new Lazy<string>(() => Get<string>(nameof(EtalonPvsSettingsFile)), PublicationOnly);      

      private readonly Lazy<int> _hangTime = new Lazy<int>(() => Get<int>(nameof(HangTime)), PublicationOnly);
      private readonly Lazy<string> _logFolder = new Lazy<string>(() => Get<string>(nameof(LogFolder)), PublicationOnly);

      private readonly Lazy<int> _minRecommendedWorkingDiskSpace =
         new Lazy<int>(() => Get<int>(nameof(MinRecommendedWorkingDiskSpace)), PublicationOnly);      

      private readonly Lazy<string> _pvsStudioSettingsDir =
         new Lazy<string>(() => Get<string>(nameof(PvsStudioSettingsDir)), PublicationOnly);

      private readonly Lazy<string> _srcEtalonFolder = new Lazy<string>(() => Get<string>(nameof(SrcEtalonFolder)),
         PublicationOnly);

      private readonly Lazy<string> _testSuitesDir = new Lazy<string>(() => Get<string>(nameof(TestSuitesDir)),
         PublicationOnly);

      private readonly Lazy<string> _vivaLogs = new Lazy<string>(() => Get<string>(nameof(VivaLogs)), PublicationOnly);

      #endregion

      private DeferredConfig()
      {
      }

      #region Свойства параметров конфигурации

      public int HangTime => _hangTime.Value;
      public int MinRecommendedWorkingDiskSpace => _minRecommendedWorkingDiskSpace.Value;
      public string TestSuitesDir => _testSuitesDir.Value;
      public string DefaultSettingsFileName => _defaultSettingsFileName.Value;
      public string LogFolder => _logFolder.Value;
      public string EtalonLogFolder => _etalonLogFolder.Value;
      public string VivaLogs => _vivaLogs.Value;
      public string SrcEtalonFolder => _srcEtalonFolder.Value;
      public string EtalonPvsSettingsFile => _etalonPvsSettingsFile.Value;      
      public string PvsStudioSettingsDir => _pvsStudioSettingsDir.Value;
      public string BackupPvsLicenseFileName => _backupPvsLicenseFileName.Value;            
      public string BackupPvsSettings => _backupPvsSettings.Value;
      public string CurrentPvsSettingsFile => _currentPvsSettingsFile.Value;
      public string CurrentPvsLicenseFile => _currentPvsLicenseFile.Value;      
      public string DefaultEtalonLogFileExtension => _defaultEtalonLogFileExtension.Value;
      public string BldSrvEtalonsFolder => _bldSrvEtalonsFolder.Value;
      public int ConcurrentTestRunsNumber => _concurrentTestRunsNumber.Value;

      #endregion

      public static DeferredConfig ConfInstance
      {
         get
         {
            if (_oneInstance != null) return _oneInstance;
            var tempSingleton = new DeferredConfig();
            Interlocked.CompareExchange(ref _oneInstance, tempSingleton, null);
            return _oneInstance;
         }
      }

      private static T Get<T>(string key)
      {
         try
         {
            return (T)AppSettingsReader.GetValue(key, typeof(T));
         }
         catch (InvalidOperationException)
         {
            return default(T);
         }
         catch (ArgumentNullException)
         {
            return default(T);
         }
      }
   }
}