using Graph.Adt;
using Graph.Algs;

namespace Graph.Tests;

[TestFixture]
public class Breadth1StDirectedPathTests
{
   [Test]
   public void Breadth1StDirectedPathTest()
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

      const int sourceVertex = 3;
      var bfsAlg = new Breadth1StDirectedPath(graph, sourceVertex);
      bfsAlg.Apply();

      for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
      {
         if (bfsAlg.HasPathTo(vtx))
         {
            Console.Write("{0:D} To {1:D} ({2:D}):  ",
               sourceVertex,
               vtx,
               bfsAlg.GetDistanceTo(vtx));
            foreach (var vtxItem in bfsAlg.GetPathTo(vtx))
            {
               Console.Write(vtxItem == sourceVertex
                  ? $"{vtxItem}"
                  : $"->{vtxItem}");
            }

            Console.WriteLine();
         }

         else
         {
            Console.WriteLine("{0:D} To {1:D} (-):  not connected", sourceVertex, vtx);
         }
      }
   }
}