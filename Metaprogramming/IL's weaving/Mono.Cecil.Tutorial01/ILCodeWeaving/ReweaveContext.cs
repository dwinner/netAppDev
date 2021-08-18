using System;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace ILCodeWeaving
{
   public class ReweaveContext
   {
      #region Properties

      public MethodDefinition Method { get; set; }
      public ModuleDefinition MainModule { get; set; }

      #endregion

      #region Public interface

      public void Returns(object returnValue)
      {
         var returnString = returnValue as string;

         //Get the site of code injection
         var ilProcessor = Method.Body.GetILProcessor();
         var firstInstruction = ilProcessor.Body.Instructions.First();

         ilProcessor.InsertBefore(firstInstruction, ilProcessor.Create(OpCodes.Ldstr, returnString));
         ilProcessor.InsertBefore(firstInstruction, ilProcessor.Create(OpCodes.Ret));
      }

      public void Throws()
      {
         //Obtain the class type through reflection
         //Then import it to the target module
         var reflectionType = typeof(Exception);
         var exceptionCtor = reflectionType.GetConstructor(new Type[] { });

         var constructorReference = MainModule.Import(exceptionCtor);

         //Get the site of code injection
         var ilProcessor = Method.Body.GetILProcessor();
         var firstInstruction = ilProcessor.Body.Instructions.First();

         ilProcessor.InsertBefore(firstInstruction, ilProcessor.Create(OpCodes.Newobj, constructorReference));
         ilProcessor.InsertBefore(firstInstruction, ilProcessor.Create(OpCodes.Throw));
      }

      public void Throws<TException>(params object[] arguments) where TException : Exception
      {
         var reflectionType = typeof(TException);
         var argumentTypes = arguments.Select(a => a.GetType()).ToArray();
         var exceptionCtor = reflectionType.GetConstructor(argumentTypes);
         var constructorReference = MainModule.Import(exceptionCtor);

         //Get the site of code injection
         var ilProcessor = Method.Body.GetILProcessor();
         var firstInstruction = ilProcessor.Body.Instructions.First();

         //Load arguments to the evaluation stack
         foreach (var argument in arguments)
            ilProcessor.InsertBefore(firstInstruction,
               ilProcessor.CreateLoadInstruction(argument));

         ilProcessor.InsertBefore(firstInstruction,
            ilProcessor.Create(OpCodes.Newobj, constructorReference));
         ilProcessor.InsertBefore(firstInstruction,
            ilProcessor.Create(OpCodes.Throw));
      }

      #endregion
   }
}