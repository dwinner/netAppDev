/**
 * Простая абстракция подключения
 */

using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace _03_ConnectToEither
{
   class Program
   {      
      static void Main(string[] args)
      {
         DatabaseServerType dbType;
         if (args.Length < 1)
         {
            PrintUsage();
            return;
         }
         if (String.CompareOrdinal("SqlServer", args[0]) == 0)
         {
            dbType = DatabaseServerType.SqlServer;
         }
         else if (String.CompareOrdinal("MySql", args[0]) == 0)
         {
            dbType = DatabaseServerType.MySql;
         }
         else
         {
            PrintUsage();
            Console.ReadKey();
            return;
         }

         try
         {
            string connectionString = GetConnectionString(dbType);
            using (DbConnection conn = CreateConnection(dbType, connectionString))
            {
               conn.Open();
               using (DbCommand cmd = CreateCommand(dbType, CreateSelectString(/*dbType*/), conn))
               using (IDataReader reader = cmd.ExecuteReader())
               {
                  while (reader.Read())
                  {
                     Console.WriteLine("{0}\t{1}\t{2}",
                        reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                  }
               }
            }
         }
         catch (DbException ex)
         {
            Console.WriteLine(ex);
         }

         Console.ReadKey();
      }      

      #region Factory-методы

      private static DbConnection CreateConnection(DatabaseServerType dbType, string connectionString)
      {
         switch (dbType)
         {
            case DatabaseServerType.SqlServer:
               return new SqlConnection(connectionString);

            case DatabaseServerType.MySql:
               return new MySqlConnection(connectionString);

            default:
               throw new InvalidOperationException();
         }
      }

      private static string GetConnectionString(DatabaseServerType dbType)
      {
         switch (dbType)
         {
            case DatabaseServerType.SqlServer:
               return ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString;

            case DatabaseServerType.MySql:
               return ConfigurationManager.ConnectionStrings["MysqlBooks"].ConnectionString;

            default:
               throw new InvalidOperationException();
         }
      }

      private static DbCommand CreateCommand(DatabaseServerType dbType, string query, DbConnection conn)
      {
         switch (dbType)
         {
            case DatabaseServerType.SqlServer:
               // Необходимо преобразовывать типы соединений,
               // потому что команды работают только с одним соединением. Note: Abstract Factory в помощь
               return new SqlCommand(query, conn as SqlConnection);

            case DatabaseServerType.MySql:
               return new MySqlCommand(query, conn as MySqlConnection);

            default:
               throw new InvalidOperationException();
         }
      }

      private static string CreateSelectString(/*DatabaseServerType dbType*/)
      {
         /*
          * Необходимо помнить, что разные серверы
          * нередко предполагают немного разный синтаксис языка SQL,
          * но у такого простого запроса, как этот, синтаксис единый
          */
         return "SELECT * FROM Books";
      }

      #endregion


      private static void PrintUsage()
      {
         Console.WriteLine("Program.exe [SqlServer | MySql]");
      }
   }

   enum DatabaseServerType
   {
      SqlServer,
      MySql
   }
}
