using System;
using System.Threading.Tasks.Dataflow;

namespace AtomicCafe
{
   public class Waiter
   {
      private readonly Restaurant _restaurant;
      private JoinBlock<Tuple<Fork, Knife, Food>, Customer> _joinBlock;
      private ActionBlock<Tuple<Tuple<Fork, Knife, Food>, Customer>> _serveFoodBlock;
      
      public Waiter(Restaurant restaurant)
      {
         _restaurant = restaurant;
         _joinBlock=new JoinBlock<Tuple<Fork, Knife, Food>, Customer>(new GroupingDataflowBlockOptions(){Greedy=false});

         _restaurant.ReadyToGo.LinkTo(_joinBlock.Target1);
         _restaurant.Customers.LinkTo(_joinBlock.Target2);

         _serveFoodBlock=new ActionBlock<Tuple<Tuple<Fork, Knife, Food>, Customer>>(new Action<Tuple<Tuple<Fork, Knife, Food>, Customer>>(ServeFood));

         _joinBlock.LinkTo(_serveFoodBlock);
      }

      private void ServeFood(Tuple<Tuple<Fork, Knife, Food>, Customer> foodServiceTuple)
      {
         var fork = foodServiceTuple.Item1.Item1;
         var knife = foodServiceTuple.Item1.Item2;
         var food = foodServiceTuple.Item1.Item3;
         var customer = foodServiceTuple.Item2;

         customer.EatAsync(fork, knife, food).ContinueWith(eatingTask =>
         {
            _restaurant.Forks.Post(fork);
            _restaurant.Knifes.Post(knife);
         });   // TODO: Replace with async/await pattern
      }      
   }
}