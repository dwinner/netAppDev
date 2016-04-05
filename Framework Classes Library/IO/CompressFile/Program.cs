/**
 * Комбинирование потоков данных (сжатие файла)
 */

using System;
using System.IO;
using System.IO.Compression;

namespace CompressFile
{
   class Program
   {
      static bool _compress;
      static string _sourceFile;
      static string _destFile;
      const int BufferSize = 16384;

      static void Main(string[] args)
      {
         if (!ParseArgs(args))
         {
            Console.WriteLine("CompressFile [compress|decompress] sourceFile destFile");
            return;
         }

         byte[] buffer = new byte[BufferSize];
         /* 
          * Сначала создаются обычные потоки данных для исходного и результирующего файлов.
          * Затем создается поток GZIP, инкапсулирующий один из потоков, в зависимости от
          * того, выполняется упаковка или распаковка. Иными словами, если поток данных
          * gzip связывается с выходным файлом, и мы пишем в него, то данные пакуются.
          */
         using (Stream inFileStream = File.Open(_sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
         using (Stream outFileStream = File.Open(_destFile, FileMode.Create, FileAccess.Write, FileShare.None))
         using (GZipStream gzipStream = new GZipStream(
             _compress ? outFileStream : inFileStream,
             _compress ? CompressionMode.Compress : CompressionMode.Decompress))
         {
            Stream inStream = _compress ? inFileStream : gzipStream;
            Stream outStream = _compress ? gzipStream : outFileStream;

            int bytesRead;
            do
            {
               bytesRead = inStream.Read(buffer, 0, BufferSize);
               outStream.Write(buffer, 0, bytesRead);
            }
            while (bytesRead > 0);
         }
      }

      private static bool ParseArgs(string[] args)
      {
         if (args.Length < 3)
            return false;

         if (string.CompareOrdinal(args[0], "compress") == 0)
            _compress = true;
         else if (string.CompareOrdinal(args[0], "decompress") == 0)
            _compress = false;
         else
            return false;
         _sourceFile = args[1];
         _destFile = args[2];
         return true;
      }
   }
}
