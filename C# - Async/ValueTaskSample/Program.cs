using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValueTaskSample
{
   internal static class Program
   {
      private const string Value = "--------------------------------";
      private static readonly Dictionary<string, string> Names = new Dictionary<string, string>();
      private static DateTime _Retrieved;
      private static IEnumerable<string> _CachedData;

      private static async Task Main()
      {
         await UseValueTaskAsync().ConfigureAwait(false);
         var greetingValueTask2 = GreetingValueTask2("Katharina");
         Console.WriteLine(greetingValueTask2.Result);

         Console.WriteLine(Value);

         for (var i = 0; i < 20; i++)
         {
            /*var data = */await GetSomeDataAsync().ConfigureAwait(false);
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
         }

         Console.ReadLine();
      }

      private static async ValueTask<IEnumerable<string>> GetSomeDataAsync()
      {
         if (_Retrieved >= DateTime.Now.AddSeconds(-5))
         {
            Console.WriteLine("data from the cache");
            return await new ValueTask<IEnumerable<string>>(_CachedData).ConfigureAwait(false);
         }

         Console.WriteLine("data from the service");
         (_CachedData, _Retrieved) = await GetTheRealDataAsync().ConfigureAwait(false);

         return _CachedData;
      }

      private static async Task UseValueTaskAsync()
      {
         var result = await GreetingValueTaskAsync("Katharina").ConfigureAwait(false);
         Console.WriteLine(result);
         var result2 = await GreetingValueTaskAsync("Katharina").ConfigureAwait(false);
         Console.WriteLine(result2);
      }

      private static async ValueTask<string> GreetingValueTaskAsync(string name)
      {
         if (Names.TryGetValue(name, out var result))
         {
            return result;
         }

         result = await GreetingAsync(name).ConfigureAwait(false);
         Names.Add(name, result);
         return result;
      }

      private static ValueTask<string> GreetingValueTask2(string name)
      {
         if (Names.TryGetValue(name, out var result))
         {
            return new ValueTask<string>(result);
         }

         var greetingTask = GreetingAsync(name);
         var awaiter = greetingTask.GetAwaiter();
         awaiter.OnCompleted(OnCompletion);

         return new ValueTask<string>(greetingTask);

         void OnCompletion()
         {
            Names.Add(name, awaiter.GetResult());
         }
      }

      private static string Greeting(string name) => $"Hello, {name}";

      private static Task<string> GreetingAsync(string name) => Task.Run(() => Greeting(name));

      private static Task<(IEnumerable<string> data, DateTime retrievedTime)> GetTheRealDataAsync() =>
         Task.FromResult((Enumerable.Range(0, 10).Select(x => $"item {x}").AsEnumerable(), DateTime.Now));
   }
}