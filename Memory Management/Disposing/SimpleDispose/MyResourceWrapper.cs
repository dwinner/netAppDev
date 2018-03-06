using System;

namespace SimpleDispose
{
   // Реализация IDisposable.
   class MyResourceWrapper : IDisposable
   {
      /// <summary>
      /// После окончания работы с объектом пользователь должен вызвать
      /// этот метод.
      /// </summary>
      public void Dispose()
      {
         // TODO: Освобождение неуправляемых ресурсов...

         // TODO: Избавление от других пригодных для очистки объектов...

         Console.WriteLine("***** In Dispose! *****");
      }
   }
}
