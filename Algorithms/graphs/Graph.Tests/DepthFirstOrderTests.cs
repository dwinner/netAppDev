using Graph.Adt;
using Graph.Algs;

namespace Graph.Tests;

[TestFixture]
public class DepthFirstOrderTests
{
   [Test]
   public void DepthFirstOrderTest()
   {
      const int verticeCount = 13;
      var graph = new DiGraph(verticeCount);
      graph.AddEdge(2, 3);
      graph.AddEdge(0, 6);
      graph.AddEdge(0, 1);
      graph.AddEdge(2, 0);
      graph.AddEdge(11, 12);
      graph.AddEdge(9, 12);
      graph.AddEdge(9, 10);
      graph.AddEdge(9, 11);
      graph.AddEdge(3, 5);
      graph.AddEdge(8, 7);
      graph.AddEdge(5, 4);
      graph.AddEdge(0, 5);
      graph.AddEdge(6, 4);
      graph.AddEdge(6, 9);
      graph.AddEdge(7, 6);

      var dfsOrder = new Depth1StOrder(verticeCount);
      dfsOrder.Search(graph);

      Console.WriteLine("   v  pre post");
      Console.WriteLine("--------------");
      for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
      {
         Console.WriteLine("{0,-4:D} {1,-4:D} {2,-4:D}",
            vtx,
            dfsOrder.GetPreOrderIndex(vtx),
            dfsOrder.GetPostOrderIndex(vtx));
      }

      Console.Write("Preorder:  ");
      foreach (var vtx in dfsOrder.GetPreVertices())
      {
         Console.Write($"{vtx} ");
      }

      Console.WriteLine();

      Console.Write("Postorder: ");
      foreach (var vtx in dfsOrder.GetPostVertices())
      {
         Console.Write($"{vtx} ");
      }

      Console.WriteLine();

      Console.Write("Reverse postorder: ");
      foreach (var vtx in dfsOrder.GetReversePostVertices())
      {
         Console.Write($"{vtx} ");
      }

      Console.WriteLine();
   }
}