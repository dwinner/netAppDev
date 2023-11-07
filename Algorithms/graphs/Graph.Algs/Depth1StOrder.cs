using System.Diagnostics;
using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="Depth1StOrder" /> class represents a data type for
///    determining depth-first search ordering of the vertices in a digraph
///    or edge-weighted digraph, including preorder, postorder, and reverse postorder.
///    <p>
///       This implementation uses depth-first search.
///       Each constructor takes Theta (<em>V</em> + <em>E</em>) time,
///       where <em>V</em> is the number of vertices and <em>E</em> is the
///       number of edges.
///       Each instance method takes Theta (1) time.
///       It uses Theta (<em>V</em>) extra space (not including the digraph).
///    </p>
/// </summary>
public sealed class Depth1StOrder
{
   private readonly bool[] _marked; // marked[v] = has v been marked in dfs?
   private readonly int[] _postOrderArray; // post[v]   = postorder number of v
   private readonly Queue<int> _postOrderQueue; // vertices in postorder
   private readonly int[] _preOrderArray; // pre[v]    = preorder  number of v
   private readonly Queue<int> _preOrderQueue; // vertices in preorder
   private int _postCounter; // counter for postorder numbering
   private int _preCounter; // counter or preorder numbering

   public Depth1StOrder(int verticeCount)
   {
      _preOrderArray = new int[verticeCount];
      _postOrderArray = new int[verticeCount];
      _postOrderQueue = new Queue<int>();
      _preOrderQueue = new Queue<int>();
      _marked = new bool[verticeCount];
   }

   public void Search(DiGraph graph)
   {
      for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
      {
         if (!_marked[vtx])
         {
            Depth1StSearch(graph, vtx);
         }
      }

      Debug.Assert(Check());
   }

   public void Search(EdgeWeightedDigraph graph)
   {
      for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
      {
         if (!_marked[vtx])
         {
            Depth1StSearch(graph, vtx);
         }
      }
   }

   // run DFS in digraph G from vertex v and compute preorder/postorder
   private void Depth1StSearch(DiGraph graph, int vertex)
   {
      _marked[vertex] = true;
      _preOrderArray[vertex] = _preCounter++;
      _preOrderQueue.Enqueue(vertex);
      foreach (var adjVtx in graph.GetAdjacentList(vertex))
      {
         if (!_marked[adjVtx])
         {
            Depth1StSearch(graph, adjVtx);
         }
      }

      _postOrderQueue.Enqueue(vertex);
      _postOrderArray[vertex] = _postCounter++;
   }

   // run DFS in edge-weighted digraph G from vertex v and compute preorder/postorder
   private void Depth1StSearch(EdgeWeightedDigraph graph, int vertex)
   {
      _marked[vertex] = true;
      _preOrderArray[vertex] = _preCounter++;
      _preOrderQueue.Enqueue(vertex);
      foreach (var edge in graph.GetAdjacencyList(vertex))
      {
         var toVtx = edge.ToVertex;
         if (!_marked[toVtx])
         {
            Depth1StSearch(graph, toVtx);
         }
      }

      _postOrderQueue.Enqueue(vertex);
      _postOrderArray[vertex] = _postCounter++;
   }

   public int GetPreOrderIndex(int vertex)
   {
      _marked.ValidateVertex(vertex);
      return _preOrderArray[vertex];
   }

   public int GetPostOrderIndex(int vertex)
   {
      _marked.ValidateVertex(vertex);
      return _postOrderArray[vertex];
   }

   // check that pre() and post() are consistent with pre(v) and post(v)
   private bool Check()
   {
      // check that post(v) is consistent with post()
      var traverseIdx = 0;
      foreach (var postVtx in GetPostVertices())
      {
         if (GetPostOrderIndex(postVtx) != traverseIdx)
         {
            Debug.WriteLine("post(v) and post() inconsistent");
            return false;
         }

         traverseIdx++;
      }

      // check that pre(v) is consistent with pre()
      traverseIdx = 0;
      foreach (var preVtx in GetPreVertices())
      {
         if (GetPreOrderIndex(preVtx) != traverseIdx)
         {
            Debug.WriteLine("pre(v) and pre() inconsistent");
            return false;
         }

         traverseIdx++;
      }

      return true;
   }

   public IEnumerable<int> GetPostVertices() => _postOrderQueue;

   public IEnumerable<int> GetPreVertices() => _preOrderQueue;

   public IEnumerable<int> GetReversePostVertices() => _postOrderQueue.Reverse();
}