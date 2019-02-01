/**
 * Выполнение команд на объектах подключения
 */

using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml;
using Connection.Library;

namespace ExecutingCommands
{
   internal class Program
   {
      private const string NorthwindConnectionStringName = "Northwind";

      private static void Main()
      {
         ExecuteNonQueryExample();
         ExecuteReaderExample();
         ExecuteScalarExample();

         foreach (XmlFormatOption formatOption in Enum.GetValues(typeof (XmlFormatOption)))
         {
            ExecuteXmlReaderExample(formatOption);
         }
      }

      private static void ExecuteNonQueryExample() // Выполнение команд, не возвращающих вывод
      {
         DbConnection dbConnection = DatabaseProvider.GetDatabaseConnection(NorthwindConnectionStringName);
         const string updateSql = "UPDATE Customers SET ContactName = 'Bob' WHERE ContactName = 'Bill'";
         try
         {
            dbConnection.Open();
            var command = new SqlCommand(updateSql, dbConnection as SqlConnection);
            int rowsAffectedCount = command.ExecuteNonQuery();
            Console.WriteLine("{0} rows affected", rowsAffectedCount);
         }
         finally
         {
            if (dbConnection != null)
            {
               dbConnection.Close();
            }
         }
      }

      private static void ExecuteReaderExample() // Выполнение команд, возвращающих результат
      {
         DbConnection dbConnection = DatabaseProvider.GetDatabaseConnection(NorthwindConnectionStringName);
         const string selectSql = "SELECT ContactName, CompanyName FROM Customers";
         try
         {
            dbConnection.Open();
            var selectCommand = new SqlCommand(selectSql, dbConnection as SqlConnection);
            SqlDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
               Console.WriteLine("Contact: {0,-20} Company: {1}", reader[0], reader[1]);
            }
         }
         finally
         {
            if (dbConnection != null)
            {
               dbConnection.Close();
            }
         }
      }

      private static void ExecuteScalarExample() // Выполнение команд, возвращающих скалярное значение
      {
         DbConnection dbConnection = DatabaseProvider.GetDatabaseConnection(NorthwindConnectionStringName);
         const string countSql = "SELECT COUNT(*) FROM Customers";
         try
         {
            dbConnection.Open();
            var countCommand = new SqlCommand(countSql, dbConnection as SqlConnection);
            object scalar = countCommand.ExecuteScalar();
            Console.WriteLine(scalar);
         }
         finally
         {
            if (dbConnection != null)
            {
               dbConnection.Close();
            }
         }
      }

      private static void ExecuteXmlReaderExample(XmlFormatOption xmlFormatOption = XmlFormatOption.Auto)
         // Чтение набора в формате XML
      {
         DbConnection connection = DatabaseProvider.GetDatabaseConnection(NorthwindConnectionStringName);
         string @select = string.Format("SELECT ContactName, CompanyName FROM Customers {0}",
            xmlFormatOption.ToOperator());
         try
         {
            connection.Open();
            var sqlCommand = new SqlCommand(select, connection as SqlConnection);
            XmlReader xmlReader = sqlCommand.ExecuteXmlReader();
            xmlReader.Read();
            string data;
            do
            {
               data = xmlReader.ReadOuterXml();
               if (!string.IsNullOrEmpty(data))
               {
                  Console.WriteLine(data);
               }
            } while (!string.IsNullOrEmpty(data));
         }
         finally
         {
            if (connection != null)
            {
               connection.Close();
            }
         }
      }
   }

   internal enum XmlFormatOption
   {
      Auto,
      Raw,
      //Explicit
   }

   internal static class XmlFormatOptionExtensions
   {
      public static string ToOperator(this XmlFormatOption xmlFormatOption)
      {
         switch (xmlFormatOption)
         {
            case XmlFormatOption.Auto:
               return "FOR XML AUTO";
               //case XmlFormatOption.Explicit:
               //   return "FOR XML EXPLICIT";
            case XmlFormatOption.Raw:
               return "FOR XML RAW";
            default:
               throw new NotSupportedException(string.Format("Not supported option: {0}", xmlFormatOption));
         }
      }
   }
}