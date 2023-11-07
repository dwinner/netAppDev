using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The BreadthFirstPaths class represents a data type for finding
///    shortest paths (number of edges) from a source vertex (or a set of source vertices)
///    to every other vertex in an undirected graph.
///    <p>
///       This implementation uses breadth-first search.
///       The constructor takes Theta (<em>V</em> + <em>E</em>) time in the
///       worst case, where <em>V</em> is the number of vertices and <em>E</em>
///       is the number of edges.
///       Each instance method takes Theta (1) time.
///       It uses Theta (<em>V</em>) extra space (not including the graph).
///    </p>
/// </summary>
public sealed class Breadth1StPath
{
   private const int Infinity = int.MaxValue;
   private readonly int[] _distanceTo; // distTo[v] = number of edges shortest s-v path
   private readonly int[] _edgeTo; // edgeTo[v] = previous edge on shortest s-v path
   private readonly PrimaryGraph _graph;
   private readonly bool[] _marked; // marked[v] = is there an s-v path
   private readonly int _sourceVertex;

   /// <summary>
   ///    Computes the shortest path between the source vertex <paramref name="sourceVertex" /> and every other vertex in the
   ///    graph <paramref name="graph" />.
   /// </summary>
   /// <param name="graph">The graph</param>
   /// <param name="sourceVertex">The source vertex</param>
   public Breadth1StPath(PrimaryGraph graph, int sourceVertex)
   {
      _graph = graph;
      _sourceVertex = sourceVertex;
      _marked = new bool[_graph.VerticeCount];
      _distanceTo = new int[_graph.VerticeCount];
      _edgeTo = new int[_graph.VerticeCount];
   }

