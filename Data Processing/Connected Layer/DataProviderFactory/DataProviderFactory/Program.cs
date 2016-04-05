using System;
using System.Data.Common;
using System.Configuration;

namespace DataProviderFactory
{
   class Program
   {
      static void Main(string[] args)
      {
         // Получение строки подключения и поставщика из *.config
         string dp = ConfigurationManager.AppSettings["provider"];
         string cnStr = ConfigurationManager.ConnectionStrings["AutoLotSqlProvider"].ConnectionString;

         // Получение генератора поставщика.
         DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(dp);

         // Получение объекта подключения.
         using (DbConnection dbConnection = dbProviderFactory.CreateConnection())
         {
            Console.WriteLine("Your connection object: {0}", dbConnection.GetType().Name);
            dbConnection.ConnectionString = cnStr;
            dbConnection.Open();

            // Создание объекта команды.
            DbCommand command = dbProviderFactory.CreateCommand();
            Console.WriteLine("Your command object: {0}", command.GetType().Name);
            command.Connection = dbConnection;
            command.CommandText = "Select * From Inventory";

            // Вывод данных с помощью объекта чтения данных.
            using (DbDataReader dataReader = command.ExecuteReader())
            {
               Console.WriteLine("Your reader object: {0}", command.GetType().Name);
               while (dataReader.Read())
               {
                  Console.WriteLine("Auto #{0} - {1}", dataReader["CarId"], dataReader["Make"]);
               }
            }
         }

         Console.ReadKey();
      }
   }
}
