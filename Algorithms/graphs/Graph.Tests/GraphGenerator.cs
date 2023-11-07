using Graph.Adt;
using Sorting.Algs.PrioQ;

namespace Graph.Tests;

using Graph = PrimaryGraph;

/// <summary>
///    The <see cref="GraphGenerator" /> class provides static methods for creating
///    various graphs, including Erdos-Renyi random graphs, random bipartite
///    graphs, random k-regular graphs, and random rooted trees.
/// </summary>
public static class GraphGenerator
{
   private static readonly Random Rnd = new();

   /// <summary>
   ///    Returns a random simple graph containing <paramref name="verticeCount" /> vertices and <paramref name="edgeCount" />
   ///    edges.
   /// </summary>
   /// <param name="verticeCount">The number of vertices</param>
   /// <param name="edgeCount">The number of vertices</param>
   /// <returns>
   ///    A random simple graph on <paramref name="verticeCount" /> vertices, containing a total of
   ///    <paramref name="edgeCount" /> edges
   /// </returns>
   /// <exception cref="ArgumentException">If no such simple graph exists</exception>
   public static Graph NewSimpleGraph(int verticeCount, int edgeCount)
   {
      if (edgeCount > (long)verticeCount * (verticeCount - 1) / 2)
      {
         throw new ArgumentException("Too many edges", nameof(edgeCount));
      }

      if (edgeCount < 0)
      {
         throw new ArgumentException("Too few edges", nameof(edgeCount));
      }

      var graph = new Graph(verticeCount);
      var set = new HashSet<Edge>(Edge.DefaultEqualityComparer);
      while (graph.EdgeCount < edgeCount)
      {
         var startVtx = Rnd.Next(verticeCount);
         var endVtx = Rnd.Next(verticeCount);
         var newEdge = new Edge(startVtx, endVtx);
         if (startVtx != endVtx && set.Add(newEdge))
         {
            graph.AddEdge(startVtx, endVtx);
         }
      }

      return graph;
   }

   /// <summary>
   ///    Returns a random simple graph on <paramref name="verticeCount" /> vertices, with an
   ///    edge between any two vertices with probability <paramref name="probability" />. This is sometimes
   ///    referred to as the Erdos-Renyi random graph model.
   /// </summary>
   /// <param name="verticeCount">The number of vertices</param>
   /// <param name="probability">The probability of choosing an edge</param>
   /// <returns>
   ///    A random simple graph on <paramref name="verticeCount" /> vertices, with an edge between
   ///    any two vertices with probability <paramref name="probability" />
   /// </returns>
   /// <exception cref="ArgumentException">If probability is not between 0 and 1</exception>
   public static Graph NewSimpleGraph(int verticeCount, double probability)
   {
      if (probability is < 0.0 or > 1.0)
      {
         throw new ArgumentException("Probability must be between 0 and 1", nameof(probability));
      }

      var graph = new Graph(verticeCount);
      for (var startVtx = 0; startVtx < verticeCount; startVtx++)
      {
         for (var endVtx = startVtx + 1; endVtx < verticeCount; endVtx++)
         {
            if (Rnd.Bernoulli(probability))
            {
               graph.AddEdge(startVtx, endVtx);
            }
         }
      }

      return graph;
   }

   /// <summary>
   ///    Returns the complete graph on <paramref name="verticeCount" /> vertices.
   /// </summary>
   /// <param name="verticeCount">The number of vertices</param>
   /// <returns>The complete graph on <paramref name="verticeCount" /> vertices</returns>
   public static Graph NewCompleteGraph(int verticeCount) => NewSimpleGraph(verticeCount, 1.0);

