namespace Decorator
{
   /// <summary>
   /// Базовый класс для декорирования элементов проекта
   /// </summary>
   public abstract class ProjectDecorator : IProjectItem
   {
      public IProjectItem ProjectItem { protected get; set; }

      protected ProjectDecorator() { }

      protected ProjectDecorator(IProjectItem projectItem)
      {
         ProjectItem = projectItem;
      }

      public double TimeRequired
      {
         get { return ProjectItem.TimeRequired; }
         set { ProjectItem.TimeRequired = value; }
      }
   }
}
