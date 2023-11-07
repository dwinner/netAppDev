using Graph.Adt;

namespace Graph.Tests;

/// <summary>
///    The <see cref="DigraphGenerator" /> class provides static methods for creating
///    various digraphs, including Erdos-Renyi random digraphs, random DAGs,
///    random rooted trees, random rooted DAGs, random tournaments, path digraphs,
///    cycle digraphs, and the complete digraph.
/// </summary>
public static class DigraphGenerator
{
   private static readonly Random Rnd = new();

   /// <summary>
   ///    Returns a random simple digraph containing <paramref name="verticeCount" /> vertices and
   ///    <paramref name="edgeCount" /> edges.
   /// </summary>
   /// <param name="verticeCount">The number of vertices</param>
   /// <param name="edgeCount">The number of edges</param>
   /// <returns>
   ///    Random simple digraph on <paramref name="edgeCount" /> vertices, containing a total of
   ///    <paramref name="edgeCount" /> edges
   /// </returns>
   /// <exception cref="ArgumentException">If no such simple digraph exists</exception>
   public static DiGraph NewSimpleDiGraph(int verticeCount, int edgeCount)
   {
      if (edgeCount > (long)verticeCount * (verticeCount - 1))
      {
         throw new ArgumentException("Too many edges", nameof(verticeCount));
      }

      if (edgeCount < 0)
      {
         throw new ArgumentException("Too few edges", nameof(edgeCount));
      }

      var graph = new DiGraph(verticeCount);
      var set = new HashSet<Edge>(Edge.DefaultEqualityComparer);
      while (graph.EdgeCount < edgeCount)
      {
         var startVertex = Rnd.Next(verticeCount);
         var endVertex = Rnd.Next(verticeCount);
         var newEdge = new Edge(startVertex, endVertex);
         if (startVertex != endVertex && set.Add(newEdge))
         {
            graph.AddEdge(startVertex, endVertex);
         }
      }

      return graph;
   }

   /// <summary>
   ///    Returns a random simple digraph on <paramref name="verticeCount" /> vertices, with an
   ///    edge between any two vertices with probability <paramref name="probability" />. This is sometimes
   ///    referred to as the Erdos-Renyi random digraph model.
   ///    This implementations takes time proportional to V^2 (even if <paramref name="probability" /> is small).
   /// </summary>
   /// <param name="verticeCount">The number of vertices</param>
   /// <param name="probability">The probability of choosing an edge</param>
   /// <returns>
   ///    A random simple digraph on <paramref name="verticeCount" /> vertices, with an edge between any two vertices with
   ///    probability <paramref name="probability" />
   /// </returns>
   /// <exception cref="ArgumentException">If probability is not between 0 and 1</exception>
   public static DiGraph NewRandomDiGraph(int verticeCount, double probability)
   {
      if (probability is < 0.0 or > 1.0)
      {
         throw new ArgumentException("Probability must be between 0 and 1", nameof(probability));
      }

      var graph = new DiGraph(verticeCount);
      for (var startVtx = 0; startVtx < verticeCount; startVtx++)
      {
         for (var endVtx = 0; endVtx < verticeCount; endVtx++)
         {
            if (startVtx != endVtx && Rnd.Bernoulli(probability))
            {
               graph.AddEdge(startVtx, endVtx);
            }
         }
      }

      return graph;
   }

   /// <summary>
   ///    Returns the complete digraph on <paramref name="verticeCount" /> vertices. In a complete digraph,
   ///    every pair of distinct vertices is connected by two antiparallel edges. There are <code>V*(V-1)</code> edges.
   /// </summary>
   /// <param name="verticeCount">The number of vertices</param>
   /// <returns>The complete digraph on <paramref name="verticeCount" /> vertices</returns>
   public static DiGraph NewCompleteDiGraph(int verticeCount)
   {
      var graph = new DiGraph(verticeCount);
      for (var startVtx = 0; startVtx < verticeCount; startVtx++)
      {
         for (var endVtx = 0; endVtx < verticeCount; endVtx++)
         {
            if (startVtx != endVtx)
            {
               graph.AddEdge(startVtx, endVtx);
            }
         }
      }

      return graph;
   }

