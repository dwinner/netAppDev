using System.Text;

namespace Graph.Adt;

/// <summary>
///    The <tt>EdgeWeightedDigraph</tt> class represents an directed graph of vertices
///    named 0 through V-1, where each edge has a real-valued weight.
///    It supports the following operations: add an edge to the digraph,
///    iterate over all of edges leaving a vertex.
///    Parallel edges and self-loops are permitted.
/// </summary>
public sealed class EdgeWeightedDigraph
{
   private readonly LinkedList<DirectedEdge>[] _adjacencyList;

   public EdgeWeightedDigraph(int verticeCount)
   {
      if (verticeCount < 0)
      {
         throw new ArgumentException(
            "Number of vertices in a Digraph must be nonnegative", nameof(verticeCount));
      }

      VerticeCount = verticeCount;
      EdgeCount = 0;
      _adjacencyList = new LinkedList<DirectedEdge>[VerticeCount];
      for (var vtx = 0; vtx < VerticeCount; vtx++)
      {
         _adjacencyList[vtx] = new LinkedList<DirectedEdge>();
      }
   }

   public int VerticeCount { get; }

   public int EdgeCount { get; set; }

   public void AddEdge(DirectedEdge edge)
   {
      var fromVtx = edge.FromVertex;
      _adjacencyList[fromVtx].AddLast(edge);
      EdgeCount++;
   }

   public IEnumerable<DirectedEdge> GetAdjacencyList(int vertex) => _adjacencyList[vertex];

   public IEnumerable<DirectedEdge> GetEdges()
   {
      var edgeList = new List<DirectedEdge>();
      for (var vtx = 0; vtx < VerticeCount; vtx++)
      {
         edgeList.AddRange(GetAdjacencyList(vtx));
      }

      return edgeList;
   }

   public int GetOutDegree(int vertex) => _adjacencyList[vertex].Count;

   public override string ToString()
   {
      var strBuilder = new StringBuilder();
      strBuilder.Append($"{VerticeCount} {EdgeCount}{Environment.NewLine}");
      for (var vtx = 0; vtx < VerticeCount; vtx++)
      {
         strBuilder.Append($"{vtx}: ");
         foreach (var edge in _adjacencyList[vtx])
         {
            strBuilder.Append($"{edge}  ");
         }

         strBuilder.Append(Environment.NewLine);
      }

      return strBuilder.ToString();
   }
}