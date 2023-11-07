using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="TopologicalSort" /> class represents a data type for
///    determining a topological order of a <em>directed acyclic graph</em> (DAG).
///    A digraph has a topological order if and only if it is a DAG.
///    The <em>hasOrder</em> operation determines whether the digraph has
///    a topological order, and if so, the <em>order</em> operation
///    returns one.
///    <p>
///       This implementation uses depth-first search.
///       The constructor takes Theta (<em>V</em> + <em>E</em>) time in the
///       worst case, where <em>V</em> is the number of vertices and <em>E</em>
///       is the number of edges.
///       Each instance method takes Theta (1) time.
///       It uses Theta (<em>V</em>) extra space (not including the digraph).
///    </p>
///    See DirectedCycle, DirectedCycleX, and
///    EdgeWeightedDirectedCycle for computing a directed cycle
///    if the digraph is not a DAG.
///    See TopologicalX for a nonrecursive queue-based algorithm
///    for computing a topological order of a DAG.
/// </summary>
public sealed class TopologicalSort
{
   private readonly int[] _vertexRank; // rank[v] = rank of vertex v in order
   private IEnumerable<int>? _order;

   public TopologicalSort(int verticeCount)
   {
      _order = null;
      _vertexRank = new int[verticeCount];
   }

   public bool HasOrder => _order != null;

   public IEnumerable<int> TopologicalOrder => _order ?? Enumerable.Empty<int>();

   /// <summary>
   ///    Gets vertex rank
   /// </summary>
   /// <param name="vertex">vertex</param>
   public int this[int vertex]
   {
      get
      {
         _vertexRank.ValidateVertex(vertex);
         return HasOrder ? _vertexRank[vertex] : -1;
      }
   }

   public void Search(DiGraph graph)
   {
      var cycleFinder = new DirectedCycle(graph);
      cycleFinder.Find();
      if (!cycleFinder.HasCycle())
      {
         var dfsOrder = new Depth1StOrder(graph.VerticeCount);
         dfsOrder.Search(graph);
         _order = dfsOrder.GetReversePostVertices();
         var i = 0;
         foreach (var vtx in TopologicalOrder)
         {
            _vertexRank[vtx] = i++;
         }
      }
   }

   public void Search(EdgeWeightedDigraph graph)
   {
      var cycleFinder = new EdgeWeightedDirectedCycle(graph);
      cycleFinder.Find();
      if (!cycleFinder.HasCycle())
      {
         var dfsOrder = new Depth1StOrder(graph.VerticeCount);
         dfsOrder.Search(graph);
         _order = dfsOrder.GetReversePostVertices();
      }
   }
}