using Graph.Adt;
using Graph.Algs;

namespace Graph.Tests;

[TestFixture]
public class LazyPrimMstTests
{
   [Test]
   public void LazyPrimMstTest()
   {
      const int verticeCount = 8;
      var graph = new EdgeWeightedGraph(verticeCount);
      graph.AddEdge(4, 5, 0.35);
      graph.AddEdge(4, 7, 0.37);
      graph.AddEdge(5, 7, 0.28);
      graph.AddEdge(0, 7, 0.16);
      graph.AddEdge(1, 5, 0.32);
      graph.AddEdge(0, 4, 0.38);
      graph.AddEdge(2, 3, 0.17);
      graph.AddEdge(1, 7, 0.19);
      graph.AddEdge(0, 2, 0.26);
      graph.AddEdge(1, 2, 0.36);
      graph.AddEdge(1, 3, 0.29);
      graph.AddEdge(2, 7, 0.34);
      graph.AddEdge(6, 2, 0.40);
      graph.AddEdge(3, 6, 0.52);
      graph.AddEdge(6, 0, 0.58);
      graph.AddEdge(6, 4, 0.93);
      var primMst = new LazyMinSpanningTree(graph);
      primMst.Apply();

      foreach (var edge in primMst.Edges)
      {
         Console.WriteLine(edge);
      }

      Console.WriteLine("Weight: {0:F}", primMst.Weight);
   }
}