using System.Xml.Serialization;

namespace SimpleSerializationSample
{
   /// <summary>
   ///    Класс, который будет сериализовываться
   ///    <remarks>Атрибуты определяют, как это происходит</remarks>
   /// </summary>
   [XmlRoot]
   public class Product
   {
      [XmlElement]
      public int ProductId { get; set; }

      [XmlElement]
      public string ProductName { get; set; }

      [XmlElement]
      public int CatId { get; set; }

      [XmlElement]
      public int SuppId { get; set; }

      [XmlElement]
      public int QtyPerUnit { get; set; }

      [XmlElement]
      public decimal UnitPrice { get; set; }

      [XmlElement]
      public short UnitsInStock { get; set; }

      [XmlElement]
      public short UnitsOnOrder { get; set; }

      [XmlElement]
      public short ReorderLevel { get; set; }

      [XmlElement]
      public bool Discont { get; set; }

      [XmlAttribute(AttributeName = "Discount")]
      public int Disc { get; set; }

      public override string ToString()
      {
         return
            string.Format(
               "ProductId: {0}, ProductName: {1}, CatId: {2}, SuppId: {3}, QtyPerUnit: {4}, UnitPrice: {5}, UnitsInStock: {6}, UnitsOnOrder: {7}, ReorderLevel: {8}, Discont: {9}, Disc: {10}",
               ProductId, ProductName, CatId, SuppId, QtyPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel,
               Discont, Disc);
      }
   }
}