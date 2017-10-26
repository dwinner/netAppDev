using System;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;

namespace MyConnectionFactory
{
   class Program
   {
      static void Main(string[] args)
      {
         // Чтение ключа поставщика.
         string dataProviderString = ConfigurationManager.AppSettings["provider"];

         // Преобразование строки в перечисление.
         DataProvider dataProvider = DataProvider.None;
         if (Enum.IsDefined(typeof(DataProvider), dataProviderString))
         {
            dataProvider = (DataProvider) Enum.Parse(typeof (DataProvider), dataProviderString);
         }
         else
         {
            Console.WriteLine("Data provider is not implemented");
         }

         // Получение конкретного подключения
         IDbConnection myConn = GetConnection(dataProvider);
         if (myConn != null)
         {
            Console.WriteLine("Your connection: {0}",
               myConn.GetType().Name);
         }

         Console.ReadLine();
      }

      enum DataProvider
      {
         SqlServer,
         OleDb,
         Odbc,
         Oracle,
         None
      }

      /// <summary>
      /// Этот метод возвращает конкретный объект подключения на основе значения
      /// перечисления DataProvider
      /// </summary>
      /// <param name="dataProvider">Поставщик подключения</param>
      /// <returns>Объект подключения</returns>
      static IDbConnection GetConnection(DataProvider dataProvider)
      {
         IDbConnection conn = null;
         switch (dataProvider)
         {
            case DataProvider.SqlServer:
               conn = new SqlConnection();
               break;

            case DataProvider.OleDb:
               conn = new OleDbConnection();
               break;

            case DataProvider.Odbc:
               conn = new OdbcConnection();
               break;

            default:
               throw new NotImplementedException("Not implemented yet for " + dataProvider);
         }
         return conn;
      }
   }
}
