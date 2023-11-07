using System.Diagnostics;
using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="LazyMinSpanningTree" /> class represents a data type for computing a
///    <em>minimum spanning tree</em> in an edge-weighted graph.
///    The edge weights can be positive, zero, or negative and need not
///    be distinct. If the graph is not connected, it computes a
///    <em>
///       minimum
///       spanning forest
///    </em>
///    , which is the union of minimum spanning trees
///    in each connected component. The <code>weight()</code> method returns the
///    weight of a minimum spanning tree and the <code>edges()</code> method
///    returns its edges.
///    <p>
///       This implementation uses a lazy version of <em>Prim's algorithm</em>
///       with a binary heap of edges.
///       The <code>Apply</code> takes Theta (<em>E</em> log <em>E</em>) time in
///       the worst case, where <em>V</em> is the number of vertices and
///       <em>E</em> is the number of edges.
///       Each instance method takes Theta(1) time.
///       It uses Theta(<em>E</em>) extra space in the worst case
///       (not including the edge-weighted graph).
///    </p>
/// </summary>
public sealed class LazyMinSpanningTree
{
   private const double Epsilon = 1.0E-12;
   private readonly PriorityQueue<Edge, double> _edgePrioQueue; // edges with one endpoint in tree
   private readonly EdgeWeightedGraph _graph;
   private readonly bool[] _marked; // marked[v] = true if v on tree
   private readonly Queue<Edge> _minSpanningQueue; // edges in the MST

   public LazyMinSpanningTree(EdgeWeightedGraph graph)
   {
      _graph = graph;
      _minSpanningQueue = new Queue<Edge>();
      _edgePrioQueue = new PriorityQueue<Edge, double>(Comparer<double>.Default);
      _marked = new bool[_graph.VerticeCount];
      Weight = default;
   }

   /// <summary>
   ///    Total weight
   /// </summary>
   public double Weight { get; private set; }

   /// <summary>
   ///    Minimum spanning edges
   /// </summary>
   public IEnumerable<Edge> Edges => _minSpanningQueue;

   /// <summary>
   ///    Run Prim from all vertices to get a minimum spanning forest
   /// </summary>
   public void Apply()
   {
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         if (!_marked[vtx])
         {
            Apply(_graph, vtx);
         }
      }

#if DEBUG
      // check optimality conditions
      Debug.Assert(Check(_graph, out var errorMessage), errorMessage);
#endif
   }

   // run Prim's algorithm
   private void Apply(EdgeWeightedGraph graph, int vertex)
   {
      Scan(graph, vertex);
      while (!_edgePrioQueue.IsEmpty()) // had better stop when mst has V-1 edges
      {
         var minEdge = _edgePrioQueue.Dequeue(); // the smallest edge on pq
         var (primaryVtx, otherVtx) = minEdge;

#if DEBUG
         Debug.Assert(_marked[primaryVtx] || _marked[otherVtx]);
#endif

         // lazy, both eitherVtx and otherVtx already scanned
         if (_marked[primaryVtx] && _marked[otherVtx])
         {
            continue;
         }

         // add minEdge to MST
         _minSpanningQueue.Enqueue(minEdge);
         Weight += minEdge.Weight;

         if (!_marked[primaryVtx])
         {
            // primaryVtx becomes part of the tree
            Scan(graph, primaryVtx);
         }

         if (!_marked[otherVtx])
         {
            // otherVtx becomes part of the tree
            Scan(graph, otherVtx);
         }
      }
   }

   // add all edges e incident to v onto _edgePrioQueue if the other endpoint has not yet been scanned
   private void Scan(EdgeWeightedGraph graph, int vertex)
   {
#if DEBUG
      Debug.Assert(!_marked[vertex]);
#endif

      _marked[vertex] = true;
      foreach (var edge in graph.GetAdjacencyEdges(vertex))
      {
         var otherVtx = edge.GetOther(vertex);
         if (!_marked[otherVtx])
         {
            _edgePrioQueue.Enqueue(edge, edge.Weight);
         }
      }
   }

   // check optimality conditions (takes time proportional to E V lg* V)
   private bool Check(EdgeWeightedGraph graph, out string errorMessage)
   {
      // check weight
      var totalWeight = Edges.Sum(edge => edge.Weight);
      if (Math.Abs(totalWeight - Weight) > Epsilon)
      {
         errorMessage = $"Total weight of edges does not equal expected weight: {totalWeight:F} vs. {Weight:F}";
         return false;
      }

      // check that it is acyclic
      var uFind = new DisjointSet(graph.VerticeCount);
      foreach (var edge in Edges)
      {
         var (primaryVtx, otherVtx) = edge;
         if (uFind.Find(primaryVtx) == uFind.Find(otherVtx))
         {
            errorMessage = "Not a forest";
            return false;
         }

         uFind.Union(primaryVtx, otherVtx);
      }

      // check that it is a spanning forest
      foreach (var edge in graph.GetEdges())
      {
         var (primaryVtx, otherVtx) = edge;
         if (uFind.Find(primaryVtx) != uFind.Find(otherVtx))
         {
            errorMessage = "Not a spanning forest";
            return false;
         }
      }

      // check that it is a minimal spanning forest (cut optimality conditions)
      foreach (var edge in Edges)
      {
         // all edges in MST except edge
         uFind = new DisjointSet(graph.VerticeCount);
         foreach (var mstEdge in _minSpanningQueue)
         {
            var (primaryVtx, otherVtx) = mstEdge;
            if (mstEdge != edge)
            {
               uFind.Union(primaryVtx, otherVtx);
            }
         }

         // check that e is min weight edge in crossing cut
         foreach (var lEdge in graph.GetEdges())
         {
            var (primaryVtx, otherVtx) = lEdge;
            if (uFind.Find(primaryVtx) != uFind.Find(otherVtx)
                && lEdge.Weight < edge.Weight)
            {
               errorMessage = $"Edge {lEdge} violates cut optimality conditions";
               return false;
            }
         }
      }

      errorMessage = string.Empty;
      return true;
   }
}