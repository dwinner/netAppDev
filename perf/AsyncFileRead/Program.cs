using System;
using System.IO;
using System.Threading.Tasks;

namespace AsyncFileRead
{
   internal static class Program
   {
      private static void Main(string[] args)
      {
         if (args.Length < 1)
         {
            Console.WriteLine("Usage: AsyncFileRead.exe inputFile");
            return;
         }

         var bytesRead = SynchronousRead(args[0]);
         Console.WriteLine("Sync Read {0} bytes", bytesRead);

         AsynchronousReadSimple(args[0]);

         var task = AsynchronousRead(args[0]);

         task.Wait();
         bytesRead = task.Result;

         Console.WriteLine("Async Read {0} bytes", bytesRead);
      }

      private static int SynchronousRead(string filename)
      {
         const int chunkSize = 0x1000;
         var buffer = new byte[chunkSize];

         using (var fileContents = new MemoryStream())
         using (var stream = new FileStream(filename, FileMode.Open))
         {
            int bytesRead;
            do
            {
               bytesRead = stream.Read(buffer, 0, buffer.Length);
               fileContents.Write(buffer, 0, bytesRead);
            } while (bytesRead > 0);

            return (int)fileContents.Length;
         }
      }

      private static void AsynchronousReadSimple(string filename)
      {
         const int chunkSize = 0x1000;
         var buffer = new byte[chunkSize];

         var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read, chunkSize, true);

         var task = fileStream.ReadAsync(buffer, 0, buffer.Length);
         task.ContinueWith(readTask =>
         {
            var amountRead = readTask.Result;
            fileStream.Dispose();
            Console.WriteLine("Async(Simple) read {0} bytes", amountRead);
         });
      }

      private static Task<int> AsynchronousRead(string filename)
      {
         const int chunkSize = 0x1000;
         var buffer = new byte[chunkSize];
         var tcs = new TaskCompletionSource<int>();

         var fileContents = new MemoryStream();
         var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read, chunkSize, true);
         fileContents.Capacity += chunkSize;

         var task = fileStream.ReadAsync(buffer, 0, buffer.Length);
         task.ContinueWith(readTask => ContinueRead(readTask, fileStream, fileContents, buffer, tcs));

         return tcs.Task;
      }

      private static void ContinueRead(Task<int> task, FileStream stream, MemoryStream fileContents, byte[] buffer,
         TaskCompletionSource<int> tcs)
      {
         if (task.IsCompleted)
         {
            var bytesRead = task.Result;
            fileContents.Write(buffer, 0, bytesRead);
            if (bytesRead > 0)
            {
               // More bytes to read, so make another async call
               var newTask = stream.ReadAsync(buffer, 0, buffer.Length);
               newTask.ContinueWith(readTask => ContinueRead(readTask, stream, fileContents, buffer, tcs));
            }
            else
            {
               // All done, dispose of resources and complete top-level task.
               tcs.TrySetResult((int)fileContents.Length);
               stream.Dispose();
               fileContents.Dispose();
            }
         }
      }
   }
}