   /// <summary>
   ///    Returns a random simple DAG containing <paramref name="verticeCount" /> vertices and <paramref name="edgeCount" />
   ///    edges.
   /// </summary>
   /// <remarks>It is not uniformly selected at random among all such DAGs.</remarks>
   /// <param name="verticeCount">The number of vertices</param>
   /// <param name="edgeCount">The number of edges</param>
   /// <returns>
   ///    A random simple DAG on <paramref name="verticeCount" /> vertices, containing a total of
   ///    <paramref name="edgeCount" /> edges
   /// </returns>
   /// <exception cref="ArgumentException">If no such simple DAG exists</exception>
   public static DiGraph NewDagDiGraph(int verticeCount, int edgeCount)
   {
      if (edgeCount > (long)verticeCount * (verticeCount - 1) / 2)
      {
         throw new ArgumentException("Too many edges", nameof(edgeCount));
      }

      if (edgeCount < 0)
      {
         throw new ArgumentException("Too few edges", nameof(edgeCount));
      }

      var graph = new DiGraph(verticeCount);
      var set = new HashSet<Edge>(Edge.DefaultEqualityComparer);
      var vertices = new int[verticeCount];
      for (var vtx = 0; vtx < verticeCount; vtx++)
      {
         vertices[vtx] = vtx;
      }

      Rnd.Shuffle(vertices);
      while (graph.EdgeCount < edgeCount)
      {
         var startVertex = Rnd.Next(verticeCount);
         var endVertex = Rnd.Next(verticeCount);
         var newEdge = new Edge(startVertex, endVertex);
         if (startVertex < endVertex && set.Add(newEdge))
         {
            graph.AddEdge(vertices[startVertex], vertices[endVertex]);
         }
      }

      return graph;
   }

   /// <summary>
   ///    Returns a random tournament digraph on <paramref name="verticeCount" /> vertices. A tournament digraph
   ///    is a digraph in which, for every pair of vertices, there is one and only one
   ///    directed edge connecting them. A tournament is an oriented complete graph.
   /// </summary>
   /// <param name="verticeCount">The number of vertices</param>
   /// <returns>A random tournament digraph on <paramref name="verticeCount" /> vertices</returns>
   public static DiGraph NewTournamentDiGraph(int verticeCount)
   {
      const double probabiity = 0.5;
      var graph = new DiGraph(verticeCount);
      for (var startVtx = 0; startVtx < graph.VerticeCount; startVtx++)
      {
         for (var endVtx = startVtx + 1; endVtx < graph.VerticeCount; endVtx++)
         {
            if (Rnd.Bernoulli(probabiity))
            {
               graph.AddEdge(startVtx, endVtx);
            }
            else
            {
               graph.AddEdge(endVtx, startVtx);
            }
         }
      }

      return graph;
   }

   /// <summary>
   ///    Returns a complete rooted-in DAG on <paramref name="verticeCount" /> vertices.
   /// </summary>
   /// <remarks>
   ///    A rooted in-tree is a DAG in which there is a single vertex reachable from every other vertex. A complete
   ///    rooted in-DAG has V*(V-1)/2 edges.
   /// </remarks>
   /// <param name="verticeCount">The number of vertices</param>
   /// <returns>A complete rooted-in DAG on <paramref name="verticeCount" /> vertices</returns>
   public static DiGraph NewCompleteRootedInDagDiGraph(int verticeCount)
   {
      var graph = new DiGraph(verticeCount);
      var vertices = new int[verticeCount];
      for (var vtx = 0; vtx < verticeCount; vtx++)
      {
         vertices[vtx] = vtx;
      }

      Rnd.Shuffle(vertices);
      for (var startVtx = 0; startVtx < verticeCount; startVtx++)
      {
         for (var endVtx = startVtx + 1; endVtx < verticeCount; endVtx++)
         {
            graph.AddEdge(vertices[startVtx], vertices[endVtx]);
         }
      }

      return graph;
   }