   /// <summary>
   ///    Returns a complete bipartite graph on <paramref name="verticeCount1" /> and <paramref name="verticeCount2" />
   ///    vertices.
   /// </summary>
   /// <param name="verticeCount1">The number of vertices in one partition</param>
   /// <param name="verticeCount2">The number of vertices in the other partition</param>
   /// <returns>A complete bipartite graph on <paramref name="verticeCount1" /> and <paramref name="verticeCount2" /> vertices</returns>
   public static Graph NewCompleteBipartiteGraph(int verticeCount1, int verticeCount2) =>
      NewBipartiteGraph(verticeCount1, verticeCount2, verticeCount1 * verticeCount2);

   /// <summary>
   ///    Returns a random simple bipartite graph on <paramref name="verticeCount1" /> and <paramref name="verticeCount2" />
   ///    vertices with <paramref name="edgeCount" /> edges.
   /// </summary>
   /// <param name="verticeCount1">The number of vertices in one partition</param>
   /// <param name="verticeCount2">The number of vertices in the other partition</param>
   /// <param name="edgeCount">The number of edges</param>
   /// <returns>
   ///    A random simple bipartite graph on <paramref name="verticeCount1" /> and <paramref name="verticeCount2" />
   ///    vertices, containing a total of <paramref name="edgeCount" /> edges
   /// </returns>
   /// <exception cref="ArgumentException">If no such simple bipartite graph exists</exception>
   public static Graph NewBipartiteGraph(int verticeCount1, int verticeCount2, int edgeCount)
   {
      if (edgeCount > (long)verticeCount1 * verticeCount2)
      {
         throw new ArgumentException("Too many edges", nameof(edgeCount));
      }

      if (edgeCount < 0)
      {
         throw new ArgumentException("Too few edges", nameof(edgeCount));
      }

      var graph = new Graph(verticeCount1 + verticeCount2);
      var vertices = new int[verticeCount1 + verticeCount2];
      for (var i = 0; i < verticeCount1 + verticeCount2; i++)
      {
         vertices[i] = i;
      }

      Rnd.Shuffle(vertices);
      var set = new HashSet<Edge>(Edge.DefaultEqualityComparer);
      while (graph.EdgeCount < edgeCount)
      {
         var startVtx = Rnd.Next(verticeCount1);
         var endVtx = verticeCount1 + Rnd.Next(verticeCount2);
         var newEdge = new Edge(vertices[startVtx], vertices[endVtx]);
         if (set.Add(newEdge))
         {
            graph.AddEdge(vertices[startVtx], vertices[endVtx]);
         }
      }

      return graph;
   }

   /// <summary>
   ///    Returns a random simple bipartite graph on <paramref name="verticeCount1" /> and <paramref name="verticeCount2" />
   ///    vertices,
   ///    containing each possible edge with probability <paramref name="probability" />.
   /// </summary>
   /// <param name="verticeCount1">The number of vertices in one partition</param>
   /// <param name="verticeCount2">The number of vertices in the other partition</param>
   /// <param name="probability">The probability that the graph contains an edge with one endpoint in either side</param>
   /// <returns>
   ///    A random simple bipartite graph on <paramref name="verticeCount1" /> and <paramref name="verticeCount2" /> vertices,
   ///    containing each possible edge with probability <paramref name="probability" />
   /// </returns>
   /// <exception cref="ArgumentException">If probability is not between 0 and 1</exception>
   public static Graph NewBipartiteGraph(int verticeCount1, int verticeCount2, double probability)
   {
      if (probability is < 0.0 or > 1.0)
      {
         throw new ArgumentException("Probability must be between 0 and 1", nameof(probability));
      }

      var vertices = new int[verticeCount1 + verticeCount2];
      for (var i = 0; i < verticeCount1 + verticeCount2; i++)
      {
         vertices[i] = i;
      }

      Rnd.Shuffle(vertices);
      var graph = new Graph(verticeCount1 + verticeCount2);
      for (var startVtx = 0; startVtx < verticeCount1; startVtx++)
      {
         for (var endVtx = 0; endVtx < verticeCount2; endVtx++)
         {
            if (Rnd.Bernoulli(probability))
            {
               graph.AddEdge(vertices[startVtx], vertices[verticeCount1 + endVtx]);
            }
         }
      }

      return graph;
   }

