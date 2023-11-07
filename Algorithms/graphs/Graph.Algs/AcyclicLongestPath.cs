using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="AcyclicLongestPath" /> class represents a data type for solving the
///    single-source longest paths problem in edge-weighted directed
///    acyclic graphs (DAGs). The edge weights can be positive, negative, or zero.
///    <p>
///       This implementation uses a topological-sort based algorithm.
///       The Apply() takes Theta (<em>V</em> + <em>E</em>) time in the
///       worst case, where <em>V</em> is the number of vertices and
///       <em>E</em> is the number of edges.
///       Each instance method takes Theta(1) time.
///       It uses Theta(<em>V</em>) extra space (not including the
///       edge-weighted digraph).
///    </p>
///    <p>
///       This correctly computes longest paths if all arithmetic performed is
///       without floating-point rounding error or arithmetic overflow.
///       This is the case if all edge weights are integers and if none of the
///       intermediate results exceeds 2<sup>52</sup>. Since all intermediate
///       results are sums of edge weights, they are bounded by <em>V C</em>,
///       where <em>V</em> is the number of vertices and <em>C</em> is the maximum
///       absolute value of any edge weight.
///    </p>
/// </summary>
public sealed class AcyclicLongestPath
{
   private readonly double[] _distancesTo; // distTo[v] = distance  of longest s->v path
   private readonly DirectedEdge?[] _edgesTo; // edgeTo[v] = last edge on longest s->v path
   private readonly EdgeWeightedDigraph _graph;
   private readonly int _sourceVertex;

   public AcyclicLongestPath(EdgeWeightedDigraph graph, int sourceVertex)
   {
      _graph = graph;
      _sourceVertex = sourceVertex;
      _distancesTo = new double[_graph.VerticeCount];
      _edgesTo = new DirectedEdge[_graph.VerticeCount];
   }

   /// <summary>
   ///    Computes the longest paths tree from the source vertex to every other vertex in the directed acyclic graph.
   /// </summary>
   /// <exception cref="InvalidOperationException">If the digraph is not acyclic</exception>
   public void Apply()
   {
      _graph.VerticeCount.ValidateVertex(_sourceVertex);
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         _distancesTo[vtx] = double.NegativeInfinity;
      }

      _distancesTo[_sourceVertex] = default;

      // relax vertices in topological order
      var topological = new TopologicalSort(_graph.VerticeCount);
      topological.Search(_graph);
      if (!topological.HasOrder)
      {
         throw new InvalidOperationException("Digraph is not acyclic.");
      }

      foreach (var edge in topological.TopologicalOrder.SelectMany(vtx => _graph.GetAdjacencyList(vtx)))
      {
         Relax(edge);
      }
   }

   /// <summary>
   ///    Returns the length of the longest path from the source vertex to vertex <paramref name="dstVertex" />.
   /// </summary>
   /// <param name="dstVertex">The destination vertex</param>
   /// <returns>
   ///    The length of the longest path from the source vertex to vertex <paramref name="dstVertex" />;
   ///    <see cref="double.NegativeInfinity" /> if no such path
   /// </returns>
   public double GetDistanceTo(int dstVertex)
   {
      _graph.VerticeCount.ValidateVertex(dstVertex);
      return _distancesTo[dstVertex];
   }

   /// <summary>
   ///    Is there a path from the source vertex to vertex <paramref name="dstVertex" />?
   /// </summary>
   /// <param name="dstVertex">The destination vertex</param>
   /// <returns>true if there is a path from the source vertex to vertex <paramref name="dstVertex" />, and false otherwise</returns>
   public bool HasPathTo(int dstVertex)
   {
      _graph.VerticeCount.ValidateVertex(dstVertex);
      return _distancesTo[dstVertex] > double.NegativeInfinity;
   }

   /// <summary>
   ///    Returns the longest path from the source vertex to vertex <paramref name="dstVertex" />.
   /// </summary>
   /// <param name="dstVertex">The destination vertex</param>
   /// <returns>
   ///    The longest path from the source vertex to vertex <paramref name="dstVertex" /> as an iterable of edges, and
   ///    empty if no such path
   /// </returns>
   public IEnumerable<DirectedEdge> GetPathTo(int dstVertex)
   {
      _graph.VerticeCount.ValidateVertex(dstVertex);
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

   // relax edge, but update if you find a *longer* path
   private void Relax(DirectedEdge edge)
   {
      var (fromVtx, toVtx, _) = edge;
      if (_distancesTo[toVtx] < _distancesTo[fromVtx] + edge.Weight)
      {
         _distancesTo[toVtx] = _distancesTo[fromVtx] + edge.Weight;
         _edgesTo[toVtx] = edge;
      }
   }
}