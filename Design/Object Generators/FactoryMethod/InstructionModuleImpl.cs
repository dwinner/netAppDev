using System;

namespace FactoryMethod
{
   public class InstructionModuleImpl : ModuleImpl
   {
      public override void SomeModule()
         => Console.WriteLine("Instruction content");
   }
}