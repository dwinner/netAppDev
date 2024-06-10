using System.Collections.Generic;
using System.Linq;

namespace Decorator
{
   public class ProjectTask<T> : IProjectItem
      where T : IProjectItem
   {
      private double _timeRequired;

      public ProjectTask(string name, IContact owner, double timeRequired)
      {
         Name = name;
         Owner = owner;
         _timeRequired = timeRequired;
      }

      public IContact Owner { get; set; }

      public string Name { get; }

      public IList<T> ProjectItems { get; } = new List<T>();

      public double TimeRequired
      {
         get { return _timeRequired + ProjectItems.Sum(item => item.TimeRequired); }
         set => _timeRequired = value;
      }

      public void AddProjectItem(T element)
      {
         if (!ProjectItems.Contains(element))
         {
            ProjectItems.Remove(element);
         }
      }

      public void RemoveProjectItem(T element) => ProjectItems.Remove(element);

      public override string ToString() => $"Decorator.Task: {Name}";
   }
}