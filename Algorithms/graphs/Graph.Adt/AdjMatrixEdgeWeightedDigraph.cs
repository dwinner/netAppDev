using System.Text;

namespace Graph.Adt;

/// <summary>
///    The <see cref="AdjMatrixEdgeWeightedDigraph" /> class represents an edge-weighted
///    digraph of vertices named 0 through <em>V</em> - 1, where each
///    directed edge is of type <see cref="DirectedEdge" /> and has a real-valued weight.
///    It supports the following two primary operations: add a directed edge
///    to the digraph and iterate over all of edges incident from a given vertex.
///    It also provides
///    methods for returning the number of vertices <em>V</em> and the number
///    of edges <em>E</em>. Parallel edges are disallowed; self-loops are permitted.
///    <p>
///       This implementation uses an adjacency-matrix representation.
///       All operations take constant time (in the worst case) except
///       iterating over the edges incident from a given vertex, which takes
///       time proportional to <em>V</em>.
///    </p>
/// </summary>
public sealed class AdjMatrixEdgeWeightedDigraph
{
   private readonly DirectedEdge?[][] _adjacencyMatrix;

   /// <summary>
   ///    Initializes an empty edge-weighted digraph with <paramref name="verticeCount" /> vertices and 0 edges.
   /// </summary>
   /// <param name="verticeCount">The number of vertices</param>
   /// <exception cref="ArgumentException">If <paramref name="verticeCount" /> is less than zero</exception>
   public AdjMatrixEdgeWeightedDigraph(int verticeCount)
   {
      if (verticeCount < 0)
      {
         throw new ArgumentException("number of vertices must be non-negative", nameof(verticeCount));
      }

      VerticeCount = verticeCount;
      EdgeCount = 0;
      _adjacencyMatrix = new DirectedEdge?[VerticeCount][];
      for (var i = 0; i < VerticeCount; i++)
      {
         _adjacencyMatrix[i] = new DirectedEdge?[VerticeCount];
      }
   }

   public int VerticeCount { get; }

   public int EdgeCount { get; private set; }

   public IEnumerable<DirectedEdge> this[int rowVertex]
   {
      get
      {
         VerticeCount.ValidateVertex(rowVertex);
         foreach (var edge in _adjacencyMatrix[rowVertex])
         {
            if (edge != null)
            {
               yield return edge;
            }
         }
      }
   }

   public DirectedEdge? this[int rowVertex, int colVertex]
   {
      get
      {
         VerticeCount.ValidateVertex(rowVertex);
         VerticeCount.ValidateVertex(colVertex);
         return _adjacencyMatrix[rowVertex][colVertex] ?? null;
      }
   }

   /// <summary>
   ///    Adds the directed edge <paramref name="edge" /> to the edge-weighted digraph (if there is not already an edge with
   ///    the same endpoints).
   /// </summary>
   /// <param name="edge">The edge</param>
   public void AddEdge(DirectedEdge edge)
   {
      var (fromVtx, toVtx, _) = edge;
      VerticeCount.ValidateVertex(fromVtx);
      VerticeCount.ValidateVertex(toVtx);
      if (_adjacencyMatrix[fromVtx][toVtx] == null)
      {
         EdgeCount++;
         _adjacencyMatrix[fromVtx][toVtx] = edge;
      }
   }

   public override string ToString()
   {
      var matrixDump = new StringBuilder();
      matrixDump.AppendLine($"{VerticeCount} {EdgeCount}");
      for (var i = 0; i < _adjacencyMatrix.Length; i++)
      {
         matrixDump.Append($"{i}: ");
         for (var j = 0; j < _adjacencyMatrix[i].Length; j++)
         {
            var edge = _adjacencyMatrix[i][j];
            var edgeDump = edge != null ? edge.ToString() : "none";
            matrixDump.Append($"{edgeDump} ");
         }

         matrixDump.AppendLine();
      }

      return matrixDump.ToString();
   }
}