using System.Collections.Generic;

namespace ChainOfResponsibility
{
   public interface IProjectItem
   {      
      IContact Owner { get; }

      string Details { get; }

      IList<IProjectItem> ProjectItems { get; }

      void Add(IProjectItem projectItem);
   }
}