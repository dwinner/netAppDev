using System.Diagnostics;
using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="BellmanFordShortestPath" /> class represents a data type for solving the
///    single-source shortest paths problem in edge-weighted digraphs with
///    no negative cycles.
///    The edge weights can be positive, negative, or zero.
///    This class finds either a shortest path from the source vertex <em>s</em>
///    to every other vertex or a negative cycle reachable from the source vertex.
///    <p>
///       This implementation uses a queue-based implementation of
///       the Bellman-Ford-Moore algorithm.
///       The Apply() takes Theta(<em>E</em> <em>V</em>) time
///       in the worst case, where <em>V</em> is the number of vertices and
///       <em>E</em> is the number of edges. In practice, it performs much better.
///       Each instance method takes Theta(1) time.
///       It uses Theta(<em>V</em>) extra space (not including the
///       edge-weighted digraph).
///    </p>
///    <p>
///       This correctly computes shortest paths if all arithmetic performed is
///       without floating-point rounding error or arithmetic overflow.
///       This is the case if all edge weights are integers and if none of the
///       intermediate results exceeds 2<sup>52</sup>. Since all intermediate
///       results are sums of edge weights, they are bounded by <em>V C</em>,
///       where <em>V</em> is the number of vertices and <em>C</em> is the maximum
///       absolute value of any edge weight.
///    </p>
/// </summary>
public sealed class BellmanFordShortestPath
{
   // for floating-point precision issues
   private const double Epsilon = double.Epsilon;

   private readonly double[] _distancesTo; // distTo[v] = distance  of shortest s->v path
   private readonly DirectedEdge?[] _edgesTo; // edgeTo[v] = last edge on shortest s->v path
   private readonly EdgeWeightedDigraph _graph;
   private readonly bool[] _onQueue; // onQueue[v] = is v currently on the queue?
   private readonly Queue<int> _relaxingQueue; // queue of vertices to relax
   private readonly int _sourceVertex;
   private int _cost; // number of calls to relax()
   private IEnumerable<DirectedEdge>? _cycle; // negative cycle (or null if no such cycle)

   public BellmanFordShortestPath(EdgeWeightedDigraph graph, int sourceVertex)
   {
      _graph = graph;
      _sourceVertex = sourceVertex;
      _distancesTo = new double[_graph.VerticeCount];
      _edgesTo = new DirectedEdge?[_graph.VerticeCount];
      _onQueue = new bool[_graph.VerticeCount];
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         _distancesTo[vtx] = double.PositiveInfinity;
      }

      _distancesTo[_sourceVertex] = default;
      _relaxingQueue = new Queue<int>();
      _relaxingQueue.Enqueue(_sourceVertex);
      _onQueue[_sourceVertex] = true;
   }

   public bool HasNegativeCycle => _cycle != null;

   public IEnumerable<DirectedEdge> NegativeCycle => _cycle ?? Enumerable.Empty<DirectedEdge>();

   /// <summary>
   ///    Computes the shortest paths tree from the source vertex to every other vertex in the edge-weighted digraph
   /// </summary>
   public void Apply()
   {
      // Bellman-Ford algorithm
      while (_relaxingQueue.Count > 0 && !HasNegativeCycle)
      {
         var vtx = _relaxingQueue.Dequeue();
         _onQueue[vtx] = false;
         Relax(vtx);
      }

#if DEBUG
      Debug.Assert(Check(out var errorMsg), errorMsg);
#endif
   }

   public double GetDistanceTo(int dstVertex)
   {
      _graph.VerticeCount.ValidateVertex(dstVertex);
      if (HasNegativeCycle)
      {
         throw new InvalidOperationException("Negative cost cycle exists");
      }

      return _distancesTo[dstVertex];
   }

   public double this[int dstVtx] => GetDistanceTo(dstVtx);

   public bool HasPathTo(int dstVertex)
   {
      _graph.VerticeCount.ValidateVertex(dstVertex);
      return _distancesTo[dstVertex] < double.PositiveInfinity;
   }

   public IEnumerable<DirectedEdge> GetPathTo(int dstVertex)
   {
      _graph.VerticeCount.ValidateVertex(dstVertex);
      if (HasNegativeCycle)
      {
         throw new InvalidOperationException("Negative cost cycle exists");
      }

      if (!HasPathTo(dstVertex))
      {
         return Enumerable.Empty<DirectedEdge>();
      }

      var path = new Stack<DirectedEdge>();
      for (var edge = _edgesTo[dstVertex]; edge != null; edge = _edgesTo[edge.FromVertex])
      {
         path.Push(edge);
      }

      return path;
   }

   // relax vertex v and put other endpoints on queue if changed
   private void Relax(int vertex)
   {
      foreach (var edge in _graph.GetAdjacencyList(vertex))
      {
         var toVertex = edge.ToVertex;
         if (_distancesTo[toVertex] > _distancesTo[vertex] + edge.Weight + Epsilon)
         {
            _distancesTo[toVertex] = _distancesTo[vertex] + edge.Weight;
            _edgesTo[toVertex] = edge;
            if (!_onQueue[toVertex])
            {
               _relaxingQueue.Enqueue(toVertex);
               _onQueue[toVertex] = true;
            }
         }

         if (++_cost % _graph.VerticeCount == 0)
         {
            FindNegativeCycle();

            // found a negative cycle
            if (HasNegativeCycle)
            {
               return;
            }
         }
      }
   }

   // by finding a cycle in predecessor graph
   private void FindNegativeCycle()
   {
      var len = _edgesTo.Length;
      var spt = new EdgeWeightedDigraph(len);
      for (var vtx = 0; vtx < len; vtx++)
      {
         var edge = _edgesTo[vtx];
         if (edge != null)
         {
            spt.AddEdge(edge);
         }
      }

      var finder = new EdgeWeightedDirectedCycle(spt);
      finder.Find();
      var negativeCycle = finder.GetCycle();
      var edges = negativeCycle as DirectedEdge[] ?? negativeCycle.ToArray();
      if (edges.Any())
      {
         _cycle = edges;
      }
   }

   private bool Check(out string errorMessage)
   {
      // has a negative cycle
      if (HasNegativeCycle)
      {
         var weight = NegativeCycle.Sum(edge => edge.Weight);
         if (weight >= 0.0)
         {
            errorMessage = $"error: weight of negative cycle = {weight}";
            return false;
         }
      }
      else
      {
         // no negative cycle reachable from source

         // check that distTo[v] and edgeTo[v] are consistent
         if (_distancesTo[_sourceVertex] != 0.0 || _edgesTo[_sourceVertex] != null)
         {
            errorMessage = "distanceTo[s] and edgeTo[s] inconsistent";
            return false;
         }

         for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
         {
            if (vtx == _sourceVertex)
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
            foreach (var edge in _graph.GetAdjacencyList(vtx))
            {
               var toVertex = edge.ToVertex;
               if (_distancesTo[vtx] + edge.Weight < _distancesTo[toVertex])
               {
                  errorMessage = $"edge {edge} not relaxed";
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

            var fromVertex = edge.FromVertex;
            var toVertex = edge.ToVertex;
            if (vtx != toVertex)
            {
               errorMessage = "w != e.ToVertex";
               return false;
            }

            if (Math.Abs(_distancesTo[fromVertex] + edge.Weight - _distancesTo[vtx]) > Epsilon)
            {
               errorMessage = $"edge {edge} on shortest path not tight";
               return false;
            }
         }
      }

      errorMessage = string.Empty;
      return true;
   }
}