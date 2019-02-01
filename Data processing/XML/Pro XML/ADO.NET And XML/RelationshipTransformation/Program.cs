/**
 * Преобразование реляционных данных
 */

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;

namespace RelationshipTransformation
{
   internal static class Program
   {
      private const string AdvWorksConnectionString =
         @"Data Source=DOTNET\DWINNER;Initial Catalog=AdventureWorksLT2012;User ID=sa;Password=bboytronik1985_DWINNER";

      private static void Main()
      {
         using (var dataSet = new DataSet("XmlProducts"))
         using (var connection = new SqlConnection(AdvWorksConnectionString))
         using (
            var productDataAdapter =
               new SqlDataAdapter("SELECT Name, StandardCost, ProductCategoryID FROM SalesLT.Product", connection))
         using (
            var categoryDataAdapter = new SqlDataAdapter("SELECT ProductCategoryID, Name FROM SalesLT.ProductCategory",
               connection))
         {
            productDataAdapter.Fill(dataSet, "Products");
            categoryDataAdapter.Fill(dataSet, "Categories");

            // Добавление отношения
            dataSet.Relations.Add(dataSet.Tables["Categories"].Columns["ProductCategoryID"],
               dataSet.Tables["Products"].Columns["ProductCategoryID"]);

            // Запись кода XML в файл для просмотра в будущем
            dataSet.WriteXml("Products.xml", XmlWriteMode.WriteSchema);

            // Создание экземпляра XmlDataDocument

            var xmlDataDocument = new XmlDataDocument(dataSet);

            XmlNodeList nodeList = xmlDataDocument.SelectNodes("//XmlProducts/Products");
            var stringBuilder = new StringBuilder();
            if (nodeList != null)
            {
               foreach (XmlNode node in nodeList)
               {
                  stringBuilder.Append(node).Append(Environment.NewLine);
               }
            }
         }
      }
   }
}