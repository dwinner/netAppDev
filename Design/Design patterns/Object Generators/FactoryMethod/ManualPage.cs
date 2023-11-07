using System;
using System.Collections.Generic;

namespace FactoryMethod
{
   public class ManualPage : PageImpl
   {
      public ManualPage()
      {
         PageCompositor = new List<ModuleImpl>();
      }

      public override void AddModule()
      {
         PageCompositor.Clear();
         PageCompositor.Add(new TechnicalModuleImpl());
         PageCompositor.Add(new PictureModuleImpl());
         PageCompositor.Add(new InstructionModuleImpl());
      }

      public override void DisplayPage()
      {
         Console.WriteLine("Manual page contains:");
         foreach (var moduleImpl in PageCompositor)
         {
            moduleImpl.SomeModule();
         }

         Console.WriteLine();
      }
   }
}