using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The DepthFirstPaths class represents a data type for finding
///    paths from a source vertex <em>s</em> to every other vertex
///    in an undirected graph.
///    <p>
///       This implementation uses depth-first search.
///       The constructor takes Theta (<em>V</em> + <em>E</em>) time in the
///       worst case, where <em>V</em> is the number of vertices and
///       <em>E</em> is the number of edges.
///       Each instance method takes Theta (1) time.
///       It uses Theta (<em>V</em>) extra space (not including the graph).
///    </p>
/// </summary>
public sealed class Depth1StPath
{
   private readonly int[] _edgeTo; // edgeTo[v] = last edge on s-v path
   private readonly PrimaryGraph _graph;
   private readonly bool[] _marked; // marked[v] = is there an s-v path?
   private readonly int _sourceVertex; // source vertex

   /// <summary>
   ///    Computes a path between <paramref name="sourceVertex" /> and every other vertex in graph <paramref name="graph" />.
   /// </summary>
   /// <param name="graph">The graph</param>
   /// <param name="sourceVertex">The source vertex</param>
   public Depth1StPath(PrimaryGraph graph, int sourceVertex)
   {
      _graph = graph;
      _sourceVertex = sourceVertex;
      _edgeTo = new int[_graph.VerticeCount];
      _marked = new bool[_graph.VerticeCount];
      _marked.ValidateVertex(sourceVertex);
   }

   /// <summary>
   ///    Search via depth first
   /// </summary>
   public void Search() => Search(_graph, _sourceVertex);

   /// <summary>
   ///    Is there a path between the source vertex and vertex <paramref name="dstVertex" />?
   /// </summary>
   /// <param name="dstVertex">The destination vertex</param>
   /// <returns>true if there is a path, false otherwise</returns>
   public bool HasPathTo(int dstVertex)
   {
      _marked.ValidateVertex(dstVertex);
      return _marked[dstVertex];
   }

   /// <summary>
   ///    Returns a path between the source vertex and vertex <paramref name="dstVertex" />, or empty path if no such path.
   /// </summary>
   /// <param name="dstVertex">The destination vertex</param>
   /// <returns>
   ///    The sequence of vertices on a path between the source vertex and vertex <paramref name="dstVertex" />
   /// </returns>
   public IEnumerable<int> GetPathTo(int dstVertex)
   {
      _marked.ValidateVertex(dstVertex);
      if (!HasPathTo(dstVertex))
      {
         return Enumerable.Empty<int>();
      }

      var path = new Stack<int>();
      for (var vtx = dstVertex; vtx != _sourceVertex; vtx = _edgeTo[vtx])
      {
         path.Push(vtx);
      }

      path.Push(_sourceVertex);

      return path;
   }

   // depth first search from srcVertex
   private void Search(PrimaryGraph graph, int srcVertex)
   {
      _marked[srcVertex] = true;
      foreach (var adjVertex in graph.GetAdjacentList(srcVertex))
      {
         if (!_marked[adjVertex])
         {
            _edgeTo[adjVertex] = srcVertex;
            Search(graph, adjVertex);
         }
      }
   }
}