using Graph.Adt;
using Graph.Algs;

namespace Graph.Tests;

[TestFixture]
public class BellmanFordShortestPathTests
{
   [Test]
   public void BellmanFordShortestPath_HasNegativeCycleTest()
   {
      const int verticeCount = 8;
      const int sourceVertex = 0;
      var graph = new EdgeWeightedDigraph(verticeCount);
      graph.AddEdge(new DirectedEdge(4, 5, 0.35));
      graph.AddEdge(new DirectedEdge(5, 4, -0.66));
      graph.AddEdge(new DirectedEdge(4, 7, 0.37));
      graph.AddEdge(new DirectedEdge(5, 7, 0.28));
      graph.AddEdge(new DirectedEdge(7, 5, 0.28));
      graph.AddEdge(new DirectedEdge(5, 1, 0.32));
      graph.AddEdge(new DirectedEdge(0, 4, 0.38));
      graph.AddEdge(new DirectedEdge(0, 2, 0.26));
      graph.AddEdge(new DirectedEdge(7, 3, 0.39));
      graph.AddEdge(new DirectedEdge(1, 3, 0.29));
      graph.AddEdge(new DirectedEdge(2, 7, 0.34));
      graph.AddEdge(new DirectedEdge(6, 2, 0.40));
      graph.AddEdge(new DirectedEdge(3, 6, 0.52));
      graph.AddEdge(new DirectedEdge(6, 0, 0.58));
      graph.AddEdge(new DirectedEdge(6, 4, 0.93));

      var shortestPath = new BellmanFordShortestPath(graph, sourceVertex);
      shortestPath.Apply();

      // print negative cycle
      if (shortestPath.HasNegativeCycle)
      {
         foreach (var edge in shortestPath.NegativeCycle)
         {
            Console.WriteLine(edge);
         }
      }
      else
      {
         // print shortest paths
         for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
         {
            if (shortestPath.HasPathTo(vtx))
            {
               Console.WriteLine("{0:D} To {1:D} ({2:F})",
                  sourceVertex,
                  vtx,
                  shortestPath.GetDistanceTo(vtx));
               foreach (var edge in shortestPath.GetPathTo(vtx))
               {
                  Console.Write($"{edge}   ");
               }

               Console.WriteLine();
            }
            else
            {
               Console.WriteLine("There is no path From {0:D} To {1:D}", sourceVertex, vtx);
            }
         }
      }
   }

   [Test]
   public void BellmanFordShortestPath_NoNegativeCycleTest()
   {
      const int verticeCount = 8;
      const int sourceVertex = 0;
      var graph = new EdgeWeightedDigraph(verticeCount);
      graph.AddEdge(new DirectedEdge(4, 5, 0.35));
      graph.AddEdge(new DirectedEdge(5, 4, 0.35));
      graph.AddEdge(new DirectedEdge(4, 7, 0.37));
      graph.AddEdge(new DirectedEdge(5, 7, 0.28));
      graph.AddEdge(new DirectedEdge(7, 5, 0.28));
      graph.AddEdge(new DirectedEdge(5, 1, 0.32));
      graph.AddEdge(new DirectedEdge(0, 4, 0.38));
      graph.AddEdge(new DirectedEdge(0, 2, 0.26));
      graph.AddEdge(new DirectedEdge(7, 3, 0.39));
      graph.AddEdge(new DirectedEdge(1, 3, 0.29));
      graph.AddEdge(new DirectedEdge(2, 7, 0.34));
      graph.AddEdge(new DirectedEdge(6, 2, -1.20));
      graph.AddEdge(new DirectedEdge(3, 6, 0.52));
      graph.AddEdge(new DirectedEdge(6, 0, -1.40));
      graph.AddEdge(new DirectedEdge(6, 4, -1.25));

      var shortestPath = new BellmanFordShortestPath(graph, sourceVertex);
      shortestPath.Apply();

      // print negative cycle
      if (shortestPath.HasNegativeCycle)
      {
         foreach (var edge in shortestPath.NegativeCycle)
         {
            Console.WriteLine(edge);
         }
      }
      else
      {
         // print shortest paths
         for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
         {
            if (shortestPath.HasPathTo(vtx))
            {
               Console.WriteLine("{0:D} To {1:D} ({2:F})",
                  sourceVertex,
                  vtx,
                  shortestPath.GetDistanceTo(vtx));
               foreach (var edge in shortestPath.GetPathTo(vtx))
               {
                  Console.Write($"{edge}   ");
               }

               Console.WriteLine();
            }
            else
            {
               Console.WriteLine("There is no path From {0:D} To {1:D}", sourceVertex, vtx);
            }
         }
      }
   }

   /// <summary>
   ///    The Arbitrage provides a client that finds an arbitrage
   ///    opportunity in a currency exchange table by constructing a
   ///    complete-digraph representation of the exchange table and then finding
   ///    a negative cycle in the digraph.
   ///    <p>
   ///       This implementation uses the Bellman-Ford algorithm to find a
   ///       negative cycle in the complete digraph.
   ///       The running time is proportional to <em>V</em><sup>3</sup> in the
   ///       worst case, where <em>V</em> is the number of currencies.
   ///    </p>
   ///    <p>
   ///       This code is guaranteed to find an arbitrage opportunity in a
   ///       currency exchange table (or report that no such arbitrage
   ///       opportunity exists) under the assumption that all arithmetic
   ///       performed is without floating-point rounding error or arithmetic
   ///       overflow. Since the code computes the logarithms of the edge weights,
   ///       floating-point rounding error will be present, and it may fail on
   ///       some pathological inputs.
   ///    </p>
   /// </summary>
   [Test]
   public void ArbitrageTest()
   {
      const int verticeCount = 5;
      (string currencyName, double[] currencyRates)[] currencies =
      {
         ("USD", new[] { 1, 0.741, 0.657, 1.061, 1.005 }),
         ("EUR", new[] { 1.349, 1, 0.888, 1.433, 1.366 }),
         ("GBP", new[] { 1.521, 1.126, 1, 1.614, 1.538 }),
         ("CHF", new[] { 0.942, 0.698, 0.619, 1, 0.953 }),
         ("CAD", new[] { 0.995, 0.732, 0.650, 1.049, 1 })
      };

      // create complete network
      var graph = new EdgeWeightedDigraph(verticeCount);
      for (var vtx = 0; vtx < verticeCount; vtx++)
      {
         var (_, currencyRates) = currencies[vtx];
         for (var curIdx = 0; curIdx < verticeCount; curIdx++)
         {
            var rate = currencyRates[curIdx];
            var edge = new DirectedEdge(vtx, curIdx, -Math.Log(rate));
            graph.AddEdge(edge);
         }
      }

      // find negative cycle
      const int sourceVertex = 0;
      var spt = new BellmanFordShortestPath(graph, sourceVertex);
      spt.Apply();
      if (spt.HasNegativeCycle)
      {
         var stake = 1000.0;
         foreach (var (fromVtx, toVtx, weight) in spt.NegativeCycle)
         {
            var fromName = currencies[fromVtx].currencyName;
            var toName = currencies[toVtx].currencyName;
            Console.Write("{0,10:F} {1} ", stake, fromName);
            stake *= Math.Exp(-weight);
            Console.WriteLine("= {0,10:F} {1}", stake, toName);
         }
      }
      else
      {
         Console.WriteLine("No arbitrage opportunity");
      }
   }
}