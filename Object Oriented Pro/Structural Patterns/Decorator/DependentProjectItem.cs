namespace Decorator
{
   public class DependentProjectItem : ProjectDecorator
   {
      public IProjectItem DependentItem { get; }

	   public DependentProjectItem(IProjectItem dependentItem)
      {
         DependentItem = dependentItem;
      }

      public override string ToString() => $"DependentItem: {DependentItem}";
   }
}
