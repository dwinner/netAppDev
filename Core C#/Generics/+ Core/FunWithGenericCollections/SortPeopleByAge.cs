using System.Collections.Generic;

namespace FunWithGenericCollections
{
   /// <summary>
   /// Компаратор для сортированного множеста
   /// <remarks>Сортирует в порядке убывания возраста</remarks>
   /// </summary>
   public class SortPeopleByAge : IComparer<Person>
   {
      public int Compare(Person firstPerson, Person secondPerson)
      {
         unchecked
         {
            return firstPerson.Age - secondPerson.Age;
         }
         // return firstPerson.Age > secondPerson.Age ? firstPerson.Age < secondPerson.Age ? -1 : 0 : 1;
      }
   }
}
