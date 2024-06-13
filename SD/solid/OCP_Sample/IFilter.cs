namespace OCP_Sample;

public interface IFilter<T>
{
   IEnumerable<T> Filter(IEnumerable<T> productItems, SpecificationBase<T> spec);
}