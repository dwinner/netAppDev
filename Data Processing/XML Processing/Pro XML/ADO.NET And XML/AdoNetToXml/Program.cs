/**
 * Преобразование данных ADO.NET в XML
 */

using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;

namespace AdoNetToXml
{
   internal static class Program
   {
      private const string AdvWorksConnectionString =
         @"Data Source=DOTNET\DWINNER;Initial Catalog=AdventureWorksLT2012;User ID=sa;Password=bboytronik1985_DWINNER";

      private static void Main()
      {
         var document = new XmlDocument();
         var dataSet = new DataSet("XmlProducts");
         using (var sqlConnection = new SqlConnection(AdvWorksConnectionString))
         {
            var dataAdapter = new SqlDataAdapter("SELECT Name, StandardCost FROM SalesLT.Product", sqlConnection);

            // Заполнение набора данных
            dataAdapter.Fill(dataSet, "Products");

            using (var memoryStream = new MemoryStream())
            using (var streamReader = new StreamReader(memoryStream))
            using (var streamWriter = new StreamWriter(memoryStream))
            {
               // Запись XML из набора данных в поток памяти
               dataSet.WriteXml(streamWriter, XmlWriteMode.IgnoreSchema);
               memoryStream.Seek(0, SeekOrigin.Begin);

               // Чтение XML из потока памяти в XmlDocument
               document.Load(streamReader);

               // Извлечение всех элементов Products
               XmlNodeList nodeList = document.SelectNodes("//XmlProducts/Products");
               var builder = new StringBuilder();
               if (nodeList != null)
               {
                  foreach (XmlNode node in nodeList)
                  {
                     builder.Append(node.InnerXml + Environment.NewLine);
                  }

                  Console.WriteLine(builder.ToString());
               }
            }
         }
      }
   }
}