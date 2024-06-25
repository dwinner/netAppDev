// class A { S<B> Bee; }
// class B { S<A> Aye; }

using System.Reflection;
using System.Reflection.Emit;

namespace CircularDeps_Solution;

internal static class Program
{
   private static void Main()
   {
      var aname = new AssemblyName("MyEmissions");

      var assemBuilder = AssemblyBuilder.DefineDynamicAssembly(
         aname, AssemblyBuilderAccess.Run);

      var modBuilder = assemBuilder.DefineDynamicModule("MainModule");

      var pub = FieldAttributes.Public;

      var aBuilder = modBuilder.DefineType("A");
      var bBuilder = modBuilder.DefineType("B");

      aBuilder.DefineField("Bee", typeof(S<>).MakeGenericType(bBuilder), pub);
      bBuilder.DefineField("Aye", typeof(S<>).MakeGenericType(aBuilder), pub);

      TypeBuilder[] uncreatedTypes = { aBuilder, bBuilder };

      ResolveEventHandler handler = delegate(object? o, ResolveEventArgs args)
      {
         var type = uncreatedTypes.FirstOrDefault(t => t.FullName == args.Name);
         return type?.CreateType().Assembly;
      };

      AppDomain.CurrentDomain.TypeResolve += handler;

      var realA = aBuilder.CreateType();
      var realB = bBuilder.CreateType();

      AppDomain.CurrentDomain.TypeResolve -= handler;
   }
}

public struct S<T>
{
   public T SomeField;
}