using System;

namespace YourFirstRxExample.Tests.UnderTest.Domain;

public interface IStockTicker
{
   event EventHandler<StockTick> StockTick;
}