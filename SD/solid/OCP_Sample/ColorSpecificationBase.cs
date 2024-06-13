namespace OCP_Sample;

public class ColorSpecificationBase(Color color) : SpecificationBase<Product>
{
   public override bool IsSatisfied(Product specification) => specification.Color == color;
}