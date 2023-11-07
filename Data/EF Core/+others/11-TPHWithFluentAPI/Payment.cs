namespace _11_TPHWithFluentAPI
{
   public abstract class Payment
   {
      public int PaymentId { get; set; }

      public string Name { get; set; }

      public decimal Amount { get; set; }
   }
}