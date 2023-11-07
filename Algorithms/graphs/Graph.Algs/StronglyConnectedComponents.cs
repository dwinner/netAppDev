using System.Diagnostics;
using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="StronglyConnectedComponents" /> class represents a data type for
///    determining the strong components in a digraph.
///    The <em>id</em> operation determines in which strong component
///    a given vertex lies; the <em>areStronglyConnected</em> operation
///    determines whether two vertices are in the same strong component;
///    and the <em>count</em> operation determines the number of strong
///    components.
///    <p>
///       The <em>component identifier</em> of a component is one of the
///       vertices in the strong component: two vertices have the same component
///       identifier if and only if they are in the same strong component.
///    </p>
///    <p>
///       This implementation uses the Kosaraju-Sharir algorithm.
///       The constructor takes Theta (<em>V</em> + <em>E</em>) time,
///       where <em>V</em> is the number of vertices and <em>E</em>
///       is the number of edges.
///       Each instance method takes Theta(1) time.
///       It uses Theta(<em>V</em>) extra space (not including the digraph).
///       For alternative implementations of the same API, see
///       TarjanSCC and GabowSCC.
///    </p>
/// </summary>
public sealed class StronglyConnectedComponents
{
   private readonly DiGraph _graph;
   private readonly int[] _id; // id[v] = id of strong component containing v
   private readonly bool[] _marked; // marked[v] = has vertex v been visited?

   public StronglyConnectedComponents(DiGraph graph)
   {
      _graph = graph;
      _marked = new bool[_graph.VerticeCount];
      _id = new int[_graph.VerticeCount];
   }

   public int Count { get; private set; }

   public void Apply()
   {
      // compute reverse postorder of reverse graph
      var reversedGraph = _graph.Reverse();
      var depth1StOrder = new Depth1StOrder(reversedGraph.VerticeCount);
      depth1StOrder.Search(reversedGraph);

      // run DFS on graph, using reverse postorder to guide calculation
      foreach (var vertex in depth1StOrder.GetReversePostVertices())
      {
         if (!_marked[vertex])
         {
            Search(_graph, vertex);
            Count++;
         }
      }

      // check that id[] gives strong components
      Debug.Assert(Check(_graph));
   }

   /// <summary>
   ///    Returns the component id of the strong component containing vertex <paramref name="vertex" />.
   /// </summary>
   /// <param name="vertex">the vertex</param>
   /// <returns>the component id of the strong component containing vertex <paramref name="vertex" /></returns>
   public int GetId(int vertex)
   {
      _graph.VerticeCount.ValidateVertex(vertex);
      return _id[vertex];
   }

   // DFS on graph
   private void Search(DiGraph graph, int vertex)
   {
      _marked[vertex] = true;
      _id[vertex] = Count;
      foreach (var adjVtx in graph.GetAdjacentList(vertex))
      {
         if (!_marked[adjVtx])
         {
            Search(graph, adjVtx);
         }
      }
   }

   // does the id[] array contain the strongly connected components?
   private bool Check(DiGraph graph)
   {
      var transitiveClosure = new TransitiveClosure(graph);
      transitiveClosure.Apply();

      for (var src = 0; src < graph.VerticeCount; src++)
      {
         for (var dst = 0; dst < graph.VerticeCount; dst++)
         {
            if (StronglyConnected(src, dst) !=
                (transitiveClosure.IsReachable(src, dst) && transitiveClosure.IsReachable(dst, src)))
            {
               return false;
            }
         }
      }

      return true;
   }

   /// <summary>
   ///    Are vertices <paramref name="vertex1" /> and <paramref name="vertex2" /> in the same strong component?
   /// </summary>
   /// <param name="vertex1">one vertex</param>
   /// <param name="vertex2">other vertex</param>
   /// <returns>
   ///    true if vertices <paramref name="vertex1" /> and <paramref name="vertex2" /> are in the same strong component,
   ///    and false otherwise
   /// </returns>
   private bool StronglyConnected(int vertex1, int vertex2)
   {
      _graph.VerticeCount.ValidateVertex(vertex1);
      _graph.VerticeCount.ValidateVertex(vertex2);
      return _id[vertex1] == _id[vertex2];
   }
}