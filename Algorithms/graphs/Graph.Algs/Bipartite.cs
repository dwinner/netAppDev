using System.Diagnostics;
using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="Bipartite" /> class represents a data type for
///    determining whether an undirected graph is <em>bipartite</em> or whether
///    it has an <em>odd-length cycle</em>.
///    A graph is bipartite if and only if it has no odd-length cycle.
///    The <em>isBipartite</em> operation determines whether the graph is
///    bipartite. If so, the <em>color</em> operation determines a
///    bipartition; if not, the <em>oddCycle</em> operation determines a
///    cycle with an odd number of edges.
///    <p>
///       This implementation uses <em>depth-first search</em>.
///       The Apply() takes Theta(<em>V</em> + <em>E</em>) time in
///       the worst case, where <em>V</em> is the number of vertices and <em>E</em>
///       is the number of edges.
///       Each instance method takes Theta(1) time.
///       It uses Theta(<em>V</em>) extra space (not including the graph).
///       See <seealso cref="BipartiteX" /> for a nonrecursive version that uses breadth-first
///       search.
///    </p>
/// </summary>
public sealed class Bipartite
{
   private readonly bool[] _bipartitionColors; // color[v] gives vertices on one side of bipartition
   private readonly int[] _edgesTo; // edgeTo[v] = last edge on path to v
   private readonly PrimaryGraph _graph;
   private readonly bool[] _markedVertices; // marked[v] = true iff v has been visited in DFS
   private Stack<int>? _oddCycle; // odd-length cycle

   public Bipartite(PrimaryGraph graph)
   {
      _graph = graph;
      IsBipartite = true;
      _bipartitionColors = new bool[graph.VerticeCount];
      _markedVertices = new bool[graph.VerticeCount];
      _edgesTo = new int[graph.VerticeCount];
   }

   /// <summary>
   ///    Returns true if the graph is bipartite.
   /// </summary>
   public bool IsBipartite { get; private set; }

   /// <summary>
   ///    Returns an odd-length cycle if the graph is not bipartite, and empty otherwise
   /// </summary>
   public IEnumerable<int> OddLenCycle => _oddCycle ?? Enumerable.Empty<int>();

   /// <summary>
   ///    Determines whether an undirected graph is bipartite and finds either a bipartition or an odd-length cycle.
   /// </summary>
   public void Apply()
   {
      for (var vertex = 0; vertex < _graph.VerticeCount; vertex++)
      {
         if (!_markedVertices[vertex])
         {
            Search(vertex);
         }
      }

#if DEBUG
      Debug.Assert(Check(out var errorMessage), errorMessage);
#endif
   }

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
         throw new InvalidOperationException("graph is not bipartite");
      }

      return _bipartitionColors[vertex];
   }

   private void Search(int vertex)
   {
      _markedVertices[vertex] = true;
      foreach (var adjVtx in _graph.GetAdjacentList(vertex))
      {
         // short circuit if odd-length cycle found
         if (_oddCycle != null)
         {
            return;
         }

         // found uncolored vertex, so recur
         if (!_markedVertices[adjVtx])
         {
            _edgesTo[adjVtx] = vertex;
            _bipartitionColors[adjVtx] = !_bipartitionColors[vertex];
            Search(adjVtx);
         }
         else if (_bipartitionColors[adjVtx] == _bipartitionColors[vertex])
         {
            // if v-w create an odd-length cycle, find it
            IsBipartite = false;
            _oddCycle = new Stack<int>();
            _oddCycle.Push(adjVtx); // don't need this unless you want to include start vertex twice
            for (var vtx = vertex; vtx != adjVtx; vtx = _edgesTo[vtx])
            {
               _oddCycle.Push(vtx);
            }

            _oddCycle.Push(adjVtx);
         }
      }
   }

   private bool Check(out string errorMessage)
   {
      // graph is bipartite
      if (IsBipartite)
      {
         for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
         {
            foreach (var adjVtx in _graph.GetAdjacentList(vtx))
            {
               if (_bipartitionColors[vtx] == _bipartitionColors[adjVtx])
               {
                  errorMessage =
                     $"edge {vtx:D} - {adjVtx:D} with {vtx:D} and {adjVtx:D} in the same side of bipartition";
                  return false;
               }
            }
         }
      }
      else
      {
         // graph has an odd-length cycle, verify cycle
         int first = -1, last = -1;
         foreach (var vtx in OddLenCycle)
         {
            if (first == -1)
            {
               first = vtx;
            }

            last = vtx;
         }

         if (first != last)
         {
            errorMessage = $"cycle begins with {first:D} and ends with {last:D}";
            return false;
         }
      }

      errorMessage = string.Empty;
      return true;
   }
}