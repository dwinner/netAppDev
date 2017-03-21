using ProgramVerificationSystems.SelfTester.Model.Poco;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProgramVerificationSystems.SelfTester.Model
{
   /// <summary>
   ///    Аргументы события при получении результата анализа
   /// </summary>
   public class TestAnalysisEventArgs : EventArgs
   {
      /// <summary>
      ///    Конструктор аргументов события анализа
      /// </summary>
      /// <param name="newStatus">Новый статус анализа</param>
      /// <param name="solutionInfoViewModel">Информация о VS-решении</param>
      /// <param name="startupMode">Конфигурация запуска</param>
      /// <param name="crashReason">Возможная причина сбоя</param>
      /// <param name="devenvExitCode">Код завершения devenv.exe</param>
      public TestAnalysisEventArgs(AnalysisStatus newStatus, SolutionInfoViewModel solutionInfoViewModel,
         StartupConfigurationMode startupMode, CrashReason crashReason = default(CrashReason),
         int devenvExitCode = default(int))
      {
         NewStatus = newStatus;
         SolutionInfo = solutionInfoViewModel;
         StartupMode = startupMode;
         CrashReason = crashReason;
         DevenvExitCode = devenvExitCode;
      }

      /// <summary>
      ///    Новый статус анализа
      /// </summary>
      public AnalysisStatus NewStatus { get; private set; }

      /// <summary>
      ///    Информация о VS-решении
      /// </summary>
      public SolutionInfoViewModel SolutionInfo { get; private set; }

      /// <summary>
      ///    Конфигурация запуска
      /// </summary>
      public StartupConfigurationMode StartupMode { get; private set; }

      /// <summary>
      ///    Возможная причина сбоя
      /// </summary>
      public CrashReason CrashReason { get; private set; }

      /// <summary>
      ///    Код завершения devenv.exe
      /// </summary>
      public int DevenvExitCode { get; private set; }
   }

   /// <summary>
   /// Аргументы события при завершении одного тестового запуста
   /// </summary>
   public class CurrentAnalysisDoneEventArgs : EventArgs
   {
      /// <summary>
      /// Кол-во завершенных запусков
      /// </summary>
      public int CurrentFinishedCount { get; private set; }

      /// <summary>
      /// Конструктор аргументов события при завершении одного тестового запуста
      /// </summary>
      /// <param name="currentFinishedCount">Кол-во завершенных запусков</param>
      public CurrentAnalysisDoneEventArgs(int currentFinishedCount)
      {
         CurrentFinishedCount = currentFinishedCount;
      }
   }

   public sealed class FaultedEventArgs : EventArgs
   {
      public IReadOnlyList<Process> FaultProcesses { get; private set; }

      public FaultedEventArgs(IReadOnlyList<Process> faultProcesses)
      {
         FaultProcesses = faultProcesses;
      }
   }

   public sealed class HangoutEventArgs : EventArgs
   {
      public IReadOnlyList<Process> HangoutProcesses { get; private set; }

      public HangoutEventArgs(IReadOnlyList<Process> hangoutProcesses)
      {
         HangoutProcesses = hangoutProcesses;
      }
   }
}