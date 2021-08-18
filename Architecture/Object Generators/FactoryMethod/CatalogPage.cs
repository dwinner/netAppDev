using System.Collections.Generic;

namespace FactoryMethod
{
   public sealed class CatalogPage : PageImpl
   {
      public CatalogPage()
      {
         PageCompositor = new List<ModuleImpl>();
      }

      public override void AddModule()
      {
         PageCompositor.Clear();
         PageCompositor.Add(new FeaturesModuleImpl());
         PageCompositor.Add(new PictureModuleImpl());
      }

      public override void DisplayPage()
      {
         foreach (var moduleImpl in PageCompositor)
         {
            moduleImpl.SomeModule();
         }
      }
   }
}