namespace Decorator
{
   public class DependentProjectItem : ProjectDecorator
   {
      public DependentProjectItem(IProjectItem dependentItem) => DependentItem = dependentItem;

      public IProjectItem DependentItem { get; }

      public override string ToString() => $"DependentItem: {DependentItem}";
   }
}