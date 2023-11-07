using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="DijkstraAllPairsShortestPath" /> class represents a data type for solving the
///    all-pairs shortest paths problem in edge-weighted digraphs
///    where the edge weights are non-negative.
///    <p>
///       This implementation runs Dijkstra's algorithm from each vertex.
///       The <code>Apply</code> takes Theta(<em>V</em> (<em>E</em> log <em>V</em>)) time
///       in the worst case, where <em>V</em> is the number of vertices and
///       <em>E</em> is the number of edges.
///       Each instance method takes Theta(1) time.
///       It uses Theta(<em>V</em><sup>2</sup>) extra space (not including the
///       edge-weighted digraph).
///    </p>
/// </summary>
public sealed class DijkstraAllPairsShortestPath
{
   private readonly EdgeWeightedDigraph _graph;
   private readonly DijkstraShortestPath[] _allPaths;

   public DijkstraAllPairsShortestPath(EdgeWeightedDigraph graph)
   {
      _graph = graph;
      _allPaths = new DijkstraShortestPath[_graph.VerticeCount];
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         _allPaths[vtx] = new DijkstraShortestPath(_graph, vtx);
      }
   }

   /// <summary>
   ///    Computes a shortest paths tree from each vertex to every other vertex in the edge-weighted digraph
   /// </summary>
   public void Apply()
   {
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         _allPaths[vtx].Apply();
      }
   }

   /// <summary>
   ///    The shortest path from vertex <paramref name="srcVertex" /> to vertex <paramref name="dstVertex" />.
   /// </summary>
   /// <param name="srcVertex">The source vertex</param>
   /// <param name="dstVertex">The destination vertex</param>
   /// <returns>
   ///    The shortest path from vertex <paramref name="srcVertex" /> to vertex <paramref name="dstVertex" />, or empty
   ///    if no such path
   /// </returns>
   public IEnumerable<DirectedEdge> GetPath(int srcVertex, int dstVertex)
   {
      _graph.VerticeCount.ValidateVertex(srcVertex);
      _graph.VerticeCount.ValidateVertex(dstVertex);
      return _allPaths[srcVertex].GetPathTo(dstVertex);
   }

   /// <summary>
   ///    Is there a path from the vertex <paramref name="srcVertex" /> to vertex <paramref name="dstVertex" />?
   /// </summary>
   /// <param name="srcVertex">The source vertex</param>
   /// <param name="dstVertex">The destination vertex</param>
   /// <returns>
   ///    true if there is a path from vertex <paramref name="srcVertex" /> to vertex <paramref name="dstVertex" />, and
   ///    false otherwise
   /// </returns>
   public bool HasPath(int srcVertex, int dstVertex) => GetDistance(srcVertex, dstVertex) < double.PositiveInfinity;

   /// <summary>
   ///    Returns the length of a shortest path from vertex <paramref name="srcVertex" /> to vertex
   ///    <paramref name="dstVertex" />.
   /// </summary>
   /// <param name="srcVertex">The source vertex</param>
   /// <param name="dstVertex">The destination vertex</param>
   /// <returns>
   ///    The length of a shortest path from vertex <paramref name="srcVertex" /> to vertex
   ///    <paramref name="dstVertex" />; <see cref="double.PositiveInfinity" /> if no such path
   /// </returns>
   public double GetDistance(int srcVertex, int dstVertex)
   {
      _graph.VerticeCount.ValidateVertex(srcVertex);
      _graph.VerticeCount.ValidateVertex(dstVertex);
      return _allPaths[srcVertex].GetDistanceTo(dstVertex);
   }
}