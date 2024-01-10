/**
 * Обобщенные контейнеры
 */

using System;
using System.Collections.Generic;

namespace FunWithGenericCollections
{
   class Program
   {
      static void Main()
      {
         Console.WriteLine("***** Fun with Generic Collections *****\n");

         UseGenericList();
         Console.WriteLine();

         UseGenericStack();
         Console.WriteLine();

         UseGenericQueue();
         Console.WriteLine();

         UseSortedSet();
         Console.WriteLine();

         Console.ReadLine();
      }

      #region Обобщенный динамический массив
      static void UseGenericList()
      {
         List<Person> people = new List<Person>()
              {
                new Person {FirstName= "Homer", LastName="Simpson", Age=47},
                new Person {FirstName= "Marge", LastName="Simpson", Age=45},
                new Person {FirstName= "Lisa", LastName="Simpson", Age=9},
                new Person {FirstName= "Bart", LastName="Simpson", Age=8}
              };
         
         Console.WriteLine("Items in list: {0}", people.Count);
         
         foreach (Person p in people)
         {
            Console.WriteLine(p);
         }
         
         Console.WriteLine("\n->Inserting new person.");
         people.Insert(2, new Person { FirstName = "Maggie", LastName = "Simpson", Age = 2 });
         Console.WriteLine("Items in list: {0}", people.Count);

         // Копируем данные в новый массив
         Person[] arrayOfPeople = people.ToArray();
         foreach (Person person in arrayOfPeople)
         {
            Console.WriteLine("First Names: {0}", person.FirstName);
         }
      }
      #endregion

      #region Обобщенный стек
      static void UseGenericStack()
      {
         Stack<Person> stackOfPeople = new Stack<Person>();
         stackOfPeople.Push(new Person { FirstName = "Homer", LastName = "Simpson", Age = 47 });
         stackOfPeople.Push(new Person { FirstName = "Marge", LastName = "Simpson", Age = 45 });
         stackOfPeople.Push(new Person { FirstName = "Lisa", LastName = "Simpson", Age = 9 });
         
         Console.WriteLine("First person is: {0}", stackOfPeople.Peek());  // Вернем самый верхний элемент без удаления
         Console.WriteLine("Popped off {0}", stackOfPeople.Pop());   // Извлечем самый верхний элемент

         Console.WriteLine("\nFirst person is: {0}", stackOfPeople.Peek());
         Console.WriteLine("Popped off {0}", stackOfPeople.Pop());

         Console.WriteLine("\nFirst person item is: {0}", stackOfPeople.Peek());
         Console.WriteLine("Popped off {0}", stackOfPeople.Pop());

         try
         {
            Console.WriteLine("\nnFirst person is: {0}", stackOfPeople.Peek());
            Console.WriteLine("Popped off {0}", stackOfPeople.Pop());
         }
         catch (InvalidOperationException ex)
         {
            Console.WriteLine("\nError! {0}", ex.Message);
         }
      }
      #endregion

      #region Обобщеннвя очередь
      static void GetCoffee(Person p)
      {
         Console.WriteLine("{0} got coffee!", p.FirstName);
      }

      static void UseGenericQueue()
      {         
         Queue<Person> peopleQ = new Queue<Person>();
         peopleQ.Enqueue(new Person { FirstName = "Homer", LastName = "Simpson", Age = 47 });
         peopleQ.Enqueue(new Person { FirstName = "Marge", LastName = "Simpson", Age = 45 });
         peopleQ.Enqueue(new Person { FirstName = "Lisa", LastName = "Simpson", Age = 9 });

         // Вернем объект в начале очереди
         Console.WriteLine("{0} is first in line!", peopleQ.Peek().FirstName);

         // Удалить элементы из очереди
         GetCoffee(peopleQ.Dequeue());
         GetCoffee(peopleQ.Dequeue());
         GetCoffee(peopleQ.Dequeue());
         
         try
         {
            GetCoffee(peopleQ.Dequeue());
         }
         catch (InvalidOperationException e)
         {
            Console.WriteLine("Error! {0}", e.Message);
         }
      }

      #endregion

      #region Use SortedSet<>
      static void UseSortedSet()
      {         
         SortedSet<Person> setOfPeople = new SortedSet<Person>(new SortPeopleByAge())
              {
                new Person {FirstName= "Homer", LastName="Simpson", Age=47},
                new Person {FirstName= "Marge", LastName="Simpson", Age=45},
                new Person {FirstName= "Lisa", LastName="Simpson", Age=9},
                new Person {FirstName= "Bart", LastName="Simpson", Age=8}
              };

         // Note: Элементы отсортированы по возрасту
         foreach (var p in setOfPeople)
         {
            Console.WriteLine(p);
         }
         Console.WriteLine();
         
         setOfPeople.Add(new Person { FirstName = "Saku", LastName = "Jones", Age = 1 });
         setOfPeople.Add(new Person { FirstName = "Mikko", LastName = "Jones", Age = 32 });

         // Все еще отсортированы по возрасту!
         foreach (Person p in setOfPeople)
         {
            Console.WriteLine(p);
         }
      }

      #endregion
   }
}
