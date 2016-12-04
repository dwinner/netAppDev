using Injectors.Core.Attributes.Generic;
using Mono.Cecil;
using System;
using CecilOpCodes = Mono.Cecil.Cil.OpCodes;

namespace Injectors.Core.Attributes
{
   [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
   [Serializable]
   public sealed class NotNullAttribute : InjectorAttribute<ParameterDefinition>
   {
      protected override void OnInject(ParameterDefinition target)
      {
         if (!target.ParameterType.IsValueType)
         {
            var method = (target.Method as MethodDefinition);
            var argumentNullExceptionCtor = method.DeclaringType.Module.Assembly.MainModule.Import(
               typeof(ArgumentNullException).GetConstructor(new Type[] { typeof(string) }));

            var processor = method.Body.GetILProcessor();
            var first = processor.Body.Instructions[0];            
            var ldArgInstruction = processor.Create(CecilOpCodes.Ldarg, target);
            ldArgInstruction.SequencePoint = new NotNullAttributeDebugger(method, target).SequencePoint;

            processor.InsertBefore(first, ldArgInstruction);
            processor.InsertBefore(first, processor.Create(CecilOpCodes.Brtrue_S, first));
            processor.InsertBefore(first, processor.Create(CecilOpCodes.Ldstr, target.Name));
            processor.InsertBefore(first, processor.Create(CecilOpCodes.Newobj, argumentNullExceptionCtor));
            processor.InsertBefore(first, processor.Create(CecilOpCodes.Throw));
         }
      }
   }
}
