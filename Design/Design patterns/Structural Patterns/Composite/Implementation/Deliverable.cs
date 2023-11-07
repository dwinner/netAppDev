using System;

namespace Composite.Implementation
{
   [Serializable]
   public class Deliverable : IProjectItem
   {
      public string Name { get; set; }

      public string Description { get; set; }

      public IContact Owner { get; set; }

      public Deliverable()
      {
      }

      public Deliverable(string name, string description, IContact owner)
      {
         Name = name;
         Description = description;
         Owner = owner;
      }

      public double TimeRequired
      {
         get { return 0; }
      }
   }
}
