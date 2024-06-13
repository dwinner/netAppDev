namespace OCP_Sample;

public static class CriteriaExtensions
{
   public static AndSpecificationBase<Product> And(this Color self, Size size) =>
      new(
         new ColorSpecificationBase(self),
         new SizeSpecificationBase(size));
}