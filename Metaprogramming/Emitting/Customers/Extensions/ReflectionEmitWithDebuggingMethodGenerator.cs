using System;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;

namespace Customers.Extensions
{
   public sealed class ReflectionEmitWithDebuggingMethodGenerator
   {
      private readonly ModuleBuilder _module;

      public ReflectionEmitWithDebuggingMethodGenerator()
      {
         var name = new AssemblyName(Guid.NewGuid().ToString("N"));
         var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
         AddDebuggingAttribute(assembly);
         _module = assembly.DefineDynamicModule($"{name.Name}.dll", true);
      }

      private void AddDebuggingAttribute(AssemblyBuilder assembly)
      {
         var debugAttribute = typeof (DebuggableAttribute);
         var debugCtor = debugAttribute.GetConstructor(new[] {typeof (DebuggableAttribute.DebuggingModes)});
         if (debugCtor != null)
         {
            var debugBuilder = new CustomAttributeBuilder(debugCtor,
               new object[]
               {DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.Default});
            assembly.SetCustomAttribute(debugBuilder);
         }
      }

      public Func<T, string> Generate<T>()
      {
         var target = typeof (T);
         var fileName = $"{target.Name}ToString.il";
         var document = _module.DefineDocument(fileName, SymDocumentType.Text, SymLanguageType.ILAssembly,
            SymLanguageVendor.Microsoft);

         var type = _module.DefineType($"{target.Namespace}.{target.Name}");
         var methodName = $"{nameof(ToString)}{target.GetHashCode()}";
         var method = type.DefineMethod(methodName, MethodAttributes.Static | MethodAttributes.Public, typeof (string),
            new[] {target});
         method.DefineParameter(1, ParameterAttributes.In, "target");

         using (var file = File.CreateText(fileName))
         {
            method.GetILGenerator().Generate(target, document, file);
         }

         var createdType = type.CreateType();
         var createdMethod = createdType.GetMethod(methodName);
         return (Func<T, string>) Delegate.CreateDelegate(typeof (Func<T, string>), createdMethod);
      }
   }
}