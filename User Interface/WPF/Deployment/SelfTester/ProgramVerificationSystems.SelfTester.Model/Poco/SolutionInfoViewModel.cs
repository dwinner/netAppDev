using ProgramVerificationSystems.SelfTester.Model.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProgramVerificationSystems.SelfTester.Model.Poco
{
   /// <summary>
   ///    Модель представления для тестируемых VS-решений
   /// </summary>
   [Serializable]
   public sealed class SolutionInfoViewModel : ViewModelBase, IEquatable<SolutionInfoViewModel>
   {
      private static readonly IComparer<SolutionInfoViewModel> OrderComparer = new ReadOrderComparer();
      private readonly TestSolutionInfo _testSolutionInfo;

      /// <summary>
      ///    Конструктор модели представления решения
      /// </summary>
      /// <param name="testSolutionInfo">Модель для теста sln</param>
      public SolutionInfoViewModel(TestSolutionInfo testSolutionInfo)
      {
         _testSolutionInfo = testSolutionInfo;
         SetInstalledConfiguration();
         InitializeNotSupportedVersions();
         ResetNotSupportedForNotInstalled();
      }

      public static IComparer<SolutionInfoViewModel> DefaultReadOrderComparer
      {
         get { return OrderComparer; }
      }

      /// <summary>
      ///    Модель для теста sln
      /// </summary>
      public TestSolutionInfo TestSolutionInfo
      {
         get { return _testSolutionInfo; }
      }

      /// <summary>
      ///    Имя файла решения
      /// </summary>
      public string SolutionFileName
      {
         get { return Path.GetFileName(_testSolutionInfo.AbsSolutionFileName); }
      }

      public bool Equals(SolutionInfoViewModel other)
      {
         return !ReferenceEquals(null, other) &&
                (ReferenceEquals(this, other) || string.Equals(SolutionFileName, other.SolutionFileName));
      }

      public override bool Equals(object obj)
      {
         return !ReferenceEquals(null, obj) &&
                (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((SolutionInfoViewModel)obj));
      }

      public override int GetHashCode()
      {
         unchecked
         {
            return SolutionFileName.GetHashCode();
         }
      }

      public static bool operator ==(SolutionInfoViewModel left, SolutionInfoViewModel right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(SolutionInfoViewModel left, SolutionInfoViewModel right)
      {
         return !Equals(left, right);
      }

      private sealed class ReadOrderComparer : IComparer<SolutionInfoViewModel>
      {
         public int Compare(SolutionInfoViewModel x, SolutionInfoViewModel y)
         {
            return x.TestSolutionInfo.ReadOrder - y.TestSolutionInfo.ReadOrder;
         }
      }

      #region Св-ва модели представления

      private AnalysisStatus _launchStatusOnPvsVs2010;
      private AnalysisStatus _launchStatusOnPvsVs2012;
      private AnalysisStatus _launchStatusOnPvsVs2013;
      private AnalysisStatus _launchStatusOnPvsVs2015;

      /// <summary>
      ///    Статус запуска для PvsVs2010 в режиме PVS-Studio
      /// </summary>
      [ColumnIndex(1)]
      public AnalysisStatus LaunchStatusOnPvsVs2010
      {
         get { return _launchStatusOnPvsVs2010; }
         set
         {
            _launchStatusOnPvsVs2010 = value;
            OnPropertyChanged();
         }
      }

      /// <summary>
      ///    Статус запуска для PvsVs2012 в режиме PVS-Studio
      /// </summary>
      [ColumnIndex(2)]
      public AnalysisStatus LaunchStatusOnPvsVs2012
      {
         get { return _launchStatusOnPvsVs2012; }
         set
         {
            _launchStatusOnPvsVs2012 = value;
            OnPropertyChanged();
         }
      }

      /// <summary>
      ///    Статус запуска для PvsVs2013 в режиме PVS-Studio
      /// </summary>
      [ColumnIndex(3)]
      public AnalysisStatus LaunchStatusOnPvsVs2013
      {
         get { return _launchStatusOnPvsVs2013; }
         set
         {
            _launchStatusOnPvsVs2013 = value;
            OnPropertyChanged();
         }
      }

      /// <summary>
      ///    Статус запуска для PvsVs2013 в режиме PVS-Studio
      /// </summary>
      [ColumnIndex(4)]
      public AnalysisStatus LaunchStatusOnPvsVs2015
      {
         get { return _launchStatusOnPvsVs2015; }
         set
         {
            _launchStatusOnPvsVs2015 = value;
            OnPropertyChanged();
         }
      }

      #endregion

      #region Служебные методы

      private void ResetNotSupportedForNotInstalled()
      {
         if (_launchStatusOnPvsVs2010 == AnalysisStatus.VsVersionNotSupported && !InstallationInfo.IsInstalledVs2010)
            _launchStatusOnPvsVs2010 = AnalysisStatus.VsVersionNotInstalled;

         if (_launchStatusOnPvsVs2012 == AnalysisStatus.VsVersionNotSupported && !InstallationInfo.IsInstalledVs2012)
            _launchStatusOnPvsVs2012 = AnalysisStatus.VsVersionNotInstalled;

         if (_launchStatusOnPvsVs2013 == AnalysisStatus.VsVersionNotSupported && !InstallationInfo.IsInstalledVs2013)
            _launchStatusOnPvsVs2013 = AnalysisStatus.VsVersionNotInstalled;

         if (_launchStatusOnPvsVs2015 == AnalysisStatus.VsVersionNotSupported && !InstallationInfo.IsInstalledVs2015)
            _launchStatusOnPvsVs2015 = AnalysisStatus.VsVersionNotInstalled;
      }

      private void InitializeNotSupportedVersions()
      {
         if (_testSolutionInfo.StartupMode == StartupConfigurationMode.All)
            return;

         switch (_testSolutionInfo.StartupMode)
         {
            case StartupConfigurationMode.PvsVs2010:
               _launchStatusOnPvsVs2012 =
                  _launchStatusOnPvsVs2013 = _launchStatusOnPvsVs2015 = AnalysisStatus.VsVersionNotSupported;
               break;

            case StartupConfigurationMode.PvsVs2012:
               _launchStatusOnPvsVs2010 =
                  _launchStatusOnPvsVs2013 = _launchStatusOnPvsVs2015 = AnalysisStatus.VsVersionNotSupported;
               break;

            case StartupConfigurationMode.PvsVs2013:
               _launchStatusOnPvsVs2010 =
                  _launchStatusOnPvsVs2012 = _launchStatusOnPvsVs2015 = AnalysisStatus.VsVersionNotSupported;
               break;

            case StartupConfigurationMode.PvsVs2015:
               _launchStatusOnPvsVs2010 =
                  _launchStatusOnPvsVs2012 = _launchStatusOnPvsVs2013 = AnalysisStatus.VsVersionNotSupported;
               break;
         }
      }

      private void SetInstalledConfiguration()
      {
         if (InstallationInfo.IsInstalledPvsStudio)
         {
            _launchStatusOnPvsVs2010 = InstallationInfo.IsInstalledVs2010
               ? AnalysisStatus.NotStarted
               : AnalysisStatus.VsVersionNotInstalled;

            _launchStatusOnPvsVs2012 = InstallationInfo.IsInstalledVs2012
               ? AnalysisStatus.NotStarted
               : AnalysisStatus.VsVersionNotInstalled;

            _launchStatusOnPvsVs2013 = InstallationInfo.IsInstalledVs2013
               ? AnalysisStatus.NotStarted
               : AnalysisStatus.VsVersionNotInstalled;

            _launchStatusOnPvsVs2015 = InstallationInfo.IsInstalledVs2015
               ? AnalysisStatus.NotStarted
               : AnalysisStatus.VsVersionNotInstalled;
         }
         else
         {
            _launchStatusOnPvsVs2010 =
               _launchStatusOnPvsVs2012 =
                  _launchStatusOnPvsVs2013 = _launchStatusOnPvsVs2015 = AnalysisStatus.PluginNotInstalled;
         }
      }

      #endregion
   }
}