   /// <summary>
   ///    Computes the shortest path between any one of the source vertices in sources and every other
   ///    vertex in graph sources.
   /// </summary>
   /// <param name="graph">The graph</param>
   public Breadth1StPath(PrimaryGraph graph)
   {
      _graph = graph;
      _marked = new bool[_graph.VerticeCount];
      _distanceTo = new int[_graph.VerticeCount];
      _edgeTo = new int[_graph.VerticeCount];
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         _distanceTo[vtx] = Infinity;
      }
   }

   /// <summary>
   ///    Is there a path between the source vertex (or sources) and vertex <paramref name="vertex" />?
   /// </summary>
   /// <param name="vertex">The vertex</param>
   /// <returns>true if there is a path, and false otherwise</returns>
   public bool HasPathTo(int vertex)
   {
      _marked.ValidateVertex(vertex);
      return _marked[vertex];
   }

   /// <summary>
   ///    Returns the number of edges in a shortest path between the source vertex (or sources) and vertex
   ///    <paramref name="vertex" />?
   /// </summary>
   /// <param name="vertex">The vertex</param>
   /// <returns>The number of edges in such a shortest path</returns>
   public int GetDistanceTo(int vertex)
   {
      _marked.ValidateVertex(vertex);
      return _distanceTo[vertex];
   }

   /// <summary>
   ///    Returns a shortest path between the source vertex (or sources) and <paramref name="vertex" />,
   ///    or null if no such path.
   /// </summary>
   /// <param name="vertex">The vertex</param>
   /// <returns>The sequence of vertices on the shortest path, as an <see cref="IEnumerable{T}" /></returns>
   public IEnumerable<int> GetPathTo(int vertex)
   {
      _marked.ValidateVertex(vertex);
      if (!HasPathTo(vertex))
      {
         return Enumerable.Empty<int>();
      }

      var path = new Stack<int>();
      int vtx;
      for (vtx = vertex; _distanceTo[vtx] != 0; vtx = _edgeTo[vtx])
      {
         path.Push(vtx);
      }

      path.Push(vtx);

      return path;
   }

   /// <summary>
   ///    Check optimality conditions for single source
   /// </summary>
   /// <param name="graph">Graph</param>
   /// <param name="srcVertex">Source vertex</param>
   /// <returns>Success/Failure status</returns>
   public (bool success, string errMsg)
      Check(PrimaryGraph graph, int srcVertex)
   {
      // check that the distance of s = 0
      if (_distanceTo[srcVertex] != 0)
      {
         return (false, $"distance of source {srcVertex} to itself = {_distanceTo[srcVertex]}");
      }

      // check that for each edge v-w dist[w] <= dist[v] + 1
      // provided v is reachable from s
      for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
      {
         foreach (var adjVtx in graph.GetAdjacentList(vtx))
         {
            if (HasPathTo(vtx) != HasPathTo(adjVtx))
            {
               var errorMsg =
                  $"edge {vtx}-{adjVtx}{Environment.NewLine}hasPathTo({vtx}) = {HasPathTo(vtx)}{Environment.NewLine}hasPathTo({adjVtx}) = {HasPathTo(adjVtx)}";
               return (false, errorMsg);
            }

            if (HasPathTo(vtx) && _distanceTo[adjVtx] > _distanceTo[vtx] + 1)
            {
               var errorMsg =
                  $"edge {vtx}-{adjVtx}{Environment.NewLine}distTo[{vtx}] = {_distanceTo[vtx]}{Environment.NewLine}distTo[{adjVtx}] = {_distanceTo[adjVtx]}";
               return (false, errorMsg);
            }
         }
      }

      // check that v = edgeTo[w] satisfies distTo[w] = distTo[v] + 1
      // provided v is reachable from s
      for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
      {
         if (!HasPathTo(vtx) || vtx == srcVertex)
         {
            continue;
         }

         var eToVtx = _edgeTo[vtx];
         if (_distanceTo[vtx] != _distanceTo[eToVtx] + 1)
         {
            var errMsg =
               $"shortest path edge {eToVtx}-{vtx}{Environment.NewLine}distTo[{eToVtx}] = {_distanceTo[eToVtx]}{Environment.NewLine}distTo[{vtx}] = {_distanceTo[vtx]}";
            return (false, errMsg);
         }
      }

      return (true, string.Empty);
   }

   public void Search()
   {
      _marked.ValidateVertex(_sourceVertex);
      Search(_graph, _sourceVertex);
   }

   public void Search(IEnumerable<int> srcVertices)
   {
      var sources = srcVertices as int[] ?? srcVertices.ToArray();
      ValidateVertices(sources);
      Search(_graph, sources);
   }

   // breadth-first search from a single source
   private void Search(PrimaryGraph graph, int srcVertex)
   {
      var queue = new Queue<int>();
      for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
      {
         _distanceTo[vtx] = Infinity;
      }

      _distanceTo[srcVertex] = 0;
      _marked[srcVertex] = true;
      queue.Enqueue(srcVertex);
      while (queue.Count != 0)
      {
         var vertex = queue.Dequeue();
         foreach (var adjVertex in graph.GetAdjacentList(vertex))
         {
            if (!_marked[adjVertex])
            {
               _edgeTo[adjVertex] = vertex;
               _distanceTo[adjVertex] = _distanceTo[vertex] + 1;
               _marked[adjVertex] = true;
               queue.Enqueue(adjVertex);
            }
         }
      }
   }

   // breadth-first search from multiple sources
   private void Search(PrimaryGraph graph, IEnumerable<int> srcVertices)
   {
      var queue = new Queue<int>();
      foreach (var vertex in srcVertices)
      {
         _marked[vertex] = true;
         _distanceTo[vertex] = 0;
         queue.Enqueue(vertex);
      }

      while (queue.Count != 0)
      {
         var srcVtx = queue.Dequeue();
         foreach (var adjVtx in graph.GetAdjacentList(srcVtx))
         {
            if (!_marked[adjVtx])
            {
               _edgeTo[adjVtx] = srcVtx;
               _distanceTo[adjVtx] = _distanceTo[srcVtx] + 1;
               _marked[adjVtx] = true;
               queue.Enqueue(adjVtx);
            }
         }
      }
   }

   private void ValidateVertices(IEnumerable<int> srcVertices)
   {
      if (srcVertices == null)
      {
         throw new ArgumentNullException(nameof(srcVertices));
      }

      var vertexCount = 0;
      foreach (var vertex in srcVertices)
      {
         vertexCount++;
         _marked.ValidateVertex(vertex);
      }

      if (vertexCount == 0)
      {
         throw new ArgumentException("Zero vertices");
      }
   }
}