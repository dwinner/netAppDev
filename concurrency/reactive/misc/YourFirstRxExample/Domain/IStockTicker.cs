namespace YourFirstRxExample.Domain;

public interface IStockTicker
{
   event EventHandler<StockTick> StockTick;
}