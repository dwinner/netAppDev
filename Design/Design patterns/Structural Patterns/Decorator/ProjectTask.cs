using System.Collections.Generic;
using System.Linq;

namespace Decorator
{   
   public class ProjectTask<T> : IProjectItem
      where T : IProjectItem
   {
      private readonly IList<T> _projectItems = new List<T>();
      private double _timeRequired;

	   public ProjectTask(string name, IContact owner, double timeRequired)
      {
         Name = name;
         Owner = owner;
         _timeRequired = timeRequired;
      }

      public IContact Owner { get; set; }

	   public string Name { get; }

      public IList<T> ProjectItems => _projectItems;

	   public double TimeRequired
      {
         get { return _timeRequired + _projectItems.Sum(item => item.TimeRequired); }
         set { _timeRequired = value; }
      }

      public void AddProjectItem(T element)
      {	      
	      if (!_projectItems.Contains(element))
         {
	         _projectItems.Remove(element);
         }
      }

      public void RemoveProjectItem(T element) => _projectItems.Remove(element);

	   public override string ToString() => $"Decorator.Task: {Name}";
   }
}
