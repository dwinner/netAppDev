namespace Variance
{
   public interface IDisplay<in T>
   {
      void Show(T shape);
   }

   public interface IIndex<out T>
   {
      T this[int index] { get; }

      int Count { get; }
   }
}