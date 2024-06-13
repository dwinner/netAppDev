namespace OCP_Sample;

// combinator
public class AndSpecificationBase<T>(SpecificationBase<T> first, SpecificationBase<T> second)
   : SpecificationBase<T>
{
   public override bool IsSatisfied(T specification) =>
      first.IsSatisfied(specification) && second.IsSatisfied(specification);
}