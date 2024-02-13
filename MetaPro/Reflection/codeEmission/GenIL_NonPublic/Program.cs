using System.Reflection;
using System.Reflection.Emit;

namespace GenIL_NonPublic;

internal static class Program
{
   private static void Main()
   {
      var dynMeth = new DynamicMethod("Foo", null, null, typeof(Test));
      var gen = dynMeth.GetILGenerator();
      var privateMethod = typeof(Test).GetMethod(
         "HelloWorld", BindingFlags.Static | BindingFlags.NonPublic);

      gen.Emit(OpCodes.Call, privateMethod); // Call HelloWorld
      gen.Emit(OpCodes.Ret);

      dynMeth.Invoke(null, null); // Hello world
   }
}

public class Test
{
   private static void HelloWorld() // private method, yet we can call it
   {
      Console.WriteLine("Hello world");
   }
}