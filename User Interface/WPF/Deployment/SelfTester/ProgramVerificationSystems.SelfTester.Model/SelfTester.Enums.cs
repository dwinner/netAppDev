using System;

namespace ProgramVerificationSystems.SelfTester.Model
{
   /// <summary>
   /// Статус прохождения анализа
   /// </summary>
   public enum AnalysisStatus
   {
      /// <summary>
      /// Неопределенный статус анализа
      /// </summary>
      [AnalysisStatusDescription("Not Supported Status")]
      None,

      /// <summary>
      /// Еще не запускался
      /// </summary>
      [AnalysisStatusDescription("-")]
      NotStarted,

      /// <summary>
      /// Версия Visual Studio не установлена на машине
      /// </summary>
      [AnalysisStatusDescription("VS not installed")]
      VsVersionNotInstalled,

      /// <summary>
      /// Plugin не установлен на машине
      /// </summary>
      [AnalysisStatusDescription("Plugin not installed")]
      PluginNotInstalled,

      /// <summary>
      /// Версия не поддерживается
      /// </summary>
      [AnalysisStatusDescription("Not supported")]
      VsVersionNotSupported,

      /// <summary>
      /// Запланирован к запуску
      /// </summary>
      [AnalysisStatusDescription("Pending")]
      InPending,

      /// <summary>
      /// В процессе обновления
      /// </summary>
      [AnalysisStatusDescription("Upgrading")]
      InUpgrading,

      /// <summary>
      /// Сравнение логов
      /// </summary>
      [AnalysisStatusDescription("Сomparing logs")]
      СomparingLogs,

      /// <summary>
      /// В процессе
      /// </summary>
      [AnalysisStatusDescription("In progress")]
      InProgress,

      /// <summary>
      /// Завершено без различий
      /// </summary>
      [AnalysisStatusDescription("OK")]
      OkFinished,

      /// <summary>
      /// Завершено с различиями
      /// </summary>
      [AnalysisStatusDescription("Diff")]
      DiffFinished,

      /// <summary>
      /// Аварийное завершение работы plugin'а
      /// </summary>
      [AnalysisStatusDescription("Fail")]
      PluginCrashed,

      /// <summary>
      /// Не существует эталонного лога
      /// </summary>
      [AnalysisStatusDescription("No Etalon")]
      NoSuchEtalon,

      /// <summary>
      /// В процессе открытия в Visual Studio
      /// </summary>
      [AnalysisStatusDescription("Opening...")]
      Opening,

      /// <summary>
      /// Открыт в Visual Studio
      /// </summary>
      [AnalysisStatusDescription("Opened In VS")]
      Opened
   }

   /// <summary>
   /// Режим конфигурации сборки
   /// </summary>
   public enum ConfigurationMode
   {
      /// <summary>
      /// Окончательная
      /// </summary>
      Release,

      /// <summary>
      /// Отладочная
      /// </summary>
      Debug
   }

   /// <summary>
   /// Возможные причины сбоев в работе plugin'а
   /// </summary>
   public enum CrashReason
   {
      /// <summary>
      /// Заглушка, сбоев не было
      /// </summary>
      [CrashMessage("No fails")]
      None,

      /// <summary>
      /// Неудачная попытка обновления решения
      /// </summary>
      [CrashMessage("Unable to upgrade solution")]
      UpgradeFail,

      /// <summary>
      /// Подвисание devenv при обновлении проекта
      /// </summary>
      [CrashMessage("Stale devenv process exceeded the allowable working time during upgrade")]
      HangoutOnUpgrade,

      /// <summary>
      /// Подвисание devenv при анализе
      /// </summary>
      [CrashMessage("Stale devenv process exceeded the allowable working time during analisys")]
      HangoutOnAnalysis,

      /// <summary>
      /// Нет результатов работы анализа
      /// </summary>
      [CrashMessage("No analysis results")]
      NoResults,

      /// <summary>
      /// Нет эталона для сравнения
      /// </summary>
      [CrashMessage("No etalon plog")]
      NoEtalon,

      /// <summary>
      /// Ненулевой код завершения
      /// </summary>
      [CrashMessage("Visual Studio abnormally terminated")]
      NotZeroExitCode
   }

   /// <summary>
   /// Разрядность платформы
   /// </summary>
   public enum PlatformType
   {
      /// <summary>
      /// 32х-битная
      /// </summary>
      Win32 = 32,

      /// <summary>
      /// 64х-битная
      /// </summary>
      X64 = 64
   }

   /// <summary>
   /// Тип препроцессора
   /// </summary>
   public enum PreprocessorType
   {
      /// <summary>
      /// Clang-препроцессор
      /// </summary>
      Clang,

      /// <summary>
      /// Visual C++-препроцессор
      /// </summary>
      VisualCpp
   }

   /// <summary>
   ///    Коды завершения Robocopy
   /// </summary>
   [Flags]
   public enum RobocopyExitCode
   {
      /// <summary>
      ///    Ничего не было скопировано
      /// </summary>
      [RobocopyExitCodeMeaning("No errors occurred, and no copying was done. The source and destination directory trees are completely synchronized")]
      NoCopying = 0x0,

      /// <summary>
      ///    Успешное копирование одного или нескольких файлов
      /// </summary>
      [RobocopyExitCodeMeaning("One or more files were copied successfully (that is, new files have arrived)")]
      Success = 0x1,

      /// <summary>
      ///    Появились дополнительные файлы или директории во время копирования
      /// </summary>
      [RobocopyExitCodeMeaning("Some Extra files or directories were detected. No files were copied.")]
      FileSystemAffected = 0x2,

      /// <summary>
      ///    Несоответствие файлов
      /// </summary>
      [RobocopyExitCodeMeaning("Some Mismatched files or directories were detected. Some housekeeping may be needed")]
      MismatchedFiles = 0x4,

      /// <summary>
      ///    Файлы не могут быть скопированы
      /// </summary>
      [RobocopyExitCodeMeaning("Some files or directories could not be copied (copy errors occurred and the retry limit was exceeded).")]
      CopyErrors = 0x8,

      /// <summary>
      ///    Неправильное использование или ошибки доступа к файлам
      /// </summary>
      [RobocopyExitCodeMeaning("Serious error. Robocopy did not copy any files. Either a usage error or an error due to insufficient access privileges on the source or destination directories")]
      SeriousError = 0x10
   }

   /// <summary>
   ///    Режимы поддерживаемых запусков
   /// </summary>
   [Flags]
   public enum StartupConfigurationMode
   {
      /// <summary>
      ///    Все
      /// </summary>
      All = 0,

      /// <summary>
      ///    PvsVs2010
      /// </summary>
      PvsVs2010 = 0x2,

      /// <summary>
      ///    PvsVs2012
      /// </summary>
      PvsVs2012 = 0x10,

      /// <summary>
      ///    PvsVs2013
      /// </summary>
      PvsVs2013 = 0x20,

      /// <summary>
      ///    PvsVs2013
      /// </summary>
      PvsVs2015 = 0x40
   }

   /// <summary>
   /// Режим запуска
   /// </summary>
   public enum LaunchMode
   {
      /// <summary>
      /// Командная строка
      /// </summary>
      CommandLine,

      /// <summary>
      /// Оконный режим
      /// </summary>
      Gui
   }
}