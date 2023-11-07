using Graph.Algs;

namespace Graph.Tests;

[TestFixture]
public class BipartiteTests
{
   [Test]
   [TestCase(13, 13, 13, 13)]
   [TestCase(7, 7, 12, 0)]
   public void BipartiteTest(int verticeCount1, int verticeCount2, int edgeCount, int randomEdgeCount)
   {
      var rnd = new Random();
      var graph = GraphGenerator.NewBipartiteGraph(verticeCount1, verticeCount2, edgeCount);
      var cnt = 0;
      while (cnt < randomEdgeCount)
      {
         var startVtx = rnd.Next(verticeCount1 + verticeCount2);
         var endVtx = rnd.Next(verticeCount1 + verticeCount2);
         if (startVtx != endVtx)
         {
            graph.AddEdge(startVtx, endVtx);
            cnt++;
         }
      }

      Console.WriteLine(graph);

      var bipartite = new Bipartite(graph);
      bipartite.Apply();

      if (bipartite.IsBipartite)
      {
         Console.WriteLine("Graph is bipartite");
         for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
         {
            Console.WriteLine($"{vtx}: {bipartite.GetColor(vtx)}");
         }
      }
      else
      {
         Console.WriteLine("Graph has an odd-length cycle: ");
         foreach (var vertex in bipartite.OddLenCycle)
         {
            Console.Write($"{vertex} ");
         }

         Console.WriteLine();
      }
   }
}