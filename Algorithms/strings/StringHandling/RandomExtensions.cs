namespace StringHandling;

public static class RandomExtensions
{
   public static void Shuffle<T>(this Random rng, T[] array)
   {
      var len = array.Length;
      while (len > 1)
      {
         var k = rng.Next(len--);
         (array[len], array[k]) = (array[k], array[len]);
      }
   }
}