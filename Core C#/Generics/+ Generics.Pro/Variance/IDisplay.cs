namespace Variance
{
   public interface IDisplay<in T>
   {
      void Show(T shape);
   }
}