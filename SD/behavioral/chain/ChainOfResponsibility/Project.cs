using System.Collections.Generic;

namespace ChainOfResponsibility
{
   public class Project : IProjectItem
   {
      public Project(string name, string details, IContact owner)
      {
         Owner = owner;
         Details = details;
         Name = name;
         ProjectItems = new List<IProjectItem>();
         Parent = null;
      }

      public string Name { get; }

      public IProjectItem Parent { get; }
      public IContact Owner { get; }

      public string Details { get; }

      public IList<IProjectItem> ProjectItems { get; }

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