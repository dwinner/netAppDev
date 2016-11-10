using System;
using System.Collections.Generic;
using System.Linq;

namespace Composite.Implementation
{
   /// <summary>
   ///    Задача. Составной класс компоновщика
   /// </summary>
   /// <typeparam name="T">Параметр для хранения IProjectItem</typeparam>
   [Serializable]
   public class WorkTask<T> : IProjectItem where T : IProjectItem
   {
      private readonly double _timeRequired;

      public WorkTask()
      {
      }

      public WorkTask(string name, string details, IContact contact, double timeRequired)
      {
         Name = name;
         Details = details;
         Owner = contact;
         _timeRequired = timeRequired;
      }

      public string Name { get; set; }
      public string Details { get; set; }
      public IContact Owner { get; set; }
      public LinkedList<T> ProjectItems { get; } = new LinkedList<T>();

      public double TimeRequired
         => _timeRequired + ProjectItems.Sum(projectItem => projectItem.TimeRequired);

      public WorkTask<T> Add(T item)
      {
         if (!ProjectItems.Contains(item)) ProjectItems.AddLast(item);
         return this;
      }

      public WorkTask<T> Remove(T item)
      {
         ProjectItems.Remove(item);
         return this;
      }
   }
}