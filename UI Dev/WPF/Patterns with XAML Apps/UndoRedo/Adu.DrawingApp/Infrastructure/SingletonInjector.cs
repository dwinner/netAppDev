using System.Threading;

namespace Adu.DrawingApp.Infrastructure
{
   /// <summary>
   ///    Thread safe generified singleton scoped wrapper
   /// </summary>
   /// <typeparam name="T">Singleton type parameter</typeparam>
   public static class SingletonInjector<T>
      where T : class, new()
   {
      private static T _instance;

      /// <summary>
      ///    Single instance
      /// </summary>
      public static T Instance
      {
         get
         {
            if (_instance != null)
            {
               return _instance;
            }

            var tempInstance = new T();
            Interlocked.CompareExchange(ref _instance, tempInstance, null);
            return _instance;
         }
      }
   }
}