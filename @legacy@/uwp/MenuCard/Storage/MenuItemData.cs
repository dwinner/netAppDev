using System.Runtime.Serialization;
using System.Xml.Serialization;
using Wrox.Metro.DataModel;

namespace Wrox.Metro.Storage
{
   [XmlType(TypeName = "MenuItem")]
   [DataContract(Name = "MenuItem")]
   public class MenuItemData
   {
      public MenuItemData()
      {

      }
      public MenuItemData(MenuItem item)
      {
         if (item != null)
         {
            this.Text = item.Text;
            this.Price = item.Price;
         }
      }

      public MenuItem ToMenuItem()
      {
         return new MenuItem
         {
            Text = this.Text,
            Price = this.Price
         };
      }
      [DataMember]
      public string Text { get; set; }
      [DataMember]
      public double Price { get; set; }
   }
}
