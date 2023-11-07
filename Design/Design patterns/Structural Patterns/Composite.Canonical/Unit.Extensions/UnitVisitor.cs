using Composite.Canonical.Implementation;
using System;

namespace Composite.Canonical.Unit.Extensions
{
   public static class UnitVisitor
   {
      public static void GetChild(this IUnit unit, int level)
      {
         if (unit.IsLeaf)
         {
            Console.WriteLine(new string('*', level) + unit.Name);
         }
         else
         {
            Console.WriteLine(new string('#', level) + unit.Name);
            foreach (var aUnit in unit.Units)
            {
               aUnit.GetChild(level + 1);
            }
         }
      }
   }
}