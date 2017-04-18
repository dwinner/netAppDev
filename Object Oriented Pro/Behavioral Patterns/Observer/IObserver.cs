namespace Observer
{
   /// <summary>
   ///    Observer interface
   /// </summary>
   /// <typeparam name="T">State type</typeparam>
   public interface IObserver<in T>
   {
      void Update(T subjectState);
   }
}