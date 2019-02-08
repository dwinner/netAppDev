using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace ILCodeWeaving
{
    public class ReweavePropContext
    {
        #region Properties

        public ModuleDefinition MainModule { get; set; }
        public PropertyDefinition Property { get; set; }
        
        #endregion

        #region Public interface

        public void Returns(object returnValue)
        {
            var getterMethod = Property.GetMethod;
            var returnString = returnValue as string;

            //Get the site of code injection
            var ilProcessor = getterMethod.Body.GetILProcessor();
            var firstInstruction = ilProcessor.Body.Instructions.First();

            ilProcessor.InsertBefore(firstInstruction, ilProcessor.Create(OpCodes.Ldstr, returnString));
            ilProcessor.InsertBefore(firstInstruction, ilProcessor.Create(OpCodes.Ret));
        }

        public void Sets(object valueToSet)
        {
            var setterMethod = Property.SetMethod;
            var stringValue = valueToSet as string;

            //Get the load instruction to replace
            var ilProcessor = setterMethod.Body.GetILProcessor();
            var argumentLoadInstructions = ilProcessor.Body.Instructions
                .Where(l => l.OpCode == OpCodes.Ldarg_1)
                .ToList();
            var fakeValueLoad = ilProcessor.Create(OpCodes.Ldstr, stringValue);

            foreach (var instruction in argumentLoadInstructions)
            {
                ilProcessor.Replace(instruction, fakeValueLoad);
            }
        }

        #endregion
    }
}
