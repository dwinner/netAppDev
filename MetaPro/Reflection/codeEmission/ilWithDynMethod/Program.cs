using System.Reflection.Emit;

namespace ilWithDynMethod;

internal static class Program
{
   private static void Main()
   {
      var dynMeth = new DynamicMethod("Foo", null, null, typeof(Test));
      var gen = dynMeth.GetILGenerator();
      gen.EmitWriteLine("Hello world");
      gen.Emit(OpCodes.Ret);
      dynMeth.Invoke(null, null); // Hello world
   }
}

public class Test;