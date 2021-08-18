/**
 * Файлы, отображаемые в память
 */

using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace MappedMemoryFiles
{
   class Program
   {
      static void Main()
      {
         const string aFile = "TextFile1.txt";
         const string mapName = "fileHandle";
         const long capacity = 0x100000;
         using (var memoryMappedFile = MemoryMappedFile.CreateFromFile(aFile, FileMode.Create, mapName, capacity))
         {
            string valueToWrite = string.Format("Written to the mapped-memory file on {0}", DateTime.Now);
            var viewAccessor = memoryMappedFile.CreateViewAccessor();
            viewAccessor.WriteArray(0, Encoding.ASCII.GetBytes(valueToWrite), 0, valueToWrite.Length);
            var readOut = new byte[valueToWrite.Length];
            viewAccessor.ReadArray(0, readOut, 0, readOut.Length);
            var finalValue = Encoding.ASCII.GetString(readOut);
            Console.WriteLine("Message: {0}", finalValue);
         }
      }
   }
}
