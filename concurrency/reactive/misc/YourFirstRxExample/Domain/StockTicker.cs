namespace YourFirstRxExample.Domain;

public class StockTicker : IStockTicker
{
   public event EventHandler<StockTick> StockTick = delegate { };

   public void Notify(StockTick tick)
   {
      StockTick(this, tick);
   }
}