namespace Iterator
{
   /// <summary>
   /// Интерфейс аггрегатора
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public interface IAggregator<T>
   {
      IIterator<T> Create();

      int Count { get; set; }

      T this[int index] { get; set; }
   }
}