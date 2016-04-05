using System;
using System.Collections.Generic;
using System.Linq;

namespace FunWithLinqExpressions
{
   class Program
   {
      public static void Main(string[] args)
      {
         Console.WriteLine("***** Query Expressions *****\n");
         
         // Этот массив будет основой для тестирования...
         ProductInfo[] itemsInStock = new[]
         {
            new ProductInfo
            {
               Name = "Mac's Coffee",
               Description = "Coffee with TEETH",
               NumberInStock = 24
            },
            new ProductInfo
            {
               Name = "Milk Maid Milk",
               Description = "Milk cow's love",
               NumberInStock = 100
            },
            new ProductInfo
            {
               Name = "Pure Silk Tofu",
               Description = "Bland as Possible",
               NumberInStock = 120
            },
            new ProductInfo
            {
               Name = "Cruchy Pops",
               Description = "Cheezy, peppery goodness",
               NumberInStock = 2
            },
            new ProductInfo
            {
               Name = "RipOff Water",
               Description = "From the tap to your wallet",
               NumberInStock = 100
            },
            new ProductInfo
            {
               Name = "Classic Valpo Pizza",
               Description = "Everyone loves pizza!",
               NumberInStock = 73
            }
         };
         
         // Здесь будем вызывать разнообразные методы!
         
         SelectEverything(itemsInStock);
         Console.WriteLine();
         
         ListProductNames(itemsInStock);
         Console.WriteLine();
         
         GetOverstock(itemsInStock);
         Console.WriteLine();
         
         GetNamesAndDescriptions(itemsInStock);
         Console.WriteLine();
         
         Array objs = GetProjectedSubset(itemsInStock);
         foreach (object o in objs)
         {
            Console.WriteLine(o);   // Вызвать ToString() на каждом анонимном объекте.
         }
         
         GetCountFromQuery();
         Console.WriteLine();
         
         ReverseEverything(itemsInStock);
         Console.WriteLine();
         
         AlphabetizeProductNames(itemsInStock);
         Console.WriteLine();
         
         DisplayDiff();
         Console.WriteLine();
         DisplayIntersection();
         Console.WriteLine();
         DisplayUnion();
         Console.WriteLine();
         DisplayConcat();
         Console.WriteLine();
         DisplayConcatNoDups();
         Console.WriteLine();
         
         AggregateOps();
         
         Console.Write("Press any key to continue . . . ");
         Console.ReadKey(true);
      }
      
      #region Выборка всего
      static void SelectEverything(ProductInfo[] products)
      {
         // Получить всё!
         Console.WriteLine("All product details:");
         var allProducts = from p in products select p;
         foreach (var prod in allProducts)
         {
            Console.WriteLine(prod.ToString());
         }
      }
      #endregion      
      
      #region Выборка имен
      static void ListProductNames(ProductInfo[] products)
      {
         // Теперь получить только наименования товаров.
         Console.WriteLine("Only product names:");
         var names = from p in products select p.Name;
         foreach (var n in names)
         {
            Console.WriteLine("Name: {0}", n);
         }
      }
      #endregion      
      
      #region Получение подмножества данных
      static void GetOverstock(ProductInfo[] products)
      {
         Console.WriteLine("The overstock items!");
         // Получить только те товары, которых на складе более 25.
         var overstock = from p in products where p.NumberInStock > 25 select p;
         foreach (ProductInfo c in overstock)
         {
            Console.WriteLine(c.ToString());
         }
      }
      #endregion
      
      #region Проекции новых типов данных
      static void GetNamesAndDescriptions(ProductInfo[] products)
      {
         Console.WriteLine("Names and descriptions:");
         var nameDesc = from p in products
            select new { p.Name, p.Description };
         foreach (var item in nameDesc)
         {
            // Можно также использовать свойства Name и Description напрямую.
            Console.WriteLine(item.ToString());
         }
      }
      
      static Array GetProjectedSubset(ProductInfo[] products)
      {
         var nameDesc = from product in products
            select new { product.Name, product.Description };
         return nameDesc.ToArray();
      }
      #endregion
      
      #region Получение количества записей из запроса
      static void GetCountFromQuery()
      {
         string[] currentVideoGames =
         {
            "Morrowind",
            "Uncharted 2",
            "Fallout 3",
            "Daxter",
            "System Shock 2"
         };
         // Получить количество из запроса
         int number =
            (from g in currentVideoGames
             where g.Length > 6
             select g).Count<string>();
         // Вывести на консоль количество элементов.
         Console.WriteLine("{0} items honor the LINQ query.", number);
      }
      #endregion
      
