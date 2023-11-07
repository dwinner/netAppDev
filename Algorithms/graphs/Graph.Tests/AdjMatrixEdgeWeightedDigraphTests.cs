using Graph.Adt;

namespace Graph.Tests;

[TestFixture]
public class AdjMatrixEdgeWeightedDigraphTests
{
   [Test]
   public void AdjMatrixEdgeWeightedDigraphTest()
   {
      const int verticeCount = 8;
      var graph = new AdjMatrixEdgeWeightedDigraph(verticeCount);
      graph.AddEdge(new DirectedEdge(5, 4, 0.35));
      graph.AddEdge(new DirectedEdge(4, 7, 0.37));
      graph.AddEdge(new DirectedEdge(5, 7, 0.28));
      graph.AddEdge(new DirectedEdge(5, 1, 0.32));
      graph.AddEdge(new DirectedEdge(4, 0, 0.38));
      graph.AddEdge(new DirectedEdge(0, 2, 0.26));
      graph.AddEdge(new DirectedEdge(3, 7, 0.39));
      graph.AddEdge(new DirectedEdge(1, 3, 0.29));
      graph.AddEdge(new DirectedEdge(7, 2, 0.34));
      graph.AddEdge(new DirectedEdge(6, 2, 0.40));
      graph.AddEdge(new DirectedEdge(3, 6, 0.52));
      graph.AddEdge(new DirectedEdge(6, 0, 0.58));
      graph.AddEdge(new DirectedEdge(6, 4, 0.93));
      Console.WriteLine(graph);
   }
}