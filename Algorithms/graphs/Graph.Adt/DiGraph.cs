using System.Text;

namespace Graph.Adt;

/// <summary>
///    The <see cref="DiGraph" /> class represents a directed graph of vertices
///    named 0 through <em>V</em> - 1.
///    It supports the following two primary operations: add an edge to the digraph,
///    iterate over all of the vertices adjacent from a given vertex.
///    It also provides methods for returning the indegree or outdegree of a vertex,
///    the number of vertices <em>V</em> in the digraph,
///    the number of edges <em>E</em> in the digraph, and the reverse digraph.
///    Parallel edges and self-loops are permitted.
///    <p>
///       This implementation uses an <em>adjacency-lists representation</em>, which
///       is a vertex-indexed array of <see cref="LinkedList{T}" /> objects.
///       It uses 02b8 Theta (<em>E</em> + <em>V</em>) space, where <em>E</em> is
///       the number of edges and <em>V</em> is the number of vertices.
///       The <code>reverse()</code> method takes Theta (<em>E</em> + <em>V</em>) time
///       and space; all other instance methods take Theta (1) time. (Though, iterating over
///       the vertices returned by adj(int) takes time proportional
///       to the outdegree of the vertex.)
///       Constructing an empty digraph with <em>V</em> vertices takes
///       Theta (<em>V</em>) time; constructing a digraph with <em>E</em> edges
///       and <em>V</em> vertices takes Theta (<em>E</em> + <em>V</em>) time.
///    </p>
/// </summary>
public sealed class DiGraph
{
   private static readonly string Nl = Environment.NewLine;
   private readonly LinkedList<int>[] _adjacentList; // adjacency list for source vertex
   private readonly int[] _inDegree; // indegree of source vertex

   public DiGraph(int verticeCount)
   {
      VerticeCount = verticeCount;
      EdgeCount = 0;
      _inDegree = new int[VerticeCount];
      _adjacentList = new LinkedList<int>[VerticeCount];
      for (var vtx = 0; vtx < VerticeCount; vtx++)
      {
         _adjacentList[vtx] = new LinkedList<int>();
      }
   }

   public int VerticeCount { get; }

   public int EdgeCount { get; private set; }

   public void AddEdge(int startVtx, int endVtx)
   {
      VerticeCount.ValidateVertex(startVtx);
      VerticeCount.ValidateVertex(endVtx);
      _adjacentList[startVtx].AddLast(endVtx);
      _inDegree[endVtx]++;
      EdgeCount++;
   }

   public IEnumerable<int> GetAdjacentList(int vtx)
   {
      VerticeCount.ValidateVertex(vtx);
      return _adjacentList[vtx];
   }

   public int GetOutDegree(int vtx)
   {
      VerticeCount.ValidateVertex(vtx);
      return _adjacentList[vtx].Count;
   }

   public int GetInDegree(int vtx)
   {
      VerticeCount.ValidateVertex(vtx);
      return _inDegree[vtx];
   }

   public DiGraph Reverse()
   {
      var reverse = new DiGraph(VerticeCount);
      for (var vtx = 0; vtx < VerticeCount; vtx++)
      {
         foreach (var adjVtx in GetAdjacentList(vtx))
         {
            reverse.AddEdge(adjVtx, vtx);
         }
      }

      return reverse;
   }

   public override string ToString()
   {
      var strBuilder = new StringBuilder();
      strBuilder.Append($"{VerticeCount} vertices, {EdgeCount} edges {Nl}");
      for (var vtx = 0; vtx < VerticeCount; vtx++)
      {
         strBuilder.Append($"{vtx:D}: ");
         foreach (var adjVtx in _adjacentList[vtx])
         {
            strBuilder.Append($"{adjVtx:D} ");
         }

         strBuilder.AppendLine();
      }

      return strBuilder.ToString();
   }
}