   /// <summary>
   ///    Returns a random rooted-in DAG on <paramref name="verticeCount" /> vertices and <paramref name="edgeCount" /> edges.
   /// </summary>
   /// <remarks>
   ///    A rooted in-tree is a DAG in which there is a single vertex reachable from every other vertex.
   ///    The DAG returned is not chosen uniformly at random among all such DAGs.
   /// </remarks>
   /// <param name="verticeCount">The number of vertices</param>
   /// <param name="edgeCount">The number of edges</param>
   /// <returns>A random rooted-in DAG on <paramref name="verticeCount" /> vertices and <paramref name="edgeCount" /> edges</returns>
   /// <exception cref="ArgumentException">In the case of <paramref name="edgeCount" /> is invalid</exception>
   public static DiGraph NewRootedInDagDiGraph(int verticeCount, int edgeCount)
   {
      if (edgeCount > (long)verticeCount * (verticeCount - 1) / 2)
      {
         throw new ArgumentException("Too many edges", nameof(edgeCount));
      }

      if (edgeCount < verticeCount - 1)
      {
         throw new ArgumentException("Too few edges", nameof(edgeCount));
      }

      var graph = new DiGraph(verticeCount);
      var set = new HashSet<Edge>(Edge.DefaultEqualityComparer);

      // fix a topological order
      var vertices = new int[verticeCount];
      for (var vtx = 0; vtx < verticeCount; vtx++)
      {
         vertices[vtx] = vtx;
      }

      Rnd.Shuffle(vertices);

      // one edge pointing from each vertex, other than the root = vertices[V-1]
      for (var startVtx = 0; startVtx < verticeCount - 1; startVtx++)
      {
         var endVtx = Rnd.Next(startVtx + 1, verticeCount);
         var newEdge = new Edge(startVtx, endVtx);
         set.Add(newEdge);
         graph.AddEdge(vertices[startVtx], vertices[endVtx]);
      }

      while (graph.EdgeCount < edgeCount)
      {
         var startVertex = Rnd.Next(verticeCount);
         var endVertex = Rnd.Next(verticeCount);
         var newEdge = new Edge(startVertex, endVertex);
         if (startVertex < endVertex && set.Add(newEdge))
         {
            graph.AddEdge(vertices[startVertex], vertices[endVertex]);
         }
      }

      return graph;
   }

   /// <summary>
   ///    Returns a complete rooted-out DAG on <paramref name="verticeCount" /> vertices.
   /// </summary>
   /// <remarks>
   ///    A rooted out-tree is a DAG in which every vertex is reachable
   ///    from a single vertex. A complete rooted in-DAG has V*(V-1)/2 edges.
   /// </remarks>
   /// <param name="verticeCount">The number of vertices</param>
   /// <returns>A complete rooted-out DAG on <paramref name="verticeCount" /> vertices</returns>
   public static DiGraph NewCompleteRootedOutDagDiGraph(int verticeCount)
   {
      var graph = new DiGraph(verticeCount);
      var vertices = new int[verticeCount];
      for (var vtx = 0; vtx < verticeCount; vtx++)
      {
         vertices[vtx] = vtx;
      }

      Rnd.Shuffle(vertices);
      for (var startVtx = 0; startVtx < verticeCount; startVtx++)
      {
         for (var endVtx = startVtx + 1; endVtx < verticeCount; endVtx++)
         {
            graph.AddEdge(vertices[endVtx], vertices[startVtx]);
         }
      }

      return graph;
   }

   /// <summary>
   ///    Returns a random rooted-out DAG on <paramref name="verticeCount" /> vertices and <paramref name="edgeCount" />
   ///    edges.
   /// </summary>
   /// <remarks>
   ///    A rooted out-tree is a DAG in which every vertex is reachable from a single vertex.
   ///    The DAG returned is not chosen uniformly at random among all such DAGs.
   /// </remarks>
   /// <param name="verticeCount">The number of vertices</param>
   /// <param name="edgeCount">The number of edges</param>
   /// <returns>A random rooted-out DAG on <paramref name="verticeCount" /> vertices and <paramref name="edgeCount" /> edges</returns>
   /// <exception cref="ArgumentException">If <paramref name="edgeCount" /> is invalid</exception>
   public static DiGraph NewRootedOutDag(int verticeCount, int edgeCount)
   {
      if (edgeCount > (long)verticeCount * (verticeCount - 1) / 2)
      {
         throw new ArgumentException("Too many edges", nameof(edgeCount));
      }

      if (edgeCount < verticeCount - 1)
      {
         throw new ArgumentException("Too few edges", nameof(edgeCount));
      }

      var graph = new DiGraph(verticeCount);
      var set = new HashSet<Edge>();

      // fix a topological order
      var vertices = new int[verticeCount];
      for (var vtx = 0; vtx < verticeCount; vtx++)
      {
         vertices[vtx] = vtx;
      }

      Rnd.Shuffle(vertices);

      // one edge pointing from each vertex, other than the root = vertices[V-1]
      for (var startVtx = 0; startVtx < verticeCount - 1; startVtx++)
      {
         var endVtx = Rnd.Next(startVtx + 1, verticeCount);
         var newEdge = new Edge(endVtx, startVtx);
         set.Add(newEdge);
         graph.AddEdge(vertices[endVtx], vertices[startVtx]);
      }

      while (graph.EdgeCount < edgeCount)
      {
         var startVtx = Rnd.Next(verticeCount);
         var endVtx = Rnd.Next(verticeCount);
         var newEdge = new Edge(endVtx, startVtx);
         if (startVtx < endVtx && set.Add(newEdge))
         {
            graph.AddEdge(vertices[endVtx], vertices[startVtx]);
         }
      }

      return graph;
   }

