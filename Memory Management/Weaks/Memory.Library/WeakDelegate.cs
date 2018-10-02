using System;

namespace Memory.Library
{
   /// <summary>
   ///    Базовый класс для реализации короткоживущего делегата
   /// </summary>
   /// <typeparam name="TDelegate">
   ///    Параметр типа
   ///    <remarks>Должен быть производным от типа <see cref="MulticastDelegate" /></remarks>
   /// </typeparam>
   public abstract class WeakDelegate<TDelegate>
      where TDelegate : class /* MulticastDelegate */
   {
      private WeakReferenceWrapper<TDelegate> _weakDelegate;

      protected WeakDelegate(TDelegate aDelegate)
      {
         var multicastDelegate = (MulticastDelegate) (object) aDelegate;
         if (multicastDelegate?.Target == null) // Contract.Requires<ArgumentNullException>(multicastDelegate != null);
         {
            throw new ArgumentNullException(nameof(multicastDelegate));
         }

         // Сохранение WeakReference в делегат
         _weakDelegate = new WeakReferenceWrapper<TDelegate>(aDelegate);
      }

      public Action<TDelegate> RemoveDelegateCode { private get; set; }

      protected TDelegate GetRealDelegate()
      {
         // Если реальный делегат еще не отправлен в мусор, возвращаем управление
         var realDelegate = _weakDelegate.Target;
         if (realDelegate != null)
         {
            return realDelegate;
         }

         // Реальный делегат отправлен в мусор, и нам больше не требуется
         // ссылка WeakReference (ее тоже можно отправить в мусор)
         _weakDelegate.Dispose();

         // Удаление делегата из цепочки (если пользователь указывает способ)
         if (RemoveDelegateCode != null)
         {
            RemoveDelegateCode(GetDelegate());
            RemoveDelegateCode = null; // Удаляем обработчик из очереди на финализацию
         }

         return null; // Реальный делегат отправлен в мусор и не может вызываться
      }

      /// <summary>
      ///    Делегат
      /// </summary>
      /// <remarks>
      ///    Все производные классы должны возвращать делегату
      ///    закрытый метод типа, совпадающего с TDelegate
      /// </remarks>
      /// <returns>Делегат</returns>
      protected abstract TDelegate GetDelegate();

      public static implicit operator TDelegate(WeakDelegate<TDelegate> aWeakDelegate)
      {
         return aWeakDelegate.GetDelegate();
      }
   }
}