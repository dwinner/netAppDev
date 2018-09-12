namespace Observer
{
   /// <summary>
   ///    Subject interface
   /// </summary>
   /// <typeparam name="T">State type</typeparam>
   public interface ISubject<T>
   {
      bool Add(IObserver<T> observer);

      void Add(params IObserver<T>[] observers);

      bool Remove(IObserver<T> observer);

      void Notify(T state);
   }
}