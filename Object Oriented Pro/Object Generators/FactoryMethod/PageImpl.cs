using System.Collections.Generic;

namespace FactoryMethod
{
   public class PageImpl : IPage<ModuleImpl>
   {
      public IList<ModuleImpl> PageCompositor { get; protected set; }

      protected PageImpl(IList<ModuleImpl> pageCompositor)
      {
         PageCompositor = pageCompositor;
      }

      protected PageImpl() { }

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