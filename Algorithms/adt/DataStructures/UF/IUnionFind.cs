namespace DataStructures.UF;

/// <summary>
///    Common interface for Union/Find alg.
/// </summary>
public interface IUnionFind
{
   /// <summary>
   ///    Return the number of components
   /// </summary>
   /// <value>The number of components (between <tt>1</tt> and <tt>N</tt>)</value>
   int Count { get; }

   /// <summary>
   ///    Are the two sites <tt>p</tt> and <tt>q</tt> in the same component?
   /// </summary>
   /// <param name="component1">The integer representing one site</param>
   /// <param name="component2">The integer representing the other site</param>
   /// <returns>
   ///    true if the two sites
   ///    <tt>
   ///       <paramref name="component1" />
   ///    </tt>
   ///    and
   ///    <tt>
   ///       <paramref name="component2" />
   ///    </tt>
   ///    are in the same component; false otherwise
   /// </returns>
   bool Connected(int component1, int component2);

   /// <summary>
   ///    Merge the component containing site
   ///    <tt>
   ///       <paramref name="component1" />
   ///    </tt>
   ///    with the component containing site
   ///    <tt>
   ///       <paramref name="component2" />
   ///    </tt>
   /// </summary>
   /// <param name="component1">The integer representing one site</param>
   /// <param name="component2">The integer representing the other site</param>
   void Union(int component1, int component2);
}