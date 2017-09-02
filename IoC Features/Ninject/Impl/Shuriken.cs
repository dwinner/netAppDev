using System;
using Interfaces;

namespace Impl
{
   public class Shuriken : IWeapon
   {
      public void Hit(string target)
      {
         Console.WriteLine("Pierced {0}'s armor", target);
      }
   }
}