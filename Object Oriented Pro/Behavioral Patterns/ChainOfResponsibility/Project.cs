using System.Collections.Generic;

namespace ChainOfResponsibility
{
   public class Project : IProjectItem
   {
      public IContact Owner { get; }

      public string Details { get; }

      public string Name { get; }

      public IList<IProjectItem> ProjectItems { get; }

      public IProjectItem Parent { get; }

      public Project(string name, string details, IContact owner)
      {
         Owner = owner;
         Details = details;
         Name = name;
         ProjectItems = new List<IProjectItem>();
         Parent = null;
      }

      public void Add(IProjectItem projectItem)
      {
         if (!ProjectItems.Contains(projectItem))
         {
            ProjectItems.Add(projectItem);
         }
      }

      public override string ToString() => Name;
   }
}