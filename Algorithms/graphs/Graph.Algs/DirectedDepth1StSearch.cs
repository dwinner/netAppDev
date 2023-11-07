using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="DirectedDepth1StSearch" /> class represents a data type for
///    determining the vertices reachable from a given source vertex
///    (or set of source vertices) in a digraph. For versions that find the paths,
///    see DepthFirstDirectedPaths and BreadthFirstDirectedPaths.
///    <p>
///       This implementation uses depth-first search.
///       The constructor takes time proportional to <em>V</em> + <em>E</em>
///       (in the worst case), where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
///       Each instance method takes Theta (1) time.
///       It uses Theta (<em>V</em>) extra space (not including the digraph).
///    </p>
/// </summary>
public sealed class DirectedDepth1StSearch
{
   private readonly DiGraph _graph;
   private readonly bool[] _marked; // marked[v] = true if v is reachable from source(s)

   public DirectedDepth1StSearch(DiGraph graph)
   {
      _graph = graph;
      _marked = new bool[_graph.VerticeCount];
   }

   public int Count { get; private set; }

   public void Search(int srcVertex)
   {
      _marked.ValidateVertex(srcVertex);
      Dfs(_graph, srcVertex);
   }

   public void Search(IEnumerable<int> srcVertices)
   {
      var vertices = srcVertices as int[] ?? srcVertices.ToArray();
      ValidateVertices(vertices);
      Dfs(_graph, vertices);
   }

   public bool IsMarked(int srcVertex)
   {
      _marked.ValidateVertex(srcVertex);
      return _marked[srcVertex];
   }

   private void Dfs(DiGraph graph, IEnumerable<int> srcVertices)
   {
      foreach (var srcVertex in srcVertices)
      {
         if (!_marked[srcVertex])
         {
            Dfs(graph, srcVertex);
         }
      }
   }

   private void Dfs(DiGraph graph, int srcVertex)
   {
      Count++;
      _marked[srcVertex] = true;
      foreach (var adjVtx in graph.GetAdjacentList(srcVertex))
      {
         if (!_marked[adjVtx])
         {
            Dfs(graph, adjVtx);
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