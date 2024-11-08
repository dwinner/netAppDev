using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeakReferenceCache
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HybridCache");
            HybridCache<int, string> cache = new HybridCache<int, string>(TimeSpan.FromSeconds(5));

            cache.Add(1, "One");
            cache.Add(2, "Two");
            Thread.Sleep(5000);
            // Read a value
            string val;
            bool success = cache.TryGetValue(1, out val);
            Console.WriteLine(val);
            Console.WriteLine("Success: " + success);

            GC.Collect();
            GC.WaitForFullGCComplete();

            // Try reading again
            success = cache.TryGetValue(1, out val);
            Console.WriteLine(val);
            Console.WriteLine("Success: " + success);
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Multi-index");

            Person p1 = new Person() { Id = "1", FirstName = "Bob", LastName = "Jones", Birthday = new DateTime(1980, 1, 1) };
            Person p2 = new Person() { Id = "2", FirstName = "Alice", LastName = "Smith", Birthday = new DateTime(1980, 1, 1) };
            Person p3 = new Person() { Id = "3", FirstName = "Eve", LastName = "Brown", Birthday = new DateTime(1978, 8, 1) };

            var db = new PersonDatabase();
            db.AddPerson(p1);
            db.AddPerson(p2);
            db.AddPerson(p3);

            // get rid of our strong references
            p1 = p2 = p3 = null;

            var targetBirthday = new DateTime(1980, 1, 1);

            PrintBirthdays(db, targetBirthday);

            db.RemovePerson("2");

            PrintBirthdays(db, targetBirthday);

            GC.Collect();
            Console.WriteLine("After GC");
            PrintBirthdays(db, targetBirthday);
        }

        private static void PrintBirthdays(PersonDatabase db, DateTime targetBirthday)
        {
            Console.WriteLine("Birthdays on Jan 1, 1980:");
            List<Person> people;
            db.TryGetByBirthday(targetBirthday, out people);
            foreach (var p in people)
            {
                Console.Write("\t");
                Console.WriteLine(p.FirstName);
            }
            Console.WriteLine("Indexes need rebuilt? " + db.NeedsIndexRebuild);
            Console.WriteLine();
        }
    }
}
