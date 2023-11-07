using System.Collections;
using Graph.Adt;
using Graph.Algs;

namespace Graph.Tests;

[TestFixture]
public class DiGraphTests
{
   [SetUp]
   public void SetUp()
   {
      _diGraph = new DiGraph(VerticeCount);
      _diGraph.AddEdge(4, 2);
      _diGraph.AddEdge(2, 3);
      _diGraph.AddEdge(3, 2);
      _diGraph.AddEdge(6, 0);
      _diGraph.AddEdge(0, 1);
      _diGraph.AddEdge(2, 0);
      _diGraph.AddEdge(11, 12);
      _diGraph.AddEdge(12, 9);
      _diGraph.AddEdge(9, 10);
      _diGraph.AddEdge(9, 11);
      _diGraph.AddEdge(7, 9);
      _diGraph.AddEdge(10, 12);
      _diGraph.AddEdge(11, 4);
      _diGraph.AddEdge(4, 3);
      _diGraph.AddEdge(3, 5);
      _diGraph.AddEdge(6, 8);
      _diGraph.AddEdge(8, 6);
      _diGraph.AddEdge(5, 4);
      _diGraph.AddEdge(0, 5);
      _diGraph.AddEdge(6, 4);
      _diGraph.AddEdge(6, 9);
      _diGraph.AddEdge(7, 6);
   }

   public static IEnumerable Vertices => Enumerable.Range(0, VerticeCount).Cast<object>();

   private DiGraph _diGraph = null!;
   private const int VerticeCount = 13;

   [Test]
   public void DiGraphTest() => Console.WriteLine(_diGraph);

   [TestCaseSource(typeof(DiGraphTests), nameof(Vertices))]
   public void DirectedDepthFirstSearchTest(int sourceVertex)
   {
      var dfs = new DirectedDepth1StSearch(_diGraph);
      dfs.Search(sourceVertex);

      // print out vertices reachable from sources
      Console.Write($"Reachable from {sourceVertex}: ");
      for (var vtx = 0; vtx < _diGraph.VerticeCount; vtx++)
      {
         if (dfs.IsMarked(vtx))
         {
            Console.Write($"{vtx} ");
         }
      }
   }

   [Test]
   public void DirectedCycleTest()
   {
      var cycleFinder = new DirectedCycle(_diGraph);
      cycleFinder.Find();
      if (cycleFinder.HasCycle())
      {
         Console.Write("Directed cycle: ");
         foreach (var cycleVtx in cycleFinder.GetCycle())
         {
            Console.Write($"{cycleVtx} ");
         }

         Console.WriteLine();
      }
      else
      {
         Console.WriteLine("No directed cycle");
      }
   }
}