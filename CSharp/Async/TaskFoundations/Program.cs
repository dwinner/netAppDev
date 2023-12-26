using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

internal record Command(string Option, string Text, Action Action);

internal static class Program
{
   private static readonly Command[] Commands =
   {
      new("-async", nameof(CallerWithAsync), CallerWithAsync),
      new("-awaiter", nameof(CallerWithAwaiter), CallerWithAwaiter),
      new("-cont", nameof(CallerWithContinuationTask), CallerWithContinuationTask),
      new("-masync", nameof(MultipleAsyncMethods), MultipleAsyncMethods),
      new("-comb", nameof(MultipleAsyncMethodsWithCombinators1), MultipleAsyncMethodsWithCombinators1),
      new("-comb2", nameof(MultipleAsyncMethodsWithCombinators2), MultipleAsyncMethodsWithCombinators2),
      new("-val", nameof(UseValueTask), UseValueTask),
      new("-casync", nameof(ConvertingAsyncPattern), ConvertingAsyncPattern)
   };

   private static readonly Dictionary<string, string> Names = new();

   private static void Main(string[] args)
   {
      if (args.Length != 1)
      {
         ShowUsage();
         return;
      }

      var command = Commands.SingleOrDefault(c => c.Option == args[0]);
      if (command == null)
      {
         ShowUsage();
         return;
      }

      command.Action();

      Console.ReadLine();
   }

   private static void ShowUsage()
   {
      Console.WriteLine("Usage: Foundations [options]");
      Console.WriteLine();
      foreach (var command in Commands)
      {
         Console.WriteLine($"{command.Option} {command.Text}");
      }
   }

   private static async void UseValueTask()
   {
      TraceThreadAndTask($"start {nameof(UseValueTask)}");
      string result = await GreetingValueTask2Async("Katharina");
      Console.WriteLine(result);
      TraceThreadAndTask($"first result {nameof(UseValueTask)}");
      string result2 = await GreetingValueTask2Async("Katharina");
      Console.WriteLine(result2);
      TraceThreadAndTask($"ended {nameof(UseValueTask)}");
   }

   private static async void ConvertingAsyncPattern()
   {
      var request = WebRequest.Create("http://www.microsoft.com") as HttpWebRequest;
      if (request == null)
      {
         return;
      }

      using WebResponse response =
         await Task.Factory.FromAsync(request.BeginGetResponse(null, null), request.EndGetResponse);

      Stream stream = response.GetResponseStream();
      using StreamReader reader = new(stream);
      string content = reader.ReadToEnd();
      Console.WriteLine(content.Substring(0, 100));
   }

   private static async void MultipleAsyncMethods()
   {
      string s1 = await GreetingAsync("Stephanie");
      string s2 = await GreetingAsync("Matthias");
      Console.WriteLine(
         $"Finished both methods.{Environment.NewLine} Result 1: {s1}{Environment.NewLine} Result 2: {s2}");
   }

   private static async void MultipleAsyncMethodsWithCombinators1()
   {
      Task<string> t1 = GreetingAsync("Stephanie");
      Task<string> t2 = GreetingAsync("Matthias");
      await Task.WhenAll(t1, t2);
      Console.WriteLine(
         $"Finished both methods.{Environment.NewLine} Result 1: {t1.Result}{Environment.NewLine} Result 2: {t2.Result}");
   }

   private static async void MultipleAsyncMethodsWithCombinators2()
   {
      Task<string> t1 = GreetingAsync("Stephanie");
      Task<string> t2 = GreetingAsync("Matthias");
      string[] result = await Task.WhenAll(t1, t2);
      Console.WriteLine(
         $"Finished both methods.{Environment.NewLine} Result 1: {result[0]}{Environment.NewLine} Result 2: {result[1]}");
   }

   private static void CallerWithContinuationTask()
   {
      TraceThreadAndTask($"started {nameof(CallerWithContinuationTask)}");

      var t1 = GreetingAsync("Stephanie");

      t1.ContinueWith(t =>
      {
         string result = t.Result;
         Console.WriteLine(result);

         TraceThreadAndTask($"ended {nameof(CallerWithContinuationTask)}");
      });
   }

   private static void CallerWithAwaiter()
   {
      TraceThreadAndTask($"starting {nameof(CallerWithAwaiter)}");
      var awaiter = GreetingAsync("Matthias").GetAwaiter();
      awaiter.OnCompleted(OnCompleteAwaiter);

      void OnCompleteAwaiter()
      {
         Console.WriteLine(awaiter.GetResult());
         TraceThreadAndTask($"ended {nameof(CallerWithAwaiter)}");
      }
   }

   private static async void CallerWithAsync()
   {
      TraceThreadAndTask($"started {nameof(CallerWithAsync)}");
      string result = await GreetingAsync("Stephanie");
      Console.WriteLine(result);
      TraceThreadAndTask($"ended {nameof(CallerWithAsync)}");
   }

   private static Task<string> GreetingAsync(string name) =>
      Task.Run(() =>
      {
         TraceThreadAndTask($"running {nameof(GreetingAsync)}");
         return Greeting(name);
      });

   private static async ValueTask<string> GreetingValueTaskAsync(string name)
   {
      if (Names.TryGetValue(name, out var result))
      {
         return result;
      }

      result = await GreetingAsync(name);
      Names.Add(name, result);
      return result;
   }

   private static ValueTask<string> GreetingValueTask2Async(string name)
   {
      if (Names.TryGetValue(name, out var result))
      {
         return new ValueTask<string>(result);
      }

      Task<string> t1 = GreetingAsync(name);

      var awaiter = t1.GetAwaiter();
      awaiter.OnCompleted(OnCompletion);
      return new ValueTask<string>(t1);

      void OnCompletion()
      {
         Names.Add(name, awaiter.GetResult());
      }
   }

   private static string Greeting(string name)
   {
      TraceThreadAndTask($"running {nameof(Greeting)}");
      Task.Delay(3000).Wait();
      return $"Hello, {name}";
   }

   public static void TraceThreadAndTask(string info)
   {
      string taskInfo = Task.CurrentId == null ? "no task" : "task " + Task.CurrentId;

      Console.WriteLine($"{info} in thread {Thread.CurrentThread.ManagedThreadId} and {taskInfo}");
   }
}