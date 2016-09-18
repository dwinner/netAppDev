using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;
using static Mono.Cecil.Cil.OpCodes;

namespace NotNullAttribute.Lib
{
   public static class InjectorExtensions
   {
      public static void InjectAssembly(FileSystemInfo assemblyLocation)
      {
         var assembly = AssemblyDefinition.ReadAssembly(assemblyLocation.FullName);
         assembly.Inject(nameof(NotNullRequiredAttribute));
         assembly.Write(assemblyLocation.FullName);
      }

      private static void Inject(this AssemblyDefinition @this, string attributeName)
         =>
         @this.GetParametersToInject(attributeName)
            .ForEach(parameterDefinition => parameterDefinition.InjectNotNullCheck());

      private static List<ParameterDefinition> GetParametersToInject(this AssemblyDefinition @this, string attributeName)
         => (from module in @this.Modules
            from type in module.Types
            from method in type.Methods
            from parameterDefinition in method.GetParametersToInject(attributeName)
            select parameterDefinition).ToList();

      private static IEnumerable<ParameterDefinition> GetParametersToInject(this IMethodSignature method,
            string attributeName)
         => method.Parameters.Where(
            paramDefinition =>
               paramDefinition.HasCustomAttributes && !paramDefinition.ParameterType.IsValueType
               && paramDefinition.CustomAttributes.Any(
                  attribute => attribute.AttributeType.Name == attributeName)).ToList();

      private static void InjectNotNullCheck(this ParameterDefinition @this)
      {
         var methodDefinition = @this.Method as MethodDefinition;
         if (methodDefinition == null)
         {
            return;
         }

         var argumentNullExceptionCtor =
            methodDefinition.DeclaringType.Module.Assembly.MainModule.Import(
               typeof(ArgumentNullException).GetConstructor(new[] {typeof(string)}));
         var processor = methodDefinition.Body.GetILProcessor();
         var bodyInstructions = processor.Body.Instructions;
         var first = bodyInstructions[0];
         processor.InsertBefore(first, processor.Create(Ldarg, @this));
         processor.InsertBefore(first, processor.Create(Brtrue_S, first));
         processor.InsertBefore(first, processor.Create(Ldstr, @this.Name));
         processor.InsertBefore(first, processor.Create(Newobj, argumentNullExceptionCtor));
         processor.InsertBefore(first, processor.Create(Throw));
      }
   }
}