using System.Collections.Generic;

namespace ChainOfResponsibility
{
   public class Project : IProjectItem
   {
      public IContact Owner { get; set; }

      public string Details { get; set; }

      public string Name { get; set; }

      public IList<IProjectItem> ProjectItems { get; private set; }

      public IProjectItem Parent { get; private set; }

      public bool IsPrimaryTask
      {
         get
         {
            return false;
         }
      }

      public Project() { }

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

      public bool Remove(IProjectItem projectItem)
      {
         return ProjectItems.Remove(projectItem);
      }

      public override string ToString()
      {
         return Name;
      }
   }
}