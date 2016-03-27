// Внедрение зависимостей для бедных

using Blogic;
using Impl;
using Interfaces;

namespace ByHandExample
{
   internal static class Program
   {
      private static void Main()
      {
         IWeapon sword = new Sword();
         var warrior1 = new Samurai(sword);

         IWeapon shuriken = new Shuriken();
         var warrior2 = new Samurai(shuriken);

         const string enemies = "the evildoers";
         warrior1.Attack(enemies);
         warrior2.Attack(enemies);
      }
   }
}