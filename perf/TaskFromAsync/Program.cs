using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TaskFromAsync
{
   internal static class Program
   {
      private const int TotalLength = 1024;
      private const int ReadSize = TotalLength / 4;

      private static void Main()
      {
         var tempFile = Path.GetTempFileName();

         var contents = new string('a', TotalLength);
         Console.WriteLine("Writing to file");
         File.WriteAllText(tempFile, contents);

         Console.WriteLine("Reading from file");
         var results = GetStringFromFileBetter(tempFile).Result;
         Console.WriteLine("Length: {0}", results.Length);
         Console.WriteLine(results);

         Console.ReadLine();
      }

      private static Task<string> GetStringFromFile(string path)
      {
         var buffer = new byte[TotalLength];

         var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None, buffer.Length,
            FileOptions.DeleteOnClose | FileOptions.Asynchronous);

         //It's <int> because stream.EndRead returns an int, indicating the number of bytes read.
         var task = Task<int>.Factory.FromAsync(stream.BeginRead, stream.EndRead, buffer, 0, buffer.Length, null);

         return task.ContinueWith(readTask =>
         {
            stream.Close();
            var bytesRead = readTask.Result;
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
         });
      }

      private static Task<string> GetStringFromFileBetter(string path)
      {
         var buffer = new byte[TotalLength];

         var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None, buffer.Length,
            FileOptions.DeleteOnClose | FileOptions.Asynchronous);

         //It's <int> because stream.EndRead returns an int, indicating the number of bytes read.
         var task = Task<int>.Factory.FromAsync(stream.BeginRead, stream.EndRead, buffer, 0, ReadSize, null);

         var tcs = new TaskCompletionSource<string>();

         task.ContinueWith(readTask => OnReadBuffer(readTask, stream, buffer, 0, tcs));

         return tcs.Task;
      }

      private static void OnReadBuffer(Task<int> readTask, Stream stream, byte[] buffer, int offset,
         TaskCompletionSource<string> tcs)
      {
         var bytesRead = readTask.Result;
         if (bytesRead > 0)
         {
            var task = Task<int>.Factory.FromAsync(stream.BeginRead, stream.EndRead, buffer, offset + bytesRead,
               Math.Min(buffer.Length - (offset + bytesRead), ReadSize), null);

            task.ContinueWith(callbackTask => OnReadBuffer(callbackTask, stream, buffer, offset + bytesRead, tcs));
         }
         else
         {
            tcs.TrySetResult(Encoding.UTF8.GetString(buffer, 0, offset));
         }
      }
   }
}