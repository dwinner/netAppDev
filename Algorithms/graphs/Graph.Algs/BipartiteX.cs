using System.Diagnostics;
using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="BipartiteX" /> class represents a data type for
///    determining whether an undirected graph is <em>bipartite</em> or whether
///    it has an <em>odd-length cycle</em>.
///    A graph is bipartite if and only if it has no odd-length cycle.
///    The <em>isBipartite</em> operation determines whether the graph is
///    bipartite. If so, the <em>color</em> operation determines a
///    bipartition; if not, the <em>oddCycle</em> operation determines a
///    cycle with an odd number of edges.
///    <p>
///       This implementation uses <em>breadth-first search</em> and is nonrecursive.
///       The <code>Apply()</code> takes Theta(<em>V</em> + <em>E</em>) time in
///       in the worst case, where <em>V</em> is the number of vertices
///       and <em>E</em> is the number of edges.
///       Each instance method takes Theta(1) time.
///       It uses Theta(<em>V</em>) extra space (not including the graph).
///       See also Bipartite for a recursive version that uses depth-first search.
///    </p>
/// </summary>
public sealed class BipartiteX
{
   private const bool White = false;
   private readonly bool[] _colors; // color[v] gives vertices on one side of bipartition
   private readonly int[] _edgesTo; // edgeTo[v] = last edge on path to v
   private readonly PrimaryGraph _graph;
   private readonly bool[] _marked; // marked[v] = true if v has been visited in DFS
   private Queue<int>? _cycleQueue; // odd-length cycle

   public BipartiteX(PrimaryGraph graph)
   {
      _graph = graph;
      IsBipartite = true;
      _colors = new bool[_graph.VerticeCount];
      _marked = new bool[_graph.VerticeCount];
      _edgesTo = new int[_graph.VerticeCount];
   }

   /// <summary>
   ///    Returns true if the graph is bipartite.
   /// </summary>
   public bool IsBipartite { get; private set; }

   /// <summary>
   ///    odd-length cycle if the graph is not bipartite, and empty
   /// </summary>
   public IEnumerable<int> OddCycle =>
      _cycleQueue is { Count: > 0 }
         ? _cycleQueue
         : Enumerable.Empty<int>();

   /// <summary>
   ///    Returns the side of the bipartite that vertex <paramref name="vertex" /> is on.
   /// </summary>
   /// <param name="vertex">The vertex</param>
   /// <returns>
   ///    The side of the bipartition that vertex <paramref name="vertex" /> is on; two vertices are in the same side of
   ///    the bipartition if and only if they have the same color
   /// </returns>
   /// <exception cref="InvalidOperationException">If this method is called when the graph is not bipartite</exception>
   public bool GetColor(int vertex)
   {
      _graph.VerticeCount.ValidateVertex(vertex);
      if (!IsBipartite)
      {
         throw new InvalidOperationException("Graph is not bipartite");
      }

      return _colors[vertex];
   }

   /// <summary>
   ///    Determines whether an undirected graph is bipartite and finds either a bipartition or an odd-length cycle.
   /// </summary>
   public void Apply()
   {
      for (var vtx = 0; vtx < _graph.VerticeCount && IsBipartite; vtx++)
      {
         if (!_marked[vtx])
         {
            Breadth1StSearch(_graph, vtx);
         }
      }

#if DEBUG
      Debug.Assert(Check(_graph, out var errorMsg), errorMsg);
#endif
   }

   private void Breadth1StSearch(PrimaryGraph graph, int vtx)
   {
      var queue = new Queue<int>();
      _colors[vtx] = White;
      _marked[vtx] = true;
      queue.Enqueue(vtx);

      while (queue.Count > 0)
      {
         var vertex = queue.Dequeue();
         foreach (var adjVtx in graph.GetAdjacentList(vtx))
         {
            if (!_marked[adjVtx])
            {
               _marked[adjVtx] = true;
               _edgesTo[adjVtx] = vertex;
               _colors[adjVtx] = !_colors[vertex];
               queue.Enqueue(adjVtx);
            }
            else if (_colors[adjVtx] == _colors[vertex])
            {
               IsBipartite = false;

               // to form odd cycle, consider s-v path and s-w path
               // and let x be closest node to v and w common to two paths
               // then (w-x path) + (x-v path) + (edge v-w) is an odd-length cycle
               // Note: distTo[v] == distTo[w];
               _cycleQueue = new Queue<int>();
               var stack = new Stack<int>();
               int startVtx = vertex, endVtx = adjVtx;
               while (startVtx != endVtx)
               {
                  stack.Push(startVtx);
                  _cycleQueue.Enqueue(endVtx);
                  startVtx = _edgesTo[startVtx];
                  endVtx = _edgesTo[endVtx];
               }

               stack.Push(startVtx);
               while (stack.Count > 0)
               {
                  _cycleQueue.Enqueue(stack.Pop());
               }

               _cycleQueue.Enqueue(adjVtx);
               return;
            }
         }
      }
   }

   private bool Check(PrimaryGraph graph, out string errorMsg)
   {
      // graph is bipartite
      if (IsBipartite)
      {
         for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
         {
            foreach (var adjVtx in graph.GetAdjacentList(vtx))
            {
               if (_colors[vtx] == _colors[adjVtx])
               {
                  errorMsg = $"edge {vtx:D}-{adjVtx:D} with {vtx:D} and {adjVtx:D} in same side of bipartition";
                  return false;
               }
            }
         }
      }
      else
      {
         // graph has an odd-length cycle, verify cycle
         int first = -1, last = -1;
         foreach (var vtx in OddCycle)
         {
            if (first == -1)
            {
               first = vtx;
            }

            last = vtx;
         }

         if (first != last)
         {
            errorMsg = $"cycle begins with {first:D} and ends with {last:D}";
            return false;
         }
      }

      errorMsg = string.Empty;
      return true;
   }
}