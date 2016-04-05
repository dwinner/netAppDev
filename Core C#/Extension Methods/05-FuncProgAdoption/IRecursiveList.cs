namespace _05_FuncProgAdoption
{
   /// <summary>
   /// Рекурсивный список
   /// </summary>
   /// <typeparam name="T">Ковариантный параметр типа</typeparam>
   /// <summary>Вид: (1 (2 (3 (4 (5 (6 (null null)))))))</summary>
   public interface IRecursiveList<out T>
   {
      T Head { get; }

      IRecursiveList<T> Tail { get; }
   }
}