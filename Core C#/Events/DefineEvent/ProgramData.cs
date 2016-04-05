using System;
using System.Threading;

namespace DefineEvent
{
   class ProgramData
   {
      private DateTime _startTime;

      /**
       * При определении собственных событий следуйте установленному образцу:
       * -public event
       * -protected virtual On... - это функция, возбуждающая событие
       */
      public event EventHandler<EventArgs> LoadStarted;

      protected virtual void OnLoadStarted()
      {
         if (LoadStarted != null)
         {
            // Данные отсутствуют, поэтому используется "пустое" событие EventArgs
            LoadStarted(this, EventArgs.Empty);
         }
      }

      public event EventHandler<ProgramDataEventArgs> LoadEnded;

      protected virtual void OnLoadEnded(TimeSpan loadTime)
      {
         if (LoadEnded != null)
         {
            LoadEnded(this, new ProgramDataEventArgs(loadTime));
         }
      }

      public void BeginLoad()
      {
         _startTime = DateTime.Now;
         var thread = new Thread(() =>
         {
            Thread.Sleep(5000);  // Имитация работы         
            OnLoadEnded(DateTime.Now - _startTime);
         });
         thread.Start();
         OnLoadStarted();  // Возбудить событие LoadEnded
      }
   }
}
