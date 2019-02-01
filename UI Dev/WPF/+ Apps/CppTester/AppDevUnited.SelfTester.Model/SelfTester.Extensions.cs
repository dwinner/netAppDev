using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AppDevUnited.SelfTester.Model.Poco;
using AppDevUnited.SelfTester.Model.Utils;

namespace AppDevUnited.SelfTester.Model
{
   /// <summary>
   ///    Метод(ы) расширения для статуса прохождения анализа
   /// </summary>
   public static class CoreExtensions
   {
      /// <summary>
      ///    Статусы, для которых невозможен запуск анализатора
      /// </summary>
      private static readonly AnalysisStatus[] DisablingStatuses =
      {
         AnalysisStatus.VsVersionNotSupported,
         AnalysisStatus.VsVersionNotInstalled,
         AnalysisStatus.PluginNotInstalled
      };

      /// <summary>
      ///    Статусы, для которых анализатор считается запущенным
      /// </summary>
      private static readonly AnalysisStatus[] ActiveStatuses =
      {
         AnalysisStatus.InProgress,
         AnalysisStatus.InPending
      };

      /// <summary>
      ///    Статус-коды, для которых анализ считается завершенным
      /// </summary>
      private static readonly AnalysisStatus[] FinishedStatuses =
      {
         AnalysisStatus.OkFinished,
         AnalysisStatus.DiffFinished,
         AnalysisStatus.PluginCrashed
      };

      /// <summary>
      ///    Статус-коды, для которых можно обновлять эталонные логи
      /// </summary>
      private static readonly AnalysisStatus[] ApproveSuitableStatuses =
      {
         AnalysisStatus.DiffFinished,
         AnalysisStatus.NoSuchEtalon
      };

      /// <summary>
      ///    Получение описания для статуса прохождения анализа
      /// </summary>
      /// <param name="analysisStatus">Статус прохождения анализа</param>
      /// <returns>Описание для статуса прохождения анализа</returns>
      public static string GetAnalysisStatusDescription(this AnalysisStatus analysisStatus)
      {
         return
            analysisStatus.GetType()
               .GetField(analysisStatus.ToString())
               .GetCustomAttributes(typeof(AnalysisStatusDescriptionAttribute), false)
               .Cast<AnalysisStatusDescriptionAttribute>()
               .First().AnalysisStatusDescription;
      }

      /// <summary>
      ///    Является ли статус анализа недоступным для запуска анализатора
      /// </summary>
      /// <param name="status">Статус</param>
      /// <returns>true, если статус анализа недоступен для запуска, false - в противном случае</returns>
      public static bool IsInDisablingStatus(this AnalysisStatus status)
      {
         return DisablingStatuses.Any(disablingStatus => status == disablingStatus);
      }

      /// <summary>
      ///    Находится ли статус анализа в запущенном, но еще незавершенном состоянии
      /// </summary>
      /// <param name="status">Статус</param>
      /// <param name="resetFail">Нужно ли сбрасывать статус аварийного завершения</param>
      /// <returns>true, если статус анализа находится в запущенном, но еще незавершенном состоянии, false - в противном случае</returns>
      public static bool IsInActiveStatus(this AnalysisStatus status, bool resetFail = false)
      {
         var actives = new List<AnalysisStatus>(ActiveStatuses.Length + 1);
         actives.AddRange(ActiveStatuses);
         if (resetFail) actives.Add(AnalysisStatus.PluginCrashed);
         return actives.Any(activeStatus => activeStatus == status);
      }

      /// <summary>
      ///    Находится ли статус анализа в завершенном состоянии
      /// </summary>
      /// <param name="status">Статус</param>
      /// <returns>true, если статус анализа находится в завершенном состоянии, false - в противном случае</returns>
      public static bool IsInFinishedStatus(this AnalysisStatus status)
      {
         return FinishedStatuses.Any(finishedStatus => finishedStatus == status);
      }

      /// <summary>
      ///    Находится ли статус анализа в состоянии, пригодном для обновления эталонов
      /// </summary>
      /// <param name="status">Статус</param>
      /// <returns>true, если статус анализа находится в состоянии, пригодном для обновления эталонов, false - в противном случае</returns>
      public static bool IsInApproveSuitableStatus(this AnalysisStatus status)
      {
         return ApproveSuitableStatuses.Any(analysisStatus => analysisStatus == status);
      }

