using System;

namespace PooledObjects
{
   internal static class Program
   {
      private static void Main()
      {
         var poolManager = new PoolManager();

         Console.WriteLine("Items in pool: {0}", poolManager.TotalCount);

         using (var obj = poolManager.GetObject<MyObject>())
         {
            obj.Data = new byte[256];
            obj.Data[0] = 13;
            obj.UsableLength = 1;
         }

         Console.WriteLine("Items in pool: {0}", poolManager.TotalCount);

         using (var obj = poolManager.GetObject<MyObject>())
         {
            Console.WriteLine("obj.UsableLength == {0}", obj.UsableLength);
            Console.WriteLine("obj.Data.Length == {0}", obj.Data.Length);

            using (var obj2 = poolManager.GetObject<MyObject>())
            {
               obj.Data = new byte[512];
               obj.Data[0] = 14;
               obj.UsableLength = 1;
            }
         }

         Console.WriteLine("Items in pool: {0}", poolManager.TotalCount);
      }
   }
}