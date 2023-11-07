using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The DepthFirstSearch class represents a data type for
///    determining the vertices connected to a given source vertex <em>s</em>
///    in an undirected graph. For versions that find the paths, see
///    DepthFirstPaths and BreadthFirstPaths.
///    <p>
///       This implementation uses depth-first search.
///       See NonrecursiveDFS for a non-recursive version.
///       The constructor takes Theta (<em>V</em> + <em>E</em>) time in the worst
///       case, where <em>V</em> is the number of vertices and <em>E</em>
///       is the number of edges.
///       Each instance method takes Theta (1) time.
///       It uses Theta (<em>V</em>) extra space (not including the graph).
///    </p>
/// </summary>
public sealed class Depth1StSearch
{
   private readonly PrimaryGraph _graph;
   private readonly bool[] _marked; // marked[v] = is there an s-v path?
   private readonly int _srcVertex;

   /// <summary>
   ///    Computes the vertices in graph <paramref name="graph" /> that are
   ///    connected to the source vertex <paramref name="srcVertex" />.
   /// </summary>
   /// <param name="graph">The graph</param>
   /// <param name="srcVertex">The source vertex</param>
   public Depth1StSearch(PrimaryGraph graph, int srcVertex)
   {
      _graph = graph;
      _srcVertex = srcVertex;
      _marked = new bool[graph.VerticeCount];
      _marked.ValidateVertex(srcVertex);
   }

   /// <summary>
   ///    Is there a path between the source vertex and vertex <paramref name="dstVertex" />?
   /// </summary>
   /// <param name="dstVertex">The destination vertex</param>
   /// <returns>true if there is a path, false otherwise</returns>
   public bool this[int dstVertex]
   {
      get
      {
         _marked.ValidateVertex(dstVertex);
         return _marked[dstVertex];
      }
   }

   /// <summary>
   ///    The number of vertices connected to the source vertex
   /// </summary>
   public int Count { get; private set; }

   /// <summary>
   ///    Depth first search from srcVertex and graph are set in the ctor
   /// </summary>
   public void Search() => Search(_graph, _srcVertex);

   private void Search(PrimaryGraph graph, int srcVertex)
   {
      Count++;
      _marked[srcVertex] = true;
      foreach (var vtx in graph.GetAdjacentList(srcVertex))
      {
         if (!_marked[vtx])
         {
            Search(graph, vtx);
         }
      }
   }
}