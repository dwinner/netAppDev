namespace Decorator
{
   public abstract class ProjectDecorator : IProjectItem
   {
      protected ProjectDecorator()
      {
      }

      protected ProjectDecorator(IProjectItem projectItem) => ProjectItem = projectItem;

      public IProjectItem ProjectItem { protected get; set; }

      public double TimeRequired
      {
         get => ProjectItem.TimeRequired;
         set => ProjectItem.TimeRequired = value;
      }
   }
}