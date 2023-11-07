using System.Diagnostics;
using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="DirectedCycle" /> class represents a data type for
///    determining whether a digraph has a directed cycle.
///    The <em>hasCycle</em> operation determines whether the digraph has
///    a simple directed cycle and, if so, the <em>cycle</em> operation
///    returns one.
///    <p>
///       This implementation uses depth-first search.
///       The constructor takes Theta (<em>V</em> + <em>E</em>) time in the worst
///       case, where <em>V</em> is the number of vertices and <em>E</em> is
///       the number of edges.
///       Each instance method takes Theta (1) time.
///       It uses Theta (<em>V</em>) extra space (not including the digraph).
///    </p>
///    <p>See Topological to compute a topological order if the digraph is acyclic.</p>
/// </summary>
public sealed class DirectedCycle
{
   private readonly int[] _edgeTo; // edgeTo[v] = previous vertex on path to v
   private readonly DiGraph _graph;
   private readonly bool[] _marked; // marked[v] = has vertex v been marked?
   private readonly bool[] _onStack; // onStack[v] = is vertex on the stack?
   private Stack<int>? _cycle; // directed cycle (or null if no such cycle)

   public DirectedCycle(DiGraph graph)
   {
      _graph = graph;
      _marked = new bool[_graph.VerticeCount];
      _onStack = new bool[_graph.VerticeCount];
      _edgeTo = new int[_graph.VerticeCount];
   }

   public void Find()
   {
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         if (!_marked[vtx] && _cycle == null)
         {
            Dfs(_graph, vtx);
         }
      }
   }

   private void Dfs(DiGraph graph, int vtx)
   {
      _onStack[vtx] = true;
      _marked[vtx] = true;
      foreach (var adjVtx in graph.GetAdjacentList(vtx))
      {
         // short circuit if directed cycle found
         if (_cycle != null)
         {
            return;
         }

         if (!_marked[adjVtx])
         {
            // found new vertex, so recur
            _edgeTo[adjVtx] = vtx;
            Dfs(graph, adjVtx);
         }
         else if (_onStack[adjVtx])
         {
            // trace back directed cycle
            _cycle = new Stack<int>();
            for (var cyclicVtx = vtx; cyclicVtx != adjVtx; cyclicVtx = _edgeTo[cyclicVtx])
            {
               _cycle.Push(cyclicVtx);
            }

            _cycle.Push(adjVtx);
            _cycle.Push(vtx);

            Debug.Assert(Check());
         }
      }

      _onStack[vtx] = false;
   }

   public bool HasCycle() => _cycle != null;

   public IEnumerable<int> GetCycle() => _cycle ?? Enumerable.Empty<int>();

   // certify that digraph has a directed cycle if it reports one
   private bool Check()
   {
      if (!HasCycle())
      {
         return true;
      }

      var cycle = GetCycle().ToArray();
      var first = cycle[0];
      var last = cycle[^1];
      if (first != last)
      {
         Debug.WriteLine($"cycle begins with {first} and ends with {last}");
         return false;
      }

      return true;
   }
}