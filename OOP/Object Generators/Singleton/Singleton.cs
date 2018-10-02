using System.Threading;

namespace Singleton
{
   /// <summary>
   ///    Параметризованный потоко-безопасный "Одиночка"
   /// </summary>
   /// <typeparam name="T">Параметр типа одиночки</typeparam>
   public static class Singleton<T>
      where T : class, new()
   {
      private static T _instance;

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