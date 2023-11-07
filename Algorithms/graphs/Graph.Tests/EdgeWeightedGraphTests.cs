using Graph.Adt;

namespace Graph.Tests;

[TestFixture]
public class EdgeWeightedGraphTests
{
   [Test]
   public void EdgeWeightedGraphTest()
   {
      const int verticeCount = 8;
      var graph = new EdgeWeightedGraph(verticeCount);
      graph.AddEdge(new Edge(4, 5, 0.35));
      graph.AddEdge(new Edge(4, 7, 0.37));
      graph.AddEdge(new Edge(5, 7, 0.28));
      graph.AddEdge(new Edge(0, 7, 0.16));
      graph.AddEdge(new Edge(1, 5, 0.32));
      graph.AddEdge(new Edge(0, 4, 0.38));
      graph.AddEdge(new Edge(2, 3, 0.17));
      graph.AddEdge(new Edge(1, 7, 0.19));
      graph.AddEdge(new Edge(0, 2, 0.26));
      graph.AddEdge(new Edge(1, 2, 0.36));
      graph.AddEdge(new Edge(1, 3, 0.29));
      graph.AddEdge(new Edge(2, 7, 0.34));
      graph.AddEdge(new Edge(6, 2, 0.40));
      graph.AddEdge(new Edge(3, 6, 0.52));
      graph.AddEdge(new Edge(6, 0, 0.58));
      graph.AddEdge(new Edge(6, 4, 0.93));

      Console.WriteLine(graph);
   }
}