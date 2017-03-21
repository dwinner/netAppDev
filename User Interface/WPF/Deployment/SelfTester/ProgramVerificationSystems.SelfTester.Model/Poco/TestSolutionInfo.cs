using System;

namespace ProgramVerificationSystems.SelfTester.Model.Poco
{
   /// <summary>
   ///    Сущностный класс для тестируемого файла решения *.sln
   /// </summary>
   [Serializable]
   public class TestSolutionInfo
   {
      public const ConfigurationMode DefaultConfigurationMode = ConfigurationMode.Release;
      public const PlatformType DefaultPlatformType = PlatformType.X64;
      public const PreprocessorType DefaultPreprocessorType = PreprocessorType.Clang;
      public const StartupConfigurationMode DefaultVisualStudioVersions = StartupConfigurationMode.All;

      /// <summary>
      ///    Порядок чтения тестируемого решения
      /// </summary>
      public int ReadOrder { get; set; }

      /// <summary>
      ///    Абсолютный путь к файлу решения
      /// </summary>
      public string AbsSolutionFileName { get; set; }

      /// <summary>
      ///    Режим конфигурации сборки
      /// </summary>
      public ConfigurationMode ConfigurationMode { get; set; }

      /// <summary>
      ///    Разрядность платформы
      /// </summary>
      public PlatformType Platform { get; set; }

      /// <summary>
      ///    Тип препроцессора
      /// </summary>
      public PreprocessorType PreprocessorType { get; set; }

      /// <summary>
      ///    Режим конфигурации запуска
      /// </summary>
      public StartupConfigurationMode StartupMode { get; set; }

      public override string ToString()
      {
         return
            string.Format(
               "Solution: {0}, Configuration mode: {1}, Platform: {2}, Preprocessor type: {3}, Startup mode: {4}",
               AbsSolutionFileName, ConfigurationMode, Platform, PreprocessorType, StartupMode);
      }
   }
}