namespace Graph.Adt;

public static class GraphExtensions
{
   public static void ValidateVertex<T>(this T[] vertexArray, int vertexIndex)
   {
      var vertexArrayLen = vertexArray.Length;
      if (vertexIndex < 0 || vertexIndex >= vertexArrayLen)
      {
         throw new ArgumentOutOfRangeException(nameof(vertexIndex),
            $"vertex {vertexIndex} is not between 0 and {vertexArrayLen - 1}");
      }
   }

   public static void ValidateVertex(this int verticeCount, int vertexIndex)
   {
      if (vertexIndex < 0 || vertexIndex >= verticeCount)
      {
         throw new ArgumentOutOfRangeException(nameof(vertexIndex),
            $"vertex {vertexIndex} is not between 0 and {verticeCount - 1}");
      }
   }

   public static bool IsEmpty<TKey, TPriority>(this PriorityQueue<TKey, TPriority> self) =>
      self.Count <= 0;
}