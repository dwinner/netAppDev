using Graph.Adt;
using Graph.Algs;

namespace Graph.Tests;

[TestFixture]
public class FloydWarshallTests
{
   [Test]
   public void FloydWarshallTest()
   {
      var random = new Random(Environment.TickCount);
      const int verticeCount = 10;
      const int edgeCount = 50;
      var graph = new AdjMatrixEdgeWeightedDigraph(verticeCount);
      for (var i = 0; i < edgeCount; i++)
      {
         var startVtx = random.Next(verticeCount);
         var endVtx = random.Next(verticeCount);
         var weight = Math.Round(random.Next(-15, 100) * 1.0E-2, 2);
         graph.AddEdge(startVtx == endVtx
            ? new DirectedEdge(startVtx, endVtx, Math.Abs(weight))
            : new DirectedEdge(startVtx, endVtx, weight));
      }

      Console.WriteLine(graph);

      // run Floyd-Warshall algorithm
      var spt = new FloydWarshall(graph);
      spt.Apply();

      // print all-pairs shortest path distances
      Console.Write("  ");
      for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
      {
         Console.Write("{0:D6} ", vtx);
      }

      Console.WriteLine();

      for (var rowVtx = 0; rowVtx < graph.VerticeCount; rowVtx++)
      {
         Console.Write("{0:D3}: ", rowVtx);
         for (var colVtx = 0; colVtx < graph.VerticeCount; colVtx++)
         {
            if (spt.HasPath(rowVtx, colVtx))
            {
               Console.Write("{0,6:F2} ", spt.GetDistance(rowVtx, colVtx));
            }
            else
            {
               Console.Write("  Inf ");
            }
         }

         Console.WriteLine();
      }

      // print negative cycle
      if (spt.HasNegativeCycle)
      {
         Console.WriteLine("Negative cost cycle:");
         foreach (var edge in spt.GetNegativeCycle())
         {
            Console.Write($"{edge} ");
         }

         Console.WriteLine();
      }
      else
      {
         // print all-pairs shortest paths
         for (var rowVtx = 0; rowVtx < graph.VerticeCount; rowVtx++)
         {
            for (var colVtx = 0; colVtx < graph.VerticeCount; colVtx++)
            {
               if (spt.HasPath(rowVtx, colVtx))
               {
                  Console.Write("{0:D} To {1:D} ({2,5:F2})  ",
                     rowVtx,
                     colVtx,
                     spt.GetDistance(rowVtx, colVtx));
                  foreach (var edge in spt.GetPath(rowVtx, colVtx))
                  {
                     Console.Write($"{edge}  ");
                  }

                  Console.WriteLine();
               }
               else
               {
                  Console.WriteLine("{0:D} To {1:D} no path", rowVtx, colVtx);
               }
            }
         }
      }
   }
}