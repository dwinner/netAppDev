using Nito.Disposables;

namespace AsyncSemaphore;

internal static class Program
{
   private static readonly SemaphoreSlim Semaphore = new(4); // 4 downloads at a time

   private static void Main()
   {
      for (var i = 0; i < 50; i++)
      {
         DownloadWithSemaphoreAsync($"http://SomeUri/{i}");
      }

      Console.ReadLine();
   }

   private static async void DownloadWithSemaphoreAsync(string uri)
   {
      using (await Semaphore.EnterAsync().ConfigureAwait(false))
      {
         await Task.Delay(500).ConfigureAwait(false); // Simulate delay while downloading
      }

      Console.WriteLine($"Downloaded {uri}");
   }
}

internal static class Extensions
{
   public static async Task<IDisposable> EnterAsync(this SemaphoreSlim self)
   {
      await self.WaitAsync().ConfigureAwait(false);
      return Disposable.Create(() => self.Release());
   }
}