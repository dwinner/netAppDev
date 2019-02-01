using System;
using System.Threading.Tasks;

namespace NActTest
{
   public class RndGeneratorImpl : IRndGenerator<int>
   {
      private const int RND_SEED = 47;
      private static readonly Random rand = new Random(RND_SEED);

      public async Task<int> GetNextNumber()
      {
         // Безопасная генерация случайного числа - медленная операция
         return await InternalRandomGen();
      }

      private static Task<int> InternalRandomGen()
      {
         return Task.Factory.StartNew(() =>
         {
            Task.Delay(5000);
            return rand.Next();
         });
      }
   }
}