using System.Collections;
using System.Collections.Generic;

namespace SimpleIndexer
{
   public class PersonCollection : IEnumerable
   {
      private List<Person> arPeople = new List<Person>();

      public Person GetPerson(int pos)
      {
         return (pos >= 0 && pos < arPeople.Count)
            ? arPeople[pos]
            : new Person.NullPerson();
      }

      public void AddPerson(Person p)
      {
         arPeople.Add(p);
      }

      public void ClearPeople()
      {
         arPeople.Clear();
      }

      public int Count
      {
         get
         {
            return arPeople.Count;
         }
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return arPeople.GetEnumerator();
      }

      // Индексатор для класса.
      public Person this[int index]
      {
         get
         {
            return GetPerson(index);
         }
         set
         {
            arPeople.Insert(index, value);
         }
      }
   }
}
