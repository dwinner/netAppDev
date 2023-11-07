using Graph.Adt;
using Graph.Algs;

namespace Graph.Tests;

public class SimpleGraphTests
{
   [SetUp]
   public void Setup()
   {
   }

   [Test]
   public void GraphTest()
   {
      const int verticeCount = 13;
      var graph = new PrimaryGraph(verticeCount);
      graph.AddEdge(0, new[] { 6, 2, 1, 5 });
      graph.AddEdge(1, new[] { 0 });
      graph.AddEdge(2, new[] { 0 });
      graph.AddEdge(3, new[] { 5, 4 });
      graph.AddEdge(4, new[] { 5, 6, 3 });
      graph.AddEdge(5, new[] { 3, 4, 0 });
      graph.AddEdge(6, new[] { 0, 4 });
      graph.AddEdge(7, new[] { 8 });
      graph.AddEdge(8, new[] { 7 });
      graph.AddEdge(9, new[] { 11, 10, 12 });
      graph.AddEdge(10, new[] { 9 });
      graph.AddEdge(11, new[] { 9, 12 });
      graph.AddEdge(12, new[] { 11, 9 });

      Console.WriteLine(graph);
   }

   [Test]
   public void DepthFirstSearchTest()
   {
      const int verticeCount = 13;
      var graph = new PrimaryGraph(verticeCount);
      graph.AddEdge(0, 5);
      graph.AddEdge(4, 3);
      graph.AddEdge(0, 1);
      graph.AddEdge(9, 12);
      graph.AddEdge(6, 4);
      graph.AddEdge(5, 4);
      graph.AddEdge(0, 2);
      graph.AddEdge(11, 12);
      graph.AddEdge(9, 10);
      graph.AddEdge(0, 6);
      graph.AddEdge(7, 8);
      graph.AddEdge(9, 11);
      graph.AddEdge(5, 3);

      const int firstVertex = 0;
      var search = new Depth1StSearch(graph, firstVertex);
      search.Search();
      for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
      {
         if (search[vtx])
         {
            Console.WriteLine($"{vtx} ");
         }
      }

      Console.WriteLine(search.Count != graph.VerticeCount ? "NOT connected" : "connected");
      Console.WriteLine(graph);
   }

   [Test]
   public void DepthFirstPathTest()
   {
      const int verticeCount = 6;
      var graph = new PrimaryGraph(verticeCount);
      graph.AddEdge(0, new[] { 2, 1, 5 });
      graph.AddEdge(1, new[] { 0, 2 });
      graph.AddEdge(2, new[] { 0, 1, 3, 4 });
      graph.AddEdge(3, new[] { 5, 4, 2 });
      graph.AddEdge(4, new[] { 3, 2 });
      graph.AddEdge(5, new[] { 3, 0 });

      const int startingVertex = 0;
      var depth1St = new Depth1StPath(graph, startingVertex);
      depth1St.Search();

      for (var srcVtx = 0; srcVtx < graph.VerticeCount; srcVtx++)
      {
         if (depth1St.HasPathTo(srcVtx))
         {
            Console.Write("{0:d} To {1:d}:  ", startingVertex, srcVtx);
            foreach (var dstVtx in depth1St.GetPathTo(srcVtx))
            {
               Console.Write(dstVtx == startingVertex ? $"{dstVtx}" : $"-{dstVtx}");
            }

            Console.WriteLine();
         }
         else
         {
            Console.WriteLine("{0:d} to {1:d}:  not connected", startingVertex, srcVtx);
         }
      }
   }

   [Test]
   public void BreadthFirstPathsTest()
   {
      var graph = new PrimaryGraph(6);
      graph.AddEdge(0, new[] { 2, 1, 5 });
      graph.AddEdge(1, new[] { 0, 2 });
      graph.AddEdge(2, new[] { 0, 1, 3, 4 });
      graph.AddEdge(3, new[] { 5, 4, 2 });
      graph.AddEdge(4, new[] { 3, 2 });
      graph.AddEdge(5, new[] { 3, 0 });

      const int sourceVertex = 0;
      var breadth1St = new Breadth1StPath(graph, sourceVertex);
      breadth1St.Search();

      var (success, errMsg) = breadth1St.Check(graph, 0);
      Assert.That(success, Is.True, errMsg);

      for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
      {
         if (breadth1St.HasPathTo(vtx))
         {
            Console.Write("{0:D} To {1:D} ({2:D}): ", sourceVertex, vtx, breadth1St.GetDistanceTo(vtx));
            foreach (var pathVtx in breadth1St.GetPathTo(vtx))
            {
               Console.Write(pathVtx == sourceVertex
                  ? $"{pathVtx}"
                  : $"-{pathVtx}");
            }

            Console.WriteLine();
         }
         else
         {
            Console.WriteLine("{0:D} To {1:D} (-): not connected", sourceVertex, vtx);
         }
      }
   }

   [Test]
   public void CycleTest()
   {
      const int verticeCount = 13;
      var graph = new PrimaryGraph(verticeCount);
      graph.AddEdge(0, 5);
      graph.AddEdge(4, 3);
      graph.AddEdge(0, 1);
      graph.AddEdge(9, 12);
      graph.AddEdge(6, 4);
      graph.AddEdge(5, 4);
      graph.AddEdge(0, 2);
      graph.AddEdge(11, 12);
      graph.AddEdge(9, 10);
      graph.AddEdge(0, 6);
      graph.AddEdge(7, 8);
      graph.AddEdge(9, 11);
      graph.AddEdge(5, 3);

      var finder = new CycleChecker(graph);
      finder.Search();
      if (finder.HasCycle && !finder.HasParallelEdges())
      {
         foreach (var v in finder.GetCycle())
         {
            Console.Write($"{v} ");
         }

         Console.WriteLine();
      }
      else
      {
         Console.WriteLine("Graph is acyclic");
      }
   }

   [Test]
   public void СonnectedСomponentsTest()
   {
      const int verticeCount = 13;
      var graph = new PrimaryGraph(verticeCount);
      graph.AddEdge(0, 5);
      graph.AddEdge(4, 3);
      graph.AddEdge(0, 1);
      graph.AddEdge(9, 12);
      graph.AddEdge(6, 4);
      graph.AddEdge(5, 4);
      graph.AddEdge(0, 2);
      graph.AddEdge(11, 12);
      graph.AddEdge(9, 10);
      graph.AddEdge(0, 6);
      graph.AddEdge(7, 8);
      graph.AddEdge(9, 11);
      graph.AddEdge(5, 3);

      var cc = new СonnectedСomponents(graph);
      cc.SearchIdx();

      var ccCount = cc.Count;
      Console.WriteLine($"Number of connected components: {ccCount} components");

      // compute list of vertices in each connected component
      var components = new Queue<int>[ccCount];
      for (var i = 0; i < ccCount; i++)
      {
         components[i] = new Queue<int>();
      }

      for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
      {
         components[cc.GetId(vtx)].Enqueue(vtx);
      }

      // print results
      for (var i = 0; i < ccCount; i++)
      {
         Console.Write($"Path #{i + 1}: ");
         foreach (var vtx in components[i])
         {
            Console.Write($"{vtx} ");
         }

         Console.WriteLine();
      }
   }
}