using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace UsersDbEntities
{
   public class MyLoggerProvider : ILoggerProvider
   {
      public void Dispose()
      {
      }

      public ILogger CreateLogger(string categoryName) => new MyLogger();

      private sealed class MyLogger : ILogger
      {
         public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
         {
            File.AppendAllText("log.txt", formatter(state, exception));
            Console.WriteLine(formatter(state, exception));
         }

         public bool IsEnabled(LogLevel logLevel) => true;

         public IDisposable BeginScope<TState>(TState state) => null;
      }
   }
}