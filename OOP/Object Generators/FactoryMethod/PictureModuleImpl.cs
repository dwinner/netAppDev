using System;

namespace FactoryMethod
{
   public class PictureModuleImpl : ModuleImpl
   {
      public override void SomeModule()
         => Console.WriteLine("Picture content");
   }
}