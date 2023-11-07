namespace Graph.Tests;

internal static class RandomExtensions
{
   internal static void Shuffle<T>(this Random rng, T[] array)
   {
      var len = array.Length;
      while (len > 1)
      {
         var k = rng.Next(len--);
         (array[len], array[k]) = (array[k], array[len]);
      }
   }

   internal static bool Bernoulli(this Random rng, in double inProbabiity)
   {
      if (inProbabiity is < 0.0 or > 1.0)
      {
         throw new ArgumentException("Probability must be between 0 and 1", nameof(inProbabiity));
      }

      var probability = (double)rng.Next(100) / 100;
      return probability < inProbabiity;
   }
}