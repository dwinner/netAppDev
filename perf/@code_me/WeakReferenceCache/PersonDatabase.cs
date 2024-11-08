using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeakReferenceCache
{
    class Person
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }

    class PersonDatabase
    {
        private Dictionary<string, Person> index = new Dictionary<string, Person>();
        private Dictionary<DateTime, List<WeakReference<Person>>> birthdayIndex = new Dictionary<DateTime, List<WeakReference<Person>>>();

        public bool NeedsIndexRebuild { get; private set; }

        public void AddPerson(Person person)
        {
            this.index[person.Id] = person;
            List<WeakReference<Person>> birthdayList;
            if (!this.birthdayIndex.TryGetValue(person.Birthday, out birthdayList))
            {
                birthdayIndex[person.Birthday] = birthdayList = new List<WeakReference<Person>>();
            }

            birthdayList.Add(new WeakReference<Person>(person));
        }

        public void RemovePerson(string id)
        {
            index.Remove(id);
        }

        public bool TryGetById(string id, out Person person)
        {
            return this.index.TryGetValue(id, out person);
        }

        public bool TryGetByBirthday(DateTime birthday, out List<Person> people)
        {
            people = null;
            List<WeakReference<Person>> weakPeople;
            if (this.birthdayIndex.TryGetValue(birthday, out weakPeople))
            {
                var list = new List<Person>(weakPeople.Count);
                foreach(var wp in weakPeople)
                {
                    Person person;
                    if (wp.TryGetTarget(out person))
                    {
                        list.Add(person);
                    }
                    else
                    {
                        // we got a null reference -- we need to rebuild the indexes
                        this.NeedsIndexRebuild = true;
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
