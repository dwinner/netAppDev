using Ninject.Modules;
using UIFactory.Core;

namespace UIFactory.Real
{
   public class RealFactoryModule : NinjectModule
   {
      public override void Load()
      {
         Bind<IUiFactory>().To<RealUiFactory>();
         Bind<ButtonBase>().To<RealButton>();
         Bind<CheckBoxBase>().To<RealCheckBox>();
      }
   }
}