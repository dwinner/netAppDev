using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TemplateMethod
{
   public class Task : ProjectItem
   {
      public double RequiredTime { get; set; }

      private readonly IList<ProjectItem> _projectItems = new List<ProjectItem>();

      public IList<ProjectItem> ProjectItems
      {
         get
         {
            return new ReadOnlyCollection<ProjectItem>(_projectItems);
         }
      }

      public Task()
      {
      }

      public Task(string newName, string newDescription, double newRate, double requiredTime)
         : base(newName, newDescription, newRate)
      {
         RequiredTime = requiredTime;
      }

      public bool Add(ProjectItem aProjectItem)
      {
         if (_projectItems.Contains(aProjectItem))
         {
            return false;
         }

         _projectItems.Add(aProjectItem);
         return true;
      }

      public bool Remove(ProjectItem aProjectItem)
      {
         return _projectItems.Remove(aProjectItem);
      }

      public override double TimeRequired()
      {
         return RequiredTime + ProjectItems.Sum(projectItem => projectItem.TimeRequired());
      }

      public override double MaterialCost()
      {
         return ProjectItems.Sum(projectItem => projectItem.MaterialCost());
      }
   }
}