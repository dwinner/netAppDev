using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace Graphic.Core
{
   public class MainModule : NinjectModule
   {
      public override void Load()
      {
         Bind<IShapeFactory>().ToFactory();
      }
   }
}