using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The Cycle class represents a data type for
///    determining whether an undirected graph has a simple cycle.
///    The <em>hasCycle</em> operation determines whether the graph has
///    a cycle and, if so, the <em>cycle</em> operation returns one.
///    <p>
///       This implementation uses depth-first search.
///       The constructor takes Theta (<em>V</em> + <em>E</em>) time in the
///       worst case, where <em>V</em> is the number of vertices and
///       <em>E</em> is the number of edges.
///       (The depth-first search part takes only <em>O</em>(<em>V</em>) time;
///       however, checking for self-loops and parallel edges takes
///       Theta (<em>V</em> + <em>E</em>) time in the worst case.
///       Each instance method takes Theta (1) time.
///       It uses Theta (<em>V</em>) extra space (not including the graph).
///    </p>
/// </summary>
public sealed class CycleChecker
{
   private readonly int[] _edgeTo;
   private readonly PrimaryGraph _graph;
   private Stack<int>? _cycle;
   private bool[] _marked;

   /// <summary>
   ///    Determines whether the undirected graph <paramref name="graph" /> has a cycle and, if so, finds such a cycle.
   /// </summary>
   /// <param name="graph">The undirected graph</param>
   public CycleChecker(PrimaryGraph graph)
   {
      _graph = graph;
      _marked = new bool[_graph.VerticeCount];
      _edgeTo = new int[_graph.VerticeCount];
   }

   public bool HasCycle => _cycle != null;

   // does this graph have two parallel edges?
   // side effect: initialize cycle to be two parallel edges
   public bool HasParallelEdges()
   {
      _marked = new bool[_graph.VerticeCount];
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         // check for parallel edges incident to v
         foreach (var adjVtx in _graph.GetAdjacentList(vtx))
         {
            if (_marked[adjVtx])
            {
               _cycle = new Stack<int>();
               _cycle.Push(vtx);
               _cycle.Push(adjVtx);
               _cycle.Push(vtx);

               return true;
            }

            _marked[adjVtx] = true;
         }

         // reset so marked[v] = false for all v
         foreach (var adjVtx in _graph.GetAdjacentList(vtx))
         {
            _marked[adjVtx] = false;
         }
      }

      return false;
   }

   public void Search()
   {
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         if (!_marked[vtx])
         {
            Search(-1, vtx);
         }
      }
   }

   private void Search(int adjVertex, int srcVertex)
   {
      _marked[srcVertex] = true;
      foreach (var adjVtx in _graph.GetAdjacentList(srcVertex))
      {
         // short circuit if cycle already found
         if (_cycle != null)
         {
            return;
         }

         if (!_marked[adjVtx])
         {
            _edgeTo[adjVtx] = srcVertex;
            Search(srcVertex, adjVtx);
         }
         else if (adjVtx != adjVertex)
         {
            // check for cycle (but disregard reverse of edge leading to v)
            _cycle = new Stack<int>();
            for (var vtx = srcVertex; vtx != adjVtx; vtx = _edgeTo[vtx])
            {
               _cycle.Push(vtx);
            }

            _cycle.Push(adjVtx);
            _cycle.Push(srcVertex);
         }
      }
   }

/*
   private bool HasSelfLoop()
   {
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         if (_graph.GetAdjacentList(vtx).Any(adjVtx => vtx == adjVtx))
         {
            _cycle = new Stack<int>();
            _cycle.Push(vtx);
            _cycle.Push(vtx);

            return true;
         }
      }

      return false;
   }
*/

   public IEnumerable<int> GetCycle() => _cycle ?? Enumerable.Empty<int>();
}