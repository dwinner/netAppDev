namespace ProtectionProxy;

internal static class Program
{
   private static void Main()
   {
      ICar car = new CarProxy(new Driver(12)); // 22
      car.Drive();
   }
}