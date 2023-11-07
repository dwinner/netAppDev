namespace DataStructures.UF;

/// <summary>
///    The <tt>QuickUnionUF</tt> class represents a union-find data structure.
///    It supports the <em>union</em> and <em>find</em> operations, along with
///    methods for determinig whether two objects are in the same component
///    and the total number of components.
///    <p>
///       This implementation uses quick union.
///       Initializing a data structure with <em>N</em> objects takes linear time.
///       Afterwards, <em>union</em>, <em>find</em>, and <em>connected</em> take
///       time linear time (in the worst case) and <em>count</em> takes constant time
///    </p>
/// </summary>
public sealed class QuickUnion : UnionFindBase
{
   public QuickUnion(int nodeCount)
      : base(nodeCount)
   {
      for (var i = 0; i < nodeCount; i++)
      {
         IdArray[i] = i;
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

      IdArray[idx1] = idx2;
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