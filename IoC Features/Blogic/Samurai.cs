using Interfaces;

namespace Blogic
{
   public class Samurai
   {
      private readonly IWeapon _sword;

      public Samurai(IWeapon sword)
      {
         _sword = sword;
      }

      public void Attack(string target)
      {
         _sword.Hit(target);
      }
   }
}