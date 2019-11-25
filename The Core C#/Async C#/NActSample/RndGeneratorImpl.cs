using System;
using System.Threading.Tasks;

namespace NActSample
{
   public class RndGeneratorImpl : IRndGenerator<int>
   {
      private const int RndSeed = 47;
      private static readonly Random _Rand = new Random(RndSeed);

      public async Task<int> GetNextNumberAsync() => await InternalRandomGenAsync();

      private static Task<int> InternalRandomGenAsync()
      {
         return Task.Factory.StartNew(() =>
         {
            Task.Delay(5000);
            return _Rand.Next();
         });
      }
   }
}