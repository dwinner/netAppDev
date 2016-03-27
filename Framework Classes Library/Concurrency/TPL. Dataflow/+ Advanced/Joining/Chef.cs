using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Joining
{
   public class Chef
   {
      private readonly Resteraunt _resteraunt;

      public Chef(Resteraunt resteraunt)
      {
         _resteraunt = resteraunt;
      }

      public async Task MakeFoodAsync()
      {
         Console.WriteLine("Chef making food");
         var knife = await _resteraunt.Knife.ReceiveAsync();
         for (var nFoodItem = 0; nFoodItem < 4; nFoodItem++)
         {
            _resteraunt.Food.Post(new Food());
         }
         Console.WriteLine("Chef made food..");
         _resteraunt.Knife.Post(knife);
      }
   }
}