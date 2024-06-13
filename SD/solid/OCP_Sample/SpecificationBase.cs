namespace OCP_Sample;

public abstract class SpecificationBase<T>
{
   public abstract bool IsSatisfied(T specification);

   public static SpecificationBase<T> operator &(
      SpecificationBase<T> first, SpecificationBase<T> second) =>
      new AndSpecificationBase<T>(first, second);
}