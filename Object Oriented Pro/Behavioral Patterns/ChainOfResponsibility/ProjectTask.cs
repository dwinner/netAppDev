using System.Collections.Generic;

namespace ChainOfResponsibility
{
   public class ProjectTask : IProjectItem
   {
      public IContact Owner
      {
         get { return _owner ?? Parent.Owner; }
         set { _owner = value; }
      }
      private IContact _owner;

      public string Details
      {
         get { return IsPrimaryTask ? _details : string.Format("{0} {1}", Parent.Details, _details); }
         set { _details = value; }
      }
      private string _details;

      public string Name { get; set; }

      public IList<IProjectItem> ProjectItems { get; private set; }

      public IProjectItem Parent { get; private set; }

      public bool IsPrimaryTask { get; private set; }

      public ProjectTask(IProjectItem newParent, string newName, string newDetails, IContact owner, bool newPrimaryTask)
      {
         Parent = newParent;
         Name = newName;
         _details = newDetails;
         _owner = owner;
         IsPrimaryTask = newPrimaryTask;
         ProjectItems = new List<IProjectItem>();
      }

      public ProjectTask(IProjectItem parent)
         : this(parent, string.Empty, string.Empty, null, false)
      { }

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