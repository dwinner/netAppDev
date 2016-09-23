using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Customers.Extensions
{
   public sealed class ReflectionEmitMethodGenerator
   {
      private readonly ModuleBuilder _module;

      public ReflectionEmitMethodGenerator()
      {
         var name = new AssemblyName(Guid.NewGuid().ToString("N"));
         var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
         _module = assembly.DefineDynamicModule(name.Name);
      }

      public Func<T, string> Generate<T>()
      {
         var target = typeof (T);
         var type = _module.DefineType($"{target.Namespace}.{target.Name}");
         var methodName = $"{nameof(ToString)}{target.GetHashCode()}";
         var method = type.DefineMethod(
            methodName, MethodAttributes.Static | MethodAttributes.Public, typeof (string),
            new[] {target});
         var ilGenerator = method.GetILGenerator();
         ilGenerator.Generate(target);
         var createdType = type.CreateType();
         var createdMethod = createdType.GetMethod(methodName);
         return (Func<T, string>) Delegate.CreateDelegate(typeof (Func<T, string>), createdMethod);
      }
   }
}