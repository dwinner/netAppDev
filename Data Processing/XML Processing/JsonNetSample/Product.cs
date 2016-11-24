namespace JsonNetSample
{
   public class Product
   {
      public int Discount { get; set; }
      public int ProductId { get; set; }
      public string ProductName { get; set; }
      public int SupplierId { get; set; }
      public int CategoryId { get; set; }
      public int QuantityPerUnit { get; set; }
      public decimal UnitPrice { get; set; }
      public short UnintsInStock { get; set; }
      public short UnitsOnOrder { get; set; }
      public short ReorderLevel { get; set; }
      public bool Discontinued { get; set; }
      public override string ToString() => $"{ProductId} {ProductName} {UnitPrice:C}";
   }
}