      #region Изменение порядка элементов в наборе
      static void ReverseEverything(ProductInfo[] products)
      {
         Console.WriteLine("Product in reverse:");
         var allProducts = from p in products select p;
         foreach (var prod in allProducts.Reverse())
         {
            Console.WriteLine(prod.ToString());
         }
      }
      #endregion
      
      #region Выражения сортировки
      static void AlphabetizeProductNames(ProductInfo[] products)
      {
         // Получить названия товаров в алфавитном порядке.
         var subset = from p in products orderby p.Name /*ascending|descending*/ select p;
         Console.WriteLine("Ordered by Name:");
         foreach (var p in subset)
         {
            Console.WriteLine(p.ToString());
         }
      }
      #endregion
      
      #region Операции над множествами
      static void DisplayDiff()  // Разность между контейнерами
      {
         List<string> myCars = new List<string>()
         {
            "Yugo",
            "Aztec",
            "BMW"
         };
         List<string> yourCars = new List<string>()
         {
            "BMW",
            "Saab",
            "Aztec"
         };
         var carDiff =
            (from aCar in myCars select aCar)
            .Except
            (from aCar in yourCars select aCar);
         Console.WriteLine("Here is what you don't have, but I do:");
         foreach (string s in carDiff)
         {
            Console.WriteLine(s);   // Yugo
         }
      }
      
      static void DisplayIntersection()   // Пересечение контейнеров
      {
         List<string> myCars = new List<string>()
         {
            "Yugo",
            "Aztec",
            "BMW"
         };
         List<string> yourCars = new List<string>()
         {
            "BMW",
            "Saab",
            "Aztec"
         };
         var carIntersect =
            (from c in myCars select c)
            .Intersect
            (from c2 in yourCars select c2);
         Console.WriteLine("Here is what we have in common:");
         foreach (string s in carIntersect)
         {
            Console.WriteLine(s);   // Aztec и BMW
         }
      }
      
      static void DisplayUnion() // Объединение контейнеров
      {
         List<string> myCars = new List<string>()
         {
            "Yugo",
            "Aztec",
            "BMW"
         };
         List<string> yourCars = new List<string>()
         {
            "BMW",
            "Saab",
            "Aztec"
         };
         // Получить объединение двух контейнеров
         var carUnion =
            (from c in myCars select c)
            .Union
            (from c2 in yourCars select c2);
         Console.WriteLine("Here is everything:");
         foreach (string s in carUnion)
         {
            Console.WriteLine(s);   // Выводит все общие члены
         }
      }
      
      static void DisplayConcat()   // Прямая конкатенация контейнеров
      {
         List<string> myCars = new List<string>()
         {
            "Yugo",
            "Aztec",
            "BMW"
         };
         List<string> yourCars = new List<string>()
         {
            "BMW",
            "Saab",
            "Aztec"
         };
         var carConcat =
            (from c in myCars select c)
            .Concat
            (from c2 in yourCars select c2);
         // Выводит все члены
         foreach (string s in carConcat)
         {
            Console.WriteLine(s);
         }
      }
      
      static void DisplayConcatNoDups()   // Исключение дубликатов
      {
         List<string> myCars = new List<string>()
         {
            "Yugo",
            "Aztec",
            "BMW"
         };
         List<string> yourCars = new List<string>()
         {
            "BMW",
            "Saab",
            "Aztec"
         };
         var carConcat =
            (from c in myCars select c)
            .Concat
            (from c2 in yourCars select c2);
         foreach (string s in carConcat.Distinct())
         {
            Console.WriteLine(s);
         }
      }
      #endregion
      
      #region Агрегатные операции LINQ
      static void AggregateOps()
      {
         double[] winterTemps = { 2.0, -21.3, 8, -4, 0, 8.2 };
         // Различные примеры агрегации.
         Console.WriteLine("Max temp: {0}",
                           (from t in winterTemps select t).Max());
         Console.WriteLine("Min temp: {0}",
                           (from t in winterTemps select t).Min());
         Console.WriteLine("Average temp: {0}",
                           (from t in winterTemps select t).Average());
         Console.WriteLine("Sum of all temps: {0}",
                           (from t in winterTemps select t).Sum());
      }
      #endregion
   }
   
   #region Класс ProductInfo
   class ProductInfo
   {
      public string Name {get; set;}
      public string Description {get; set;}
      public int NumberInStock {get; set;}
      public override string ToString()
      {
         return string.Format("Name={0}, Description={1}, NumberInStock={2}",
                              Name,
                              Description,
                              NumberInStock);
      }

   }
   #endregion   
}