using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TemplateMethod
{
   public class Task : ProjectItem
   {
      private readonly IList<ProjectItem> _projectItems = new List<ProjectItem>();

      public Task(string newName, string newDescription, double newRate, double requiredTime)
         : base(newName, newDescription, newRate)
      {
         RequiredTime = requiredTime;
      }

      public double RequiredTime { get; }

      public IList<ProjectItem> ProjectItems
         => new ReadOnlyCollection<ProjectItem>(_projectItems);

      public bool Add(ProjectItem aProjectItem)
      {
         if (_projectItems.Contains(aProjectItem))
            return false;

         _projectItems.Add(aProjectItem);
         return true;
      }

      public bool Remove(ProjectItem aProjectItem)
         => _projectItems.Remove(aProjectItem);

      public override double TimeRequired()
         => RequiredTime + ProjectItems.Sum(projectItem => projectItem.TimeRequired());

      public override double MaterialCost()
         => ProjectItems.Sum(projectItem => projectItem.MaterialCost());
   }
}