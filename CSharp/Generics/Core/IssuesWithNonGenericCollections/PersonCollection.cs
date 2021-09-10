using System.Collections;

namespace IssuesWithNonGenericCollections
{
   public class PersonCollection : IEnumerable
   {
      private readonly ArrayList _arPeople = new ArrayList();

      // Приведение для вызвавшего кода
      public Person GetPerson(int pos)
      {
         return (Person) _arPeople[pos];
      }

      // Вставлять только Person
      public void AddPerson(Person p)
      {
         _arPeople.Add(p);
      }

      public void ClearPeople()
      {
         _arPeople.Clear();
      }

      public int Count
      {
         get
         {
            return _arPeople.Count;
         }
      }

      // Нумератор для поддержи Foreach
      IEnumerator IEnumerable.GetEnumerator()
      {
         return _arPeople.GetEnumerator();
      }
   }
}
