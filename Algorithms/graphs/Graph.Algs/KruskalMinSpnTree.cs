using System.Diagnostics;
using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="KruskalMinSpnTree" /> class represents a data type for computing a
///    <em>minimum spanning tree</em> in an edge-weighted graph.
///    The edge weights can be positive, zero, or negative and need not
///    be distinct. If the graph is not connected, it computes a
///    <em>
///       minimum
///       spanning forest
///    </em>
///    , which is the union of minimum spanning trees
///    in each connected component. The weight() method returns the
///    weight of a minimum spanning tree and the edges() method
///    returns its edges.
///    <p>
///       This implementation uses <em>Kruskal's algorithm</em> and the
///       union-find data type.
///       The constructor takes Theta (<em>E</em> log <em>E</em>) time in
///       the worst case.
///       Each instance method takes Theta(1) time.
///       It uses Theta(<em>E</em>) extra space (not including the graph).
///    </p>
///    <p>
///       This weight() method correctly computes the weight of the MST
///       if all arithmetic performed is without floating-point rounding error
///       or arithmetic overflow.
///       This is the case if all edge weights are non-negative integers
///       and the weight of the MST does not exceed 2<sup>52</sup>.
///    </p>
/// </summary>
public sealed class KruskalMinSpnTree
{
   private const double Epsilon = 1.0E-12;
   private readonly EdgeWeightedGraph _graph;
   private readonly Queue<Edge> _minSpanningQueue = new(); // edges in MST

   public KruskalMinSpnTree(EdgeWeightedGraph graph) => _graph = graph;

   /// <summary>
   ///    Returns the edges in a minimum spanning tree (or forest).
   /// </summary>
   public IEnumerable<Edge> Edges => _minSpanningQueue;

   /// <summary>
   ///    Returns the sum of the edge weights in a minimum spanning tree (or forest).
   /// </summary>
   public double Weight { get; private set; }

   /// <summary>
   ///    Compute a minimum spanning tree (or forest) of an edge-weighted graph.
   /// </summary>
   public void Apply()
   {
      // create array of edges, sorted by weight
      var edges = _graph.GetEdges().ToArray();
      Array.Sort(edges);

      // run greedy algorithm
      var uFind = new DisjointSet(_graph.VerticeCount);
      for (var i = 0; i < _graph.EdgeCount && _minSpanningQueue.Count < _graph.VerticeCount - 1; i++)
      {
         var edge = edges[i];
         var (primary, other) = edge;

         // v-w does not create a cycle
         if (uFind.Find(primary) != uFind.Find(other))
         {
            uFind.Union(primary, other); // merge v and w components
            _minSpanningQueue.Enqueue(edge); // add edge e to mst
            Weight += edge.Weight;
         }
      }

      // check optimality conditions
#if DEBUG
      Debug.Assert(Check(_graph, out var errorMessage), errorMessage);
#endif
   }

   // check optimality conditions (takes time proportional to E V lg* V)
   private bool Check(EdgeWeightedGraph graph, out string errorMessage)
   {
      // check total weight
      var total = Edges.Sum(e => e.Weight);
      if (Math.Abs(total - Weight) > Epsilon)
      {
         errorMessage = $"Weight of edges does not equal weight(): {total:F} vs. {Weight:F}";
         return false;
      }

      // check that it is acyclic
      var uFind = new DisjointSet(graph.VerticeCount);
      foreach (var edge in Edges)
      {
         var (primary, other) = edge;
         if (uFind.Find(primary) == uFind.Find(other))
         {
            errorMessage = "Not a forest";
            return false;
         }

         uFind.Union(primary, other);
      }

      // check that it is a spanning forest
      foreach (var edge in graph.GetEdges())
      {
         var (primary, other) = edge;
         if (uFind.Find(primary) != uFind.Find(other))
         {
            errorMessage = "Not a spanning forest";
            return false;
         }
      }

      // check that it is a minimal spanning forest (cut optimality conditions)
      foreach (var topEdge in Edges)
      {
         // all edges in MST except e
         uFind = new DisjointSet(graph.VerticeCount);
         foreach (var edge in _minSpanningQueue)
         {
            var (primary, other) = edge;
            if (edge != topEdge)
            {
               uFind.Union(primary, other);
            }
         }

         // check that e is min weight edge in crossing cut
         foreach (var edge in graph.GetEdges())
         {
            var (primary, other) = edge;
            if (uFind.Find(primary) != uFind.Find(other) && edge.Weight < topEdge.Weight)
            {
               errorMessage = "Edge " + edge + " violates cut optimality conditions";
               return false;
            }
         }
      }

      errorMessage = string.Empty;
      return true;
   }
}