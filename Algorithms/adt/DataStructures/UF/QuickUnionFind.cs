namespace DataStructures.UF;

/// <summary>
///    The <tt>QuickFindUF</tt> class represents a union-find data structure.
///    It supports the <em>union</em> and <em>find</em> operations, along with
///    methods for determinig whether two objects are in the same component
///    and the total number of components.
///    <p>
///       This implementation uses quick find.
///       Initializing a data structure with <em>N</em> objects takes linear time.
///       Afterwards, <em>find</em>, <em>connected</em>, and <em>count</em>
///       takes constant time but <em>union</em> takes linear time.
///    </p>
/// </summary>
public sealed class QuickUnionFind : UnionFindBase
{
   public QuickUnionFind(int nodeCount)
      : base(nodeCount)
   {
      for (var i = 0; i < nodeCount; i++)
      {
         IdArray[i] = i;
      }
   }

   public override void Union(int component1, int component2)
   {
      if (Connected(component1, component2))
      {
         return;
      }

      var idx1 = IdArray[component1];
      for (var i = 0; i < IdArray.Length; i++)
      {
         if (IdArray[i] == idx1)
         {
            IdArray[i] = IdArray[component2];
         }
      }

      Count--;
   }

   protected override int Find(int componentId) => IdArray[componentId];

   public override bool Connected(int component1, int component2) =>
      IdArray[component1] == IdArray[component2];
}