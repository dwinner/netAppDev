using System.Diagnostics;
using Graph.Adt;
using Sorting.Algs.PrioQ;

namespace Graph.Algs;

/// <summary>
///    The <see cref="DijkstraShortestPath" /> class represents a data type for solving the
///    single-source shortest paths problem in edge-weighted digraphs
///    where the edge weights are non-negative.
///    <p>
///       This implementation uses <em>Dijkstra's algorithm</em> with a
///       <em>binary heap</em>. The apply() takes
///       Theta (<em>E</em> log <em>V</em>) time in the worst case,
///       where <em>V</em> is the number of vertices and <em>E</em> is
///       the number of edges. Each instance method takes Theta(1) time.
///       It uses Theta(<em>V</em>) extra space (not including the edge-weighted digraph).
///    </p>
///    <p>
///       This correctly computes shortest paths if all arithmetic performed is
///       without floating-point rounding error or arithmetic overflow.
///       This is the case if all edge weights are integers and if none of the
///       intermediate results exceeds 2<sup>52</sup>. Since all intermediate
///       results are sums of edge weights, they are bounded by <em>V C</em>,
///       where <em>V</em> is the number of vertices and <em>C</em> is the maximum
///       weight of any edge.
///    </p>
/// </summary>
public sealed class DijkstraShortestPath
{
   private readonly double[] _distancesTo; // distTo[v] = distance  of shortest s->v path
   private readonly DirectedEdge?[] _edgesTo; // edgeTo[v] = last edge on shortest s->v path
   private readonly EdgeWeightedDigraph _graph;
   private readonly int _sourceVertex;
   private readonly IndexMinPrioQueue<double> _vertexQueue; // priority queue of vertices

   public DijkstraShortestPath(EdgeWeightedDigraph graph, int sourceVertex)
   {
      _graph = graph;
      _sourceVertex = sourceVertex;
      var negativeEdge = _graph.GetEdges().FirstOrDefault(edge => edge.Weight < 0);
      if (negativeEdge != null)
      {
         throw new InvalidOperationException($"Edge {negativeEdge} has negative weight");
      }

      _distancesTo = new double[_graph.VerticeCount];
      _edgesTo = new DirectedEdge[_graph.VerticeCount];
      _graph.VerticeCount.ValidateVertex(sourceVertex);
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         _distancesTo[vtx] = double.PositiveInfinity;
      }

