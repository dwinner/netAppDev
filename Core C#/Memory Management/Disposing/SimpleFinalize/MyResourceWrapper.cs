using System;

namespace SimpleFinalize
{
   // Переопределение System.Object.Finalize() с использованием синтаксиса финализатора.
   class MyResourceWrapper
   {
      ~MyResourceWrapper()
      {
         // TODO: Здесь производится очистка неуправляемых ресурсов.         
         Console.Beep();
      }
   }
}
