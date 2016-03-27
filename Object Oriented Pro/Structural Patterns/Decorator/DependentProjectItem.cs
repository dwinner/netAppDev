namespace Decorator
{
   public class DependentProjectItem : ProjectDecorator
   {
      public IProjectItem DependentItem { get; set; }

      public DependentProjectItem() { }

      public DependentProjectItem(IProjectItem dependentItem)
      {
         DependentItem = dependentItem;
      }

      public override string ToString()
      {
         return string.Format("DependentItem: {0}", DependentItem);
      }
   }
}
