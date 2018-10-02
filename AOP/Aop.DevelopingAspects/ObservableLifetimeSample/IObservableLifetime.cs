using System;

namespace ObservableLifetimeSample
{
   /// <summary>
   ///    Акспект вводит и реализует этот интерфейс
   /// </summary>
   public interface IObservableLifetime
   {
      event EventHandler Disposed;
      event EventHandler Finalized;
   }
}