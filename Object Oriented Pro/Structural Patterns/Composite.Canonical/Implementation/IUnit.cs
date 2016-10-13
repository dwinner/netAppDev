using System.Collections.Generic;

namespace Composite.Canonical.Implementation
{
   /// <summary>
   /// Интерфейс компоновщика объектов
   /// </summary>   
   public interface IUnit
   {
      string Name { get; }

      void Add(IUnit unit);

      void Remove(IUnit unit);

      bool IsLeaf { get; }

      IEnumerable<IUnit> Units { get; }
   }
}