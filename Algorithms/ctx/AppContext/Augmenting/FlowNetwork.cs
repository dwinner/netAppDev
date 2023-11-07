using System.Text;

namespace AppContext.Augmenting;

/// <summary>
///    The <see cref="FlowNetwork" /> class represents a capacitated network
///    with vertices named 0 through <em>V</em> - 1, where each directed
///    edge is of type <see cref="FlowEdge" /> and has a real-valued capacity
///    and flow.
///    It supports the following two primary operations: add an edge to the network,
///    iterate over all of the edges incident to or from a vertex. It also provides
///    methods for returning the number of vertices <em>V</em> and the number
///    of edges <em>E</em>. Parallel edges and self-loops are permitted.
///    <p>
///       This implementation uses an adjacency-lists representation, which
///       is a vertex-indexed array of <see cref="LinkedList{T}" /> objects.
///       All operations take constant time (in the worst case) except
///       iterating over the edges incident to a given vertex, which takes
///       time proportional to the number of such edges.
///    </p>
/// </summary>
public sealed class FlowNetwork
{
   private readonly LinkedList<FlowEdge>[] _adjacencyLists;

   public FlowNetwork(int verticeCount)
   {
      if (verticeCount < 0)
      {
         throw new ArgumentException("Number of vertices in a Graph must be non-negative", nameof(verticeCount));
      }

      VerticeCount = verticeCount;
      EdgeCount = 0;
      _adjacencyLists = new LinkedList<FlowEdge>[VerticeCount];
      for (var i = 0; i < VerticeCount; i++)
      {
         _adjacencyLists[i] = new LinkedList<FlowEdge>();
      }
   }

   /// <summary>
   ///    The number of vertices in the edge-weighted graph.
   /// </summary>
   public int VerticeCount { get; }

   /// <summary>
   ///    The number of edges in the edge-weighted graph.
   /// </summary>
   public int EdgeCount { get; private set; }

   /// <summary>
   ///    List of all edges - excludes self loops
   /// </summary>
   public IEnumerable<FlowEdge> Edges
   {
      get
      {
         for (var vtx = 0; vtx < VerticeCount; vtx++)
         {
            foreach (var edge in GetAdjacencyList(vtx))
            {
               if (edge.EndVertex != vtx)
               {
                  yield return edge;
               }
            }
         }
      }
   }

   /// <summary>
   ///    Returns the edges incident on vertex <paramref name="vertex" /> (includes both edges pointing to and from
   ///    <paramref name="vertex" />).
   /// </summary>
   /// <param name="vertex">The vertex</param>
   /// <returns>The edges incident on vertex <paramref name="vertex" /></returns>
   public IEnumerable<FlowEdge> GetAdjacencyList(int vertex)
   {
      ValidateVertex(vertex);
      return _adjacencyLists[vertex];
   }

   /// <summary>
   ///    Add edge to the flow network
   /// </summary>
   /// <param name="flowEdge">Flow edge</param>
   public void AddEdge(FlowEdge flowEdge)
   {
      var startVtx = flowEdge.StartVertex;
      var endVtx = flowEdge.EndVertex;
      ValidateVertex(startVtx);
      ValidateVertex(endVtx);
      _adjacencyLists[startVtx].AddLast(flowEdge);
      _adjacencyLists[endVtx].AddLast(flowEdge);
      EdgeCount++;
   }

   public override string ToString()
   {
      var flowNetworkDump = new StringBuilder();
      flowNetworkDump.AppendLine($"{VerticeCount} {EdgeCount}");
      for (var vtx = 0; vtx < VerticeCount; vtx++)
      {
         var curVtx = vtx;
         flowNetworkDump.AppendJoin(" ", GetAdjacencyList(curVtx).Where(edge => edge.EndVertex != curVtx));
         flowNetworkDump.AppendLine();
      }

      return flowNetworkDump.ToString();
   }

   private void ValidateVertex(int vertex)
   {
      if (vertex < 0 || vertex >= VerticeCount)
      {
         throw new ArgumentOutOfRangeException(nameof(vertex), $"vertex '{vertex}' is out of range");
      }
   }
}