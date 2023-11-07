using System.Text;

namespace Graph.Adt;

/// <summary>
///    The <see cref="EdgeWeightedGraph" /> class represents an edge-weighted
///    graph of vertices named 0 through <em>V</em> – 1, where each
///    undirected edge is of type <see cref="Edge" /> and has a real-valued weight.
///    It supports the following two primary operations: add an edge to the graph,
///    iterate over all of the edges incident to a vertex. It also provides
///    methods for returning the degree of a vertex, the number of vertices
///    <em>V</em> in the graph, and the number of edges <em>E</em> in the graph.
///    Parallel edges and self-loops are permitted.
///    By convention, a self-loop <em>v</em>-<em>v</em> appears in the
///    adjacency list of <em>v</em> twice and contributes two to the degree
///    of <em>v</em>.
///    <p>
///       This implementation uses an <em>adjacency-lists representation</em>, which
///       is a vertex-indexed array of {@link Bag} objects.
///       It uses Theta (<em>E</em> + <em>V</em>) space, where <em>E</em> is
///       the number of edges and <em>V</em> is the number of vertices.
///       All instance methods take Theta (1) time. (Though, iterating over
///       the edges returned by adj(int) takes time proportional to the degree of the vertex.)
///       Constructing an empty edge-weighted graph with <em>V</em> vertices takes
///       Theta (<em>V</em>) time; constructing an edge-weighted graph with
///       <em>E</em> edges and <em>V</em> vertices takes
///       Theta (<em>E</em> + <em>V</em>) time.
///    </p>
/// </summary>
public sealed class EdgeWeightedGraph
{
   private static readonly string NewLine = Environment.NewLine;

   private readonly LinkedList<Edge>[] _adjacencyList;

   public EdgeWeightedGraph(int verticeCount)
   {
      if (verticeCount < 0)
      {
         throw new ArgumentException("Number of vertices must be non-negative", nameof(verticeCount));
      }

      VerticeCount = verticeCount;
      EdgeCount = 0;
      _adjacencyList = new LinkedList<Edge>[VerticeCount];
      for (var vtx = 0; vtx < VerticeCount; vtx++)
      {
         _adjacencyList[vtx] = new LinkedList<Edge>();
      }
   }

   public EdgeWeightedGraph(EdgeWeightedGraph graph)
      : this(graph.VerticeCount)
   {
      EdgeCount = graph.EdgeCount;
      for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
      {
         // reverse so that adjacency list is in the same order as original
         var reverse = new Stack<Edge>();
         foreach (var edge in graph._adjacencyList[vtx])
         {
            reverse.Push(edge);
         }

         foreach (var edge in reverse)
         {
            _adjacencyList[vtx].AddLast(edge);
         }
      }
   }

   public int EdgeCount { get; set; }

   public int VerticeCount { get; }

   /// <summary>
   ///    Adds the undirected edge <paramref name="edge" /> to this edge-weighted graph.
   /// </summary>
   /// <param name="edge">The edge</param>
   public void AddEdge(Edge edge)
   {
      var (eitherVtx, otherVtx) = edge;
      VerticeCount.ValidateVertex(eitherVtx);
      VerticeCount.ValidateVertex(otherVtx);
      _adjacencyList[eitherVtx].AddLast(edge);
      _adjacencyList[otherVtx].AddLast(edge);
      EdgeCount++;
   }

   public void AddEdge(int startVtx, int endVtx, double weight) => AddEdge(new Edge(startVtx, endVtx, weight));

   /// <summary>
   ///    Returns the edges incident on vertex <paramref name="vertex" />.
   /// </summary>
   /// <param name="vertex">The vertex</param>
   /// <returns>The edges incident on vertex <paramref name="vertex" /> as an <see cref="IEnumerable{T}" /></returns>
   public IEnumerable<Edge> GetAdjacencyEdges(int vertex)
   {
      VerticeCount.ValidateVertex(vertex);
      return _adjacencyList[vertex];
   }

   /// <summary>
   ///    Returns the degree of vertex <paramref name="vertex" />.
   /// </summary>
   /// <param name="vertex">The vertex</param>
   /// <returns>The degree of vertex <paramref name="vertex" /></returns>
   public int GetDegree(int vertex)
   {
      VerticeCount.ValidateVertex(vertex);
      return _adjacencyList[vertex].Count;
   }

   /// <summary>
   ///    Returns all edges in this edge-weighted graph.
   /// </summary>
   /// <returns>All edges in this edge-weighted graph</returns>
   /// <exception cref="ArgumentException">if the vertex is not one of the endpoints of this edge</exception>
   public IEnumerable<Edge> GetEdges()
   {
      var list = new LinkedList<Edge>();
      for (var vtx = 0; vtx < VerticeCount; vtx++)
      {
         var selfLoops = 0;
         foreach (var adjEdge in GetAdjacencyEdges(vtx))
         {
            if (adjEdge.GetOther(vtx) > vtx)
            {
               list.AddLast(adjEdge);
            }
            else if (adjEdge.GetOther(vtx) == vtx)
            {
               // add only one copy of each self loop (self loops will be consecutive)
               if (selfLoops % 2 == 0)
               {
                  list.AddLast(adjEdge);
               }

               selfLoops++;
            }
         }
      }

      return list;
   }

   public override string ToString()
   {
      var strBuilder = new StringBuilder();
      strBuilder.Append($"{VerticeCount} {EdgeCount}{NewLine}");
      for (var vtx = 0; vtx < VerticeCount; vtx++)
      {
         strBuilder.Append($"{vtx}: ");
         foreach (var edge in _adjacencyList[vtx])
         {
            strBuilder.Append($"{edge}  ");
         }

         strBuilder.Append(NewLine);
      }

      return strBuilder.ToString();
   }
}