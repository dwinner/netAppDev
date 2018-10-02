namespace Iterator
{
   /// <summary>
   ///    Интерфейс аггрегатора
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public interface IAggregator<T>
   {
      int Count { get; }

      T this[int index] { get; set; }
      IIterator<T> Create();
   }
}