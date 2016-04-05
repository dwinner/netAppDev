using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqOverCollections
{
   #region Simple Car
   class Car
   {
      public string PetName { get; set; }
      public string Color { get; set; }
      public int Speed { get; set; }
      public string Make { get; set; }
   }
   #endregion

   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** LINQ over Generic Collections *****\n");

         // Создадим список объектов Car.
         List<Car> myCars = new List<Car>()
         {
            new Car{ PetName = "Henry", Color = "Silver", Speed = 100, Make = "BMW"},
            new Car{ PetName = "Daisy", Color = "Tan", Speed = 90, Make = "BMW"},
            new Car{ PetName = "Mary", Color = "Black", Speed = 55, Make = "VW"},
            new Car{ PetName = "Clunker", Color = "Rust", Speed = 5, Make = "Yugo"},
            new Car{ PetName = "Melvin", Color = "White", Speed = 43, Make = "Ford"}
         };

         GetFastCars(myCars);
         Console.WriteLine();
         GetFastBMWs(myCars);
         Console.WriteLine();
         LINQOverArrayList();
         Console.WriteLine();
         OfTypeAsFilter();
         Console.ReadLine();
      }

      #region Найти быстрые машины!
      static void GetFastCars(List<Car> myCars)
      {         
         // Найти все объекты Car в списке, у которых скорость больше 55-ти
         var fastCars = from c in myCars where c.Speed > 55 select c;

         foreach (var car in fastCars)
         {
            Console.WriteLine("{0} is going too fast!", car.PetName);
         }
      }

      static void GetFastBMWs(List<Car> myCars)
      {
         // Найти "быстрые" BMW в списке
         var fastCars = from c in myCars where c.Speed > 90 && c.Make == "BMW" select c;
         foreach (var car in fastCars)
         {
            Console.WriteLine("{0} is going too fast!", car.PetName);
         }
      }
      #endregion

      #region LINQ в необобщенных контейнерах
      static void LINQOverArrayList()
      {
         Console.WriteLine("***** LINQ over ArrayList *****");

         // Необобщенная коллекция объектов Car.
         ArrayList myCars = new ArrayList()
         {
            new Car{ PetName = "Henry", Color = "Silver", Speed = 100, Make = "BMW"},
            new Car{ PetName = "Daisy", Color = "Tan", Speed = 90, Make = "BMW"},
            new Car{ PetName = "Mary", Color = "Black", Speed = 55, Make = "VW"},
            new Car{ PetName = "Clunker", Color = "Rust", Speed = 5, Make = "Yugo"},
            new Car{ PetName = "Melvin", Color = "White", Speed = 43, Make = "Ford"}
         };

         // Трансформация необобщенного ArrayList в IEnumerable<T>.
         var myCarsEnum = myCars.OfType<Car>();

         // Создание запроса уже по конкретному типу.
         var fastCars = from c in myCarsEnum where c.Speed > 55 select c;

         foreach (var car in fastCars)
         {
            Console.WriteLine("{0} is going too fast!", car.PetName);
         }
      }
      #endregion

      #region Фильтрация типа в необобщенных контейнерах через OfType()
      static void OfTypeAsFilter()
      {
         // Извлекаем целые значения из ArrayList.
         ArrayList myStuff = new ArrayList();
         myStuff.AddRange(new object[] { 10, 400, 8, false, new Car(), "string data" });
         var myInts = myStuff.OfType<int>();

         // Вывод на печать 10, 400, и 8.
         foreach (int i in myInts)
         {
            Console.WriteLine("Int value: {0}", i);
         }
      }
      #endregion
   }
}
