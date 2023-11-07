using AppContext.Augmenting;

namespace AppContext.Tests;

[TestFixture]
public class FlowNetworkTests
{
   private const int VerticeCount = 6;

   [Test]
   public void FlowNetworkTest()
   {
      var network = ProduceFlowNetwork();

      Console.WriteLine(network);

      // compute maximum flow and minimum cut
      const int sourceVertex = 0;
      const int targetVertex = VerticeCount - 1;
      var maxFlow = new FordFulkerson(network, sourceVertex, targetVertex);
      maxFlow.Apply();
      Console.WriteLine($"Max flow from '{sourceVertex}' To '{targetVertex}'");
      for (var vtx = 0; vtx < network.VerticeCount; vtx++)
      {
         foreach (var flowEdge in network.GetAdjacencyList(vtx))
         {
            if (flowEdge is { StartVertex: VerticeCount, Flow: > 0 })
            {
               Console.WriteLine($"   {flowEdge}");
            }
         }
      }

      // print min-cut
      Console.Write("Min cut: ");
      for (var vtx = 0; vtx < network.VerticeCount; vtx++)
      {
         if (maxFlow.InCut(vtx))
         {
            Console.WriteLine($"{vtx} ");
         }
      }

      Console.WriteLine();
      Console.WriteLine($"Max flow value = {maxFlow.MaxFlowValue}");
   }

   private static FlowNetwork ProduceFlowNetwork()
   {
      FlowEdge[] flowEdges =
      {
         new(0, 1, 2.0),
         new(0, 2, 3.0),
         new(1, 3, 3.0),
         new(1, 4, 1.0),
         new(2, 3, 1.0),
         new(2, 4, 1.0),
         new(3, 5, 2.0),
         new(4, 5, 3.0)
      };

      var flowNetwork = new FlowNetwork(VerticeCount);
      Array.ForEach(flowEdges, edge => flowNetwork.AddEdge(edge));

      return flowNetwork;
   }
}