using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Customers.Extensions
{
   public sealed class ReflectionEmitMethodGenerator
   {
      private AssemblyBuilder _assembly;
      private ModuleBuilder _module;
      private AssemblyName _name;

      public ReflectionEmitMethodGenerator()
      {
         _name=new AssemblyName(Guid.NewGuid().ToString("N"));
         _assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(_name, AssemblyBuilderAccess.Run);
         _module = _assembly.DefineDynamicModule(_name.Name);
      }

      public Func<T, string> Generate<T>()
      {
         var target = typeof(T);
         var type = _module.DefineType($"{target.Namespace}.{target.Name}");
         var methodName = $"{nameof(ToString)}{target.GetHashCode()}";
         var method = type.DefineMethod(methodName, MethodAttributes.Static|MethodAttributes.Public, typeof(string),new[] {target});
         var ilGenerator = method.GetILGenerator();
         ilGenerator.Generate(target);
         var createdType = type.CreateType();
         var createdMethod = createdType.GetMethod(methodName);
         return (Func<T, string>) Delegate.CreateDelegate(typeof (Func<T, string>), createdMethod);
      }
   }

   internal static class IlGeneratorExtensions
   {
      public static void Generate(this ILGenerator @this, Type type)
      {         
      }
   }
}