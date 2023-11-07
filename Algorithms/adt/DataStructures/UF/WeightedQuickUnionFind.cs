namespace DataStructures.UF;

/// <summary>
///    The <tt>WeightedQuickUnionUF</tt> class represents a union-find data structure.
///    It supports the <em>union</em> and <em>find</em> operations, along with
///    methods for determinig whether two objects are in the same component
///    and the total number of components.
///    <p>
///       This implementation uses weighted quick union by size (without path compression).
///       Initializing a data structure with <em>N</em> objects takes linear time.
///       Afterwards, <em>union</em>, <em>find</em>, and <em>connected</em> take
///       logarithmic time (in the worst case) and <em>count</em> takes constant time.
///    </p>
/// </summary>
public sealed class WeightedQuickUnionFind : UnionFindBase
{
   private readonly int[] _weights; // _weightArray[i] = number of objects in subtree rooted at i

   public WeightedQuickUnionFind(int nodeCount)
      : base(nodeCount)
   {
      _weights = new int[nodeCount];
      for (var i = 0; i < nodeCount; i++)
      {
         IdArray[i] = i;
         _weights[i] = 1;
      }
   }

   public override void Union(int component1, int component2)
   {
      var idx1 = Find(component1);
      var idx2 = Find(component2);
      if (idx1 == idx2)
      {
         return;
      }

      // make smaller root point to larger one
      if (_weights[idx1] < _weights[idx2])
      {
         IdArray[idx1] = idx2;
         _weights[idx2] += _weights[idx1];
      }
      else
      {
         IdArray[idx2] = idx1;
         _weights[idx1] += _weights[idx2];
      }
   }

   protected override int Find(int componentId)
   {
      while (componentId != IdArray[componentId])
      {
         componentId = IdArray[componentId];
      }

      return componentId;
   }
}