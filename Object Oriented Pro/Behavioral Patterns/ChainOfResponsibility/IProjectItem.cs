using System.Collections.Generic;

namespace ChainOfResponsibility
{
   public interface IProjectItem
   {      
      IContact Owner { get; set; }

      string Details { get; set; }

      string Name { get; set; }

      IList<IProjectItem> ProjectItems { get; }

      IProjectItem Parent { get; }

      bool IsPrimaryTask { get; }

      void Add(IProjectItem projectItem);

      bool Remove(IProjectItem projectItem);
   }
}