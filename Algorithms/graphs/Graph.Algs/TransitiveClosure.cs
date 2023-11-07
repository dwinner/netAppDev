using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="TransitiveClosure" /> class represents a data type for
///    computing the transitive closure of a digraph.
///    <p>
///       This implementation runs depth-first search from each vertex.
///       The constructor takes Theta (<em>V</em>(<em>V</em> + <em>E</em>))
///       in the worst case, where <em>V</em> is the number of vertices and
///       <em>E</em> is the number of edges.
///       Each instance method takes Theta(1) time.
///       It uses Theta(<em>V</em><sup>2</sup>) extra space (not including the digraph).
///    </p>
///    <p>
///       For large digraphs, you may want to consider a more sophisticated algorithm.
///       <a href="http://www.cs.hut.fi/~enu/thesis.html">Nuutila</a> proposes two
///       algorithm for the problem (based on strong components and an interval representation)
///       that runs in Theta(<em>E</em> + <em>V</em>) time on typical digraphs.
///    </p>
/// </summary>
public sealed class TransitiveClosure
{
   private readonly DiGraph _graph;
   private readonly DirectedDepth1StSearch[] _transitiveArray; // tc[v] = reachable from v

   public TransitiveClosure(DiGraph graph)
   {
      _graph = graph;
      _transitiveArray = new DirectedDepth1StSearch[_graph.VerticeCount];
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         _transitiveArray[vtx] = new DirectedDepth1StSearch(_graph);
      }
   }

   public void Apply()
   {
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         _transitiveArray[vtx].Search(vtx);
      }
   }

   /// <summary>
   ///    Is there a directed path from vertex <paramref name="sourceVtx" /> to vertex <paramref name="targetVtx" /> in the
   ///    digraph?
   /// </summary>
   /// <param name="sourceVtx">the source vertex</param>
   /// <param name="targetVtx">the target vertex</param>
   /// <returns>
   ///    true if there is a directed path from <paramref name="sourceVtx" /> to <paramref name="targetVtx" /> false
   ///    otherwise
   /// </returns>
   public bool IsReachable(int sourceVtx, int targetVtx)
   {
      _graph.VerticeCount.ValidateVertex(sourceVtx);
      _graph.VerticeCount.ValidateVertex(targetVtx);

      return _transitiveArray[sourceVtx].IsMarked(targetVtx);
   }
}