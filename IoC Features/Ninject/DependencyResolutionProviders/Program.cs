// Другие способы управления корнями компоновки

using Impl;
using Interfaces;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;

namespace DependencyResolutionProviders
{
   internal static class Program
   {
      private static void Main()
      {
         IKernel kernel = new StandardKernel(new SpecialModule(), new FactoryModule());
         var weapon = kernel.Get<IWeapon>();
         weapon.Hit("test");  // NOTE: Исключение!
      }
   }

   public class SpecialModule : NinjectModule // Специальный модуль
   {
      public override void Load()
      {
         Bind<IWeapon>().ToProvider(new SwordProvider());
      }
   }

   public class FactoryModule : NinjectModule // Фабричный модуль
   {
      public override void Load()
      {
         Bind<IWeapon>().ToMethod(context => new Shuriken());
      }
   }

   public class SwordProvider : Provider<Sword> // Control freak
   {
      protected override Sword CreateInstance(IContext context)
      {
         var sword = new Sword();
         // NOTE: Какая-то нетривиальная логика инициализации объекта
         return sword;
      }
   }
}