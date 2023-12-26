using System.Collections;

namespace StructuralComparison
{
   public sealed class TupleComparer : IEqualityComparer
   {
      public new bool Equals(object x, object y) => x.Equals(y);

      public int GetHashCode(object obj) => obj.GetHashCode();
   }
}