using System;
using System.Threading;
using Microsoft.Win32;

namespace AppDevUnited.SelfTester.Model.Utils
{
   /// <summary>
   ///    Информация об установленных сущностях
   /// </summary>
   public static class InstallationInfo
   {
      /// <summary>
      ///    Установлена ли Visual Studio 2010
      /// </summary>
      public static bool IsInstalledVs2010
      {
         get { return InstalledVs2010.Value; }
      }

      /// <summary>
      ///    Установлена ли Visual Studio 2012
      /// </summary>
      public static bool IsInstalledVs2012
      {
         get { return InstalledVs2012.Value; }
      }

      /// <summary>
      ///    Установлена ли Visual Studio 2013
      /// </summary>
      public static bool IsInstalledVs2013
      {
         get { return InstalledVs2013.Value; }
      }

      /// <summary>
      ///    Установлена ли Visual Studio 2015
      /// </summary>
      public static bool IsInstalledVs2015
      {
         get { return InstalledVs2015.Value; }
      }

      /// <summary>
      ///    Путь к процессу devenv.exe для Visual Studio 2010
      /// </summary>
      private static string Vs2010Path
      {
         get { return Vs2010VersionPath.Value; }
      }

      /// <summary>
      ///    Путь к процессу devenv.exe для Visual Studio 2012
      /// </summary>
      private static string Vs2012Path
      {
         get { return Vs2012VersionPath.Value; }
      }

      /// <summary>
      ///    Путь к процессу devenv.exe для Visual Studio 2013
      /// </summary>
      private static string Vs2013Path
      {
         get { return Vs2013VersionPath.Value; }
      }

      /// <summary>
      ///    Путь к процессу devenv.exe для Visual Studio 2013
      /// </summary>
      private static string Vs2015Path
      {
         get { return Vs2015VersionPath.Value; }
      }

      /// <summary>
      ///    Установлен ли PVS-Studio
      /// </summary>
      public static bool IsInstalledPvsStudio
      {
         get { return InstalledPvsStudio.Value; }
      }

      /// <summary>
      ///    Извлекает путь к установленной Visual Studio
      /// </summary>
      /// <param name="vsVersion">Версия Visual Studio</param>
      /// <returns>Путь к установленной Visual Studio</returns>
      public static string RetrieveVisualStudioPath(StartupConfigurationMode vsVersion)
      {
         switch (vsVersion)
         {
            case StartupConfigurationMode.PvsVs2010:
               return Vs2010Path;
            case StartupConfigurationMode.PvsVs2012:
               return Vs2012Path;
            case StartupConfigurationMode.PvsVs2013:
               return Vs2013Path;
            case StartupConfigurationMode.PvsVs2015:
               return Vs2015Path;
            default:
               throw new InvalidOperationException("Visual Studio version must be concrete here");
         }
      }

      /// <summary>
      ///    Установлена ли определенная версия Visual Studio
      /// </summary>
      /// <param name="versionNumber">Номер версии</param>
      /// <returns>true, если установлена, false - в противном случае</returns>
      private static bool IsInstalledVsVersion(string versionNumber)
      {
         var version =
            Registry.GetValue(
               string.Format("HKEY_LOCAL_MACHINE\\Software\\Wow6432Node\\Microsoft\\VisualStudio\\{0}", versionNumber),
               "InstallDir", null);
         return version != null;
      }

      /// <summary>
      ///    Путь к Visual Studio определенной версии
      /// </summary>
      /// <param name="versionNumber">Номер версии</param>
      /// <returns>Путь к Visual Studio определенной версии</returns>
      private static string RetrieveVersionPath(string versionNumber)
      {
         var vsPathValue =
            Registry.GetValue(
               string.Format("HKEY_LOCAL_MACHINE\\Software\\Wow6432Node\\Microsoft\\VisualStudio\\{0}", versionNumber),
               "InstallDir", string.Empty) as string;
         return vsPathValue ?? string.Empty;
      }

      private static readonly Lazy<bool> InstalledVs2010 = new Lazy<bool>(() => IsInstalledVsVersion("10.0"),
         LazyThreadSafetyMode.PublicationOnly);

      private static readonly Lazy<bool> InstalledVs2012 = new Lazy<bool>(() => IsInstalledVsVersion("11.0"),
         LazyThreadSafetyMode.PublicationOnly);

      private static readonly Lazy<bool> InstalledVs2013 = new Lazy<bool>(() => IsInstalledVsVersion("12.0"),
         LazyThreadSafetyMode.PublicationOnly);

      private static readonly Lazy<bool> InstalledVs2015 = new Lazy<bool>(() => IsInstalledVsVersion("14.0"),
         LazyThreadSafetyMode.PublicationOnly);

      private static readonly Lazy<string> Vs2010VersionPath = new Lazy<string>(() => RetrieveVersionPath("10.0"),
         LazyThreadSafetyMode.PublicationOnly);

      private static readonly Lazy<string> Vs2012VersionPath = new Lazy<string>(() => RetrieveVersionPath("11.0"),
         LazyThreadSafetyMode.PublicationOnly);

      private static readonly Lazy<string> Vs2013VersionPath = new Lazy<string>(() => RetrieveVersionPath("12.0"),
         LazyThreadSafetyMode.PublicationOnly);

      private static readonly Lazy<string> Vs2015VersionPath = new Lazy<string>(() => RetrieveVersionPath("14.0"),
         LazyThreadSafetyMode.PublicationOnly);

      private static readonly Lazy<bool> InstalledPvsStudio = new Lazy<bool>(() =>
      {
         var pvsStudioRegValue = Environment.Is64BitProcess
            ? @"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\PVS-Studio_is1"
            : @"Software\Microsoft\Windows\CurrentVersion\Uninstall\PVS-Studio_is1";
         var pvsInLmKey = Registry.LocalMachine.OpenSubKey(pvsStudioRegValue);
         var pvsInCuKey = Registry.CurrentUser.OpenSubKey(pvsStudioRegValue);
         return pvsInCuKey != null || pvsInLmKey != null;
      }, LazyThreadSafetyMode.PublicationOnly);
   }
}