using System;

#if !PVSDIFF
using ProgramVerificationSystems.SelfTester.Model.Poco;
#endif

namespace ProgramVerificationSystems.SelfTester.Model
{
   /// <summary>
   /// Атрибут для описания статусов состояния анализа
   /// </summary>
   [AttributeUsage(AttributeTargets.All)]
   public sealed class AnalysisStatusDescriptionAttribute : Attribute
   {
      private readonly string _analysisStatusDescription;

      public string AnalysisStatusDescription
      {
         get { return _analysisStatusDescription; }
      }

      public AnalysisStatusDescriptionAttribute(string analysisStatusDescription)
      {
         _analysisStatusDescription = analysisStatusDescription;
      }
   }

   /// <summary>
   ///    Атрибут для идентификации индекса столбца в модели представления <see cref="SolutionInfoViewModel" />
   /// </summary>
   [AttributeUsage(AttributeTargets.Property)]
   public sealed class ColumnIndexAttribute : Attribute
   {
      /// <summary>
      ///    Конструктор атрибута
      /// </summary>
      /// <param name="columnIndex">Индекс столбца в модели представления <see cref="SolutionInfoViewModel" /></param>
      public ColumnIndexAttribute(int columnIndex)
      {
         ColumnIndex = columnIndex;
      }

      /// <summary>
      ///    Индекс столбца в модели представления <see cref="SolutionInfoViewModel" />
      /// </summary>
      public int ColumnIndex { get; private set; }
   }

   /// <summary>
   /// Артибут для описания сбоя devenv.exe
   /// </summary>
   [AttributeUsage(AttributeTargets.All, Inherited = false)]
   public sealed class CrashMessageAttribute : Attribute
   {
      private readonly string _crashMessage;

      public string CrashMessage
      {
         get { return _crashMessage; }
      }

      public CrashMessageAttribute(string crashMessage)
      {
         _crashMessage = crashMessage;
      }
   }

   /// <summary>
   ///    Атрибут для расшифровки кода завершения Robocopy
   /// </summary>
   [AttributeUsage(AttributeTargets.All, Inherited = false)]
   public sealed class RobocopyExitCodeMeaningAttribute : Attribute
   {
      /// <summary>
      ///    Конструктор
      /// </summary>
      /// <param name="exitCodeMeaning">Расшифровка кода завершения</param>
      public RobocopyExitCodeMeaningAttribute(string exitCodeMeaning)
      {
         ExitCodeMeaning = exitCodeMeaning;
      }

      /// <summary>
      ///    Расшифровка кода завершения
      /// </summary>
      public string ExitCodeMeaning { get; private set; }
   }

   /// <summary>
   /// Атрибут, связанный с расширением файла лога для типа Plugin'а
   /// </summary>
   [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
   public sealed class LogFileExtensionAttribute : Attribute
   {
      readonly string _logFileExtension;

      public LogFileExtensionAttribute(string logFileExtension)
      {
         _logFileExtension = logFileExtension;
      }

      public string LogFileExtension
      {
         get { return _logFileExtension; }
      }
   }
}