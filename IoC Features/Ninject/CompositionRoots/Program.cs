// Корни компоновки

using Blogic;
using Impl;
using Interfaces;
using Ninject;
using Ninject.Modules;

namespace CompositionRoots
{
   internal static class Program
   {
      private static void Main()
      {
         var kernel = CompositionRoot.Impl;  // kernel.Load("*.dll");
         var samurai = kernel.Get<Samurai>();
         samurai.Attack("The evildoers");
      }
   }

   public class WarriorModule : NinjectModule // Первый модуль привязки компонентов
   {
      private readonly bool _isSword;

      public WarriorModule(bool isSword)
      {
         _isSword = isSword;
      }

      public override void Load()
      {
         if (_isSword)
         {
            Bind<IWeapon>().To<Sword>();
         }
         else
         {
            Bind<IWeapon>().To<Shuriken>();
         }
      }
   }

   public class SamuraiModule : NinjectModule // Другой модуль привязки компонентов
   {
      public override void Load()
      {
         Bind<Samurai>().ToSelf().InSingletonScope();
      }
   }

   public static class CompositionRoot // Корень компоновки
   {
      static CompositionRoot()
      {
         Impl = new StandardKernel(new SamuraiModule(), new WarriorModule(false));
      }

      public static IKernel Impl { get; private set; }
   }
}