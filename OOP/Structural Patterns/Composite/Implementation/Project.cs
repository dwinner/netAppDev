using System;
using System.Collections.Generic;
using System.Linq;

namespace Composite.Implementation
{
   /// <summary>
   ///    Проект. Составной класс компоновщика.
   /// </summary>
   /// <typeparam name="T">Параметр для хранения IProjectItem</typeparam>
   [Serializable]
   public class Project<T> : IProjectItem
      where T : IProjectItem
   {
      public Project()
      {
      }

      public Project(string name, string description)
      {
         Name = name;
         Description = description;
      }

      public string Name { get; set; }
      public string Description { get; private set; }
      public LinkedList<T> ProjectItems { get; } = new LinkedList<T>();

      public double TimeRequired
         => ProjectItems.Sum(projectItem => projectItem.TimeRequired);

      public void Add(params T[] items)
         => Array.ForEach(
            items.Where(arg => !ProjectItems.Contains(arg)).ToArray(), item => ProjectItems.AddLast(item));

      public Project<T> Add(T item)
      {
         if (!ProjectItems.Contains(item)) ProjectItems.AddLast(item);
         return this;
      }

      public Project<T> Remove(T item)
      {
         ProjectItems.Remove(item);
         return this;
      }
   }
}