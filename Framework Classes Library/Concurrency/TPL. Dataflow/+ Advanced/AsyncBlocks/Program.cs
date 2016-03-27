// Неблокирующий подход

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AsyncBlocks
{
   internal static class Program
   {
      private static void Main()
      {
         SimpleAsync();

         SynchronousHappySites();

         string[] urls =
         {
            "http://www.bbc.co.uk",
            "http://www.cia.gov",
            "http://www.theregister.co.uk"
         };

         var happySites = new List<string>();
         var isHappySiteBlock =
            new TransformBlock<string, Tuple<string, bool>>((Func<string, Task<Tuple<string, bool>>>) IsHappyAsync,
               new ExecutionDataflowBlockOptions {MaxDegreeOfParallelism = 1});

         var addToHappySitesBlock = new ActionBlock<Tuple<string, bool>>(
            tuple => happySites.Add(tuple.Item1));

         isHappySiteBlock.LinkTo(addToHappySitesBlock,
            new DataflowLinkOptions {PropagateCompletion = true},
            tuple => tuple.Item2);

         isHappySiteBlock.LinkTo(DataflowBlock.NullTarget<Tuple<string, bool>>());

         foreach (var url in urls)
         {
            isHappySiteBlock.Post(url);
         }

         isHappySiteBlock.Complete();

         addToHappySitesBlock.Completion.Wait();
         happySites.ForEach(Console.WriteLine);
      }

      private static async Task<Tuple<string, bool>> IsHappyAsync(string url)
      {
         //var client = new WebClient();

         //return client.DownloadStringTaskAsync(url)
         //    .ContinueWith(dt =>
         //    {
         //        string content = dt.Result;
         //        return Tuple.Create(url, content.ToLower().Contains("happy"));
         //    });

         //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
         using (var client = new WebClient())
         {
            var content = await client.DownloadStringTaskAsync(url);
            return Tuple.Create(url, content.ToLower().Contains("happy"));
         }
      }

      private static void SynchronousHappySites()
      {
         string[] urls =
         {
            "http://www.bbc.co.uk",
            "http://www.cia.gov",
            "http://www.theregister.co.uk"
         };

         var happySites = new List<string>();
         var isHappySiteBlock =
            new TransformBlock<string, Tuple<string, bool>>((Func<string, Tuple<string, bool>>) IsHappy,
               new ExecutionDataflowBlockOptions {MaxDegreeOfParallelism = 1});

         var addToHappySitesBlock = new ActionBlock<Tuple<string, bool>>(
            tuple => happySites.Add(tuple.Item1));

         isHappySiteBlock.LinkTo(addToHappySitesBlock,
            new DataflowLinkOptions {PropagateCompletion = true},
            tuple => tuple.Item2);

         isHappySiteBlock.LinkTo(DataflowBlock.NullTarget<Tuple<string, bool>>());

         foreach (var url in urls)
         {
            isHappySiteBlock.Post(url);
         }

         isHappySiteBlock.Complete();

         addToHappySitesBlock.Completion.Wait();
         happySites.ForEach(Console.WriteLine);
      }

      private static Tuple<string, bool> IsHappy(string url)
      {
         Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
         using (var client = new WebClient())
         {
            var content = client.DownloadString(url);
            return Tuple.Create(url, content.ToLower().Contains("happy"));
         }
      }

      private static void SimpleAsync()
      {
         var consumeBlock =
            //new ActionBlock<int>((Action<int>)Body);
            new ActionBlock<int>((Func<int, Task>) AsyncBody);


         while (true)
         {
            consumeBlock.Post(5);
            Console.ReadLine();
         }
      }

      private static string Download(string url)
      {
         var client = new WebClient();

         return client.DownloadString(url);
      }

      private static Task<string> DownloadAsync(string url)
      {
         var client = new WebClient();

         return client.DownloadStringTaskAsync(url);
      }

      private static void Body(int arg)
      {
         Console.WriteLine("Running...");
         for (var i = 0; i < arg; i++)
         {
            Console.WriteLine("{0}:{1}", Thread.CurrentThread.ManagedThreadId, i);
            Thread.Sleep(1000);
         }
      }

      private static async Task AsyncBody(int arg)
      {
         Console.WriteLine("Running...");
         for (var i = 0; i < arg; i++)
         {
            Console.WriteLine("{0}:{1}", Thread.CurrentThread.ManagedThreadId, i);
            await Task.Delay(1000);
         }
      }
   }
}