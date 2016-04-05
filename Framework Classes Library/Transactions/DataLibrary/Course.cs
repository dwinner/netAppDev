using System;

namespace DataLibrary
{
   /// <summary>
   /// Сущностный класс таблицы курсов
   /// </summary>
   [Serializable]
   public class Course
   {
      public int Id { get; set; }

      public string Number { get; set; }

      public string Title { get; set; }
   }
}