using System;

namespace DefineEvent
{
   // Ваш собственный класс EventArgs должен быть производным
   // от стандартного EventArgs
   class ProgramDataEventArgs : EventArgs
   {
      public TimeSpan LoadTime { get; private set; }

      public ProgramDataEventArgs(TimeSpan loadTime)
      {
         LoadTime = loadTime;
      }
   }
}
