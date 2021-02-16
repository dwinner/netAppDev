namespace Iterator
{
   /// <summary>
   /// Интерфейс итератора
   /// </summary>
   /// <typeparam name="T">Тип, лежащих в основе, объектов</typeparam>
   public interface IIterator<out T>
   {
      T First();

      T Next();

      bool IsDone();

      T CurrentItem();
   }
}