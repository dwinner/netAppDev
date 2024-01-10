using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace QueueSample
{
   public class ProcessDocuments
   {
      private readonly DocumentManager _documentManager;

      private ProcessDocuments(DocumentManager documentManager) =>
         _documentManager = documentManager ?? throw new ArgumentNullException(nameof(documentManager));

      public static Task StartAsync(DocumentManager documentManager) =>
         Task.Run(new ProcessDocuments(documentManager).RunAsync);

      private async Task RunAsync()
      {
         Random random = new();
         Stopwatch stopwatch = new();
         stopwatch.Start();
         var stop = false;
         do
         {
            if (stopwatch.Elapsed >= TimeSpan.FromSeconds(5))
            {
               stop = true;
            }

            if (_documentManager.IsDocumentAvailable)
            {
               stopwatch.Restart();
               Document doc = _documentManager.GetDocument();
               Console.WriteLine($"Processing document {doc.Title}");
            }

            await Task.Delay(random.Next(20)); // wait a random time before processing the next document
         } while (!stop);

         Console.WriteLine("stopped reading documents");
      }
   }
}