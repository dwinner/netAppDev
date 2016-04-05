using System;
using System.Data;
using System.Data.EntityClient;
using System.Linq;

namespace InventoryEdmConsoleApp
{
   public static class Program
   {
      static void Main(string[] args)
      {
         AddNewRecord();
         PrintAllInventories();
         UpdateRecord();
         RemoveRecord();
         RemoveRecordWithLinq();
         LinqQueries();
         EntityDataReader();
         Console.ReadLine();
      }

      private static void PrintAllInventories() // Вывод всех элементов таблицы
      {
         using (var context = new AutoLotEntities())
         {
            foreach (Car car in context.Cars)
            {
               Console.WriteLine(car);
            }
         }
      }

      private static void AddNewRecord()  // Добавление новой записи
      {
         using (var context = new AutoLotEntities())
         {
            try
            {
               context.Cars.Add(new Car()
                  {
                     CarId = 3333,
                     Make = "Yugo",
                     Color = "Brown"
                  });
               context.SaveChanges();
            }
            catch (Exception exception)
            {
               Console.WriteLine(exception.InnerException.Message);
            }
         }
      }

      private static void RemoveRecord()  // Удаление записи
      {
         using (var entities = new AutoLotEntities())
         {
            // EntityKey key = new EntityKey("AutoLotEntities.Cars", "CarId", 2222);
            Car carToDelete = entities.Cars.Find(2222); // Поиск по ключу для искомой сущности
            if (carToDelete != null)
            {
               entities.Cars.Remove(carToDelete);
               entities.SaveChanges();
            }
         }
      }

      private static void UpdateRecord()  // Обновление записи
      {
         using (var entities = new AutoLotEntities())
         {
            Car carToUpdate = entities.Cars.Find(2222);
            if (carToUpdate != null)
            {
               carToUpdate.Color = "Blue";
               entities.SaveChanges();
            }
         }
      }

      private static void LinqQueries()   // LINQ To Entities
      {
         using (var entities = new AutoLotEntities())
         {
            var colorsMakes = from item in entities.Cars // Получить проекцию новых данных
                              select new
                                 {
                                    item.Color,
                                    item.Make
                                 };
            foreach (var colorsMake in colorsMakes)
            {
               Console.WriteLine(colorsMake);
            }
            var idsLessThanThousand = from item in entities.Cars  // Получить элементы с CarId < 1000
                                      where item.CarId < 1000
                                      select item;
            foreach (var car in idsLessThanThousand)
            {
               Console.WriteLine(car);
            }
         }
      }

      private static void RemoveRecordWithLinq()   // Удаление записи через LINQ
      {
         using (var entities = new AutoLotEntities())
         {
            var carToDelete = (from car in entities.Cars
                               where car.CarId == 2222
                               select car).FirstOrDefault();
            if (carToDelete != null)
            {
               entities.Cars.Remove(carToDelete);
               entities.SaveChanges();
            }
         }         
      }

      private static void SelectByLinq()  // Получение данных через LINQ
      {
         using (var entities = new AutoLotEntities())
         {
            var allData = entities.Cars.ToArray();
            var colorMakes = from item in allData
                             select new
                                {
                                   item.Color,
                                   item.Make
                                };
            var ids = from item in allData
                      where item.CarId < 1000
                      select item;
         }
      }

      private static void EntityDataReader()
      {
         using (var entityConnection = new EntityConnection("name=AutoLotEntities"))   // Создать объект соединения на основе файла *.config
         {
            entityConnection.Open();
            const string query = "SELECT VALUE car FROM AutoLotEntities.Cars AS car"; // Построить запрос Entity SQL
            using (EntityCommand entityCommand = entityConnection.CreateCommand())  // Создать командный объект
            {
               entityCommand.CommandText = query;
               // Получить объект для чтения данных и обработать записи
               using (EntityDataReader entityDataReader = entityCommand.ExecuteReader(CommandBehavior.SequentialAccess))
               {
                  while (entityDataReader.Read())
                  {
                     Console.WriteLine("----- RECORD -----");
                     Console.WriteLine("Id: {0}", entityDataReader["CarId"]);
                     Console.WriteLine("Make: {0}", entityDataReader["Make"]);
                     Console.WriteLine("Color: {0}", entityDataReader["Color"]);
                     Console.WriteLine("Pet name: {0}", entityDataReader["CarNickname"]);
                     Console.WriteLine();
                  }
               }
            }
         }
      }

      /* Entity SQL example
      private static void FunWithEntitySQL()
      {
         using (AutoLotEntities context = new AutoLotEntities())
         {
            // Build a string containing Entity SQL syntax.
            string query = "SELECT VALUE car FROM AutoLotEntities.Cars AS car WHERE car.Color='black'";

            // Now build a ObjectQuery<T> based on the string.
            var blackCars = context.CreateQuery<Car>(query);            

            foreach (var item in blackCars)
            {
               Console.WriteLine(item);
            }
         }
      }
      */
   }
}
