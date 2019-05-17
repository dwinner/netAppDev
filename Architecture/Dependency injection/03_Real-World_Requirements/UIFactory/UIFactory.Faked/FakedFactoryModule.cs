using Ninject.Modules;
using UIFactory.Core;

namespace UIFactory.Faked
{
   public class FakedFactoryModule : NinjectModule
   {
      public override void Load()
      {
         Bind<IUiFactory>().To<FakedUiFactory>();
         Bind<ButtonBase>().To<FakedButton>();
         Bind<CheckBoxBase>().To<FakedCheckBox>();
      }
   }
}