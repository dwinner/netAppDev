namespace DataStructures.UF;

/// <summary>
///    Base things for Union Find alg.
/// </summary>
public abstract class UnionFindBase : IUnionFind
{
   /// <summary>
   ///    Initializes an empty union-find data structure with <tt>N</tt>
   ///    isolated components <tt>0</tt> through <tt>N-1</tt>
   /// </summary>
   /// <param name="nodeCount">The number of sites</param>
   protected UnionFindBase(int nodeCount)
   {
      Count = nodeCount;
      IdArray = new int[nodeCount];
   }

   /// <summary>
   ///    Source array of components to be connected
   /// </summary>
   protected int[] IdArray { get; init; }

   /// <inheritdoc />
   public int Count { get; protected set; }

   /// <inheritdoc />
   public abstract void Union(int component1, int component2);

   /// <inheritdoc />
   public virtual bool Connected(int component1, int component2) =>
      Find(component1) == Find(component2);

   /// <summary>
   ///    Return the component identifier for the component containing site
   ///    <tt>
   ///       <paramref name="componentId" />
   ///    </tt>
   /// </summary>
   /// <param name="componentId">The integer representing one object</param>
   /// <returns>
   ///    The component identifier for the component containing site
   ///    <tt>
   ///       <paramref name="componentId" />
   ///    </tt>
   /// </returns>
   protected abstract int Find(int componentId);
}