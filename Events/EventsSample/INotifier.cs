using System;

namespace EventsSample
{
   public interface INotifier<in T>
      where T : EventArgs
   {
      void Notify(object sender, T eventArgs);
   }
}