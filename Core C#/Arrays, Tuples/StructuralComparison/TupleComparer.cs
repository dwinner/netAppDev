using System.Collections;

namespace StructuralComparison
{
   public class TupleComparer : IEqualityComparer
   {
      public new bool Equals(object x, object y)
      {
         return x.Equals(y);
      }

      public int GetHashCode(object obj)
      {
         return obj.GetHashCode();
      }
   }
}