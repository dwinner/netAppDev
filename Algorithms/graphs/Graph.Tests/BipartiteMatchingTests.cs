using Graph.Algs;

namespace Graph.Tests;

[TestFixture]
public class BipartiteMatchingTests
{
   [Test]
   public void BipartiteMatchingTest()
   {
      const int verticeCount1 = 10;
      const int verticeCount2 = 10;
      const int edgeCount = 20;
      var bipartiteGraph = GraphGenerator.NewBipartiteGraph(verticeCount1, verticeCount2, edgeCount);
      var matching = new BipartiteMatching(bipartiteGraph);
      matching.Apply();

      // print maximum matching
      Console.WriteLine("Number of edges in max matching        = {0:D}", matching.Cardinality);
      Console.WriteLine("Number of vertices in min vertex cover = {0:D}", matching.Cardinality);
      Console.WriteLine("Graph has a perfect matching           = {0}", matching.IsPerfect());
      Console.WriteLine();

      Console.Write("Max matching: ");
      for (var vtx = 0; vtx < bipartiteGraph.VerticeCount; vtx++)
      {
         var mate = matching.GetMate(vtx);

         // print each edge only once
         if (matching.IsMatched(vtx) && vtx < mate)
         {
            Console.Write($"{vtx}-{mate} ");
         }
      }

      Console.WriteLine();

      // print minimum vertex cover
      Console.Write("Min vertex cover: ");
      for (var vtx = 0; vtx < bipartiteGraph.VerticeCount; vtx++)
      {
         if (matching.InMinVertexCover(vtx))
         {
            Console.Write($"{vtx} ");
         }
      }

      Console.WriteLine();
   }
}