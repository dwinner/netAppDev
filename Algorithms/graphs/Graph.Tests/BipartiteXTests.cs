using Graph.Algs;

namespace Graph.Tests;

[TestFixture]
public class BipartiteXTests
{
   [Test]
   public void BipartiteXTest()
   {
      Random random = new();
      const int verticeCount1 = 7;
      const int verticeCount2 = 7;
      const int edgeCount = 25;
      const int factor = 10;

      // create random bipartite graph with V1 vertices on left side,
      // V2 vertices on right side, and E edges; then add F random edges

      var graph = GraphGenerator.NewBipartiteGraph(verticeCount1, verticeCount2, edgeCount);
      for (var i = 0; i < factor; i++)
      {
         var startVtx = random.Next(verticeCount1 + verticeCount2);
         var endVtx = random.Next(verticeCount1 + verticeCount2);
         graph.AddEdge(startVtx, endVtx);
      }

      Console.WriteLine(graph);

      var bipartiteX = new BipartiteX(graph);
      bipartiteX.Apply();
      if (bipartiteX.IsBipartite)
      {
         Console.WriteLine("Graph is bipartite");
         for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
         {
            Console.WriteLine($"{vtx}: {bipartiteX.GetColor(vtx)}");
            Console.WriteLine();
         }
      }
      else
      {
         Console.Write("Graph has an odd-length cycle: ");
         foreach (var x in bipartiteX.OddCycle)
         {
            Console.Write(x + " ");
         }

         Console.WriteLine();
      }
   }
}