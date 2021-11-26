/**
 * Создание, чтение и запись двоичного файла
 */

using System;
using System.IO;

namespace FileCopy
{
   class Program
   {
      static void Main(string[] args)
      {
         if (args.Length < 2)
         {
            Console.WriteLine("Usage: FileCopy [SourceFile] [DestinationFile]");
            return;
         }
         DateTime start = DateTime.Now;
         string sourceFile = args[0];
         string destFile = args[1];
         const int bufferSize = 0x4000;
         byte[] buffer = new byte[bufferSize];

         UInt64 totalBytes = 0;

         using (FileStream inStream = File.Open(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
         using (FileStream outStream = File.Open(destFile, FileMode.Create, FileAccess.Write, FileShare.None))
         {
            int bytesCopied;
            do
            {
               bytesCopied = inStream.Read(buffer, 0, bufferSize);
               if (bytesCopied > 0)
               {
                  outStream.Write(buffer, 0, bytesCopied);
                  totalBytes += (UInt64)bytesCopied;
               }
            }
            while (bytesCopied > 0);
         }
         FileInfo info = new FileInfo(sourceFile);
         long size = info.Length;
         DateTime end = DateTime.Now;
         Console.WriteLine("{0:N0} bytes copied in {1}", totalBytes, end - start);
      }
   }
}
