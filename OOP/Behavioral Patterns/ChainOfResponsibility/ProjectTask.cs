using System.Collections.Generic;

namespace ChainOfResponsibility
{
   public class ProjectTask : IProjectItem
   {
      private string _details;
      private IContact _owner;

      public ProjectTask(
      	 IProjectItem newParent, string newName, string newDetails, IContact owner, bool newPrimaryTask)
      {
         Parent = newParent;
         Name = newName;
         _details = newDetails;
         _owner = owner;
         IsPrimaryTask = newPrimaryTask;
         ProjectItems = new List<IProjectItem>();
      }

      public IContact Owner
      {
         get { return _owner ?? Parent.Owner; }
         set { _owner = value; }
      }

      public string Details
      {
         get { return IsPrimaryTask ? _details : $"{Parent.Details} {_details}"; }
         set { _details = value; }
      }

      public string Name { get; set; }

      public IList<IProjectItem> ProjectItems { get; }

      public IProjectItem Parent { get; }

      public bool IsPrimaryTask { get; }

      public void Add(IProjectItem projectItem)
      {
         if (!ProjectItems.Contains(projectItem))
            ProjectItems.Add(projectItem);
      }

      public bool Remove(IProjectItem projectItem) => ProjectItems.Remove(projectItem);

      public override string ToString() => Name;
   }
}