   /// <summary>
   ///    Returns a random simple digraph on <paramref name="verticeCount" /> vertices, <paramref name="edgeCount" />
   ///    edges and (at least) <paramref name="strongComponentCount" /> strong components. The vertices are randomly
   ///    assigned integer labels between 0 and <code><paramref name="strongComponentCount" /> - 1</code>  (corresponding to
   ///    strong components). Then, a strong component is creates among the vertices
   ///    with the same label. Next, random edges (either between two vertices with
   ///    the same labels or from a vertex with a smaller label to a vertex with a
   ///    larger label). The number of components will be equal to the number of
   ///    distinct labels that are assigned to vertices.
   /// </summary>
   /// <param name="verticeCount">The number of vertices</param>
   /// <param name="edgeCount">The number of edges</param>
   /// <param name="strongComponentCount">The (maximum) number of strong components</param>
   /// <returns>
   ///    A random simple digraph on <paramref name="verticeCount" /> vertices and <paramref name="edgeCount" /> edges,
   ///    with (at most) <paramref name="strongComponentCount" /> strong components
   /// </returns>
   /// <exception cref="ArgumentException">
   ///    If <paramref name="strongComponentCount" /> is larger than
   ///    <paramref name="verticeCount" />
   /// </exception>
   public static DiGraph NewStrongDiGraph(int verticeCount, int edgeCount, int strongComponentCount)
   {
      if (strongComponentCount >= verticeCount || strongComponentCount <= 0)
      {
         throw new ArgumentException("Number of components must be between 1 and V", nameof(strongComponentCount));
      }

      if (edgeCount <= 2 * (verticeCount - strongComponentCount))
      {
         throw new ArgumentException("Number of edges must be at least 2(V-c)", nameof(edgeCount));
      }

      if (edgeCount > (long)verticeCount * (verticeCount - 1) / 2)
      {
         throw new ArgumentException("Too many edges", nameof(edgeCount));
      }

      // the digraph
      var graph = new DiGraph(verticeCount);

      // edges added to G (to avoid duplicate edges)
      var set = new HashSet<Edge>();
      var label = new int[verticeCount];
      for (var vtx = 0; vtx < verticeCount; vtx++)
      {
         label[vtx] = Rnd.Next(strongComponentCount);
      }

      // make all vertices with label c a strong component by
      // combining a rooted in-tree and a rooted out-tree
      for (var vtx = 0; vtx < strongComponentCount; vtx++)
      {
         // how many vertices in component c
         var count = 0;
         for (var v = 0; v < graph.VerticeCount; v++)
         {
            if (label[v] == vtx)
            {
               count++;
            }
         }

         var vertices = new int[count];
         var j = 0;
         for (var v = 0; v < verticeCount; v++)
         {
            if (label[v] == vtx)
            {
               vertices[j++] = v;
            }
         }

         Rnd.Shuffle(vertices);

         // rooted-in tree with root = vertices[count-1]
         for (var startVtx = 0; startVtx < count - 1; startVtx++)
         {
            var endVtx = Rnd.Next(startVtx + 1, count);
            var newEdge = new Edge(endVtx, startVtx);
            set.Add(newEdge);
            graph.AddEdge(vertices[endVtx], vertices[startVtx]);
         }

         // rooted-out tree with root = vertices[count-1]
         for (var startVtx = 0; startVtx < count - 1; startVtx++)
         {
            var endVtx = Rnd.Next(startVtx + 1, count);
            var newEdge = new Edge(startVtx, endVtx);
            set.Add(newEdge);
            graph.AddEdge(vertices[startVtx], vertices[endVtx]);
         }
      }

      while (graph.EdgeCount < edgeCount)
      {
         var startVtx = Rnd.Next(verticeCount);
         var endVtx = Rnd.Next(verticeCount);
         var newEdge = new Edge(startVtx, endVtx);
         if (startVtx != endVtx && label[startVtx] <= label[endVtx] && set.Add(newEdge))
         {
            graph.AddEdge(startVtx, endVtx);
         }
      }

      return graph;
   }

