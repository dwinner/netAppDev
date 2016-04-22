namespace Variance
{
   /// <summary>
   /// Ковариантный интерфейс
   /// </summary>
   /// <typeparam name="T">Ковариантный параметр типа</typeparam>
   public interface IIndex<out T>
   {
      T this[int index] { get; }

      int Count { get; }
   }
}