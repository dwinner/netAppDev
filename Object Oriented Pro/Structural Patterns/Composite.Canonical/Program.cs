using Composite.Canonical.Implementation;
using Composite.Canonical.Unit.Extensions;
/**
 * Каноническая форма компоновщика объектов
 */

using System;

namespace Composite.Canonical
{
   static class Program
   {
      static void Main()
      {
         IUnit root = new Branch("US (Root)");
         IUnit ny = new Office("A (Unit)");
         IUnit ca = new Office("B (Unit)");

         if (!root.IsLeaf)
         {
            root.Add(ny);
            root.Add(ca);
         }

         IUnit rootHawaii = new Branch("Canada Branch (Branch)");
         if (!root.IsLeaf)
         {
            root.Add(rootHawaii);
         }

         IUnit branchUk = new Branch("UK Branch (Branch)");
         IUnit ldnc = new Office("C Office (Unit)");
         IUnit ldnw = new Office("D Office (Unit)");
         if (!branchUk.IsLeaf)
         {
            branchUk.Add(ldnc);
            branchUk.Add(ldnw);
         }
         if (!root.IsLeaf)
         {
            root.Add(branchUk);
         }

         IUnit dummy = new Office("D Office");
         if (!dummy.IsLeaf)
         {
            ldnc.Add(dummy);
         }

         root.GetChild(0);

         if (!root.IsLeaf)
         {
            root.Remove(rootHawaii);
         }
         if (!branchUk.IsLeaf)
         {
            branchUk.Remove(ldnc);
         }
         Console.WriteLine("Remove Hawaii branch and London City office");
         root.GetChild(0);

         Console.ReadKey();
      }
   }
}