   /// <summary>
   ///    Returns a path graph on <paramref name="verticeCount" /> vertices.
   /// </summary>
   /// <param name="verticeCount">The number of vertices in the path</param>
   /// <returns>A path graph on <paramref name="verticeCount" /> vertices</returns>
   public static Graph NewPathGraph(int verticeCount)
   {
      var graph = new Graph(verticeCount);
      var vertices = new int[verticeCount];
      for (var i = 0; i < verticeCount; i++)
      {
         vertices[i] = i;
      }

      Rnd.Shuffle(vertices);
      for (var i = 0; i < verticeCount - 1; i++)
      {
         graph.AddEdge(vertices[i], vertices[i + 1]);
      }

      return graph;
   }

   /// <summary>
   ///    Returns a complete binary tree graph on <paramref name="verticeCount" /> vertices.
   /// </summary>
   /// <param name="verticeCount">The number of vertices in the binary tree</param>
   /// <returns>A complete binary tree graph on <paramref name="verticeCount" /> vertices</returns>
   public static Graph NewBinaryTreeGraph(int verticeCount)
   {
      var graph = new Graph(verticeCount);
      var vertices = new int[verticeCount];
      for (var i = 0; i < verticeCount; i++)
      {
         vertices[i] = i;
      }

      Rnd.Shuffle(vertices);
      for (var i = 1; i < verticeCount; i++)
      {
         graph.AddEdge(vertices[i], vertices[(i - 1) / 2]);
      }

      return graph;
   }

   /// <summary>
   ///    Returns a cycle graph on <paramref name="verticeCount" /> vertices.
   /// </summary>
   /// <param name="verticeCount">The number of vertices in the cycle</param>
   /// <returns>A cycle graph on <paramref name="verticeCount" /> vertices</returns>
   public static Graph NewCycleGraph(int verticeCount)
   {
      var graph = new Graph(verticeCount);
      var vertices = new int[verticeCount];
      for (var i = 0; i < verticeCount; i++)
      {
         vertices[i] = i;
      }

      Rnd.Shuffle(vertices);
      for (var i = 0; i < verticeCount - 1; i++)
      {
         graph.AddEdge(vertices[i], vertices[i + 1]);
      }

      graph.AddEdge(vertices[verticeCount - 1], vertices[0]);

      return graph;
   }

   /// <summary>
   ///    Returns an Eulerian cycle graph on <paramref name="verticeCount" /> vertices.
   /// </summary>
   /// <param name="verticeCount">The number of vertices in the cycle</param>
   /// <param name="edgeCount">The number of edges in the cycle</param>
   /// <returns>
   ///    A graph that is an Eulerian cycle on <paramref name="verticeCount" /> vertices and
   ///    <paramref name="edgeCount" /> edges
   /// </returns>
   /// <exception cref="ArgumentException">
   ///    If <paramref name="verticeCount" /> or <paramref name="edgeCount" /> is less or equal to zero
   /// </exception>
   public static Graph NewEulerianCycleGraph(int verticeCount, int edgeCount)
   {
      if (edgeCount <= 0)
      {
         throw new ArgumentException("An Eulerian cycle must have at least one edge", nameof(edgeCount));
      }

      if (verticeCount <= 0)
      {
         throw new ArgumentException("An Eulerian cycle must have at least one vertex", nameof(verticeCount));
      }

      var graph = new Graph(verticeCount);
      var vertices = new int[edgeCount];
      for (var i = 0; i < edgeCount; i++)
      {
         vertices[i] = Rnd.Next(verticeCount);
      }

      for (var i = 0; i < edgeCount - 1; i++)
      {
         graph.AddEdge(vertices[i], vertices[i + 1]);
      }

      graph.AddEdge(vertices[edgeCount - 1], vertices[0]);

      return graph;
   }

