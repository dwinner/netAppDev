namespace DataSlotSample;

internal static class Program
{
   private static void Main()
   {
      var test = new Test();
      new Thread(() =>
      {
         Thread.Sleep(1000);
         test.SecurityLevel++;
         Console.WriteLine(test.SecurityLevel);
      }).Start();
      new Thread(() =>
      {
         Thread.Sleep(2000);
         test.SecurityLevel++;
         Console.WriteLine(test.SecurityLevel);
      }).Start();
      new Thread(() =>
      {
         Thread.Sleep(3000);
         test.SecurityLevel++;
         Console.WriteLine(test.SecurityLevel);
      }).Start();
   }
}

internal class Test
{
   private const string SlotName = "securityLevel";

   // The same LocalDataStoreSlot object can be used across all threads.
   private readonly LocalDataStoreSlot _secSlot = Thread.GetNamedDataSlot(SlotName);

   // This property has a separate value on each thread.
   public int SecurityLevel
   {
      get
      {
         var data = Thread.GetData(_secSlot);
         return data == null ? 0 : (int)data; // null == uninitialized
      }
      set => Thread.SetData(_secSlot, value);
   }
}