using System;
using Microsoft.Office.Interop.Word;

namespace WordEditTimerAddIn
{
   /// <summary>
   ///    Сущностный класс таймера для MS Word-документа
   /// </summary>
   public sealed class DocumentTimer
   {
      /// <summary>
      ///    Дркумент Word
      /// </summary>
      public Document Document { get; set; }

      /// <summary>
      ///    Время последней активизации
      /// </summary>
      public DateTime LastActive { get; set; }

      /// <summary>
      ///    Признак активности таймера
      /// </summary>
      public bool IsActive { get; set; }

      /// <summary>
      ///    Общее время редактирования
      /// </summary>
      public TimeSpan EditTime { get; set; }
   }
}