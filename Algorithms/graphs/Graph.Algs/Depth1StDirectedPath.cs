using Graph.Adt;

namespace Graph.Algs;

public sealed class Depth1StDirectedPath
{
   private readonly int[] _edgesTo; // edgeTo[v] = last edge on path from s to v
   private readonly DiGraph _graph;
   private readonly bool[] _markedVertices; // marked[v] = true if v is reachable from s
   private readonly int _sourceVertex; // source vertex

   public Depth1StDirectedPath(DiGraph graph, int sourceVertex)
   {
      _graph = graph;
      _markedVertices = new bool[_graph.VerticeCount];
      _edgesTo = new int[_graph.VerticeCount];
      _sourceVertex = sourceVertex;
   }

   public void Search() => Search(_sourceVertex);

   // is there a directed path from s to v?
   public bool HasPathTo(int vertex) => _markedVertices[vertex];

   public IEnumerable<int> GetPathTo(int vertex)
   {
      if (!HasPathTo(vertex))
      {
         return Enumerable.Empty<int>();
      }

      var pathStack = new Stack<int>();
      for (var prevVtx = vertex; prevVtx != _sourceVertex; prevVtx = _edgesTo[prevVtx])
      {
         pathStack.Push(prevVtx);
      }

      pathStack.Push(_sourceVertex);
      return pathStack;
   }

   private void Search(int sourceVertex)
   {
      _markedVertices[sourceVertex] = true;
      foreach (var adjVtx in _graph.GetAdjacentList(sourceVertex))
      {
         if (!_markedVertices[adjVtx])
         {
            _edgesTo[adjVtx] = sourceVertex;
            Search(adjVtx);
         }
      }
   }
}