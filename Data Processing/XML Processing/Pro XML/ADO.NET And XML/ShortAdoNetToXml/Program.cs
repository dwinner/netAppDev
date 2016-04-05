/**
 * Упрощенное преобразование данных из ADO.NET в XML
 */

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;

namespace ShortAdoNetToXml
{
   internal static class Program
   {
      private const string AdvWorksConnectionString =
         @"Data Source=DOTNET\DWINNER;Initial Catalog=AdventureWorksLT2012;User ID=sa;Password=bboytronik1985_DWINNER";

      private static void Main()
      {
         // Создание набора данных
         using (var dataSet = new DataSet("XmlProducts"))
         using (var connection = new SqlConnection(AdvWorksConnectionString))
         using (var sqlDataAdapter = new SqlDataAdapter("SELECT Name, StandardCost FROM SalesLT.Product", connection))
         {
            sqlDataAdapter.Fill(dataSet, "Products");
            dataSet.WriteXml("sample.xml", XmlWriteMode.WriteSchema);
#pragma warning disable 618
            var xmlDataDocument = new XmlDataDocument(dataSet);
#pragma warning restore 618

            // Извлечение всех элементов Products
            XmlNodeList nodeList = xmlDataDocument.GetElementsByTagName("Products");
            var builder = new StringBuilder();
            foreach (XmlNode node in nodeList)
            {
               builder.Append(node.InnerXml).Append(Environment.NewLine);
            }
         }
      }
   }
}