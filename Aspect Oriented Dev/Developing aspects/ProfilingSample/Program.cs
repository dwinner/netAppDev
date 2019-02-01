/**
 * Профилирование
 */

namespace ProfilingSample
{
   using System.IO;
   using System.Net;
   using System.Security.Cryptography;
   using System.Threading;
   using System.Threading.Tasks;

   internal static class Program
   {
      private static void Main()
      {
         GetRandomBytes();
         SleepSync();
         SleepAsync().GetAwaiter().GetResult();
         ReadAndHashSync();
         ReadAndHashAsync().GetAwaiter().GetResult();
      }

      [Profile]
      private static void SleepSync()
      {
         Thread.Sleep(200);
      }

      [Profile]
      private static async Task SleepAsync() => await Task.Delay(200).ConfigureAwait(false);

      [Profile]
      private static void GetRandomBytes()
      {
         for (var i = 0; i < 100; i++)
         {
            RandomNumberGenerator generator = RandomNumberGenerator.Create();
            var randomBytes = new byte[32 * 1024 * 1024];
            generator.GetBytes(randomBytes);
         }
      }

      [Profile]
      private static void ReadAndHashSync()
      {
         HashAlgorithm hashAlgorithm = HashAlgorithm.Create("SHA256");
         if (hashAlgorithm != null)
         {
            hashAlgorithm.Initialize();

            var webClient = new WebClient();
            var buffer = new byte[16 * 1024];
            using (
               Stream stream = webClient.OpenRead("http://www.nasa.gov/images/content/178493main_sig07-009-hires.jpg"))
            {
               if (stream != null)
               {
                  int countRead;
                  while ((countRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                  {
                     hashAlgorithm.ComputeHash(buffer, 0, countRead);
                  }
               }
            }
         }
      }

      [Profile]
      private static async Task ReadAndHashAsync()
      {
         HashAlgorithm hashAlgorithm = HashAlgorithm.Create("SHA256");
         if (hashAlgorithm != null)
         {
            hashAlgorithm.Initialize();

            var webClient = new WebClient();
            var buffer = new byte[16 * 1024];
            using (
               Stream stream = webClient.OpenRead("http://www.nasa.gov/images/content/178493main_sig07-009-hires.jpg"))
            {
               if (stream != null)
               {
                  int countRead;
                  while ((countRead = await stream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)) != 0)
                  {
                     hashAlgorithm.ComputeHash(buffer, 0, countRead);
                  }
               }
            }
         }
      }
   }
}