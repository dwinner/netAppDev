/**
 * Асинхронные стримы (C# 8.0+)
 * Фактически асинхронный стрим объединяет асинхронность и итераторы
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_streams
{
   internal static class Program
   {
      private static async Task Main()
      {
         // Простейший случай
         await foreach (var number in GetNumbersAsync())
         {
            Console.WriteLine(number);
         }

         // Псевдо-внешнее хранилище с асинхронным стримом
         var repository = new Repository();
         var data = repository.GetDataAsync();
         await foreach (var item in data)
         {
            Console.WriteLine(item);
         }
      }

      private static async IAsyncEnumerable<int> GetNumbersAsync()
      {
         for (var i = 0; i < 10; i++)
         {
            await Task.Delay(TimeSpan.FromSeconds(0.1))
               .ConfigureAwait(false);
            yield return i;
         }
      }
   }

   internal class Repository
   {
      private readonly string[] _data =
      {
         "Tom",
         "Sam",
         "Kate",
         "Alice",
         "Bob"
      };

      internal async IAsyncEnumerable<string> GetDataAsync()
      {
         for (var i = 0; i < _data.Length; i++)
         {
            Console.WriteLine($"Getting {i + 1} element...");
            await Task.Delay(TimeSpan.FromSeconds(0.5)).ConfigureAwait(false);
            yield return _data[i];
         }
      }
   }

   /*internal class AsyncEnumerableRepo<T> : IAsyncEnumerator<T>
   {
      private readonly T[] _streamData;
      private int _currentIndex;

      public AsyncEnumerableRepo(T[] streamData) => _streamData = streamData;

      public ValueTask DisposeAsync()
      {
         _currentIndex = 0; // rewind the current index
         return new ValueTask(Task.FromResult(true));
      }

      public ValueTask<bool> MoveNextAsync() =>
         _currentIndex < _streamData.Length
            ? new ValueTask<bool>(true)
            : new ValueTask<bool>(false);

      public T Current => _streamData[_currentIndex];
   }*/
}