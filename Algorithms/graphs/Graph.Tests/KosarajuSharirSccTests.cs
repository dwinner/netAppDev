using Graph.Adt;
using Graph.Algs;

namespace Graph.Tests;

[TestFixture]
public class KosarajuSharirSccTests
{
   [Test]
   public void KosarajuSharirSccTest()
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

      var scc = new StronglyConnectedComponents(graph);
      scc.Apply();

      // number of connected components
      var sccCount = scc.Count;
      Console.WriteLine($"{sccCount} strong components");

      // compute list of vertices in each strong component
      var componentQueueArray = new Queue<int>[sccCount];
      for (var i = 0; i < sccCount; i++)
      {
         componentQueueArray[i] = new Queue<int>();
      }

      for (var vtx = 0; vtx < graph.VerticeCount; vtx++)
      {
         componentQueueArray[scc.GetId(vtx)].Enqueue(vtx);
      }

      // print results
      for (var i = 0; i < sccCount; i++)
      {
         foreach (var vtx in componentQueueArray[i])
         {
            Console.Write($"{vtx} ");
         }

         Console.WriteLine();
      }
   }
}