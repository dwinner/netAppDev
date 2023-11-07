namespace Graph.Adt;

/// <summary>
///    The <see cref="SymbolDigraph" /> class represents a digraph, where the
///    vertex names are arbitrary strings.
///    By providing mappings between string vertex names and integers,
///    it serves as a wrapper around the <see cref="DiGraph" /> data type, which assumes the vertex names are integers
///    between 0 and <em>V</em> - 1.
///    <p>
///       This implementation uses an <see cref="Dictionary{TKey,TValue}" /> to map from strings to integers,
///       an array to map from integers to strings, and a <see cref="DiGraph" /> to store the underlying graph.
///       The <em>indexOf</em> and <em>contains</em> operations take time
///       proportional to log <em>V</em>, where <em>V</em> is the number of vertices.
///       The <em>nameOf</em> operation takes constant time.
///    </p>
/// </summary>
public sealed class SymbolDigraph
{
   private readonly string[] _keys; // index  -> string
   private readonly Dictionary<string, int> _nameToVertexMap; // string -> index

   public SymbolDigraph(IList<(string fromVertex, string toVertex)> vertices)
   {
      _nameToVertexMap = new Dictionary<string, int>();
      InitVertexMap(vertices);
      _keys = new string[_nameToVertexMap.Count];
      InitEdges(vertices);
   }

   public SymbolDigraph(IEnumerable<(string fromVertex, IEnumerable<string> toVertices)> vertices)
   {
      _nameToVertexMap = new Dictionary<string, int>();
      var flattenEdges = new List<(string fromVertex, string toVertex)>();
      foreach (var (fromVtx, toVertices) in vertices)
      {
         flattenEdges.AddRange(toVertices.Select(toVtx => (fromVtx, toVtx)));
      }

      InitVertexMap(flattenEdges);
      _keys = new string[_nameToVertexMap.Count];
      InitEdges(flattenEdges);
   }

   public DiGraph Graph { get; private set; } = null!;

   public int this[string namedKey]
   {
      get
      {
         if (_nameToVertexMap.TryGetValue(namedKey, out var indexOf))
         {
            return indexOf;
         }

         throw new ArgumentException(nameof(namedKey));
      }
   }

   public string this[int vtx]
   {
      get
      {
         _keys.ValidateVertex(vtx);
         return _keys[vtx];
      }
   }

   public bool Contains(string namedKey) => _nameToVertexMap.ContainsKey(namedKey);

   private void InitEdges(IList<(string fromVertex, string toVertex)> vertices)
   {
      // inverted index to get string keys in an array
      foreach (var namedKey in _nameToVertexMap.Keys)
      {
         var keyIdx = _nameToVertexMap[namedKey];
         _keys[keyIdx] = namedKey;
      }

      // second pass builds the digraph by connecting first vertex on each
      // line to all others
      Graph = new DiGraph(_nameToVertexMap.Count);
      for (var i = 0; i < vertices.Count; i++)
      {
         var nameFrom = vertices[i].fromVertex;
         var nameTo = vertices[i].toVertex;
         var fromVtx = _nameToVertexMap[nameFrom];
         var toVtx = _nameToVertexMap[nameTo];
         Graph.AddEdge(fromVtx, toVtx);
      }
   }

   private void InitVertexMap(IList<(string fromVertex, string toVertex)> vertices)
   {
      // First pass builds the index by reading strings to associate
      // distinct strings with an index
      for (var i = 0; i < vertices.Count; i++)
      {
         _nameToVertexMap.TryAdd(vertices[i].fromVertex, _nameToVertexMap.Count);
         _nameToVertexMap.TryAdd(vertices[i].toVertex, _nameToVertexMap.Count);
      }
   }
}