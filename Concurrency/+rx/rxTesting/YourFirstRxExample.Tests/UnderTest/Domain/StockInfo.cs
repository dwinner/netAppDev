namespace YourFirstRxExample.Tests.UnderTest.Domain;

internal class StockInfo
{
   public StockInfo(string symbol, decimal price)
   {
      Symbol = symbol;
      PrevPrice = price;
   }

   public string Symbol { get; set; }

   public decimal PrevPrice { get; set; }
}