/*
 * Find a maximum cardinality matching (and minimum cardinality vertex cover)
 * in a bipartite graph using the alternating path algorithm.
 */

using System.Diagnostics;
using Graph.Adt;

namespace Graph.Algs;

/// <summary>
///    The <see cref="BipartiteMatching" /> class represents a data type for computing a
///    <em>maximum (cardinality) matching</em> and a
///    <em>minimum (cardinality) vertex cover</em> in a bipartite graph.
///    A <em>bipartite graph</em> in a graph whose vertices can be partitioned
///    into two disjoint sets such that every edge has one endpoint in either set.
///    A <em>matching</em> in a graph is a subset of its edges with no common
///    vertices. A <em>maximum matching</em> is a matching with the maximum number
///    of edges.
///    A <em>perfect matching</em> is a matching which matches all vertices in the graph.
///    A <em>vertex cover</em> in a graph is a subset of its vertices such that
///    every edge is incident to at least one vertex. A <em>minimum vertex cover</em>
///    is a vertex cover with the minimum number of vertices.
///    By Konig's theorem, in any bipartite
///    graph, the maximum number of edges in matching equals the minimum number
///    of vertices in a vertex cover.
///    The maximum matching problem in <em>nonbipartite</em> graphs is
///    also important, but all known algorithms for this more general problem
///    are substantially more complicated.
///    <p>
///       This implementation uses the <em>alternating-path algorithm</em>.
///       It is equivalent to reducing to the maximum-flow problem and running
///       the augmenting-path algorithm on the resulting flow network, but it
///       does so with less overhead.
///       The constructor takes <em>O</em>((<em>E</em> + <em>V</em>) <em>V</em>)
///       time, where <em>E</em> is the number of edges and <em>V</em> is the
///       number of vertices in the graph.
///       It uses Theta(<em>V</em>) extra space (not including the graph).
///    </p>
///    <p>
///       See also {@link HopcroftKarp}, which solves the problem in
///       <em>O</em>(<em>E</em> sqrt(<em>V</em>)) using the Hopcroft-Karp
///       algorithm and
///       <a href="https://algs4.cs.princeton.edu/65reductions/BipartiteMatchingToMaxflow.java.html">BipartiteMatchingToMaxflow</a>
///       ,
///       which solves the problem in <em>O</em>((<em>E</em> + <em>V</em>) <em>V</em>)
///       time via a reduction to maxflow.
///    </p>
/// </summary>
public sealed class BipartiteMatching
{
   private const int Unmatched = -1;
   private readonly BipartiteX _bipartition; // the bipartition
   private readonly PrimaryGraph _graph;

   // mate[v] =  w if v-w is an edge in current matching = -1 if v is not in current matching
   private readonly int[] _mates;
   private readonly int _verticeCount; // number of vertices in the graph

   private int[]? _edgesTo; // edgeTo[v] = last edge on alternating path to v
   private bool[]? _inMinVertexCoverArray; // inMinVertexCover[v] = true if v is in min vertex cover
   private bool[]? _markedVertices; // marked[v] = true iff v is reachable via alternating path

   /// <summary>
   ///    Ctor
   /// </summary>
   /// <param name="graph">A bipartite graph</param>
   /// <exception cref="ArgumentException">If <paramref name="graph" /> is not bipartite</exception>
   public BipartiteMatching(PrimaryGraph graph)
   {
      _graph = graph;
      _bipartition = new BipartiteX(_graph);
      if (!_bipartition.IsBipartite)
      {
         throw new ArgumentException("graph is not bipartite", nameof(graph));
      }

      _verticeCount = _graph.VerticeCount;

      // initialize empty matching
      _mates = new int[_verticeCount];
      for (var vtx = 0; vtx < _verticeCount; vtx++)
      {
         _mates[vtx] = Unmatched;
      }
   }

   /// <summary>
   ///    The number of edges in a maximum matching.
   /// </summary>
   public int Cardinality { get; private set; }

