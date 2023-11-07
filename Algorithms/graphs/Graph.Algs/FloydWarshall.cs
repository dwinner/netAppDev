using System.Diagnostics;
using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="FloydWarshall" /> class represents a data type for solving the
///    all-pairs shortest paths problem in edge-weighted digraphs with
///    no negative cycles.
///    The edge weights can be positive, negative, or zero.
///    This class finds either a shortest path between every pair of vertices
///    or a negative cycle.
///    <p>
///       This implementation uses the Floyd-Warshall algorithm.
///       The apply() takes Theta(<em>V</em><sup>3</sup>) time,
///       where <em>V</em> is the number of vertices.
///       Each instance method takes Theta(1) time.
///       It uses Theta(<em>V</em><sup>2</sup>) extra space
///       (not including the edge-weighted digraph).
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
public sealed class FloydWarshall
{
   private readonly double[][] _distancesTo; // distTo[v][w] = length of shortest v->w path
   private readonly DirectedEdge?[][] _edgesTo; // edgeTo[v][w] = last edge on shortest v->w path
   private readonly AdjMatrixEdgeWeightedDigraph _graph;
   private readonly int _verticeCount;

   public FloydWarshall(AdjMatrixEdgeWeightedDigraph graph)
   {
      _graph = graph;
      _verticeCount = _graph.VerticeCount;

      // initialize distances to infinity
      _distancesTo = new double[_verticeCount][];
      for (var i = 0; i < _verticeCount; i++)
      {
         _distancesTo[i] = new double[_verticeCount];
         for (var j = 0; j < _verticeCount; j++)
         {
            _distancesTo[i][j] = double.PositiveInfinity;
         }
      }

      _edgesTo = new DirectedEdge[_verticeCount][];
      for (var i = 0; i < _verticeCount; i++)
      {
         _edgesTo[i] = new DirectedEdge[_verticeCount];
      }

      // initialize distances using edge-weighted digraph's
      for (var vtx = 0; vtx < _verticeCount; vtx++)
      {
         foreach (var edge in _graph[vtx])
         {
            var (fromVtx, toVtx, weight) = edge;
            _distancesTo[fromVtx][toVtx] = weight;
            _edgesTo[fromVtx][toVtx] = edge;
         }

         // in case of self-loops
         if (_distancesTo[vtx][vtx] >= 0.0)
         {
            _distancesTo[vtx][vtx] = 0.0;
            _edgesTo[vtx][vtx] = null;
         }
      }
   }

   public bool HasNegativeCycle { get; private set; }

   /// <summary>
   ///    Computes a shortest paths tree from each vertex to every other vertex in
   ///    the edge-weighted digraph {@code G}. If no such shortest path exists for
   ///    some pair of vertices, it computes a negative cycle.
   /// </summary>
   public void Apply()
   {
      // Floyd-Warshall updates
      for (var rowVtx = 0; rowVtx < _verticeCount; rowVtx++)
      {
         // compute shortest paths using only 0, 1, ..., i as intermediate vertices
         for (var colVtx = 0; colVtx < _verticeCount; colVtx++)
         {
            if (_edgesTo[colVtx][rowVtx] == null)
            {
               continue;
            }

            for (var vtx = 0; vtx < _verticeCount; vtx++)
            {
               if (_distancesTo[colVtx][vtx] > _distancesTo[colVtx][rowVtx] + _distancesTo[rowVtx][vtx])
               {
                  _distancesTo[colVtx][vtx] = _distancesTo[colVtx][rowVtx] + _distancesTo[rowVtx][vtx];
                  _edgesTo[colVtx][vtx] = _edgesTo[rowVtx][vtx];
               }
            }

            // check for negative cycle
            if (_distancesTo[colVtx][colVtx] < 0.0)
            {
               HasNegativeCycle = true;
               return;
            }
         }
      }

#if DEBUG
      Debug.WriteLineIf(Check(out var errorMessage), errorMessage);
#endif
   }

   /// <summary>
   ///    Gets negative cycle, or empty if there is no such cycle.
   /// </summary>
   /// <returns>Negative cycle, or empty</returns>
   public IEnumerable<DirectedEdge> GetNegativeCycle()
   {
      for (var rowVtx = 0; rowVtx < _distancesTo.Length; rowVtx++)
      {
         // negative cycle in v's predecessor graph
         if (_distancesTo[rowVtx][rowVtx] < 0.0)
         {
            var edgesLen = _edgesTo.Length;
            var spt = new EdgeWeightedDigraph(edgesLen);
            for (var colVtx = 0; colVtx < edgesLen; colVtx++)
            {
               var edge = _edgesTo[rowVtx][colVtx];
               if (edge != null)
               {
                  spt.AddEdge(edge);
               }
            }

            var finder = new EdgeWeightedDirectedCycle(spt);
            finder.Find();
#if DEBUG
            Debug.Assert(finder.HasCycle(), "Has no negative cycle");
#endif
            return finder.GetCycle();
         }
      }

      return Enumerable.Empty<DirectedEdge>();
   }

   public bool HasPath(int sourceVtx, int targetVtx)
   {
      _verticeCount.ValidateVertex(sourceVtx);
      _verticeCount.ValidateVertex(targetVtx);

      return _distancesTo[sourceVtx][targetVtx] < double.PositiveInfinity;
   }

   public double GetDistance(int sourceVtx, int targetVtx)
   {
      _verticeCount.ValidateVertex(sourceVtx);
      _verticeCount.ValidateVertex(targetVtx);
      if (HasNegativeCycle)
      {
#if DEBUG
         Debug.WriteLine("Negative cost cycle exists");
#endif
      }

      return _distancesTo[sourceVtx][targetVtx];
   }

   public IEnumerable<DirectedEdge> GetPath(int sourceVtx, int targetVtx)
   {
      _verticeCount.ValidateVertex(sourceVtx);
      _verticeCount.ValidateVertex(targetVtx);
      if (HasNegativeCycle)
      {
         throw new InvalidOperationException("Negative cost cycle exists");
      }

      if (!HasPath(sourceVtx, targetVtx))
      {
         return Enumerable.Empty<DirectedEdge>();
      }

      var path = new Stack<DirectedEdge>();
      for (var edge = _edgesTo[sourceVtx][targetVtx];
           edge != null;
           edge = _edgesTo[sourceVtx][edge.FromVertex])
      {
         path.Push(edge);
      }

      return path;
   }

   private bool Check(out string errorMessage)
   {
      // no negative cycle
      if (!HasNegativeCycle)
      {
         for (var rowVtx = 0; rowVtx < _verticeCount; rowVtx++)
         {
            foreach (var adjEdge in _graph[rowVtx])
            {
               var toVertex = adjEdge.ToVertex;
               for (var colVtx = 0; colVtx < _verticeCount; colVtx++)
               {
                  var currentDistance = Math.Round(_distancesTo[colVtx][toVertex], 2);
                  var deltaDistance = Math.Round(_distancesTo[colVtx][rowVtx] + adjEdge.Weight, 2);
                  if (currentDistance > deltaDistance)
                  {
                     errorMessage = $"edge {adjEdge} is eligible";
                     return false;
                  }
               }
            }
         }
      }

      errorMessage = string.Empty;
      return true;
   }
}