using System.Collections.Generic;

namespace Composite.Canonical.Implementation
{
   public class Branch : IUnit
   {
      private readonly LinkedList<IUnit> _nodes = new LinkedList<IUnit>();

      public Branch(string name)
      {
         Name = name;
         IsLeaf = false;
      }

      public string Name { get; private set; }

      public void Add(IUnit unit)
      {
         _nodes.AddLast(unit);
      }

      public void Remove(IUnit unit)
      {
         _nodes.Remove(unit);
      }

      public bool IsLeaf { get; private set; }

      public IEnumerable<IUnit> Units
      {
         get
         {
            return _nodes;
         }
      }
   }
}