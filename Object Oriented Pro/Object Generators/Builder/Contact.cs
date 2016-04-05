using System;

namespace Builder
{
   /// <summary>
   /// Интерфейс контактов.
   /// </summary>   
   public interface IContact
   {
      string FirstName { get; set; }
      string LastName { get; set; }
      string Title { get; set; }
      string Organization { get; set; }      
   }

   /// <summary>
   /// Реализация интерфейса IContact
   /// </summary>  
   [Serializable]
   public class ContactImpl : IContact
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Title { get; set; }
      public string Organization { get; set; }

      public ContactImpl(string firstName, string lastName, string title, string organization)
      {
         FirstName = firstName;
         LastName = lastName;
         Title = title;
         Organization = organization;
      }

      public override string ToString()
      {
         return FirstName + " " + LastName;
      }
   }
}
