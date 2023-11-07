namespace Graph.Adt;

/// <summary>
///    The <see cref="SymbolGraph" /> class represents an undirected graph, where the
///    vertex names are arbitrary strings.
///    By providing mappings between string vertex names and integers,
///    it serves as a wrapper around the
///    <see cref="PrimaryGraph" /> data type, which assumes the vertex names are integers
///    between 0 and <em>V</em> - 1.
///    <p>
///       This implementation uses an <see cref="Dictionary{TKey,TValue}" /> to map from strings to integers,
///       an array to map from integers to strings, and a <see cref="PrimaryGraph" /> to store
///       the underlying graph.
///       The <em>indexOf</em> and <em>contains</em> operations take time
///       proportional to log <em>V</em>, where <em>V</em> is the number of vertices.
///       The <em>nameOf</em> operation takes constant time.
///    </p>
/// </summary>
public sealed class SymbolGraph
{
   private readonly string[] _keys; // index  -> string
   private readonly Dictionary<string, int> _nameToIndexMap; // string -> index

   public SymbolGraph((string vertex1, string vertex2)[] vertices)
   {
      _nameToIndexMap = new Dictionary<string, int>(EqualityComparer<string>.Default);
      foreach (var (vertex1, vertex2) in vertices)
      {
         _nameToIndexMap.TryAdd(vertex1, _nameToIndexMap.Count);
         _nameToIndexMap.TryAdd(vertex2, _nameToIndexMap.Count);
      }

      _keys = new string[_nameToIndexMap.Count];
      foreach (var key in _nameToIndexMap.Keys)
      {
         _keys[_nameToIndexMap[key]] = key;
      }

      Graph = new PrimaryGraph(_nameToIndexMap.Count);
      foreach (var (vertex1, vertex2) in vertices)
      {
         var startVtx = _nameToIndexMap[vertex1];
         var endVtx = _nameToIndexMap[vertex2];
         Graph.AddEdge(startVtx, endVtx);
      }
   }

   /// <summary>
   ///    Returns the integer associated with the vertex named <paramref name="namedVertex" />.
   /// </summary>
   /// <param name="namedVertex">The name of a vertex</param>
   /// <returns>
   ///    The integer (between 0 and <em>V</em> - 1) associated with the vertex named <paramref name="namedVertex" />
   /// </returns>
   public int this[string namedVertex] => _nameToIndexMap[namedVertex];

   /// <summary>
   ///    Returns the name of the vertex associated with the integer <paramref name="vertex" />.
   /// </summary>
   /// <param name="vertex">The integer corresponding to a vertex (between 0 and <em>V</em> - 1)</param>
   /// <returns>The name of the vertex associated with the integer <paramref name="vertex" /></returns>
   /// <exception cref="ArgumentOutOfRangeException">In the case of invalid index</exception>
   public string this[int vertex]
   {
      get
      {
         var verticeCount = Graph.VerticeCount;
         if (vertex < 0 || vertex >= verticeCount)
         {
            throw new ArgumentOutOfRangeException(nameof(vertex));
         }

         return _keys[vertex];
      }
   }

   /// <summary>
   ///    Returns the graph associated with the symbol graph.
   ///    <remarks>It is the client's responsibility not to mutate the graph.</remarks>
   /// </summary>
   public PrimaryGraph Graph { get; }

   /// <summary>
   ///    Does the graph contain the vertex named <paramref name="namedVertex" />?
   /// </summary>
   /// <param name="namedVertex">The name of a vertex</param>
   /// <returns>true if <paramref name="namedVertex" /> is the name of a vertex, and false otherwise</returns>
   public bool Contains(string namedVertex) => _nameToIndexMap.ContainsKey(namedVertex);
}