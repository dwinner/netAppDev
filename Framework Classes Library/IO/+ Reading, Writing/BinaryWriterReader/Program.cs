using System;
using System.IO;

namespace BinaryWriterReader
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with Binary Writers / Readers *****\n");

         // Открыть двоичный поток для записи 
         FileInfo fileInfo = new FileInfo("BinFile.dat");
         using (BinaryWriter binaryWriter = new BinaryWriter(fileInfo.OpenWrite()))
         {
            // Напечатать тип потока: FileStream
            Console.WriteLine("Base stream is: {0}", binaryWriter.BaseStream);

            // Создать данные для сохранения
            double aDouble = 1234.67;
            int anInt = 34567;
            string aString = "A, B, C";

            // Записать данные в поток
            binaryWriter.Write(aDouble);
            binaryWriter.Write(anInt);
            binaryWriter.Write(aString);
         }

         Console.WriteLine("Done!");

         // Чтение двоичных данных из потока.
         using (BinaryReader binaryReader = new BinaryReader(fileInfo.OpenRead()))
         {
            Console.WriteLine(binaryReader.ReadDouble());
            Console.WriteLine(binaryReader.ReadInt32());
            Console.WriteLine(binaryReader.ReadString());
         }

         Console.ReadLine();
      }

   }
}
