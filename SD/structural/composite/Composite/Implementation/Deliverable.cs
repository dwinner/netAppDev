using System;

namespace Composite.Implementation
{
   [Serializable]
   public class Deliverable : IProjectItem
   {
      public Deliverable()
      {
      }

      public Deliverable(string name, string description, IContact owner)
      {
         Name = name;
         Description = description;
         Owner = owner;
      }

      public string Name { get; set; }

      public string Description { get; set; }

      public IContact Owner { get; set; }

      public double TimeRequired => 0;
   }
}