// Контекстные привязки

using System;
using Impl;
using Interfaces;
using Ninject;
using Ninject.Modules;
using Ninject.Planning.Bindings;

namespace ContextualBindingSystem
{
   internal static class Program
   {
      private static void Main()
      {
         NamedBindings.Demo();
         ConstraintBindings.Demo();
      }
   }

   internal static class NamedBindings // Привязки по имени
   {
      internal static void Demo()
      {
         IKernel kernel = new StandardKernel(new SimpleNamedModule());
         var weapon = kernel.Get<IWeapon>();
         var attack = new WeakAttack(weapon);
         attack.Attack("test");

         kernel.Get<IWeapon>("Weak"); // NOTE: Service location!
      }

      private class SimpleNamedModule : NinjectModule
      {
         public override void Load()
         {
            Bind<IWeapon>().To<Shuriken>().Named("Strong");
            Bind<IWeapon>().To<Dagger>().Named("Weak");
         }
      }

      private class WeakAttack
      {
         private readonly IWeapon _weapon;

         public WeakAttack([Named("Weak")] IWeapon weapon)
         {
            _weapon = weapon;
         }

         public void Attack(string victim)
         {
            _weapon.Hit(victim);
         }
      }

      private class Dagger : IWeapon
      {
         public void Hit(string target)
         {
            Console.WriteLine("Stab {0} to death", target);
         }
      }
   }

   internal static class ConstraintBindings // Ограничивающие привязки
   {
      internal static void Demo()
      {
      }

      [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
         AllowMultiple = true)]
      private class SwimmerAttribute : ConstraintAttribute
      {
         public override bool Matches(IBindingMetadata metadata)
         {
            return metadata.Has("CanSwim") && metadata.Get<bool>("CanSwim");
         }
      }

      private class WarriorsModule : NinjectModule
      {
         public override void Load()
         {
            Bind<IWarrior>().To<Ninja>();
            Bind<IWarrior>().To<Samurai>().WithMetadata("CanSwim", false);
            Bind<IWarrior>().To<SpecialNinja>().WithMetadata("CanSwim", true);
         }
      }

      private class AmphibiousAttack
      {
         public AmphibiousAttack([Swimmer] IWarrior warrior)
         {
            //
         }
      }

      private interface IWarrior
      {
      }

      private class Ninja : IWarrior
      {
      }

      private class Samurai : IWarrior
      {
      }

      private class SpecialNinja : IWarrior
      {
      }
   }
}