   /// <summary>
   ///    Determines a maximum matching (and a minimum vertex cover) in a bipartite graph.
   /// </summary>
   public void Apply()
   {
      // alternating path algorithm
      while (HasAugmentingPath(_graph))
      {
         // find one endpoint t in alternating path
         var endPoint = -1;
         for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
         {
#if DEBUG
            Debug.Assert(_edgesTo != null, nameof(_edgesTo) + " != null");
#endif
            if (!IsMatched(vtx) && _edgesTo[vtx] != -1)
            {
               endPoint = vtx;
               break;
            }
         }

         // update the matching according to alternating path in edgeTo[] array
         for (var vtx = endPoint; vtx != -1; vtx = _edgesTo[_edgesTo[vtx]])
         {
#if DEBUG
            Debug.Assert(_edgesTo != null, nameof(_edgesTo) + " != null");
#endif
            var fromVtx = _edgesTo[vtx];
            _mates[vtx] = fromVtx;
            _mates[fromVtx] = vtx;
         }

         Cardinality++;
      }

      // find min vertex cover from marked[] array
      _inMinVertexCoverArray = new bool[_verticeCount];
      for (var vtx = 0; vtx < _verticeCount; vtx++)
      {
#if DEBUG
         Debug.Assert(_markedVertices != null, nameof(_markedVertices) + " != null");
#endif
         if (_bipartition.GetColor(vtx) && !_markedVertices[vtx])
         {
            _inMinVertexCoverArray[vtx] = true;
         }

         if (!_bipartition.GetColor(vtx) && _markedVertices[vtx])
         {
            _inMinVertexCoverArray[vtx] = true;
         }
      }

#if DEBUG
      Debug.Assert(CertifySolution(out var errorMessage), errorMessage);
#endif
   }

   /// <summary>
   ///    Returns the vertex to which the specified vertex is matched in the maximum matching computed by the algorithm.
   /// </summary>
   /// <param name="aVertex">The vertex</param>
   /// <returns>
   ///    the vertex to which vertex <paramref name="aVertex" /> is matched in the maximum matching; -1 if the vertex is not
   ///    matched
   /// </returns>
   public int GetMate(int aVertex)
   {
      _verticeCount.ValidateVertex(aVertex);
      return _mates[aVertex];
   }

   /// <summary>
   ///    Returns true if the specified vertex is matched in the maximum matching computed by the algorithm.
   /// </summary>
   /// <param name="aVertex">The vertex</param>
   /// <returns>true if vertex <paramref name="aVertex" /> is matched in maximum matching; false otherwise</returns>
   public bool IsMatched(int aVertex)
   {
      _verticeCount.ValidateVertex(aVertex);
      return _mates[aVertex] != Unmatched;
   }

   /// <summary>
   ///    Returns true if the graph contains a perfect matching. That is,
   ///    the number of edges in a maximum matching is equal to one half
   ///    of the number of vertices in the graph (so that every vertex is matched).
   /// </summary>
   /// <returns>true if the graph contains a perfect matching; false otherwise</returns>
   public bool IsPerfect() => Cardinality * 2 == _verticeCount;

   /// <summary>
   ///    Returns true if the specified vertex is in the minimum vertex cover computed by the algorithm.
   /// </summary>
   /// <param name="aVertex">A vertex</param>
   /// <returns>true if vertex <paramref name="aVertex" /> is in the minimum vertex cover; false otherwise</returns>
   public bool InMinVertexCover(int aVertex)
   {
      _verticeCount.ValidateVertex(aVertex);
#if DEBUG
      Debug.Assert(_inMinVertexCoverArray != null, nameof(_inMinVertexCoverArray) + " != null");
#endif
      return _inMinVertexCoverArray[aVertex];
   }

