using System;
using System.Threading;

namespace Singleton
{
   /// <summary>
   /// Потокобезопасный "Одиночка"
   /// </summary>
   public sealed class TsSingleton
   {
      #region Реализация одиночки

      private static TsSingleton _instance;

      private TsSingleton() { }

      public static TsSingleton Instance
      {
         get
         {
            if (_instance != null)
            {
               return _instance;
            }
            var tempSingleton = new TsSingleton();
            Interlocked.CompareExchange(ref _instance, tempSingleton, null);
            return _instance;
         }
      }

      #endregion


      #region Методы одиночного экземпляра

      public void PrintMe()
      {
         Console.WriteLine("Print me");
      }

      #endregion
   }
}
