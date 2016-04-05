using System;

namespace Memory.Library
{
   /// <summary>
   /// Класс, поддерживающий необобщенный делегат EventHandler
   /// </summary>
   public sealed class WeakEventHandler : WeakDelegate<EventHandler>
   {
      public WeakEventHandler(EventHandler aDelegate)
         : base(aDelegate)
      {
      }

      protected override EventHandler GetDelegate()
      {
         return Callback;
      }

      // Сигнатура этого закрытого метода должна совпадать с сигнатурой нужного делегата
      private void Callback(object sender, EventArgs e)
      {
         // Если метод не отправлен в мусор, вызываем его
         GetRealDelegate()?.Invoke(sender, e);
      }
   }
}