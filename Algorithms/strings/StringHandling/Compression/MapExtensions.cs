namespace StringHandling.Compression;

public static class MapExtensions
{
   public static Dictionary<TVal, TKey> Inverse<TKey, TVal>(this Dictionary<TKey, TVal> map)
      where TKey : notnull
      where TVal : notnull
   {
      var inverseMap = new Dictionary<TVal, TKey>();
      foreach (var (key, value) in map)
      {
         inverseMap.Add(value, key);
      }

      return inverseMap;
   }
}