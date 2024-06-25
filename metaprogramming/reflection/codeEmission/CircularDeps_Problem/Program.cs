// class A { S<B> Bee; }
// class B { S<A> Aye; }

using System.Reflection;
using System.Reflection.Emit;

namespace CircularDeps_Problem;

internal static class Program
{
   private static void Main()
   {
      var aname = new AssemblyName("MyEmissions");
      var assemBuilder = AssemblyBuilder.DefineDynamicAssembly(aname, AssemblyBuilderAccess.Run);
      var modBuilder = assemBuilder.DefineDynamicModule("MainModule");
      var pub = FieldAttributes.Public;

      var aBuilder = modBuilder.DefineType("A");
      var bBuilder = modBuilder.DefineType("B");

      aBuilder.DefineField("Bee", typeof(S<>).MakeGenericType(bBuilder), pub);
      bBuilder.DefineField("Aye", typeof(S<>).MakeGenericType(aBuilder), pub);

      var realA = aBuilder.CreateType(); // TypeLoadException: cannot load type B
      var realB = bBuilder.CreateType();
   }
}

public struct S<T>
{
   public T SomeField;
}