using System.Collections;
using System.Collections.Generic;

namespace StringIndexer
{
   public class PersonCollection : IEnumerable
   {
      private Dictionary<string, Person> listPeople = new Dictionary<string, Person>();

      // Индексатор для строкового значения.
      public Person this[string name]
      {
         get
         {
            return listPeople[name];
         }
         set
         {
            listPeople[name] = value;
         }
      }

      public void ClearPeople()
      {
         listPeople.Clear();
      }

      public int Count
      {
         get
         {
            return listPeople.Count;
         }
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return listPeople.GetEnumerator();
      }
   }
}
