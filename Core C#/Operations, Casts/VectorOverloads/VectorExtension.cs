namespace VectorOverloads
{
   public static class VectorExtension
   {
      public static double ScalarMultiply(Vector first, Vector second)
      {
         return first.X * second.X + first.Y * second.Y + first.Z * second.Z;
      }
   }
}