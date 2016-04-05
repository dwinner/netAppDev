using System;
using System.Text;
using System.Xml.Serialization;
using SimpleSerializationSample;

namespace ComplexObjSerialization
{
   public class Inventory
   {
      // NOTE: Должна быть запись атрибута для каждого типа данных
      [XmlArrayItem("Product", typeof(Product)), XmlArrayItem("BookProduct", typeof(BookProduct))]
      public Product[] InventoryItems { get; set; }

      public override string ToString()
      {
         var outText = new StringBuilder();
         foreach (var item in InventoryItems)
         {
            outText.Append(item.ProductName).Append(Environment.NewLine);
         }

         return outText.ToString();
      }
   }
}