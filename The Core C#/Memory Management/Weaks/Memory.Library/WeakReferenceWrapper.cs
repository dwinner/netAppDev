using System;

namespace Memory.Library
{
   /// <summary>
   ///    Параметризованная оболочка класса WeakReference
   /// </summary>
   /// <typeparam name="T">Параметр типа</typeparam>
   public struct WeakReferenceWrapper<T> : IDisposable
      where T : class
   {
      private readonly WeakReference<T> _weakReference;

      public WeakReferenceWrapper(T target)
      {
         _weakReference = new WeakReference<T>(target);
      }

      public T Target
      {
         get
         {
            T target;
            return _weakReference.TryGetTarget(out target) ? target : default(T);
         }
      }

      public void Dispose()
      {
         _weakReference.SetTarget(default(T));
      }
   }
}