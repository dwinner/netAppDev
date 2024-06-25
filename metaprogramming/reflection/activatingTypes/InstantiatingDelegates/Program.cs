namespace InstantiatingDelegates;

internal class Program
{
   private static int Square(int x) => x * x; // Static method
   private int Cube(int x) => x * x * x; // Instance method

   private static void Main(string[] args)
   {
      var staticD = Delegate.CreateDelegate(typeof(IntFunc), typeof(Program), "Square");

      var program = new Program();
      var instanceD = Delegate.CreateDelegate(typeof(IntFunc), program, "Cube");

      Console.WriteLine(staticD.DynamicInvoke(3)); // 9
      Console.WriteLine(instanceD.DynamicInvoke(3)); // 27

      var f = (IntFunc)staticD;
      Console.WriteLine(f(3)); // 9 (but much faster!)
   }

   private delegate int IntFunc(int x);
}