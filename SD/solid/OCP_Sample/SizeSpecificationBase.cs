namespace OCP_Sample;

public class SizeSpecificationBase(Size size) : SpecificationBase<Product>
{
   public override bool IsSatisfied(Product specification) => specification.Size == size;
}