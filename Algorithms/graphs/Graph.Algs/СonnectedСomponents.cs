using System.Diagnostics;
using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="СonnectedСomponents" /> class represents a data type for
///    determining the connected components in an undirected graph.
///    The <em>id</em> operation determines in which connected component
///    a given vertex lies; the <em>connected</em> operation
///    determines whether two vertices are in the same connected component;
///    the <em>count</em> operation determines the number of connected
///    components; and the <em>size</em> operation determines the number
///    of vertices in the connect component containing a given vertex.
///    The <em>component identifier</em> of a connected component is one of the
///    vertices in the connected component: two vertices have the same component
///    identifier if and only if they are in the same connected component.
///    <p>
///       This implementation uses depth-first search.
///       The constructor takes  Theta (<em>V</em> + <em>E</em>) time,
///       where <em>V</em> is the number of vertices and <em>E</em> is the
///       number of edges.
///       Each instance method takes Theta (1) time.
///       It uses Theta (<em>V</em>) extra space (not including the graph).
///    </p>
/// </summary>
public sealed class СonnectedСomponents
{
   // private readonly EdgeWeightedGraph? _edgeGraph;
   private readonly int[] _id; // id[v] = id of connected component containing v
   private readonly PrimaryGraph? _idxGraph;
   private readonly bool[] _marked; // marked[v] = has vertex v been marked?
   private readonly int[] _size; // size[id] = number of vertices in given component

   public СonnectedСomponents(PrimaryGraph graph)
   {
      _idxGraph = graph;
      _marked = new bool[graph.VerticeCount];
      _id = new int[graph.VerticeCount];
      _size = new int[graph.VerticeCount];
   }

/*
   public СonnectedСomponents(EdgeWeightedGraph graph)
   {
      _edgeGraph = graph;
      _marked = new bool[graph.VerticeCount];
      _id = new int[graph.VerticeCount];
      _size = new int[graph.VerticeCount];
   }
*/

   public int Count { get; private set; }

   public void SearchIdx()
   {
      Debug.Assert(_idxGraph != null, nameof(_idxGraph) + " != null");

      for (var vtx = 0; vtx < _idxGraph.VerticeCount; vtx++)
      {
         if (!_marked[vtx])
         {
            Search(_idxGraph, vtx);
            Count++;
         }
      }
   }

/*
   private void SearchEdg()
   {
      Debug.Assert(_edgeGraph != null, nameof(_edgeGraph) + " != null");

      for (var vtx = 0; vtx < _edgeGraph.VerticeCount; vtx++)
      {
         if (!_marked[vtx])
         {
            Search(_edgeGraph, vtx);
            Count++;
         }
      }
   }
*/

   private void Search(PrimaryGraph graph, int vtx)
   {
      _marked[vtx] = true;
      _id[vtx] = Count;
      _size[Count]++;
      foreach (var adjVtx in graph.GetAdjacentList(vtx))
      {
         if (!_marked[adjVtx])
         {
            Search(graph, adjVtx);
         }
      }
   }

/*
   private void Search(EdgeWeightedGraph graph, int vtx)
   {
      _marked[vtx] = true;
      _id[vtx] = Count;
      _size[Count]++;
      foreach (var adjEdge in graph.GetAdjacencyEdges(vtx))
      {
         var otherVtx = adjEdge.GetOther(vtx);
         if (!_marked[otherVtx])
         {
            Search(graph, otherVtx);
         }
      }
   }
*/

   public int GetId(int vtx)
   {
      _marked.ValidateVertex(vtx);
      return _id[vtx];
   }

   public int GetSize(int vtx)
   {
      _marked.ValidateVertex(vtx);
      return _size[_id[vtx]];
   }

   public bool Connected(int startVtx, int endVtx)
   {
      _marked.ValidateVertex(startVtx);
      _marked.ValidateVertex(endVtx);
      return GetId(startVtx) == GetId(endVtx);
   }
}