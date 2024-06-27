using System.Reflection;
using System.Reflection.Emit;

namespace DynamicAssemblyMp;

public static class MyTypeGenerator
{
   public static Type Generate()
   {
      var name = new AssemblyName("MyDynamicAssembly");
      var assembly = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
      var module = assembly.DefineDynamicModule("MyDynamicModule");
      var typeBuilder = module.DefineType("MyType", TypeAttributes.Public | TypeAttributes.Class);
      var methodBuilder = typeBuilder.DefineMethod("SaySomething", MethodAttributes.Public);
      methodBuilder.SetParameters(typeof(string));
      methodBuilder.DefineParameter(0, ParameterAttributes.None, "message");
      var ilGen = methodBuilder.GetILGenerator();

      var consoleType = typeof(Console);
      var writeLineMethod = consoleType.GetMethod(nameof(Console.WriteLine), [typeof(string)])!;

      ilGen.Emit(OpCodes.Ldarg_1);
      ilGen.EmitCall(OpCodes.Call, writeLineMethod, [typeof(string)]);
      ilGen.Emit(OpCodes.Ret);

      var toStringMethod = typeof(object).GetMethod(nameof(ToString))!;
      var newToStringMethod = typeBuilder.DefineMethod(
         nameof(ToString),
         toStringMethod.Attributes,
         typeof(string),
         []);
      var ilGenerator = newToStringMethod.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldstr, "A message from ToString()");
      ilGenerator.Emit(OpCodes.Ret);
      typeBuilder.DefineMethodOverride(newToStringMethod, toStringMethod);

      return typeBuilder.CreateType();
   }
}