namespace Prototype
{
   public interface ICopy<out T>
   {
      T Copy(bool deepCopy = false);
   }
}
