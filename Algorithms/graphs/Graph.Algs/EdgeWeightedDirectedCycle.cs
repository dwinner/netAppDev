using System.Diagnostics;
using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="EdgeWeightedDirectedCycle" /> class represents a data type for
///    determining whether an edge-weighted digraph has a directed cycle.
///    The <em>hasCycle</em> operation determines whether the edge-weighted
///    digraph has a directed cycle and, if so, the <em>cycle</em> operation
///    returns one.
///    <p>
///       This implementation uses <em>depth-first search</em>.
///       The constructor takes Theta(<em>V</em> + <em>E</em>) time in the
///       worst case, where <em>V</em> is the number of vertices and
///       <em>E</em> is the number of edges.
///       Each instance method takes Theta(1) time.
///       It uses Theta(<em>V</em>) extra space (not including the
///       edge-weighted digraph).
///    </p>
///    <p>
///       See <see cref="TopologicalSort" /> to compute a topological order if the
///       edge-weighted digraph is acyclic.
///    </p>
/// </summary>
public sealed class EdgeWeightedDirectedCycle
{
   private readonly DirectedEdge[] _edgesTo; // edgeTo[v] = previous vertex on path to v
   private readonly EdgeWeightedDigraph _graph;
   private readonly bool[] _marked; // marked[v] = has vertex v been marked?
   private readonly bool[] _onStack; // onStack[v] = is vertex on the stack?
   private Stack<DirectedEdge>? _cycle; // directed cycle (or null if no such cycle)

   public EdgeWeightedDirectedCycle(EdgeWeightedDigraph graph)
   {
      _graph = graph;
      _marked = new bool[_graph.VerticeCount];
      _onStack = new bool[_graph.VerticeCount];
      _edgesTo = new DirectedEdge[_graph.VerticeCount];
   }

   /// <summary>
   ///    Determines whether the edge-weighted digraph has a directed cycle and,
   ///    if so, finds such a cycle.
   /// </summary>
   public void Find()
   {
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         if (!_marked[vtx])
         {
            Search(_graph, vtx);
         }
      }

#if DEBUG
      Debug.Assert(Check(out var errorMessage), errorMessage);
#endif
   }

   private void Search(EdgeWeightedDigraph graph, int vtx)
   {
      _onStack[vtx] = true;
      _marked[vtx] = true;
      foreach (var adjEdge in graph.GetAdjacencyList(vtx))
      {
         var toVtx = adjEdge.ToVertex;

         // short circuit if directed cycle found
         if (_cycle != null)
         {
            return;
         }

         if (!_marked[toVtx])
         {
            // found new vertex, so recur
            _edgesTo[toVtx] = adjEdge;
            Search(graph, toVtx);
         }
         else if (_onStack[toVtx])
         {
            // trace back directed cycle
            _cycle = new Stack<DirectedEdge>();
            var startEdge = adjEdge;
            while (startEdge.FromVertex != toVtx)
            {
               _cycle.Push(startEdge);
               startEdge = _edgesTo[startEdge.FromVertex];
            }

            _cycle.Push(startEdge);
            return;
         }
      }

      _onStack[vtx] = false;
   }

   public bool HasCycle() => _cycle != null;

   public IEnumerable<DirectedEdge> GetCycle() => _cycle ?? Enumerable.Empty<DirectedEdge>();

   // certify that digraph has a directed cycle if it reports one
   private bool Check(out string errorMessage)
   {
      // edge-weighted digraph is cyclic
      if (HasCycle())
      {
         // verify cycle
         DirectedEdge? firstEdge = null,
            lastEdge = null;
         foreach (var edge in GetCycle())
         {
            firstEdge ??= edge;
            if (lastEdge != null && lastEdge.ToVertex != edge.FromVertex)
            {
               errorMessage = $"cycle edges {lastEdge} and {edge} is not incident";
               return false;
            }

            lastEdge = edge;
         }

         // cycle() contains no edges
         if (firstEdge == null || lastEdge == null)
         {
            errorMessage = "cycle contains no edges";
            return false;
         }

         // first and last edges in cycle are not incident
         if (lastEdge.ToVertex != firstEdge.FromVertex)
         {
            errorMessage = $"cycle edges {lastEdge} and {firstEdge} is not incident";
            return false;
         }
      }


      errorMessage = string.Empty;
      return true;
   }
}