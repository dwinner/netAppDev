using System;
using System.Threading.Tasks;

namespace Joining
{
   public class Customer
   {
      // private static Random _rnd = new Random();
      private readonly int _id;
      // private JoinBlock<Fork, Knife, Food> _dinningBlock;
      // private ActionBlock<Tuple<Fork, Knife, Food>> _eatingBlock;

      public Customer(int id)
      {
         Console.WriteLine("New hungry customer {0}", id);
         _id = id;
         // _dinningBlock = dinningBlock;
         // _eatingBlock = eatingBlock;
      }

      public async Task EatAsync(Fork fork, Knife knife, Food food)
      {
         Console.WriteLine("Yummy {0}", _id);
         await Task.Delay(2000);
         Console.WriteLine("Burp {0}", _id);
      }
   }
}