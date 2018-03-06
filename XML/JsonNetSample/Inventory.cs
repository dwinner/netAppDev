using System.Text;

namespace JsonNetSample
{
   class Inventory
   {
      public Product[] InventoryItems { get; set; }

      public override string ToString()
      {
         StringBuilder outText=new StringBuilder();
         foreach (var product in InventoryItems)
         {
            outText.AppendLine(product.ProductName);
         }

         return outText.ToString();
      }
   }
}