/**
 * Наблюдаемые контейнеры
 */

using System;
using System.Collections.ObjectModel;

namespace FunWithObservableCollection
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with Observable Collections *****\n");

         // Создать наблюдаемую коллекцию и добавить несколько объектов
         ObservableCollection<Person> people = new ObservableCollection<Person>
            {
                new Person{ FirstName = "Peter", LastName = "Murphy", Age = 52 },
                new Person{ FirstName = "Kevin", LastName = "Key", Age = 48 },
            };

         // Зарегистрировать событие изменения коллекции
         people.CollectionChanged += people_CollectionChanged;         

         // Добавить новый элемент
         people.Add(new Person("Fred", "Smith", 32));

         // Удалить элемент
         people.RemoveAt(0);

         Console.ReadLine();
      }

      #region Обратный вызов при изменении коллекции
      static void people_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
      {
         // Какое действие возбудило событие?
         Console.WriteLine("Action for this event: {0}", e.Action);

         // Что-то было удалено
         if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
         {
            // Покажем удаленные элементы
            Console.WriteLine("Here are the OLD items:");
            foreach (Person person in e.OldItems)
            {
               Console.WriteLine(person.ToString());
            }
            Console.WriteLine();
         }

         // Что-то было добавлено
         if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
         {
            // Покажем добавленные элементы
            Console.WriteLine("Here are the NEW items:");
            foreach (Person person in e.NewItems)
            {
               Console.WriteLine(person.ToString());
            }
         }
      }
      #endregion
   }
}
