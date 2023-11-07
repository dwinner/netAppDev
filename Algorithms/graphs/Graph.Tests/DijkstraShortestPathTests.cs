using Graph.Adt;
using Graph.Algs;

namespace Graph.Tests;

[TestFixture]
public class DijkstraShortestPathTests
{
   [SetUp]
   public void SetUp()
   {
      _graph = new EdgeWeightedDigraph(VerticeCount);
      _graph.AddEdge(new DirectedEdge(4, 5, 0.35));
      _graph.AddEdge(new DirectedEdge(5, 4, 0.35));
      _graph.AddEdge(new DirectedEdge(4, 7, 0.37));
      _graph.AddEdge(new DirectedEdge(5, 7, 0.28));
      _graph.AddEdge(new DirectedEdge(7, 5, 0.28));
      _graph.AddEdge(new DirectedEdge(5, 1, 0.32));
      _graph.AddEdge(new DirectedEdge(0, 4, 0.38));
      _graph.AddEdge(new DirectedEdge(0, 2, 0.26));
      _graph.AddEdge(new DirectedEdge(7, 3, 0.39));
      _graph.AddEdge(new DirectedEdge(1, 3, 0.29));
      _graph.AddEdge(new DirectedEdge(2, 7, 0.34));
      _graph.AddEdge(new DirectedEdge(6, 2, 0.40));
      _graph.AddEdge(new DirectedEdge(3, 6, 0.52));
      _graph.AddEdge(new DirectedEdge(6, 0, 0.58));
      _graph.AddEdge(new DirectedEdge(6, 4, 0.93));
   }

   private const int VerticeCount = 8;
   private const int SourceVertex = 0;
   private EdgeWeightedDigraph _graph = null!;

   [Test(
      Description = "Dijkstra's algorithm. Computes the shortest path tree. Assumes all weights are non-negative.",
      TestOf = typeof(DijkstraShortestPath))]
   public void DijkstraShortestPathTest()
   {
      // compute shortest paths
      var shortestPath = new DijkstraShortestPath(_graph, SourceVertex);
      shortestPath.Apply();

      // print shortest path
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         if (shortestPath.HasPathTo(vtx))
         {
            Console.Write("{0:D} to {1:D} ({2:F}):  ",
               SourceVertex,
               vtx,
               shortestPath.GetDistanceTo(vtx));
            foreach (var edge in shortestPath.GetPathTo(vtx))
            {
               Console.Write("[{0}] ", edge);
            }

            Console.WriteLine();
         }
         else
         {
            Console.WriteLine("{0:D} to {1:D} no path\n", SourceVertex, vtx);
         }
      }
   }

   [Test(
      Description = "Dijkstra's algorithm run from each vertex.",
      TestOf = typeof(DijkstraAllPairsShortestPath))]
   public void DijkstraAllPairsShortestPathTest()
   {
      // compute shortest paths between all pairs of vertices
      var shortestPath = new DijkstraAllPairsShortestPath(_graph);
      shortestPath.Apply();

      // print all-pairs shortest path distances
      Console.WriteLine("  ");
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         Console.Write("{0:D} ", vtx);
      }

      Console.WriteLine();

      for (var srcVtx = 0; srcVtx < _graph.VerticeCount; srcVtx++)
      {
         Console.Write("{0:D}: ", srcVtx);
         for (var dstVtx = 0; dstVtx < _graph.VerticeCount; dstVtx++)
         {
            if (shortestPath.HasPath(srcVtx, dstVtx))
            {
               Console.Write("{0:D} to {1:D} {2:F}  ",
                  srcVtx,
                  dstVtx,
                  shortestPath.GetDistance(srcVtx, dstVtx));
               foreach (var edge in shortestPath.GetPath(srcVtx, dstVtx))
               {
                  Console.Write($"{edge}  ");
               }

               Console.WriteLine();
            }
            else
            {
               Console.WriteLine("{0:D} to {1:D} no path", srcVtx, dstVtx);
            }
         }
      }
   }
}