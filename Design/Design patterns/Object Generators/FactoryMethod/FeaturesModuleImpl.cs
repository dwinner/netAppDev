using System;

namespace FactoryMethod
{
   public class FeaturesModuleImpl : ModuleImpl
   {
      public override void SomeModule()
         => Console.WriteLine("Technical content");
   }
}