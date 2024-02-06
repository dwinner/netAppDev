using System.IO.MemoryMappedFiles;

namespace MemoryMappedUnsafeSample;

internal static class Program
{
   private static void Main()
   {
      File.WriteAllBytes("unsafe.bin", new byte[100]);

      var data = new Data { X = 123, Y = 456 };

      using var mmf = MemoryMappedFile.CreateFromFile("unsafe.bin");
      using var accessor = mmf.CreateViewAccessor();

      accessor.Write(0, ref data);
      accessor.Read(0, out data);
      Console.WriteLine(data.X + " " + data.Y); // 123 456

      unsafe
      {
         byte* pointer = null;
         try
         {
            accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref pointer);
            var intPointer = (int*)pointer;
            Console.WriteLine(*intPointer); // 123
         }
         finally
         {
            if (pointer != null)
            {
               accessor.SafeMemoryMappedViewHandle.ReleasePointer();
            }
         }
      }
   }
}

internal struct Data
{
   public int X, Y;
}