using System;

namespace DataLibrary
{
   /// <summary>
   /// Сущностный класс таблицы студентов
   /// </summary>
   [Serializable]
   public class Student
   {
      public int Id { get; set; }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string Company { get; set; }

      public override string ToString()
      {
         return string.Format("FirstName: {0}, LastName: {1}, Id: {2}, Company: {3}", FirstName, LastName, Id, Company);
      }
   }
}