   /// <summary>
   ///    Returns an Eulerian path digraph on <paramref name="verticeCount" /> vertices.
   /// </summary>
   /// <param name="verticeCount">The number of vertices in the path</param>
   /// <param name="edgeCount">The number of edges in the path</param>
   /// <returns>
   ///    A digraph that is a directed Eulerian path on <paramref name="verticeCount" /> vertices and
   ///    <paramref name="edgeCount" /> edges
   /// </returns>
   /// <exception cref="ArgumentException">
   ///    If either <paramref name="verticeCount" /> or <paramref name="edgeCount" /> is less or equal to 0
   /// </exception>
   public static DiGraph NewEulerianPathDiGraph(int verticeCount, int edgeCount)
   {
      if (edgeCount < 0)
      {
         throw new ArgumentException("negative number of edges", nameof(edgeCount));
      }

      if (verticeCount <= 0)
      {
         throw new ArgumentException("An Eulerian path must have at least one vertex", nameof(verticeCount));
      }

      var graph = new DiGraph(verticeCount);
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
   ///    Returns an Eulerian cycle digraph on <paramref name="verticeCount" /> vertices.
   /// </summary>
   /// <param name="verticeCount">The number of vertices in the cycle</param>
   /// <param name="edgeCount">The number of edges in the cycle</param>
   /// <returns>
   ///    A digraph that is a directed Eulerian cycle on <paramref name="verticeCount" /> vertices and
   ///    <paramref name="edgeCount" /> edges
   /// </returns>
   /// <exception cref="ArgumentException">
   ///    if either <paramref name="verticeCount" /> or <paramref name="edgeCount" /> is less or equal to zero
   /// </exception>
   public static DiGraph NewEulerianCycleDiGraph(int verticeCount, int edgeCount)
   {
      if (edgeCount <= 0)
      {
         throw new ArgumentException("An Eulerian cycle must have at least one edge", nameof(edgeCount));
      }

      if (verticeCount <= 0)
      {
         throw new ArgumentException("An Eulerian cycle must have at least one vertex", nameof(verticeCount));
      }

      var graph = new DiGraph(verticeCount);
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
   ///    Returns a cycle digraph on <paramref name="verticeCount" /> vertices.
   /// </summary>
   /// <param name="verticeCount">The number of vertices in the cycle</param>
   /// <returns>A digraph that is a directed cycle on <paramref name="verticeCount" /> vertices</returns>
   public static DiGraph NewCycleDiGraph(int verticeCount)
   {
      var graph = new DiGraph(verticeCount);
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
   ///    Returns a complete binary tree digraph on <paramref name="verticeCount" /> vertices.
   /// </summary>
   /// <param name="verticeCount">The number of vertices in the binary tree</param>
   /// <returns>A digraph that is a complete binary tree on <paramref name="verticeCount" /> vertices</returns>
   public static DiGraph NewBinaryTreeDiGraph(int verticeCount)
   {
      var graph = new DiGraph(verticeCount);
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
   ///    Returns a path digraph on <paramref name="verticeCount" /> vertices.
   /// </summary>
   /// <param name="verticeCount">The number of vertices in the path</param>
   /// <returns>A digraph that is a directed path on <paramref name="verticeCount" /> vertices</returns>
   public static DiGraph NewPathDiGraph(int verticeCount)
   {
      var graph = new DiGraph(verticeCount);
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
   ///    Returns a random rooted-out tree on <paramref name="verticeCount" /> vertices. A rooted out-tree
   ///    is an oriented tree in which each vertex is reachable from a single vertex.
   ///    It is also known as a <em>arborescence</em> or <em>branching</em>.
   ///    The tree returned is not chosen uniformly at random among all such trees.
   /// </summary>
   /// <param name="verticeCount">The number of vertices</param>
   /// <returns>A random rooted-out tree on <paramref name="verticeCount" /> vertices</returns>
   public static DiGraph NewRootedOutTreeDiGraph(int verticeCount) =>
      NewRootedOutDag(verticeCount, verticeCount - 1);

   /// <summary>
   ///    Returns a random rooted-in tree on <paramref name="verticeCount" /> vertices.
   ///    A rooted in-tree is an oriented tree in which there is a single vertex
   ///    reachable from every other vertex.
   ///    The tree returned is not chosen uniformly at random among all such trees.
   /// </summary>
   /// <param name="verticeCount">The number of vertices</param>
   /// <returns>A random rooted-in tree on <paramref name="verticeCount" /> vertices</returns>
   public static DiGraph NewRootedInTreeDiGraph(int verticeCount) =>
      NewRootedInDagDiGraph(verticeCount, verticeCount - 1);
}