      public const string EtalonLogExt = ".plog";
   }

   /// <summary>
   ///    Методы расширения для чтения информации о причинах сбоя devenv
   /// </summary>
   public static class CrashReasonExtensions
   {
      /// <summary>
      ///    Получает описание причины сбоя devenv
      /// </summary>
      /// <param name="crashReason">Тип сбоя</param>
      /// <returns>Причина сбоя devenv</returns>
      /// <exception cref="ArgumentNullException">Для данного типа сбоя отсутствует атрибут</exception>
      public static string GetCrashMessage(this CrashReason crashReason)
      {
         return
            crashReason.GetType()
               .GetField(crashReason.ToString())
               .GetCustomAttributes(typeof(CrashMessageAttribute), false)
               .Cast<CrashMessageAttribute>()
               .First()
               .CrashMessage;
      }
   }

   public static class RobocopyExitCodeExtensions
   {
      public const RobocopyExitCode FailureThreshold =
         RobocopyExitCode.NoCopying | RobocopyExitCode.Success | RobocopyExitCode.FileSystemAffected |
         RobocopyExitCode.MismatchedFiles;

      public static string GetRobocopyExitCodeMeaning(this RobocopyExitCode exitCode)
      {
         return
            exitCode.GetType()
               .GetField(exitCode.ToString())
               .GetCustomAttributes(typeof(RobocopyExitCodeMeaningAttribute), false)
               .Cast<RobocopyExitCodeMeaningAttribute>()
               .First()
               .ExitCodeMeaning;
      }

      public static IEnumerable<T> GetEnumValues<T>(this Enum systemEnum)
      {
         return Enum.GetValues(systemEnum.GetType()).Cast<T>();
      }
   }

   /// <summary>
   ///    Методы расширения для модели представления
   /// </summary>
   public static class SolutionInfoViewModelExtensions
   {
      private static readonly Dictionary<string, int> PropertyToColumnIndexMap = new Dictionary<string, int>();

      static SolutionInfoViewModelExtensions()
      {
         var properties = typeof(SolutionInfoViewModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);
         foreach (var propertyInfo in properties)
         {
            foreach (var attribute in propertyInfo.GetCustomAttributes(true))
            {
               var columnIndexAttribute = attribute as ColumnIndexAttribute;
               if (columnIndexAttribute != null)
               {
                  var propertyName = propertyInfo.Name;
                  var columnIndex = columnIndexAttribute.ColumnIndex;
                  PropertyToColumnIndexMap.Add(propertyName, columnIndex);
                  break;
               }
            }
         }
      }

      /// <summary>
      ///    Получение индекса столбца модели представления для имени свойства
      /// </summary>
      /// <param name="solutionInfoViewModel">Модель представления</param>
      /// <param name="propertyName">Имя свойства</param>
      /// <returns>Индекс столбца</returns>
      /// <exception cref="InvalidOperationException">Если свойство с таким именем не существует</exception>
      public static int GetColumnIndex(this SolutionInfoViewModel solutionInfoViewModel, string propertyName)
      {
         if (PropertyToColumnIndexMap.ContainsKey(propertyName))
            return PropertyToColumnIndexMap[propertyName];

         throw new InvalidOperationException(string.Format("{0} is not exists in type {1}", propertyName,
            solutionInfoViewModel.GetType().Name));
      }

      /// <summary>
      ///    Установка нового статуса для анализируемого решения
      /// </summary>
      /// <param name="startupConfigurationMode">Конфигурация запуска</param>
      /// <param name="solutionInfo">Модель решения</param>
      /// <param name="newStatusValue">Новый статус</param>
      public static void SetStatusValue(this SolutionInfoViewModel solutionInfo,
         StartupConfigurationMode startupConfigurationMode, AnalysisStatus newStatusValue)
      {
         switch (startupConfigurationMode)
         {
            case StartupConfigurationMode.PvsVs2010:
               solutionInfo.LaunchStatusOnPvsVs2010 = newStatusValue;
               break;

            case StartupConfigurationMode.PvsVs2012:
               solutionInfo.LaunchStatusOnPvsVs2012 = newStatusValue;
               break;

            case StartupConfigurationMode.PvsVs2013:
               solutionInfo.LaunchStatusOnPvsVs2013 = newStatusValue;
               break;

            case StartupConfigurationMode.PvsVs2015:
               solutionInfo.LaunchStatusOnPvsVs2015 = newStatusValue;
               break;

            case StartupConfigurationMode.All:
               throw new ArgumentException("startupConfigurationMode");
            default:
               throw new InvalidOperationException(string.Format("Unsupported Visual Studio version: {0}",
                  startupConfigurationMode));
         }
      }

