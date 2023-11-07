using Graph.Adt;
using Graph.Algs;

namespace Graph.Tests;

[TestFixture]
public class TransitiveClosureTests
{
   [Test]
   public void TransitiveClosureTest()
   {
      const int verticeCount = 13;
      var graph = new DiGraph(verticeCount);
      graph.AddEdge(4, 2);
      graph.AddEdge(2, 3);
      graph.AddEdge(3, 2);
      graph.AddEdge(6, 0);
      graph.AddEdge(0, 1);
      graph.AddEdge(2, 0);
      graph.AddEdge(11, 12);
      graph.AddEdge(12, 9);
      graph.AddEdge(9, 10);
      graph.AddEdge(9, 11);
      graph.AddEdge(7, 9);
      graph.AddEdge(10, 12);
      graph.AddEdge(11, 4);
      graph.AddEdge(4, 3);
      graph.AddEdge(3, 5);
      graph.AddEdge(6, 8);
      graph.AddEdge(8, 6);
      graph.AddEdge(5, 4);
      graph.AddEdge(0, 5);
      graph.AddEdge(6, 4);
      graph.AddEdge(6, 9);
      graph.AddEdge(7, 6);

      var transitiveClosure = new TransitiveClosure(graph);
      transitiveClosure.Apply();
      for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
      {
         Console.Write("{0,3:D}", vtx);
      }

      Console.WriteLine();
      Console.WriteLine("--------------------------------------------");

      // print transitive closure
      for (var src = 0; src < graph.VerticeCount; src++)
      {
         Console.Write("{0,3:D}: ", src);
         for (var dst = 0; dst < graph.VerticeCount; dst++)
         {
            Console.Write(transitiveClosure.IsReachable(src, dst) ? "  T" : "   ");
         }

         Console.WriteLine();
      }
   }
}