using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="Breadth1StDirectedPath" /> class represents a data type for
///    finding shortest paths (number of edges) from a source vertex <em>s</em>
///    (or set of source vertices) to every other vertex in the digraph.
///    <p>
///       This implementation uses breadth-first search.
///       The apply() takes Theta(<em>V</em> + <em>E</em>) time in the
///       worst case, where <em>V</em> is the number of vertices and <em>E</em> is
///       the number of edges.
///       Each instance method takes Theta(1) time.
///       It uses Theta(<em>V</em>) extra space (not including the digraph).
///    </p>
/// </summary>
public sealed class Breadth1StDirectedPath
{
   private readonly int[] _distancesTo; // distTo[v] = length of shortest s->v path
   private readonly int[] _edgesTo; // edgeTo[v] = last edge on shortest s->v path
   private readonly DiGraph _graph;
   private readonly bool[] _markedVertices; // marked[v] = is there an s->v path?
   private readonly int _sourceVertex;
   private readonly int _vtxCount;

   public Breadth1StDirectedPath(DiGraph graph, int sourceVertex)
   {
      _graph = graph;
      _sourceVertex = sourceVertex;
      _vtxCount = _graph.VerticeCount;
      _markedVertices = new bool[_vtxCount];
      _distancesTo = new int[_vtxCount];
      _edgesTo = new int[_vtxCount];
      Array.Fill(_distancesTo, int.MaxValue);
      _vtxCount.ValidateVertex(sourceVertex);
   }

   /// <summary>
   ///    Computes the shortest path from the source vertex and every other vertex in graph.
   /// </summary>
   public void Apply()
   {
      Queue<int> trackingQueue = new();
      _markedVertices[_sourceVertex] = true;
      _distancesTo[_sourceVertex] = 0;
      trackingQueue.Enqueue(_sourceVertex);
      while (trackingQueue.Count > 0)
      {
         var vertex = trackingQueue.Dequeue();
         foreach (var adjVtx in _graph.GetAdjacentList(vertex))
         {
            if (!_markedVertices[adjVtx])
            {
               _edgesTo[adjVtx] = vertex;
               _distancesTo[adjVtx] = _distancesTo[vertex] + 1;
               _markedVertices[adjVtx] = true;
               trackingQueue.Enqueue(adjVtx);
            }
         }
      }
   }

   public bool HasPathTo(int vertex)
   {
      _vtxCount.ValidateVertex(vertex);
      return _markedVertices[vertex];
   }

   public int GetDistanceTo(int vertex)
   {
      _vtxCount.ValidateVertex(vertex);
      return _distancesTo[vertex];
   }

   public IEnumerable<int> GetPathTo(int vertex)
   {
      _vtxCount.ValidateVertex(vertex);
      if (!HasPathTo(vertex))
      {
         return Enumerable.Empty<int>();
      }

      Stack<int> path = new();
      int loopVtx;
      for (loopVtx = vertex; _distancesTo[loopVtx] != 0; loopVtx = _edgesTo[loopVtx])
      {
         path.Push(loopVtx);
      }

      path.Push(loopVtx);

      return path;
   }
}