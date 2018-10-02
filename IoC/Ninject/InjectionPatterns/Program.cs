// Стандартные шаблоны внедрения зависимостей

using System;
using Impl;
using Interfaces;
using Ninject;
using Ninject.Modules;

namespace InjectionPatterns
{
   internal static class Program
   {
      private static void Main()
      {
         IKernel stdKernel = new StandardKernel(new DefaultModule());
         var soldier = stdKernel.Get<Soldier>();
         //var weapon = stdKernel.Get<IWeapon>();
         soldier.Attack("Enemies");
      }
   }

   public class DefaultModule : NinjectModule
   {
      public override void Load()
      {
         Bind<IWeapon>().To<Sword>();
         Bind<Soldier>().ToSelf();
      }
   }

   public class Soldier
   {
      private readonly IWeapon _weapon;

      //[Inject]
      public Soldier(IWeapon weapon)
      {
         if (weapon == null)
            throw new ArgumentNullException("weapon");
         _weapon = weapon;
      }

      public void Attack(string target)
      {
         _weapon.Hit(target);
      }
   }
}