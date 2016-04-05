/**
 * Необобщенные контейнеры страдают от потери производительности
 * при операциях упаковки/распаковки и потенциально опасных приведений типов.
 */

using System;
using System.Collections.Generic;
using System.Collections;

namespace IssuesWithNonGenericCollections
{
   class Program
   {
      static void Main()
      {
         Console.WriteLine("***** Working with Non-Generic Collections *****");

         SimpleBoxUnboxOperation();
         Console.WriteLine();

         WorkWithArrayList();
         Console.WriteLine();

         ArrayListOfRandomObjects();
         Console.WriteLine();

         UsePersonCollection();
         Console.WriteLine();

         UseGenericList();
         Console.WriteLine();

         Console.ReadLine();
      }

      #region Упаковка/Распаковка
      static void SimpleBoxUnboxOperation()
      {
         // Создать значимый тип
         const int myInt = 25;

         // Упаковать значимый тип в объект
         object boxedInt = myInt;

         // Распаковка значений может породить исключение InvalidCastException
         try
         {
            long unboxedInt = (long) boxedInt;
         }
         catch (InvalidCastException ex)
         {
            Console.WriteLine(ex.Message);
         }
      }
      #endregion

      #region ArrayList Examples
      static void WorkWithArrayList()
      {         
         // Значимые типы автоматически подвергнуться упаковки в System.Object
         ArrayList myInts = new ArrayList {10, 20, 35};
         
         // Распаковка обратно в int при явном приведении
         int i = (int) myInts[0];

         // WriteLine() нужен объект, поэтому вновь произойдет упаковка!
         Console.WriteLine("Value of your int: {0}", i);
      }

      static void ArrayListOfRandomObjects()
      {
         // Динамический массив объектов может содержать ВСЕ
         ArrayList allMyObjects = new ArrayList
            {
               true,
               new OperatingSystem(PlatformID.MacOSX, new Version(10, 0)),
               66,
               3.14
            };
      }
      #endregion

      #region Необобщенный контейнер
      static void UsePersonCollection()
      {
         Console.WriteLine("***** Custom Person Collection *****\n");
         PersonCollection myPeople = new PersonCollection();
         myPeople.AddPerson(new Person("Homer", "Simpson", 40));
         myPeople.AddPerson(new Person("Marge", "Simpson", 38));
         myPeople.AddPerson(new Person("Lisa", "Simpson", 9));
         myPeople.AddPerson(new Person("Bart", "Simpson", 7));
         myPeople.AddPerson(new Person("Maggie", "Simpson", 2));         

         foreach (Person p in myPeople)
         {
            Console.WriteLine(p);
         }
      }
      #endregion

      #region Первый взгляд на обобщения
      static void UseGenericList()
      {
         Console.WriteLine("***** Fun with Generics *****\n");
         
         List<Person> morePeople = new List<Person> {new Person("Frank", "Black", 50)};
         Console.WriteLine(morePeople[0]);
         
         List<int> moreInts = new List<int> {10, 2};
         int sum = moreInts[0] + moreInts[1];         
      }
      #endregion
   }
}