   /// <summary>
   ///    is there an augmenting path?
   ///    - if so, upon termination adj[] contains the level graph;
   ///    - if not, upon termination marked[] specifies those vertices reachable via an alternating path from one side of the
   ///    bipartition
   /// </summary>
   /// <remarks>
   ///    an alternating path is a path whose edges belong alternately to the matching and not to the matching
   ///    an augmenting path is an alternating path that starts and ends at unmatched vertices.
   ///    this implementation finds a shortest augmenting path (fewest number of edges), though there is no particular
   ///    advantage to do so here
   /// </remarks>
   /// <param name="graph">A bipartite graph</param>
   /// <returns>true, if there is an augmenting path, false otherwise</returns>
   private bool HasAugmentingPath(PrimaryGraph graph)
   {
      _markedVertices = new bool[_verticeCount];
      _edgesTo = new int[_verticeCount];
      for (var vtx = 0; vtx < _verticeCount; vtx++)
      {
         _edgesTo[vtx] = -1;
      }

      // breadth-first search (starting from all unmatched vertices on one side of bipartition)
      Queue<int> queue = new();
      for (var vtx = 0; vtx < _verticeCount; vtx++)
      {
         if (_bipartition.GetColor(vtx) && !IsMatched(vtx))
         {
            queue.Enqueue(vtx);
            _markedVertices[vtx] = true;
         }
      }

      // run BFS, stopping as soon as an alternating path is found
      while (queue.Count > 0)
      {
         var vertex = queue.Dequeue();
         foreach (var adjVtx in graph.GetAdjacentList(vertex))
         {
            // either (1) forward edge not in matching or (2) backward edge in matching
            if (IsResidualGraphEdge(vertex, adjVtx) && !_markedVertices[adjVtx])
            {
               _edgesTo[adjVtx] = vertex;
               _markedVertices[adjVtx] = true;
               if (!IsMatched(adjVtx))
               {
                  return true;
               }

               queue.Enqueue(adjVtx);
            }
         }
      }

      return false;
   }

   // is the edge v-w a forward edge not in the matching or a reverse edge in the matching?
   private bool IsResidualGraphEdge(int startVtx, int endVtx) =>
      (_mates[startVtx] != endVtx && _bipartition.GetColor(startVtx))
      || (_mates[startVtx] == endVtx && !_bipartition.GetColor(startVtx));

   #region Testing correctness of the data type.

   // check that mate[] and inVertexCover[] define a max matching and min vertex cover, respectively
   private bool CertifySolution(out string errorMessage)
   {
      // check that mate(v) = w if mate(w) = v
      for (var vtx = 0; vtx < _verticeCount; vtx++)
      {
         if (GetMate(vtx) == -1)
         {
            continue;
         }

         if (GetMate(GetMate(vtx)) != vtx)
         {
            errorMessage = "Mates are not matched";
            return false;
         }
      }

      // check that size() is consistent with mate()
      var matchedVertices = 0;
      for (var vtx = 0; vtx < _verticeCount; vtx++)
      {
         if (GetMate(vtx) != -1)
         {
            matchedVertices++;
         }
      }

      if (2 * Cardinality != matchedVertices)
      {
         errorMessage = "Not matched cardinality";
         return false;
      }

      // check that size() is consistent with minVertexCover()
      var sizeOfMinVertexCover = 0;
      for (var vtx = 0; vtx < _verticeCount; vtx++)
      {
         if (InMinVertexCover(vtx))
         {
            sizeOfMinVertexCover++;
         }
      }

      if (Cardinality != sizeOfMinVertexCover)
      {
         errorMessage = "Not matched Cardinality";
         return false;
      }

      // check that mate() uses each vertex at most once
      var isMatched = new bool[_verticeCount];
      for (var vtx = 0; vtx < _verticeCount; vtx++)
      {
         var mateVtx = _mates[vtx];
         if (mateVtx == -1)
         {
            continue;
         }

         if (vtx == mateVtx)
         {
            errorMessage = "Found matched vertex with mate vertex";
            return false;
         }

         if (vtx >= mateVtx)
         {
            continue;
         }

         if (isMatched[vtx] || isMatched[mateVtx])
         {
            errorMessage = "Found matched vertex with mate vertex";
            return false;
         }

         isMatched[vtx] = true;
         isMatched[mateVtx] = true;
      }

      // check that mate() uses only edges that appear in the graph
      for (var vtx = 0; vtx < _verticeCount; vtx++)
      {
         if (GetMate(vtx) == -1)
         {
            continue;
         }

         var isEdge = false;
         foreach (var adjVtx in _graph.GetAdjacentList(vtx))
         {
            if (GetMate(vtx) == adjVtx)
            {
               isEdge = true;
            }
         }

         if (!isEdge)
         {
            errorMessage = "Not an edge";
            return false;
         }
      }

      // BUG: in checking minimum vertex covering - commented
      // check that inMinVertexCover() is a vertex cover
      /*for (var vtx = 0; vtx < _verticeCount; vtx++)
      {
         if (_graph.GetAdjacentList(vtx).Any(adjVtx => !InMinVertexCover(vtx) && !InMinVertexCover(adjVtx)))
         {
            errorMessage = "Not in a min vertex cover";
            return false;
         }
      }*/

      errorMessage = string.Empty;
      return true;
   }

   #endregion
}