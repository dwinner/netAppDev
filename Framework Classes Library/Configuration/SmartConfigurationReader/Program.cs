// "Упрощение" чтения параметров конфигурационного файла

using static System.Console;
using static SmartConfigurationReader.DeferredConfig;

namespace SmartConfigurationReader
{
   internal static class Program
   {
      private static void Main()
      {
         WriteLine("{0} = {1}", nameof(DeferredConfig.HangTime), ConfInstance.HangTime);
         WriteLine("{0} = {1}", nameof(DeferredConfig.MinRecommendedWorkingDiskSpace),
            ConfInstance.MinRecommendedWorkingDiskSpace);
         WriteLine("{0} = {1}", nameof(DeferredConfig.TestSuitesDir), ConfInstance.TestSuitesDir);
         WriteLine("{0} = {1}", nameof(DeferredConfig.DefaultSettingsFileName),
            ConfInstance.DefaultSettingsFileName);
         WriteLine("{0} = {1}", nameof(DeferredConfig.LogFolder), ConfInstance.LogFolder);
         WriteLine("{0} = {1}", nameof(DeferredConfig.EtalonLogFolder),
            ConfInstance.EtalonLogFolder);
         WriteLine("{0} = {1}", nameof(DeferredConfig.VivaLogs), ConfInstance.VivaLogs);
         WriteLine("{0} = {1}", nameof(DeferredConfig.SrcEtalonFolder),
            ConfInstance.SrcEtalonFolder);
         WriteLine("{0} = {1}", nameof(DeferredConfig.EtalonPvsSettingsFile),
            ConfInstance.EtalonPvsSettingsFile);                 
         WriteLine("{0} = {1}", nameof(DeferredConfig.PvsStudioSettingsDir),
            ConfInstance.PvsStudioSettingsDir);
         WriteLine("{0} = {1}", nameof(DeferredConfig.BackupPvsLicenseFileName),
            ConfInstance.BackupPvsLicenseFileName);         
         WriteLine("{0} = {1}", nameof(DeferredConfig.BackupPvsSettings),
            ConfInstance.BackupPvsSettings);
         WriteLine("{0} = {1}", nameof(DeferredConfig.CurrentPvsSettingsFile),
            ConfInstance.CurrentPvsSettingsFile);
         WriteLine("{0} = {1}", nameof(DeferredConfig.CurrentPvsLicenseFile),
            ConfInstance.CurrentPvsLicenseFile);         
         WriteLine("{0} = {1}", nameof(DeferredConfig.DefaultEtalonLogFileExtension),
            ConfInstance.DefaultEtalonLogFileExtension);
         WriteLine("{0} = {1}", nameof(DeferredConfig.BldSrvEtalonsFolder),
            ConfInstance.BldSrvEtalonsFolder);
         WriteLine("{0} = {1}", nameof(DeferredConfig.ConcurrentTestRunsNumber),
            ConfInstance.ConcurrentTestRunsNumber);
      }
   }
}