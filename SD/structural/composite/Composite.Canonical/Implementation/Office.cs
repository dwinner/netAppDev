using System;
using System.Collections.Generic;

namespace Composite.Canonical.Implementation
{
   public class Office : IUnit
   {
      public Office(string name)
      {
         Name = name;
         IsLeaf = true;
      }

      public string Name { get; }

      public void Add(IUnit unit)
      {
         throw new NotSupportedException("Нельзя добавить элементы в лист");
      }

      public void Remove(IUnit unit)
      {
         throw new NotSupportedException("Нельзя удалять элементы из листа");
      }

      public bool IsLeaf { get; }

      public IEnumerable<IUnit> Units => null;
   }
}