   /// <summary>
   ///    Returns an Eulerian path graph on <paramref name="verticeCount" /> vertices.
   /// </summary>
   /// <param name="verticeCount">The number of vertices in the path</param>
   /// <param name="edgeCount">The number of edges in the path</param>
   /// <returns>
   ///    A graph that is an Eulerian path on <paramref name="verticeCount" /> vertices and
   ///    <paramref name="edgeCount" /> edges
   /// </returns>
   /// <exception cref="ArgumentException">
   ///    If <paramref name="verticeCount" /> or <paramref name="edgeCount" /> is less or
   ///    equal to zero
   /// </exception>
   public static Graph NewEulerianPath(int verticeCount, int edgeCount)
   {
      if (edgeCount < 0)
      {
         throw new ArgumentException("negative number of edges", nameof(edgeCount));
      }

      if (verticeCount <= 0)
      {
         throw new ArgumentException("An Eulerian path must have at least one vertex", nameof(verticeCount));
      }

      var graph = new Graph(verticeCount);
      var vertices = new int[edgeCount + 1];
      for (var i = 0; i < edgeCount + 1; i++)
      {
         vertices[i] = Rnd.Next(verticeCount);
      }

      for (var i = 0; i < edgeCount; i++)
      {
         graph.AddEdge(vertices[i], vertices[i + 1]);
      }

      return graph;
   }

   /// <summary>
   ///    Returns a wheel graph on <paramref name="verticeCount" /> vertices.
   /// </summary>
   /// <param name="verticeCount">The number of vertices in the wheel</param>
   /// <returns>
   ///    A wheel graph on <paramref name="verticeCount" /> vertices: a single vertex connected to every vertex in a
   ///    cycle on (<paramref name="verticeCount" /> - 1) vertices
   /// </returns>
   /// <exception cref="ArgumentException">If <paramref name="verticeCount" /> is less or equal to one</exception>
   public static Graph NewWheelGraph(int verticeCount)
   {
      if (verticeCount <= 1)
      {
         throw new ArgumentException("Number of vertices must be at least 2", nameof(verticeCount));
      }

      var graph = new Graph(verticeCount);
      var vertices = new int[verticeCount];
      for (var i = 0; i < verticeCount; i++)
      {
         vertices[i] = i;
      }

      Rnd.Shuffle(vertices);

      // simple cycle on V-1 vertices
      for (var i = 1; i < verticeCount - 1; i++)
      {
         graph.AddEdge(vertices[i], vertices[i + 1]);
      }

      graph.AddEdge(vertices[verticeCount - 1], vertices[1]);

      // connect vertices[0] to every vertex on cycle
      for (var i = 1; i < verticeCount; i++)
      {
         graph.AddEdge(vertices[0], vertices[i]);
      }

      return graph;
   }

   /// <summary>
   ///    Returns a star graph on <paramref name="verticeCount" /> vertices.
   /// </summary>
   /// <param name="verticeCount">The number of vertices in the star</param>
   /// <returns>A star graph on <paramref name="verticeCount" /> vertices: a single vertex connected to every other vertex</returns>
   /// <exception cref="ArgumentException">If <paramref name="verticeCount" /> is less or equal to zero</exception>
   public static Graph NewStarGraph(int verticeCount)
   {
      if (verticeCount <= 0)
      {
         throw new ArgumentException("Number of vertices must be at least 1", nameof(verticeCount));
      }

      var graph = new Graph(verticeCount);
      var vertices = new int[verticeCount];
      for (var i = 0; i < verticeCount; i++)
      {
         vertices[i] = i;
      }

      Rnd.Shuffle(vertices);

      // connect vertices[0] to every other vertex
      for (var i = 1; i < verticeCount; i++)
      {
         graph.AddEdge(vertices[0], vertices[i]);
      }

      return graph;
   }

