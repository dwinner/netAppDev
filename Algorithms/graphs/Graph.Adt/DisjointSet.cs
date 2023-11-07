namespace Graph.Adt;

/// <summary>
///    The <see cref="DisjointSet" /> class represents a <em>union–find data type</em>
///    (also known as the <em>disjoint-sets data type</em>).
///    It supports the classic <em>union</em> and <em>find</em> operations,
///    along with a <em>count</em> operation that returns the total number
///    of sets.
///    <p>
///       The union–find data type models a collection of sets containing
///       <em>n</em> elements, with each element in exactly one set.
///       The elements are named 0 through <em>n</em>–1.
///       Initially, there are <em>n</em> sets, with each element in its
///       own set. The <em>canonical element</em> of a set
///       (also known as the <em>root</em>, <em>identifier</em>,
///       <em>leader</em>, or <em>set representative</em>)
///       is one distinguished element in the set. Here is a summary of
///       the operations:
///       <ul>
///          <li>
///             <em>find</em>(<em>p</em>) returns the canonical element
///             of the set containing <em>p</em>. The <em>find</em> operation
///             returns the same value for two elements if and only if
///             they are in the same set.
///          </li>
///          <li>
///             <em>union</em>(<em>p</em>, <em>q</em>) merges the set
///             containing element <em>p</em> with the set containing
///             element <em>q</em>. That is, if <em>p</em> and <em>q</em>
///             are in different sets, replace these two sets
///             with a new set that is the union of the two.
///          </li>
///          <li><em>count</em>() returns the number of sets.</li>
///       </ul>
///    </p>
///    <p>
///       The canonical element of a set can change only when the set
///       itself changes during a call to <em>union</em> cannot
///       change during a call to either <em>find</em> or <em>count</em>.
///    </p>
///    <p>
///       This implementation uses <em>weighted quick union by rank</em>
///       with <em>path compression by halving</em>.
///       The constructor takes Theta(<em>n</em>) time, where
///       <em>n</em> is the number of elements.
///       The <em>union</em> and <em>find</em> operations take
///       Theta(log <em>n</em>) time in the worst case.
///       The <em>count</em> operation takes Theta(1) time.
///       Moreover, starting from an empty data structure with <em>n</em> sites,
///       any intermixed sequence of <em>m</em> <em>union</em> and <em>find</em>
///       operations takes <em>O</em>(<em>m</em> alpha(<em>n</em>)) time,
///       where alpha(<em>n</em>) is the inverse of
///       <a href="https://en.wikipedia.org/wiki/Ackermann_function#Inverse">Ackermann's function</a>.
///    </p>
/// </summary>
public sealed class DisjointSet
{
   private readonly int[] _parent; // parent[i] = parent of i
   private readonly byte[] _rank; // rank[i] = rank of subtree rooted at i (never more than 31)

   /// <summary>
   ///    Initializes an empty union-find data structure with <paramref name="elementCount" /> elements through
   ///    <paramref name="elementCount" /> - 1
   /// </summary>
   /// <param name="elementCount">the number of elements</param>
   /// <exception cref="ArgumentException">If <paramref name="elementCount" /> is negative</exception>
   public DisjointSet(int elementCount)
   {
      if (elementCount < 0)
      {
         throw new ArgumentException($"{elementCount} cannot be negative", nameof(elementCount));
      }

      Count = elementCount;
      _parent = new int[elementCount];
      _rank = new byte[elementCount];
      for (var i = 0; i < elementCount; i++)
      {
         _parent[i] = i;
         _rank[i] = 0;
      }
   }

   /// <summary>
   ///    Returns the number of sets.
   /// </summary>
   public int Count { get; private set; }

   /// <summary>
   ///    Returns the canonical element of the set containing element <paramref name="element" />
   /// </summary>
   /// <param name="element">An element</param>
   /// <returns>The canonical element of the set containing <paramref name="element" /></returns>
   public int Find(int element)
   {
      Validate(element);
      while (element != _parent[element])
      {
         _parent[element] = _parent[_parent[element]]; // path compression by halving
         element = _parent[element];
      }

      return element;
   }

   /// <summary>
   ///    Merges the set containing element <paramref name="element1" /> with the set containing element
   ///    <paramref name="element2" />
   /// </summary>
   /// <param name="element1">One element</param>
   /// <param name="element2">The other element</param>
   public void Union(int element1, int element2)
   {
      var rootP = Find(element1);
      var rootQ = Find(element2);
      if (rootP == rootQ)
      {
         return;
      }

      // make root of smaller rank point to root of larger rank
      if (_rank[rootP] < _rank[rootQ])
      {
         _parent[rootP] = rootQ;
      }
      else if (_rank[rootP] > _rank[rootQ])
      {
         _parent[rootQ] = rootP;
      }
      else
      {
         _parent[rootQ] = rootP;
         _rank[rootP]++;
      }

      Count--;
   }

   // validate that p is a valid index
   private void Validate(int index)
   {
      var len = _parent.Length;
      if (index < 0 || index >= len)
      {
         throw new ArgumentOutOfRangeException($"index {index} is not between 0 and {len - 1}");
      }
   }
}