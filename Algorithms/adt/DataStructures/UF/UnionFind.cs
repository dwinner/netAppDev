/*
 * Weighted quick-union by rank with path compression by halving. Input:
 * 4 3
 * 3 8
 * 6 5
 * 9 4
 * 2 1
 * 5 0
 * 7 2
 * 6 1
 * 2 components
 */

using System.Diagnostics;

namespace DataStructures.UF;

/// <summary>
///    The <tt>UF</tt> class represents a  The <em>union-find data type</em>
///    (also known as the <em>disjoint-sets data type</em>)
///    It supports the <em>union</em> and <em>find</em> operations,
///    along with a <em>connected</em> methods for determinig whether
///    two sites in the same component and a <em>count</em> method that
///    returns the total number of components.
///    <p>
///       The union-find data type models connectivity among a set of <em>N</em>
///       sites, named 0 through <em>N</em> 1.
///       The <em>is-connected-to</em> relation must be an
///       <em>equivalence relation</em>:
///    </p>
///    <ul>
///       <li> <em>Reflexive</em>: <em>p</em> is connected to <em>p</em>.</li>
///       <li>
///          <em>Symmetric</em>: If <em>p</em> is connected to <em>q</em>,
///          <em>q</em> is connected to <em>p</em>.
///       </li>
///       <li>
///          <em>Transitive</em>: If <em>p</em> is connected to <em>q</em>
///          and <em>q</em> is connected to <em>r</em>, then
///          <em>p</em> is connected to <em>r</em>.
///       </li>
///    </ul>
///    An equivalence relation partitions the sites into
///    <em>equivalence classes</em> (or <em>components</em>). In this case,
///    two sites are in the same component if and only if they are connected.
///    Both sites and components are identified with integers between 0 and
///    <em>N</em> 1.
///    Initially, there are <em>N</em> components, with each site in its
///    own component.  The <em>component identifier</em> of a component
///    (also known as the <em>root</em>, <em>canonical element</em>, <em>leader</em>,
///    or <em>set representative</em>) is one of the sites in the component:
///    two sites have the same component identifier if and only if they are
///    in the same component.
///    <ul>
///       <li>
///          <em>union</em>(<em>p</em>, <em>q</em>) adds a
///          connection between the two sites <em>p</em> and <em>q</em>.
///          If <em>p</em> and <em>q</em> are in different components,
///          then it replaces
///          these two components with a new component that is the union of
///          the two.
///       </li>
///       <li>
///          <em>find</em>(<em>p</em>) returns the component
///          identifier of the component containing <em>p</em>.
///       </li>
///       <li>
///          <em>connected</em>(<em>p</em>, <em>q</em>)
///          returns true if both <em>p</em> and <em>q</em>
///          are in the same component, and false otherwise.
///       </li>
///       <li><em>count</em>() returns the number of components.</li>
///    </ul>
///    The component identifier of a component can change
///    only during a call to <em>union</em> it cannot change during a call
///    to <em>find</em>, <em>connected</em>, or <em>count</em>.
///    <p>
///       This implementation uses weighted quick union by rank with path compression
///       by halving. Initializing a data structure with <em>N</em> sites takes linear time.
///       Afterwards, the <em>union</em>, <em>find</em>, and <em>connected</em>
///       operations take logarithmic time (in the worst case) and the
///       <em>count</em> takes constant time.
///       Moreover, the amortized time per <em>union</em>, <em>find</em>,
///       and <em>connected</em> operation has inverse Ackermann complexity.
///    </p>
/// </summary>
public sealed class UnionFind : UnionFindBase
{
   private readonly byte[] _rankArray; // _rankArray[i] = rank of subtree rooted at i (cannot be more than 31)

   public UnionFind(int nodeCount)
      : base(nodeCount)
   {
      Count = nodeCount;
      IdArray = new int[nodeCount];
      _rankArray = new byte[nodeCount];
      for (var i = 0; i < nodeCount; i++)
      {
         IdArray[i] = i;
         _rankArray[i] = 0;
      }
   }

   /// <inheritdoc />
   public override void Union(int component1, int component2)
   {
      var id1 = Find(component1);
      var id2 = Find(component2);
      if (id1 == id2)
      {
         return;
      }

      if (_rankArray[id1] < _rankArray[id2])
      {
         IdArray[id1] = id2;
      }
      else if (_rankArray[id1] > _rankArray[id2])
      {
         IdArray[id2] = id1;
      }
      else
      {
         IdArray[id2] = id1;
         _rankArray[id1]++;
      }

      Count--;
   }

   /// <inheritdoc />
   protected override int Find(int componentId)
   {
      Debug.Assert(componentId >= 0 && componentId < IdArray.Length);

      var id = IdArray[componentId];
      while (componentId != id)
      {
         IdArray[componentId] = IdArray[id];
         componentId = id;
      }

      return componentId;
   }

   public int FindComp(int componentId) => Find(componentId);
}