      _distancesTo[sourceVertex] = default;
      _vertexQueue = new IndexMinPrioQueue<double>(_graph.VerticeCount);
   }

   /// <summary>
   ///    Computes a shortest-paths tree from the source vertex to every other
   /// </summary>
   public void Apply()
   {
      // relax vertices in order of distance from source vertex
      _vertexQueue.Insert(_sourceVertex, _distancesTo[_sourceVertex]);
      while (!_vertexQueue.IsEmpty)
      {
         var minVtx = _vertexQueue.DelMin();
         foreach (var edge in _graph.GetAdjacencyList(minVtx))
         {
            Relax(edge);
         }
      }

      // check optimality conditions
#if DEBUG
      Debug.Assert(Check(_graph, _sourceVertex, out var errorMessage), errorMessage);
#endif
   }

   /// <summary>
   ///    Returns the length of a shortest path from the source vertex to vertex <paramref name="dstVertex" />
   /// </summary>
   /// <param name="dstVertex">The destination vertex</param>
   /// <returns>
   ///    The length of a shortest path from the source vertex to vertex <paramref name="dstVertex" />;
   ///    <see cref="double.PositiveInfinity" /> if no such path
   /// </returns>
   public double GetDistanceTo(int dstVertex)
   {
      _graph.VerticeCount.ValidateVertex(dstVertex);
      return _distancesTo[dstVertex];
   }

   /// <summary>
   ///    Returns true if there is a path from the source vertex to vertex <paramref name="dstVertex" />
   /// </summary>
   /// <param name="dstVertex">The destination vertex</param>
   /// <returns>true if there is a path from the source vertex to vertex <paramref name="dstVertex" />; false otherwise</returns>
   public bool HasPathTo(int dstVertex)
   {
      _graph.VerticeCount.ValidateVertex(dstVertex);
      return _distancesTo[dstVertex] < double.PositiveInfinity;
   }

   /// <summary>
   ///    Returns the shortest path from the source vertex to vertex <paramref name="v" />
   /// </summary>
   /// <param name="v">The destination vertex</param>
   /// <returns>
   ///    The shortest path from the source vertex to vertex <paramref name="v" /> as an iterable of edges, or empty if
   ///    no such path
   /// </returns>
   public IEnumerable<DirectedEdge> GetPathTo(int v)
   {
      _graph.VerticeCount.ValidateVertex(v);
      if (!HasPathTo(v))
      {
         return Enumerable.Empty<DirectedEdge>();
      }

      var path = new Stack<DirectedEdge>();
      for (var currentEdge = _edgesTo[v];
           currentEdge != null;
           currentEdge = _edgesTo[currentEdge.FromVertex])
      {
         path.Push(currentEdge);
      }

      return path;
   }

   private void Relax(DirectedEdge edge)
   {
      // relax edge e and update pq if changed
      var (fromVtx, toVtx, weight) = edge;
      if (_distancesTo[toVtx] > _distancesTo[fromVtx] + weight)
      {
         _distancesTo[toVtx] = _distancesTo[fromVtx] + weight;
         _edgesTo[toVtx] = edge;
         if (_vertexQueue.Contains(toVtx))
         {
            _vertexQueue.DecreaseKey(toVtx, _distancesTo[toVtx]);
         }
         else
         {
            _vertexQueue.Insert(toVtx, _distancesTo[toVtx]);
         }
      }
   }

   // check optimality conditions:
   // (i) for all edges e:            distTo[e.to()] <= distTo[e.from()] + e.weight()
   // (ii) for all edge e on the SPT: distTo[e.to()] == distTo[e.from()] + e.weight()
   private bool Check(EdgeWeightedDigraph graph, int srcVertex, out string errorMessage)
   {
      // check that edge weights are non-negative
      var negativeEdge = graph.GetEdges().FirstOrDefault(edge => edge.Weight < 0);
      if (negativeEdge != null)
      {
         errorMessage = $"Negative edge weight detected: {negativeEdge}";
         return false;
      }

      // check that distTo[v] and edgeTo[v] are consistent
      if (_distancesTo[srcVertex] != 0.0 || _edgesTo[srcVertex] != null)
      {
         errorMessage = "distTo[s] and edgeTo[s] inconsistent";
         return false;
      }

      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         if (vtx == srcVertex)
         {
            continue;
         }

         if (_edgesTo[vtx] == null && !double.IsPositiveInfinity(_distancesTo[vtx]))
         {
            errorMessage = "distTo[] and edgeTo[] inconsistent";
            return false;
         }
      }

      // check that all edges e = v->w satisfy distTo[w] <= distTo[v] + e.weight()
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         foreach (var adjEdge in _graph.GetAdjacencyList(vtx))
         {
            var toVtx = adjEdge.ToVertex;
            if (_distancesTo[vtx] + adjEdge.Weight < _distancesTo[toVtx])
            {
               errorMessage = $"Edge {adjEdge} is not relaxed";
               return false;
            }
         }
      }

      // check that all edges e = v->w on SPT satisfy distTo[w] == distTo[v] + e.weight()
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         var edge = _edgesTo[vtx];
         if (edge == null)
         {
            continue;
         }

         var fromVtx = edge.FromVertex;
         if (vtx != edge.ToVertex)
         {
            errorMessage = $"{vtx} isn't equal to {edge.ToVertex}";
            return false;
         }

         if (Math.Abs(_distancesTo[fromVtx] + edge.Weight - _distancesTo[vtx]) > double.Epsilon)
         {
            errorMessage = $"Edge {edge} on shortest path is not tight";
            return false;
         }
      }

      errorMessage = string.Empty;
      return true;
   }
}