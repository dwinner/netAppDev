namespace Decorator
{
   /// <summary>
   /// Завершенная задача
   /// </summary>
   public class DeliverableTask : IProjectItem
   {
      public DeliverableTask()
      {
         TimeRequired = 0;
      }

      public DeliverableTask(string name, string description, IContact owner)
      {
         TimeRequired = 0;
         Name = name;
         Description = description;
         Owner = owner;
      }

      public string Name { get; set; }

      public string Description { get; set; }

      public IContact Owner { get; set; }

      public double TimeRequired { get; set; }
   }
}
