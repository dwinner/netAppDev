var cancellation = new CancellationTokenSource();

// These are the 50 URIs that we want to download.
var uris = Enumerable.Range(0, 50).Select(i => $"http://SomeUri/{i}");

await Parallel.ForEachAsync(uris,
   new ParallelOptions
   {
      MaxDegreeOfParallelism = 5, // Limit concurrency to a maximum of 5 downloads at a time.
      CancellationToken = cancellation.Token
   },
   async (uri, cancelToken) =>
   {
      // Simulate delay while downloading
      for (var i = 0; i <= 10; i++)
      {
         await Task.Delay(100, cancelToken).ConfigureAwait(false);
         OnProgress(i * 10);
      }

      Console.WriteLine($"Downloaded {uri}");
      return;

      static void OnProgress(int progress) => Console.Write($"{progress} ");
   }).ConfigureAwait(false);

Console.ReadLine();