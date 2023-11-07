using System.Diagnostics;
using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="BoruvkaMinSpanningTree" /> class represents a data type for computing a
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
///       This implementation uses <em>Boruvka's algorithm</em> and the union-find
///       data type.
///       The apply() takes Theta(<em>E</em> log <em>V</em>) time in
///       the worst case, where <em>V</em> is the number of vertices and
///       <em>E</em> is the number of edges.
///       Each instance method takes Theta(1) time.
///       It uses Theta(<em>V</em>) extra space (not including the edge-weighted graph).
///    </p>
///    <p>
///       This weight() method correctly computes the weight of the MST
///       if all arithmetic performed is without floating-point rounding error
///       or arithmetic overflow.
///       This is the case if all edge weights are non-negative integers
///       and the weight of the MST does not exceed 2<sup>52</sup>.
///    </p>
/// </summary>
public sealed class BoruvkaMinSpanningTree
{
   private readonly EdgeWeightedGraph _graph;
   private readonly List<Edge> _minSpanningEdges = new(); // edges in MST

   public BoruvkaMinSpanningTree(EdgeWeightedGraph graph) => _graph = graph;

   /// <summary>
   ///    The edges in a minimum spanning tree (or forest).
   /// </summary>
   public IEnumerable<Edge> Edges => _minSpanningEdges;

   /// <summary>
   ///    The sum of the edge weights in a minimum spanning tree (or forest).
   /// </summary>
   public double TotalWeight { get; private set; }

   /// <summary>
   ///    Compute a minimum spanning tree (or forest) of an edge-weighted graph.
   /// </summary>
   public void Apply()
   {
      var vtxCount = _graph.VerticeCount;
      var uFind = new DisjointSet(vtxCount);

      // repeat at most log V times or until we have V-1 edges
      for (var tStep = 1;
           tStep < vtxCount && _minSpanningEdges.Count < vtxCount - 1;
           tStep += tStep)
      {
         // foreach tree in forest, find closest edge
         // if edge weights are equal, ties are broken in favor of first edge in G.edges()
         var closest = new Edge?[vtxCount];
         foreach (var edge in _graph.GetEdges())
         {
            var (eitherVtx, otherVtx) = edge;
            int eitherIdx = uFind.Find(eitherVtx),
               otherIdx = uFind.Find(otherVtx);
            if (eitherIdx == otherIdx)
            {
               continue; // same tree
            }

            var eitherEdge = closest[eitherIdx];
            if (eitherEdge == null || Less(edge, eitherEdge))
            {
               closest[eitherIdx] = edge;
            }

            var otherEdge = closest[otherIdx];
            if (otherEdge == null || Less(edge, otherEdge))
            {
               closest[otherIdx] = edge;
            }
         }

         // add newly discovered edges to MST
         for (var vtx = 0; vtx < vtxCount; vtx++)
         {
            var edge = closest[vtx];
            if (edge != null)
            {
               var (eitherVtx, otherVtx) = edge;

               // don't add the same edge twice
               if (uFind.Find(eitherVtx) != uFind.Find(otherVtx))
               {
                  _minSpanningEdges.Add(edge);
                  TotalWeight += edge.Weight;
                  uFind.Union(eitherVtx, otherVtx);
               }
            }
         }
      }

      // check optimality conditions
#if DEBUG
      Debug.Assert(Check(out var errorMessage), errorMessage);
#endif
   }

   // is the weight of edge e strictly less than that of edge f?
   private static bool Less(Edge edge1, Edge edge2) => edge1.CompareTo(edge2) < 0;

   // check optimality conditions (takes time proportional to E V lg* V)
   private bool Check(out string errorMessage)
   {
      // check weight
      var totalWeight = Edges.Sum(edge => edge.Weight);
      if (Math.Abs(totalWeight - TotalWeight) > double.Epsilon)
      {
         errorMessage = $"Weight of edges does not equal weight(): {totalWeight:F} vs. {TotalWeight:F}";
         return false;
      }

      // check that it is acyclic
      var uFind = new DisjointSet(_graph.VerticeCount);
      foreach (var (eitherVtx, otherVtx) in Edges)
      {
         if (uFind.Find(eitherVtx) == uFind.Find(otherVtx))
         {
            errorMessage = "Not a forest";
            return false;
         }

         uFind.Union(eitherVtx, otherVtx);
      }

      // check that it is a spanning forest
      foreach (var (eitherVtx, otherVtx) in _graph.GetEdges())
      {
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
         uFind = new DisjointSet(_graph.VerticeCount);
         foreach (var spanEdge in _minSpanningEdges)
         {
            var (eitherVtx, otherVtx) = spanEdge;
            if (spanEdge != edge)
            {
               uFind.Union(eitherVtx, otherVtx);
            }
         }

         // check that e is min weight edge in crossing cut
         foreach (var lEdge in _graph.GetEdges())
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