   /// <summary>
   ///    Returns a uniformly random <paramref name="vertexDegree" />-regular graph on <paramref name="verticeCount" />
   ///    vertices
   ///    (not necessarily simple). The graph is simple with probability only about e^(-k^2/4),
   ///    which is tiny when k = 14.
   /// </summary>
   /// <param name="verticeCount">The number of vertices in the graph</param>
   /// <param name="vertexDegree">Degree of each vertex</param>
   /// <returns>
   ///    A uniformly random <paramref name="vertexDegree" />-regular graph on <paramref name="verticeCount" />
   ///    vertices.
   /// </returns>
   /// <exception cref="ArgumentException">If there is no way to pass such parameters' combination</exception>
   public static Graph NewRegularGraph(int verticeCount, int vertexDegree)
   {
      if (verticeCount * vertexDegree % 2 != 0)
      {
         throw new ArgumentException(
            $"Number of vertices * {nameof(vertexDegree)} must be even", nameof(verticeCount));
      }

      var graph = new Graph(verticeCount);

      // create k copies of each vertex
      var vertices = new int[verticeCount * vertexDegree];
      for (var v = 0; v < verticeCount; v++)
      {
         for (var j = 0; j < vertexDegree; j++)
         {
            vertices[v + verticeCount * j] = v;
         }
      }

      // pick a random perfect matching
      Rnd.Shuffle(vertices);
      for (var i = 0; i < verticeCount * vertexDegree / 2; i++)
      {
         graph.AddEdge(vertices[2 * i], vertices[2 * i + 1]);
      }

      return graph;
   }

   /// <summary>
   ///    Returns a uniformly random tree on <paramref name="verticeCount" /> vertices.
   /// </summary>
   /// <remarks>This algorithm uses a Prufer sequence and takes time proportional to <em>V log V</em></remarks>
   /// <param name="verticeCount">The number of vertices in the tree</param>
   /// <returns>A uniformly random tree on <paramref name="verticeCount" /> vertices</returns>
   public static Graph NewTreeGraph(int verticeCount)
   {
      /*
       * http://www.proofwiki.org/wiki/Labeled_Tree_from_Prüfer_Sequence
       * http://citeseerx.ist.psu.edu/viewdoc/download?doi=10.1.1.36.6484&rep=rep1&type=pdf
       */
      var graph = new Graph(verticeCount);

      // special case
      if (verticeCount == 1)
      {
         return graph;
      }

      // Cayley's theorem: there are V^(V-2) labeled trees on V vertices
      // Prufer sequence: sequence of V-2 values between 0 and V-1
      // Prufer's proof of Cayley's theorem: Prufer sequences are in 1-1
      // with labeled trees on V vertices
      var prufer = new int[verticeCount - 2];
      for (var i = 0; i < verticeCount - 2; i++)
      {
         prufer[i] = Rnd.Next(verticeCount);
      }

      // degree of vertex v = 1 + number of times it appears in Prufer sequence
      var degree = new int[verticeCount];
      for (var vtx = 0; vtx < verticeCount; vtx++)
      {
         degree[vtx] = 1;
      }

      for (var i = 0; i < verticeCount - 2; i++)
      {
         degree[prufer[i]]++;
      }

      // pq contains all vertices of degree 1
      var prioQueue = new MinPrioQueue<int>();
      for (var vtx = 0; vtx < verticeCount; vtx++)
      {
         if (degree[vtx] == 1)
         {
            prioQueue.Insert(vtx);
         }
      }

      // repeatedly delMin() degree 1 vertex that has the minimum index
      for (var i = 0; i < verticeCount - 2; i++)
      {
         var minVtx = prioQueue.DelMin();
         graph.AddEdge(minVtx, prufer[i]);
         degree[minVtx]--;
         degree[prufer[i]]--;
         if (degree[prufer[i]] == 1)
         {
            prioQueue.Insert(prufer[i]);
         }
      }

      graph.AddEdge(prioQueue.DelMin(), prioQueue.DelMin());

      return graph;
   }
}