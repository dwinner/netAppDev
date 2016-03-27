/**
 * Сериализация сложных объектов
 */

using System.IO;
using System.Xml.Serialization;
using SimpleSerializationSample;

namespace ComplexObjSerialization
{
   internal static class Program
   {
      private static void Main()
      {
         // Создание объекта XmlAttributes
         var attributes = new XmlAttributes();

         // Добавление сериализуемых типов объектов
         attributes.XmlElements.Add(new XmlElementAttribute("Book", typeof(BookProduct)));
         attributes.XmlElements.Add(new XmlElementAttribute("Product", typeof(Product)));
         var attributeOverrides = new XmlAttributeOverrides();

         // Добавление атрибутов в коллекцию атрибутов
         attributeOverrides.Add(typeof(Inventory), "InventoryItems", attributes);

         // Создание объектов Product и Book
         var newProduct = new Product { ProductId = 100, ProductName = "Product Thing", SuppId = 10 };

         var newBookProduct = new BookProduct
         {
            ProductId = 101,
            ProductName = "How to use your new product thing",
            SuppId = 10,
            IsbnNumber = "123456789"
         };

         Product[] products = { newProduct, newBookProduct };
         var inventory = new Inventory { InventoryItems = products };

         using (TextWriter writer = new StreamWriter("inventory.xml"))
         {
            var serializer = new XmlSerializer(typeof(Inventory), attributeOverrides);
            serializer.Serialize(writer, inventory);
         }
      }
   }
}