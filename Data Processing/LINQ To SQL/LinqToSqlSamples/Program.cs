/**
 * Запросы к строго-типизированным классам LINQ To SQL
 */

using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Transactions;
using Northwind.DataContext;

namespace LinqToSqlSamples
{
   internal static class Program
   {
      private static void Main()
      {
         SelectProducts();
         RawSelectProducts();
         UsingTransactions();
         Filtering();
         Joining();
         Grouping();
         UsingStoredProc();

         Console.ReadLine();
      }

      private static void Filtering()
      {
         IEnumerable<Product> filterProduct = FilterProduct("L");
         foreach (var product in filterProduct)
         {
            Console.WriteLine(product.ProductName);
         }
      }

      private static void SelectProducts() // Выборка
      {
         using (var dataContext = new NorthwindDataContext())
         {
            Table<Product> products = dataContext.Products;
            foreach (Product product in products)
            {
               Console.WriteLine("{0} | {1} | {2}", product.ProductID, product.ProductName, product.UnitsInStock);
            }
         }
      }

      private static void RawSelectProducts() // Выполнение "сырых" запросов на контексте
      {
         using (var dataContext = new NorthwindDataContext())
         {
            IEnumerable<Product> products = dataContext.ExecuteQuery<Product>("SELECT * FROM Products");
            foreach (Product product in products)
            {
               Console.WriteLine("{0} | {1}", product.ProductID, product.ProductName);
            }
         }
      }

      private static void UsingTransactions() // Использование транзакций
      {
         using (var dataContext = new NorthwindDataContext())
         using (var txScope = new TransactionScope())
         {
            var firstProduct = new Product
            {
               ProductName = "Bill's product"
            };
            dataContext.Products.InsertOnSubmit(firstProduct);

            var secondProduct = new Product
            {
               ProductName = "Another product"
            };
            dataContext.Products.InsertOnSubmit(secondProduct);

            try
            {
               dataContext.SubmitChanges();

               Console.WriteLine(firstProduct.ProductID);
               Console.WriteLine(secondProduct.ProductID);
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
            }

            txScope.Complete();
         }
      }

      private static IEnumerable<Product> FilterProduct(string productNamePrefix) // Фильтрация
      {
         using (var dataContext = new NorthwindDataContext())
         {
            return (from product in dataContext.Products
                    where product.ProductName.StartsWith(productNamePrefix)
                    select product).ToArray();
         }
      }

      private static void Joining() // Соединение таблиц
      {
         using (var dataContext = new NorthwindDataContext { Log = Console.Out })
         {
            var query = from customer in dataContext.Customers
                        join order in dataContext.Orders on customer.CustomerID equals order.CustomerID
                        orderby customer.CustomerID
                        select new
                        {
                           customer.CustomerID,
                           customer.CompanyName,
                           customer.Country,
                           order.OrderID,
                           order.OrderDate
                        };

            foreach (var item in query)
            {
               Console.WriteLine("{0} | {1} | {2} | {3} | {4}", item.CustomerID, item.CompanyName, item.Country,
                  item.OrderID, item.OrderDate);
            }
         }
      }

      private static void Grouping() // Группирование результатов
      {
         using (var dataContext = new NorthwindDataContext())
         {
            var q = from product in dataContext.Products
                    orderby product.Category.CategoryName ascending
                    group product by product.Category.CategoryName
                       into g
                       select new
                       {
                          Category = g.Key,
                          Products = g
                       };

            foreach (var item in q)
            {
               Console.WriteLine(item.Category);

               foreach (Product product in item.Products)
               {
                  Console.WriteLine("\t" + product.ProductName);
               }

               Console.WriteLine();
            }
         }
      }

      private static void UsingStoredProc() // Использование хранимых процедур
      {
         using (var dataContext = new NorthwindDataContext())
         {
            ISingleResult<Ten_Most_Expensive_ProductsResult> result = dataContext.Ten_Most_Expensive_Products();
            foreach (Ten_Most_Expensive_ProductsResult item in result)
            {
               Console.WriteLine(item.TenMostExpensiveProducts + " | " + item.UnitPrice);
            }
         }
      }
   }
}