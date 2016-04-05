using System;

namespace SimpleGC
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with System.GC *****");

         // Число байтов в управляемой куче.
         Console.WriteLine("Estimated bytes on heap: {0}",
           GC.GetTotalMemory(false));

         // Количество поколений сборщика мусора на этой ОС.
         Console.WriteLine("This OS has {0} object generations.\n",
           (GC.MaxGeneration + 1));

         Car refToMyCar = new Car("Zippy", 100);
         Console.WriteLine(refToMyCar.ToString());

         // Поколение ссылки refToMyCar.
         Console.WriteLine("\nGeneration of refToMyCar is: {0}",
           GC.GetGeneration(refToMyCar));

         // Большое количество объектов.
         object[] tonsOfObjects = new object[50000];
         for (int i = 0; i < 50000; i++)
            tonsOfObjects[i] = new object();

         // Чистим ссылки первого (эфемерного) поколения объектов.
         GC.Collect(0, GCCollectionMode.Forced);
         GC.WaitForPendingFinalizers();

         // Поколение ссылки refToMyCar увеличилось.
         Console.WriteLine("Generation of refToMyCar is: {0}",
           GC.GetGeneration(refToMyCar));

         // Проверим доступность объекта tonsOfObjects[9000].
         if (tonsOfObjects[9000] != null)
         {
            Console.WriteLine("Generation of tonsOfObjects[9000] is: {0}",
              GC.GetGeneration(tonsOfObjects[9000]));
         }
         else
            Console.WriteLine("tonsOfObjects[9000] is no longer alive.");

         // Как часто сборщик мусора чистил все доступные поколения объектов
         for (int i = 0; i <= GC.MaxGeneration; i++)
         {
            Console.WriteLine("Gen {0} has been swept {0} times",
               i, GC.CollectionCount(i));
         }

         Console.ReadLine();
      }

      /// <summary>
      /// Локальный объект будет уничтожен при следующей сборке мусора
      /// </summary>
      static void MakeACar()
      {
         Car myCar = new Car();
         myCar = null;
      }
   }
}