      /// <summary>
      ///    Получение текущего статуса для анализируемого решения
      /// </summary>
      /// <param name="startupMode">Конфигурация запуска</param>
      /// <param name="solution">Модель решения</param>
      /// <returns>Текущий статус</returns>
      public static AnalysisStatus GetStatusValue(this StartupConfigurationMode startupMode,
         SolutionInfoViewModel solution)
      {
         AnalysisStatus currentSlnStatus;

         switch (startupMode)
         {
            case StartupConfigurationMode.PvsVs2010:
               currentSlnStatus = solution.LaunchStatusOnPvsVs2010;
               break;

            case StartupConfigurationMode.PvsVs2012:
               currentSlnStatus = solution.LaunchStatusOnPvsVs2012;
               break;

            case StartupConfigurationMode.PvsVs2013:
               currentSlnStatus = solution.LaunchStatusOnPvsVs2013;
               break;

            case StartupConfigurationMode.PvsVs2015:
               currentSlnStatus = solution.LaunchStatusOnPvsVs2015;
               break;

            case StartupConfigurationMode.All:
               throw new ArgumentException("startupConfigurationMode");

            default:
               currentSlnStatus = AnalysisStatus.None;
               break;
         }

         return currentSlnStatus;
      }
   }

   /// <summary>
   ///    Расширения для работы с режимами поддерживаемых запусков
   /// </summary>
   public static class StartupConfigurationModeExtensions
   {
      /// <summary>
      ///    Конфигурации запуска, подходящие для эталонов
      /// </summary>
      private static readonly StartupConfigurationMode[] EtalonSuitableConfigurationModes =
      {
         StartupConfigurationMode.PvsVs2010,
         StartupConfigurationMode.PvsVs2012,
         StartupConfigurationMode.PvsVs2013,
         StartupConfigurationMode.PvsVs2015
      };

      /// <summary>
      ///    Установлена ли версия Visual Studio для конфигурации запуска
      /// </summary>
      /// <param name="startupConfigurationMode">Kонфигурациz запуска</param>
      /// <param name="errorMessage">Сообщение об ошибке</param>
      /// <returns>true, если для конфигурации запуска установлена VS, false - в противном случае</returns>
      public static bool IsVsInstalled(this StartupConfigurationMode startupConfigurationMode, out string errorMessage)
      {
         if (startupConfigurationMode == StartupConfigurationMode.All)
         {
            errorMessage = string.Empty;
            return true;
         }

         switch (startupConfigurationMode)
         {
            case StartupConfigurationMode.PvsVs2010:
               if (!InstallationInfo.IsInstalledVs2010)
               {
                  errorMessage = "Visual Studio 2010 is not installed on this machine";
                  return false;
               }
               break;

            case StartupConfigurationMode.PvsVs2012:
               if (!InstallationInfo.IsInstalledVs2012)
               {
                  errorMessage = "Visual Studio 2012 is not installed on this machine";
                  return false;
               }
               break;

            case StartupConfigurationMode.PvsVs2013:
               if (!InstallationInfo.IsInstalledVs2013)
               {
                  errorMessage = "Visual Studio 2013 is not installed on this machine";
                  return false;
               }
               break;

            case StartupConfigurationMode.PvsVs2015:
               if (!InstallationInfo.IsInstalledVs2015)
               {
                  errorMessage = "Visual Studio 2015 is not installed on this machine";
                  return false;
               }
               break;
         }

         errorMessage = string.Empty;
         return true;
      }

      /// <summary>
      ///    Подходит ли конфигурация запуска для эталонов
      /// </summary>
      /// <param name="startup">Конфигурация запуска</param>
      /// <returns>true, если подходит, false - в противном случае</returns>
      public static bool IsSuitForEtalons(this StartupConfigurationMode startup)
      {
         return EtalonSuitableConfigurationModes.Any(mode => startup == mode);
      }
   }
}