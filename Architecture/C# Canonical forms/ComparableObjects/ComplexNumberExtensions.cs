using static System.Math;

namespace ComparableObjects
{
   public static class ComplexNumberExtensions
   {
      public static double ComputeMagnitude(this ComplexNumber @this)
         => Sqrt(Pow(@this.Real, 2) + Pow(@this.Imaginary, 2));
   }
}