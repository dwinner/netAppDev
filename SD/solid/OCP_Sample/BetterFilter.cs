namespace OCP_Sample;

public class BetterFilter : IFilter<Product>
{
   public IEnumerable<Product> Filter(IEnumerable<Product> productItems, SpecificationBase<Product> spec) =>
      productItems.Where(spec.IsSatisfied);
}