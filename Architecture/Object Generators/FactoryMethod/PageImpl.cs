using System.Collections.Generic;

namespace FactoryMethod
{
   public class PageImpl : IPage<ModuleImpl>
   {
      protected PageImpl()
      {
      }

      public IList<ModuleImpl> PageCompositor { get; protected set; }

      public virtual void AddModule()
      {
         PageCompositor.Clear();
         PageCompositor.Add(new FeaturesModuleImpl());
         PageCompositor.Add(new PictureModuleImpl());
      }

      public virtual void DisplayPage()
      {
         foreach (var moduleImpl in PageCompositor)
         {
            moduleImpl.SomeModule();
         }
      }
   }
}