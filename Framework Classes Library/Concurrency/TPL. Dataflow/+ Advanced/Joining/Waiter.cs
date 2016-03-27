using System;
using System.Threading.Tasks.Dataflow;

namespace Joining
{
   public class Waiter
   {
      private readonly Resteraunt _resteraunt;

      public Waiter(Resteraunt resteraunt)
      {
         _resteraunt = resteraunt;
         var joinBlock =
            new JoinBlock<Tuple<Fork, Knife, Food>, Customer>(new GroupingDataflowBlockOptions { Greedy = false });

         resteraunt.ReadyToGo.LinkTo(joinBlock.Target1);
         resteraunt.Customers.LinkTo(joinBlock.Target2);

         var serveFoodBlock = new ActionBlock<Tuple<Tuple<Fork, Knife, Food>, Customer>>(
            new Action<Tuple<Tuple<Fork, Knife, Food>, Customer>>(ServeFood));

         joinBlock.LinkTo(serveFoodBlock);
      }

      private void ServeFood(Tuple<Tuple<Fork, Knife, Food>, Customer> foodServiceTuple)
      {
         var fork = foodServiceTuple.Item1.Item1;
         var knife = foodServiceTuple.Item1.Item2;
         var food = foodServiceTuple.Item1.Item3;
         var customer = foodServiceTuple.Item2;

         customer.EatAsync(fork, knife, food)
            .ContinueWith(eatingTask =>
            {
               _resteraunt.Forks.Post(fork);
               _resteraunt.Knife.Post(knife);
            });
      }
   }
}