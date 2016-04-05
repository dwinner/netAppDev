using System;
using System.Threading;

namespace EventAreas
{
   public static class EventExtensions
   {
      /// <exception cref="Exception">A delegate callback throws an exception.</exception>
      public static void Raise<TEventArgs>(this TEventArgs e, object sender, ref EventHandler<TEventArgs> eventDelegate)
         where TEventArgs : EventArgs
      {
         // Копирование ссылки на поле делегата во временное поле
         // для безопасности в отношении потоков
         var temp = Interlocked.CompareExchange(ref eventDelegate, null, null);
         if (temp != null)
            temp(sender, e);
      }
   }
}