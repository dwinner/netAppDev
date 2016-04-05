namespace Observer
{
   /// <summary>
   /// Интерфейс наблюдателя
   /// </summary>
   /// <typeparam name="T">Тип состояния</typeparam>
   public interface IObserver<in T>
   {
      void Update(T subjectState);
   }
}