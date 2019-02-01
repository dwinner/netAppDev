using System;

namespace FactoryMethod
{
   public sealed class TechnicalModuleImpl : ModuleImpl
   {
      public override void SomeModule()
         => Console.WriteLine("Technical content");
   }
}