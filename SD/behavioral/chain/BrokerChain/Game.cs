namespace BrokerChain;

public class Game // mediator pattern
{
   public event EventHandler<Query>? Queries; // effectively a chain

   public void PerformQuery(object sender, Query query)
   {
      Queries?.Invoke(sender, query);
   }
}