using System.Diagnostics;
using Graph.Adt;
using Sorting.Algs.PrioQ;

namespace Graph.Algs;

/// <summary>
///    The <see cref="MinSpanningTree" /> class represents a data type for computing a
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
///       This implementation uses <em>Prim's algorithm</em> with an indexed
///       binary heap. The constructor takes Theta (<em>E</em> log <em>V</em>) time in
///       the worst case, where <em>V</em> is the number of vertices and <em>E</em> is the number of edges.
///       Each instance method takes Theta (1) time.
///       It uses Theta (<em>V</em>) extra space (not including the edge-weighted graph).
///    </p>
///    <p>
///       This <code>weight()</code> method correctly computes the weight of the MST
///       if all arithmetic performed is without floating-point rounding error
///       or arithmetic overflow.
///       This is the case if all edge weights are non-negative integers
///       and the weight of the MST does not exceed 2<sup>52</sup>.
///    </p>
/// </summary>
public sealed class MinSpanningTree
{
   private const double Epsilon = 1.0E-12;
   private readonly double[] _distancesTo; // distTo[v] = weight of shortest such edge
   private readonly Edge?[] _edgesTo; // edgeTo[v] = shortest edge from tree vertex to non-tree vertex
   private readonly EdgeWeightedGraph _graph;
   private readonly bool[] _marked; // marked[v] = true if v on tree, false otherwise
   private readonly IndexMinPrioQueue<double> _vertexPriorityQueue;

   public MinSpanningTree(EdgeWeightedGraph graph)
   {
      _graph = graph;
      _edgesTo = new Edge[_graph.VerticeCount];
      _distancesTo = new double[_graph.VerticeCount];
      _marked = new bool[_graph.VerticeCount];
      _vertexPriorityQueue = new IndexMinPrioQueue<double>(_graph.VerticeCount);
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         _distancesTo[vtx] = double.PositiveInfinity;
      }
   }

   /// <summary>
   ///    The sum of the edge weights in a minimum spanning tree (or forest).
   /// </summary>
   public double TotalWeight => Edges.Sum(edge => edge.Weight);

   /// <summary>
   ///    Returns the edges in a minimum spanning tree (or forest).
   /// </summary>
   public IEnumerable<Edge> Edges => _edgesTo.Where(edge => edge != null).Reverse()!;

   /// <summary>
   ///    Compute a minimum spanning tree (or forest) of an edge-weighted graph.
   ///    <remarks>Run from each vertex to find minimum spanning forest</remarks>
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

   // run Prim's algorithm in graph, starting from vertex
   private void Apply(EdgeWeightedGraph graph, int vertex)
   {
      _distancesTo[vertex] = default;
      _vertexPriorityQueue.Insert(vertex, _distancesTo[vertex]);
      while (!_vertexPriorityQueue.IsEmpty)
      {
         var minVtx = _vertexPriorityQueue.DelMin();
         Scan(graph, minVtx);
      }
   }

   private void Scan(EdgeWeightedGraph graph, int vertex)
   {
      _marked[vertex] = true;
      foreach (var adjEdge in graph.GetAdjacencyEdges(vertex))
      {
         var otherVtx = adjEdge.GetOther(vertex);
         if (_marked[otherVtx])
         {
            // v-w is obsolete edge
            continue;
         }

         if (adjEdge.Weight < _distancesTo[otherVtx])
         {
            _distancesTo[otherVtx] = adjEdge.Weight;
            _edgesTo[otherVtx] = adjEdge;
            if (_vertexPriorityQueue.Contains(otherVtx))
            {
               _vertexPriorityQueue.DecreaseKey(otherVtx, _distancesTo[otherVtx]);
            }
            else
            {
               _vertexPriorityQueue.Insert(otherVtx, _distancesTo[otherVtx]);
            }
         }
      }
   }

   // check optimality conditions (takes time proportional to E V lg* V)
   private bool Check(EdgeWeightedGraph graph, out string errorMessage)
   {
      // check weight
      var totalWeight = Edges.Sum(edge => edge.Weight);
      if (Math.Abs(totalWeight - TotalWeight) > Epsilon)
      {
         errorMessage = $"Weight of edges does not equal weight(): {totalWeight:F} vs. {TotalWeight:F}";
         return false;
      }

      // check that it is acyclic
      var uFind = new DisjointSet(graph.VerticeCount);
      foreach (var edge in Edges)
      {
         var (eitherVtx, otherVtx) = edge;
         if (uFind.Find(eitherVtx) == uFind.Find(otherVtx))
         {
            errorMessage = "Not a forest";
            return false;
         }

         uFind.Union(eitherVtx, otherVtx);
      }

      // check that it is a spanning forest
      foreach (var edge in graph.GetEdges())
      {
         var (eitherVtx, otherVtx) = edge;
         if (uFind.Find(eitherVtx) != uFind.Find(otherVtx))
         {
            errorMessage = "Not a spanning forest";
            return false;
         }
      }

      // check that it is a minimal spanning forest (cut optimality conditions)
      foreach (var edge in Edges)
      {
         // all edges in MST except e
         uFind = new DisjointSet(graph.VerticeCount);
         foreach (var lEdge in Edges)
         {
            var (eitherVtx, otherVtx) = lEdge;
            if (lEdge != edge)
            {
               uFind.Union(eitherVtx, otherVtx);
            }
         }

         // check that e is min weight edge in crossing cut
         foreach (var lEdge in graph.GetEdges())
         {
            var (eitherVtx, otherVtx) = lEdge;
            if (uFind.Find(eitherVtx) != uFind.Find(otherVtx) && lEdge.Weight < edge.Weight)
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