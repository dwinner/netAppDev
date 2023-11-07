using System.Diagnostics;
using System.Text;

namespace Graph.Adt;

/// <summary>
///    The Graph class represents an undirected graph of vertices
///    named 0 through <em>V</em> – 1.
///    It supports the following two primary operations: add an edge to the graph,
///    iterate over all of the vertices adjacent to a vertex. It also provides
///    methods for returning the degree of a vertex, the number of vertices
///    <em>V</em> in the graph, and the number of edges <em>E</em> in the graph.
///    Parallel edges and self-loops are permitted.
///    By convention, a self-loop <em>v</em>-<em>v</em> appears in the
///    adjacency list of <em>v</em> twice and contributes two to the degree
///    of <em>v</em>.
///    <p>
///       This implementation uses an <em>adjacency-lists representation</em>, which
///       is a vertex-indexed array of Bag objects.
///       It uses Theta (<em>E</em> + <em>V</em>) space, where <em>E</em> is
///       the number of edges and <em>V</em> is the number of vertices.
///       All instance methods take Theta (1) time. (Though, iterating over
///       the vertices returned by #adj(int) takes time proportional
///       to the degree of the vertex.)
///       Constructing an empty graph with <em>V</em> vertices takes
///       Theta (<em>V</em>) time; constructing a graph with <em>E</em> edges
///       and <em>V</em> vertices takes Theta (<em>E</em> + <em>V</em>) time.
///    </p>
/// </summary>
public sealed class PrimaryGraph
{
   private readonly LinkedList<int>[] _adjacencyList;

   /// <summary>
   ///    Initializes an empty graph with <paramref name="verticeCount" /> vertices and 0 edges.
   /// </summary>
   /// <param name="verticeCount">number of vertices</param>
   public PrimaryGraph(int verticeCount)
   {
      Debug.Assert(verticeCount > 0);

      VerticeCount = verticeCount;
      EdgeCount = 0;
      _adjacencyList = new LinkedList<int>[VerticeCount];
      for (var i = 0; i < VerticeCount; i++)
      {
         _adjacencyList[i] = new LinkedList<int>();
      }
   }

   /// <summary>
   ///    Number of vertices in this graph
   /// </summary>
   public int VerticeCount { get; }

   /// <summary>
   ///    Number of edges in this graph
   /// </summary>
   public int EdgeCount { get; private set; }

   /// <summary>
   ///    Adds the undirected edge v-w to this graph.
   /// </summary>
   /// <param name="fromVertex">One vertex in the edge</param>
   /// <param name="toVertex">The other vertex in the edge</param>
   public void AddEdge(int fromVertex, int toVertex)
   {
      VerticeCount.ValidateVertex(fromVertex);
      VerticeCount.ValidateVertex(toVertex);
      EdgeCount++;
      _adjacencyList[fromVertex].AddLast(toVertex);
      _adjacencyList[toVertex].AddLast(fromVertex);
   }

   public void AddEdge(int vertex, IEnumerable<int> adjValues)
   {
      VerticeCount.ValidateVertex(vertex);
      foreach (var adjValue in adjValues)
      {
         VerticeCount.ValidateVertex(adjValue);
         EdgeCount++;
         _adjacencyList[vertex].AddLast(adjValue);
      }
   }

   /// <summary>
   ///    Returns the vertices adjacent to vertex <paramref name="vertex" />.
   /// </summary>
   /// <param name="vertex">The vertex index</param>
   /// <returns>the vertices adjacent to <paramref name="vertex" /></returns>
   public IEnumerable<int> GetAdjacentList(int vertex)
   {
      VerticeCount.ValidateVertex(vertex);
      return _adjacencyList[vertex];
   }

   /// <summary>
   ///    Returns the degree of vertex <paramref name="vertex" />
   /// </summary>
   /// <param name="vertex">The vertex</param>
   /// <returns>the degree of vertex <paramref name="vertex" /></returns>
   public int GetDegree(int vertex)
   {
      VerticeCount.ValidateVertex(vertex);
      return _adjacencyList[vertex].Count;
   }

   public override string ToString()
   {
      var newline = Environment.NewLine;
      var strBld = new StringBuilder();
      strBld.Append($"{VerticeCount} vertices, {EdgeCount} edges {newline}");
      for (var i = 0; i < VerticeCount; i++)
      {
         strBld.Append(i + ": ");
         foreach (var val in _adjacencyList[i])
         {
            strBld.Append($"{val} ");
         }

         strBld.Append(newline);
      }

      return strBld.ToString();
   }
}