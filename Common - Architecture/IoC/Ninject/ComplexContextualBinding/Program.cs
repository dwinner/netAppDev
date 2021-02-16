// Сложные привязки в зависимостях

using System;
using Ninject;
using Ninject.Modules;

namespace ComplexContextualBinding
{
   internal static class Program
   {
      private static void Main()
      {
         IKernel k = new StandardKernel();
      }
   }

   internal class SimpleModule : NinjectModule
   {
      public override void Load()
      {
         // Bind<IWarrior>().To<Samurai>().WhenInjectedInto(typeof(OnLandAttack));
         // Bind<IWarrior>().To<SpecialNinja>().WhenInjectedInto(typeof(AmphibiousAttack));

         Bind<IWarrior>().To<Ninja>();
         Bind<IWarrior>().To<Samurai>().WhenClassHas<ClimberNeeded>();
         Bind<IWarrior>().To<Samurai>().WhenTargetHas<ClimberNeeded>();
         Bind<IWarrior>().To<SpecialNinja>().WhenMemberHas<SwimmerNeeded>();
      }
   }

   internal interface IWarrior
   {
   }

   internal class Samurai : IWarrior
   {
   }

   internal class SpecialNinja : IWarrior
   {
   }

   internal class Ninja : IWarrior
   {
   }

   internal class SwimmerNeeded : Attribute
   {
   }

   internal class ClimberNeeded : Attribute
   {
   }

   internal class MultiAttack
   {
      public MultiAttack([ClimberNeeded] IWarrior mountainWarrior)
      {
      }

      [Inject, SwimmerNeeded]
      private IWarrior OffShoreWarrior { get; set; }

      [Inject]
      private IWarrior AnyOldWarrior { get; set; }
   }

   [ClimberNeeded]
   internal class MountainousAttack
   {
      [Inject, SwimmerNeeded]
      private IWarrior HighlandLakeSwimmer { get; set; }

      [Inject]
      private IWarrior StandardMountainWarrior { get; set; }
   }
}