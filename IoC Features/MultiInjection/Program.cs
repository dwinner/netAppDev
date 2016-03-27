// Множественное внедрение зависимостей

using System;
using Ninject;
using Ninject.Modules;

namespace MultiInjection
{
   internal static class Program
   {
      private static void Main()
      {
         IKernel stdKernel = new StandardKernel(new TestModule());
         var samurai = stdKernel.Get<MultiSamurai>();
         samurai.Attack("your enemy");

         // Или так
         var weapons = stdKernel.GetAll<IWeapon>();
         foreach (var weapon in weapons)
         {
            Console.WriteLine(weapon.Hit("the evildoers"));
         }
      }
   }

   public interface IWeapon
   {
      string Hit(string target);
   }

   public class Sword : IWeapon
   {
      public string Hit(string target)
      {
         return string.Format("Slice {0} in half", target);
      }
   }

   public class Dagger : IWeapon
   {
      public string Hit(string target)
      {
         return string.Format("Stab {0} to death", target);
      }
   }

   public class MultiSamurai
   {
      private readonly IWeapon[] _weapons;

      public MultiSamurai(params IWeapon[] weapons)
      {
         _weapons = weapons;
      }

      public void Attack(string target)
      {
         Array.ForEach(_weapons, weapon => Console.WriteLine(weapon.Hit(target)));
      }
   }

   public class TestModule : NinjectModule   // Корень компоновки
   {
      public override void Load()
      {
         Bind<IWeapon>().To<Sword>();
         Bind<IWeapon>().To<Dagger>();
      }
   }
}