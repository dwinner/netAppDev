using System;
using System.Collections.Generic;

namespace WeakReferenceCache
{
   internal class PersonDatabase
   {
      private readonly Dictionary<DateTime, List<WeakReference<Person>>> _birthdayIndex =
         new Dictionary<DateTime, List<WeakReference<Person>>>();

      private readonly Dictionary<string, Person> _index = new Dictionary<string, Person>();

      public bool NeedsIndexRebuild { get; private set; }

      public void AddPerson(Person person)
      {
         _index[person.Id] = person;
         if (!_birthdayIndex.TryGetValue(person.Birthday, out var birthdayList))
         {
            _birthdayIndex[person.Birthday] = birthdayList = new List<WeakReference<Person>>();
         }

         birthdayList.Add(new WeakReference<Person>(person));
      }

      public void RemovePerson(string id)
      {
         _index.Remove(id);
      }

      public bool TryGetById(string id, out Person person) => _index.TryGetValue(id, out person);

      public bool TryGetByBirthday(DateTime birthday, out List<Person> people)
      {
         people = null;
         if (_birthdayIndex.TryGetValue(birthday, out var weakPeople))
         {
            var list = new List<Person>(weakPeople.Count);
            foreach (var weakRef in weakPeople)
            {
               if (weakRef.TryGetTarget(out var person))
               {
                  list.Add(person);
               }
               else
               {
                  // we got a null reference -- we need to rebuild the indexes
                  NeedsIndexRebuild = true;
               }
            }

            if (list.Count > 0)
            {
               people = list;
               return true;
            }
         }

         return false;
      }
   }
}