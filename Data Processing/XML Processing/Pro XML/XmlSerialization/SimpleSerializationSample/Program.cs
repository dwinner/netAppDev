/**
 * Простой пример сериализации объектов в XML
 */

using System;
using System.IO;
using System.Xml.Serialization;

namespace SimpleSerializationSample
{
   internal static class Program
   {
      private static void Main()
      {
         // Создание нового объекта Product
         var newProduct = new Product
         {
            ProductId = 200,
            CatId = 100,
            Discont = false,
            ProductName = "Serialize Objects",
            QtyPerUnit = 6,
            ReorderLevel = 1,
            SuppId = 1,
            UnitPrice = 1000,
            UnitsInStock = 10,
            UnitsOnOrder = 0,
            Disc = 7
         };

         const string serialprodXml = "serialprod.xml";

         // Сериализация объекта
         using (TextWriter textWriter = new StreamWriter(serialprodXml))
         {
            var xmlSerializer = new XmlSerializer(typeof(Product));
            xmlSerializer.Serialize(textWriter, newProduct);
         }

         // Десериализация объекта
         using (var fileStream = new FileStream(serialprodXml, FileMode.Open))
         {
            var serializer = new XmlSerializer(typeof(Product));
            var product = serializer.Deserialize(fileStream) as Product;
            if (product != null)
            {
               Console.WriteLine(product);
            }
         }
      }
   }
}